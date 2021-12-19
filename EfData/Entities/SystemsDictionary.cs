using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfData.Entities
{
    [Table("systems_dictionary")]
    public class SystemsDictionary
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public List<ThemeDictionary> Themes { get; set; }
        public List<UserSubscription> UserSubscription { get; set; }
    }
}
