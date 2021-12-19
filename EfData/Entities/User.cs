using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfData.Entities
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("first_name")]
        public string FName { get; set; }

        [Column("second_name")]
        public string SName { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        public List<UserSubscription> UserSubscription { get; set; }

        public List<Notification> Notification { get; set; }
    }
}
