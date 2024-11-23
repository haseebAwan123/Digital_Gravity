namespace Digital_Gravity.Models
{
    public class Users
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
