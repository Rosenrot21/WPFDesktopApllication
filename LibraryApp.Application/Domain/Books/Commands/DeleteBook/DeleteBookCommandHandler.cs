using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Application.Domain.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : ICommand
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly ObservableCollection<Book> _books;
        private readonly BookViewModel _viewModel;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository, ObservableCollection<Book> books, BookViewModel viewModel)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _books = books;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.SelectedBook != null;
        }

        public void Execute(object parameter)
        {
            if (_viewModel.SelectedBook == null) return;

            try
            {
                var bookId = _viewModel.SelectedBook.Id;
                var bookAuthors = _bookAuthorRepository.GetAuthorsForBook(bookId);
                foreach (var author in bookAuthors)
                {
                    _bookAuthorRepository.Delete(bookId, author.Id);
                }

                _bookRepository.Delete(bookId);
                _books.Remove(_viewModel.SelectedBook);
                _viewModel.RemoveAuthorsCacheForBook(bookId);
                MessageBox.Show("Книга успішно видалена!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _viewModel.SelectedBook = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні книги: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
