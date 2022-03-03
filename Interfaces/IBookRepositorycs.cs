using LibApp.Models;
using System.Collections.Generic;

namespace LibApp.Interfaces
{
    public interface IBookRepository
    {
        public IEnumerable<Book> GetBooks();
        public Book GetBookById(int Id);
        public void AddBook(Book book);
        public void DeleteBook(int id);
        public void UpdateBook(Book book);
        void Save();

    }
}