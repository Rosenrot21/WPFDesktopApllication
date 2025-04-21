using System.Collections.ObjectModel;

using LibraryApp.Application.Domain.Books.Queries;
using System.Windows;
using System.Windows.Input;

using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Application.Domain.Books.Commands.AddBook
{
    public class AddBookCommandHandler : ICommand
    {
        private readonly IBookRepository _bookRepository;
        private readonly ObservableCollection<Book> _books;
        private readonly Book _newBook;
        private readonly BookViewModel _viewModel;
        private readonly CanAddBookQueryHandler _canAddBookQueryHandler;

        public AddBookCommandHandler(IBookRepository bookRepository, ObservableCollection<Book> books, Book newBook, BookViewModel viewModel)
        {
            _bookRepository = bookRepository;
            _books = books;
            _newBook = newBook;
            _viewModel = viewModel;
            _canAddBookQueryHandler = new CanAddBookQueryHandler(bookRepository);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_newBook.Title) && _newBook.Year > 0;
        }

        public void Execute(object parameter)
        {
            var command = new AddBookCommand(new Book { Title = _newBook.Title, Year = _newBook.Year });
            if (!_canAddBookQueryHandler.Handle(new CanAddBookQuery(command.Book.Title)))
            {
                MessageBox.Show("Книга з такою назвою вже існує!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _bookRepository.Add(command.Book);
                _books.Add(command.Book);
                _viewModel.UpdateAuthorsCacheForNewBook(command.Book);
                MessageBox.Show("Книга успішно додана!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _newBook.Title = string.Empty;
                _newBook.Year = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні книги: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
