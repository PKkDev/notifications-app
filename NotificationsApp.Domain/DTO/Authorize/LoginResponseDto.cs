namespace NotificationsApp.Domain.DTO.Authorize
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }

        public LoginResponseDto(int id, string token, string role, string name)
        {
            Id = id;
            Token = token;
            Role = role;
            Name = name;
        }
    }
}
