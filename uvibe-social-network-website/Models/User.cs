using System.ComponentModel.DataAnnotations;

namespace uvibe_social_network_website.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsPublic { get; set; } //true: can be seen by others
    }
}
