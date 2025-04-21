using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Application.Domain.Books.Commands;

namespace LibraryDesktopApplication;

public class AuthorsToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Book book && parameter is BookViewModel viewModel)
        {
            var authors = viewModel.GetAuthorsForBook(book);
            if (authors != null && authors.Any())
            {
                return string.Join(", ", authors.Select(a => a.Name));
            }
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
