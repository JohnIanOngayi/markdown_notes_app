using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace markdown_notes_app.Core.Entities
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Consider Guid Later

        [Required(ErrorMessage = "Title Is Required")]
        [StringLength(200, ErrorMessage = "Title Cannot Be Longer Than 200 Chars")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "No File Has Been Uploaded")]
        [StringLength(10485760, ErrorMessage = "File Must Be Less Than 10MB")] //10MB Limit
        public string Content { get; set; } = string.Empty;

        //[Required]
        //[StringLength(64)]
        //public string? ContentHash { get; set; } // Integrity and Caching

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
