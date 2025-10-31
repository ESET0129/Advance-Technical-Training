using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementAPI.Model
{
    public class Book
    {
        [Key]  // Marks this as the primary key
        public int BookId { get; set; }  // Primary key property

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;  // Book title, required

        [StringLength(50)]
        public string? Genre { get; set; }  // Optional genre property

        [ForeignKey("Author")]
        public int AuthorId { get; set; }  // Foreign key pointing to Author

        public Author? Author { get; set; }  // Navigation property to Author
    }
}
