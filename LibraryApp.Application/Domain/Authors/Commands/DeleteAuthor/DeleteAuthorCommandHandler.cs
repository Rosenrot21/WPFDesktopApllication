using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Common;
using System.Windows.Input;
using System.Windows;

namespace LibraryApp.Application.Domain.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandHandler : ICommand
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly ObservableCollection<Author> _authors;
        private readonly AuthorViewModel _viewModel;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IBookAuthorRepository bookAuthorRepository, ObservableCollection<Author> authors, AuthorViewModel viewModel)
        {
            _authorRepository = authorRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _authors = authors;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedAuthor != null;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.SelectedAuthor == null) return;

            try
            {
                var authorId = _viewModel.SelectedAuthor.Id;
                var booksWithAuthor = _bookAuthorRepository.GetBooksForAuthor(authorId); 
                if (booksWithAuthor.Any())
                {
                    MessageBox.Show("Цей автор пов'язаний з книгами. Спочатку видаліть його з усіх книг!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _authorRepository.Delete(authorId);
                _authors.Remove(_viewModel.SelectedAuthor);
                MessageBox.Show("Автор успішно видалений!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModel.SelectedAuthor = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні автора: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
