namespace MagniboardBackend.Data.DTO
{
    public class UserRoleDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RoleDTO Role { get; set; }
    }
}
