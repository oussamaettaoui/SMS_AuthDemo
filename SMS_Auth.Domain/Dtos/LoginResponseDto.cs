namespace SMS_Auth.Domain.Dtos
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
