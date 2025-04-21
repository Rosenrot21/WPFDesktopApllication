using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LibraryApp.Application.Domain.Books.Queries;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Common;
using System.Windows.Input;
using System.Windows;

namespace LibraryApp.Application.Domain.Books.Commands.RemoveAuthorFromBook
{
    public class RemoveAuthorFromBookCommandHandler : ICommand
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly BookViewModel _viewModel;
        private readonly CanRemoveAuthorFromBookQueryHandler _canRemoveAuthorFromBookQueryHandler;
        private readonly Dictionary<int, ObservableCollection<Author>> _bookAuthorsCache;

        public RemoveAuthorFromBookCommandHandler(
            IBookAuthorRepository bookAuthorRepository,
            BookViewModel viewModel,
            Dictionary<int, ObservableCollection<Author>> bookAuthorsCache)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _viewModel = viewModel;
            _bookAuthorsCache = bookAuthorsCache;
            _canRemoveAuthorFromBookQueryHandler = new CanRemoveAuthorFromBookQueryHandler(bookAuthorRepository);
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

            var command = new RemoveAuthorFromBookCommand(_viewModel.SelectedBook, _viewModel.AuthorViewModel.SelectedAuthor);
            if (!_canRemoveAuthorFromBookQueryHandler.Handle(new CanRemoveAuthorFromBookQuery(command.Book.Id, command.Author.Id)))
            {
                MessageBox.Show($"Автор {command.Author.Name} не пов'язаний з цією книгою.", "Попередження", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                _bookAuthorRepository.Delete(command.Book.Id, command.Author.Id);

                // Оновлюємо кеш
                if (_bookAuthorsCache.ContainsKey(command.Book.Id))
                {
                    var authorToRemove = _bookAuthorsCache[command.Book.Id].FirstOrDefault(a => a.Id == command.Author.Id);
                    if (authorToRemove != null)
                        _bookAuthorsCache[command.Book.Id].Remove(authorToRemove);
                }

                _viewModel.BooksView.Refresh();
                MessageBox.Show($"Автор {command.Author.Name} видалений з книги!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні автора з книги: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
