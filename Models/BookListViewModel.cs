using System.Collections.Generic;

namespace Library_test.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> SelectedGenres { get; set; }
    }
}
