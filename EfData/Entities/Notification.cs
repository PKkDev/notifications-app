using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfData.Entities
{
    [Table("notification")]
    public class Notification
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("system")]
        public string System { get; set; }

        [Column("theme")]
        public string Theme { get; set; }

        [Column("is_sended")]
        public bool IsSended { get; set; }

        public User User { get; set; }
    }
}
