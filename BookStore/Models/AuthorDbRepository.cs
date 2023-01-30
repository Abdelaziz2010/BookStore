
using BookStore.Models.Repositories;

namespace BookStore.Models
{
    public class AuthorDbRepository:IBookStoreRepository<Author>
    {
        //AuthorDbRepository: is a class that implements IBookStoreRepository interface

        BookStoreDbContext db;

        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public IList<Author> GetAll()
        {
            return db.Authors.ToList();  //.tolist()-->convert it to list
        }
        public Author Get(int id)
        {
            var author = db.Authors.SingleOrDefault(x => x.Id == id);
            return author;
        }
       
        public void Add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges(); //commit changes to database
        }        

        public void Update(int id, Author newAuthor)
        {
            db.Authors.Update(newAuthor);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var author = Get(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public List<Author> Search(string term)
        {
            return db.Authors.Where(a => a.FullName.Contains(term)).ToList();
        }
    }
}
