using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Application.Domain.Authors.Commands.AddAuthor;
using LibraryApp.Application.Domain.Authors.Commands.DeleteAuthor;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Application.Domain.Authors.Commands
{
    public class AuthorViewModel : INotifyPropertyChanged
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private ObservableCollection<Author> _authors;
        private Author _selectedAuthor;
        private Author _newAuthor;

        public ICommand AddAuthorCommand { get; }
        public ICommand DeleteAuthorCommand { get; }

        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set { _authors = value; OnPropertyChanged(); }
        }

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set { _selectedAuthor = value; OnPropertyChanged(); }
        }

        public Author NewAuthor
        {
            get => _newAuthor;
            set { _newAuthor = value; OnPropertyChanged(); }
        }

        public AuthorViewModel(IAuthorRepository authorRepository, IBookAuthorRepository bookAuthorRepository = null)
        {
            _authorRepository = authorRepository;
            _bookAuthorRepository = bookAuthorRepository;
            Authors = new ObservableCollection<Author>(_authorRepository.GetAll());
            NewAuthor = new Author();
            AddAuthorCommand = new AddAuthorCommandHandler(_authorRepository, Authors, NewAuthor);
            DeleteAuthorCommand = new DeleteAuthorCommandHandler(_authorRepository, _bookAuthorRepository, Authors, this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
