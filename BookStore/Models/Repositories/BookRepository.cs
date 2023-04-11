using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        //BookRepository: is a class that implements IBookStoreRepository interface.
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Title = "The Harafish",
                    Description = "no description",
                    Author = new Author{Id = 1,FullName="Naguib Mahfouz" },
                    ImageUrl="2.jfif"
                },
                new Book
                {
                    Id = 2,
                    Title = "Beyond Good and Evil",
                    Description = "no data",
                    Author = new Author{Id = 2,FullName="Friedrich Nietzsche" },
                    ImageUrl="5.jfif"
                },
                new Book
                {
                    Id = 3,
                    Title = "The Days",
                    Description = "nothing",
                    Author = new Author{Id = 3,FullName="Taha Hussein" },
                    ImageUrl="3.jfif"
                },
                new Book
                {
                    Id = 4,
                    Title = "Crime and Punishment",
                    Description = "no description",
                    Author = new Author{Id = 4,FullName="Fyodor Dostoevsky" },
                    ImageUrl="4.jfif"
                },
                new Book
                {
                    Id = 5,
                    Title = "War and Peace",
                    Description = "no description",
                    Author = new Author{Id = 5,FullName="Tolstoy " },
                    ImageUrl="1.jfif"
                }
            };
        }
        public IList<Book> GetAll()
        {
            return books;
        }
        public Book Get(int id)
        {
            var book = books.SingleOrDefault(d => d.Id == id);
            return book;
        }
        public void Add(Book book)
        {
            book.Id = books.Max(d => d.Id) + 1;
            books.Add(book);
        }
        public void Update(int id,Book newBook)
        {
            var book = Get(id);
            book.Title = newBook.Title;
            book.Description = newBook.Description;
            book.Author = newBook.Author;
            book.ImageUrl = newBook.ImageUrl;
        }
        public void Delete(int id)
        {
            var book=Get(id);  
            books.Remove(book);
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }
    }
}
