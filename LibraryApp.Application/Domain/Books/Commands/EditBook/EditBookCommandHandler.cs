using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp.Core.Domain;
using System.Windows.Input;

namespace LibraryApp.Application.Domain.Books.Commands.EditBook
{
    public class EditBookCommandHandler : ICommand
    {
        private readonly BookViewModel _viewModel;

        public EditBookCommandHandler(BookViewModel viewModel)
        {
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
            _viewModel.IsEditing = !_viewModel.IsEditing;
            if (_viewModel.IsEditing)
            {
                _viewModel.NewBook.Title = _viewModel.SelectedBook.Title;
                _viewModel.NewBook.Year = _viewModel.SelectedBook.Year;
                _viewModel.OnPropertyChanged(nameof(_viewModel.NewBook));
            }
        }
    }
}
