using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Model
{
    //public class Author
    //{
    //    [Key]
    //    public int AuthorId { get; set; }
    //    [Required]
    //    [StringLength(50)]
    //    public string Name { get; set; }
    //    [StringLength(100)]
    //    public string? Country { get; set; }

    //    public ICollection<Book>? Books { get; set; }
    //    public object?[]? ID { get; internal set; }
    //}
    public class Author
    {
        public int AuthorID { get; set; }   // Correct primary key property
        public string Name { get; set; }
        public string? Country { get; set; }
        public ICollection<Book>? Books { get; set; } // Navigation property
    }
}
