using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LibraryApp.Application.Domain.Books.Commands.CancelEdit
{
    public class CancelEditCommandHandler : ICommand
    {
        private readonly BookViewModel _viewModel;

        public CancelEditCommandHandler(BookViewModel viewModel)
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
            return true; 
        }

        public void Execute(object parameter)
        {
            _viewModel.IsEditing = false;
            _viewModel.NewBook.Title = _viewModel.SelectedBook?.Title ?? string.Empty;
            _viewModel.NewBook.Year = _viewModel.SelectedBook?.Year ?? 0;
            _viewModel.OnPropertyChanged(nameof(_viewModel.NewBook));
        }
    }
}
