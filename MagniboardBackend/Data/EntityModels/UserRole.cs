namespace MagniboardBackend.Data.EntityModels
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
    }
}
