using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Infrastructure.Core.Domain;
using System.Windows.Data;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Application.Domain.Books.Commands;

namespace LibraryDesktopApplication
{
    public class AuthorsToStringMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is Book book && values[1] is BookViewModel viewModel)
            {
                var authors = viewModel.GetAuthorsForBook(book);
                if (authors != null && authors.Any())
                {
                    return string.Join(", ", authors.Select(a => a.Name));
                }
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
