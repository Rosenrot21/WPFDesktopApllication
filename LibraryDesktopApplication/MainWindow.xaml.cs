using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryApp.Application.Domain.Books.Commands;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Infrastructure;
using LibraryApp.Infrastructure.Core.Domain;
using LibraryApp.Persistence.LibraryDb;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryDesktopApplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    private string _currentSortColumn = null;
    private ListSortDirection _currentSortDirection = ListSortDirection.Ascending;
    private readonly IServiceProvider _serviceProvider;

    public MainWindow()
    {
        _serviceProvider = ServiceConfiguration.ConfigureServices();
        InitializeComponent();
        DataContext = new BookViewModel(
            _serviceProvider.GetRequiredService<IBookRepository>(),
            _serviceProvider.GetRequiredService<IBookAuthorRepository>(),
            _serviceProvider.GetRequiredService<IAuthorRepository>());
    }

    private void GridViewColumnHeader_Clicked(object sender, RoutedEventArgs e)
    {
        if (sender is GridViewColumnHeader header && header.Tag is string sortBy)
        {
            var viewModel = DataContext as BookViewModel;
            if (viewModel == null) return;

            var view = viewModel.BooksView;
            if (view == null) return;

            ListSortDirection newDirection = ListSortDirection.Ascending;
            if (_currentSortColumn == sortBy && _currentSortDirection == ListSortDirection.Ascending)
            {
                newDirection = ListSortDirection.Descending;
            }

            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(sortBy, newDirection));

            _currentSortColumn = sortBy;
            _currentSortDirection = newDirection;
        }
    }
}