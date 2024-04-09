namespace PizzeriaAPI.Identity.DTO.User
{
    public class RequestUserDto
    {
        public required string Email { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
