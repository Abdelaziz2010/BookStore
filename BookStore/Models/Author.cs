
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
