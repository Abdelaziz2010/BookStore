
namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepository<TEntity>
    {
        //IBookStoreRepository: is the Interface that  holds the declarations   
        //of the operations(functions) that will be applied on models.
        IList<TEntity> GetAll();             //List():display all elements.
        TEntity Get(int id);                 //Find():searches for specific elements by id.
        void Add(TEntity entity);            //Create():add new element.
        void Update(int id,TEntity entity);  //Update():update existing element.
        void Delete(int id);                 //Delete():delete specific elements by id.
        List<TEntity> Search(string term);
    }
}
