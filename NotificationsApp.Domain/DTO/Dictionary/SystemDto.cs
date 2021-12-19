using System.Collections.Generic;

namespace NotificationsApp.Domain.DTO.Dictionary
{
    public class SystemDto
    {
        public int Id { get; set; }

        public string SystemName { get; set; }

        public List<ThemeDto> Themes { get; set; }

        public SystemDto(int id, string name)
        {
            Id = id;
            SystemName = name;
            Themes = new List<ThemeDto>();
        }
    }
}
