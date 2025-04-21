using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Application.Domain.Books.Commands.SaveEdit
{
    public class SaveEditCommandHandler : ICommand
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookViewModel _viewModel;

        public SaveEditCommandHandler(IBookRepository bookRepository, BookViewModel viewModel)
        {
            _bookRepository = bookRepository;
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.IsEditing && _viewModel.SelectedBook != null &&
                   !string.IsNullOrWhiteSpace(_viewModel.NewBook.Title) && _viewModel.NewBook.Year > 0;
        }

        public void Execute(object parameter)
        {
            try
            {
                var book = _bookRepository.Find(_viewModel.SelectedBook.Id);
                if (book != null)
                {
                    book.Title = _viewModel.NewBook.Title;
                    book.Year = _viewModel.NewBook.Year;
                    _bookRepository.Update(book);

                    _viewModel.SelectedBook.Title = _viewModel.NewBook.Title;
                    _viewModel.SelectedBook.Year = _viewModel.NewBook.Year;
                    _viewModel.BooksView.Refresh();
                    MessageBox.Show("Книга успішно відредагована!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    _viewModel.IsEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при редагуванні книги: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
