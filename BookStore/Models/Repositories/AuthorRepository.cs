
namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        //AuthorRepository: is a class that implements IBookStoreRepository interface

        IList<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author
                { 
                   Id = 1,
                   FullName="Naguib Mahfouz"
                },
                new Author
                {
                   Id = 2,
                   FullName="Friedrich Nietzsche"
                },
                new Author
                {
                   Id = 3,
                   FullName="Taha Hussein"
                },
                new Author
                {
                   Id = 4,
                   FullName="Fyodor Dostoevsky"
                },
                new Author
                {
                    Id= 5,
                    FullName="Tolstoy"
                }
            }; 
        }
        public void Add(Author author)
        {
            author.Id = authors.Max(x => x.Id) + 1;
            authors.Add(author);
        }

        public void Delete(int id)
        {
            var author = Get(id);
            authors.Remove(author);
        }

        public Author Get(int id)
        {
            var author = authors.SingleOrDefault(x => x.Id == id);
            return author;
        }

        public IList<Author> GetAll()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return authors.Where(a=>a.FullName.Contains(term)).ToList();
        }

        public void Update(int id, Author newAuthor)
        {
            var author=Get(id);
            author.FullName = newAuthor.FullName;
        }
    }
}
