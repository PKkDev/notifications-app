using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfData.Entities
{
    [Table("theme_dictionary")]
    public class ThemeDictionary
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("system_id")]
        public int SystemsDictionaryId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public SystemsDictionary System { get; set; }
        public List<UserSubscription> UserSubscription { get; set; }
    }
}
