using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LibraryApp.Application.Domain.Authors.Queries;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Authors.Models;
using System.Windows.Input;
using System.Windows;

namespace LibraryApp.Application.Domain.Authors.Commands.AddAuthor
{
    public class AddAuthorCommandHandler : ICommand
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ObservableCollection<Author> _authors;
        private readonly CanAddAuthorQueryHandler _canAddAuthorQueryHandler;
        private Author _newAuthor;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository, ObservableCollection<Author> authors, Author newAuthor)
        {
            _authorRepository = authorRepository;
            _authors = authors;
            _newAuthor = newAuthor;
            _canAddAuthorQueryHandler = new CanAddAuthorQueryHandler(authorRepository);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(_newAuthor.Name);
        }

        public void Execute(object parameter)
        {
            var command = new AddAuthorCommand(new Author { Name = _newAuthor.Name });
            if (!_canAddAuthorQueryHandler.Handle(new CanAddAuthorQuery(command.Author.Name)))
            {
                MessageBox.Show("Автор з таким ім'ям вже існує!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _authorRepository.Add(command.Author);
                _authors.Add(command.Author);
                MessageBox.Show("Автор успішно доданий!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _newAuthor.Name = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні автора: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
