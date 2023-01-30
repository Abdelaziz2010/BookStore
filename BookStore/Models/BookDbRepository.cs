
using BookStore.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class BookDbRepository: IBookStoreRepository<Book>
    {
        //BookDbRepository: is a class that implements IBookStoreRepository interface.
        BookStoreDbContext db;
        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;   
        }
        public IList<Book> GetAll()
        {
            return db.Books.Include(a=>a.Author).ToList(); //get the name of th author/Include(a=>a.Author)
        }
        public Book Get(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(d => d.Id == id);
            return book;
        }
        public void Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
        public void Update(int id, Book newBook)
        {
            db.Books.Update(newBook);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var book = Get(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }
        public List<Book> Search(string term)
        {
            var result=db.Books.Include(a=>a.Author)
                .Where(b=>b.Title.Contains(term)
                        ||b.Description.Contains(term)
                        ||b.Author.FullName.Contains(term)).ToList();
       
            return result;
        }
    }
}

