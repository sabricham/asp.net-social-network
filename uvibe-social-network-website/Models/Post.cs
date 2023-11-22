using System.ComponentModel.DataAnnotations;

namespace uvibe_social_network_website.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime CreationDateTime { get; set; }

        public bool IsPublic { get; set; } //true: can be seen by others
        public bool IsCommentable { get; set; } //true: others can comment
        public bool IsShareable { get; set; } //true: anybody can share
    }
}
