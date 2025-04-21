using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Infrastructure.Core.Domain;
using System.Windows.Data;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Application.Domain.Books.Commands;

namespace LibraryDesktopApplication;

public class BookToAuthorsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Book book)
        {
            var viewModel = App.Current.MainWindow?.DataContext as BookViewModel;
            if (viewModel != null)
            {
                var authors = viewModel.GetAuthorsForBook(book);
                return authors; 
            }
        }
        return new ObservableCollection<Author>(); 
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
