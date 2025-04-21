using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using LibraryApp.Application.Domain.Books.Commands.AddBook;
using LibraryApp.Application.Domain.Books.Commands.CancelEdit;
using LibraryApp.Application.Domain.Books.Commands.DeleteBook;
using LibraryApp.Application.Domain.Books.Commands.EditBook;
using LibraryApp.Application.Domain.Books.Commands.SaveEdit;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Application.Domain.Authors.Commands;
using LibraryApp.Application.Domain.Books.Commands.AddAuthorToBookCommand;
using LibraryApp.Application.Domain.Books.Commands.RemoveAuthorFromBook;

namespace LibraryApp.Application.Domain.Books.Commands
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IAuthorRepository _authorRepository;
        private ObservableCollection<Book> _books;
        private Book _selectedBook;
        private ICollectionView _booksView;
        private readonly AuthorViewModel _authorViewModel;
        private bool _isEditing;
        private readonly Dictionary<int, ObservableCollection<Author>> _bookAuthorsCache;

        public ICommand AddBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand SaveEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand AddAuthorToBookCommand { get; }
        public ICommand RemoveAuthorFromBookCommand { get; }

        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
                BooksView = CollectionViewSource.GetDefaultView(_books);
            }
        }

        public AuthorViewModel AuthorViewModel => _authorViewModel;

        public ICollectionView BooksView
        {
            get => _booksView;
            set { _booksView = value; OnPropertyChanged(); }
        }

        public Book NewBook { get; set; } = new Book();

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EditButtonText));
            }
        }

        public string EditButtonText => IsEditing ? "Скасувати редагування" : "Редагувати книгу";

        public BookViewModel(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _authorRepository = authorRepository;
            _bookAuthorsCache = new Dictionary<int, ObservableCollection<Author>>();

            Books = new ObservableCollection<Book>(_bookRepository.GetAll());
            BooksView = CollectionViewSource.GetDefaultView(Books);
            _authorViewModel = new AuthorViewModel(_authorRepository, _bookAuthorRepository);

            // Ініціалізуємо кеш авторів для всіх книг
            foreach (var book in Books)
            {
                var authors = _bookAuthorRepository.GetAuthorsForBook(book.Id);
                _bookAuthorsCache[book.Id] = new ObservableCollection<Author>(authors);
            }

            AddBookCommand = new AddBookCommandHandler(_bookRepository, Books, NewBook, this);
            DeleteBookCommand = new DeleteBookCommandHandler(_bookRepository, _bookAuthorRepository, Books, this);
            EditBookCommand = new EditBookCommandHandler(this);
            SaveEditCommand = new SaveEditCommandHandler(_bookRepository, this);
            CancelEditCommand = new CancelEditCommandHandler(this);
            AddAuthorToBookCommand = new AddAuthorToBookCommandHandler(_bookAuthorRepository, this, _bookAuthorsCache);
            RemoveAuthorFromBookCommand = new RemoveAuthorFromBookCommandHandler(_bookAuthorRepository, this, _bookAuthorsCache);
        }

        public ObservableCollection<Author> GetAuthorsForBook(Book book)
        {
            if (book == null || !_bookAuthorsCache.ContainsKey(book.Id))
                return new ObservableCollection<Author>();

            return _bookAuthorsCache[book.Id];
        }

        public void UpdateAuthorsCacheForNewBook(Book book)
        {
            var authors = _bookAuthorRepository.GetAuthorsForBook(book.Id);
            _bookAuthorsCache[book.Id] = new ObservableCollection<Author>(authors);
        }

        public void RemoveAuthorsCacheForBook(int bookId)
        {
            if (_bookAuthorsCache.ContainsKey(bookId))
                _bookAuthorsCache.Remove(bookId);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}