
using BookStore.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [MaxLength(120)]
        [MinLength(5)]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<Author>Authors { get; set; }

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
