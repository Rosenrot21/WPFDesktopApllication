using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Application.Domain.Books.Queries;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Core.Domain.Books.Models;
using System.Windows.Input;
using System.Windows;

namespace LibraryApp.Application.Domain.Books.Commands.AddAuthorToBookCommand
{
    public class AddAuthorToBookCommandHandler : ICommand
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly BookViewModel _viewModel;
        private readonly CanAddAuthorToBookQueryHandler _canAddAuthorToBookQueryHandler;
        private readonly Dictionary<int, ObservableCollection<Author>> _bookAuthorsCache;

        public AddAuthorToBookCommandHandler(
            IBookAuthorRepository bookAuthorRepository,
            BookViewModel viewModel,
            Dictionary<int, ObservableCollection<Author>> bookAuthorsCache)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _viewModel = viewModel;
            _bookAuthorsCache = bookAuthorsCache;
            _canAddAuthorToBookQueryHandler = new CanAddAuthorToBookQueryHandler(bookAuthorRepository);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedBook != null && _viewModel.AuthorViewModel.SelectedAuthor != null;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.SelectedBook == null || _viewModel.AuthorViewModel.SelectedAuthor == null) return;

            var command = new AddAuthorToBookCommand(_viewModel.SelectedBook, _viewModel.AuthorViewModel.SelectedAuthor);
            if (!_canAddAuthorToBookQueryHandler.Handle(new CanAddAuthorToBookQuery(command.Book.Id, command.Author.Id)))
            {
                MessageBox.Show($"Автор {command.Author.Name} уже доданий до цієї книги.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var bookAuthor = new BookAuthor
                {
                    BookId = command.Book.Id,
                    AuthorId = command.Author.Id
                };
                _bookAuthorRepository.Add(bookAuthor);

                // Оновлюємо кеш
                if (!_bookAuthorsCache.ContainsKey(command.Book.Id))
                    _bookAuthorsCache[command.Book.Id] = new ObservableCollection<Author>();
                _bookAuthorsCache[command.Book.Id].Add(command.Author);

                _viewModel.BooksView.Refresh();
                MessageBox.Show($"Автор {command.Author.Name} доданий до книги!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні автора до книги: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
