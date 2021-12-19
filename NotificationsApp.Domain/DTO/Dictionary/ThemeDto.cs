namespace NotificationsApp.Domain.DTO.Dictionary
{
    public class ThemeDto
    {
        public int Id { get; set; }

        public string ThemeName { get; set; }

        public ThemeDto(int id, string name)
        {
            Id = id;
            ThemeName = name;
        }
    }
}
