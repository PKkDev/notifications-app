using EfData.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfData.Entities
{
    [Table("user_subscription")]
    public class UserSubscription
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("system_id")]
        public int SystemId { get; set; }

        [Column("theme_id")]
        public int ThemeId { get; set; }

        [Column("type")]
        public TypeSubscription Type { get; set; }

        public User User { get; set; }
        public ThemeDictionary ThemeDictionary { get; set; }
        public SystemsDictionary SystemsDictionary { get; set; }
    }
}
