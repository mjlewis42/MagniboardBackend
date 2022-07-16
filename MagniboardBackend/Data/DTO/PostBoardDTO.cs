namespace MagniboardBackend.Data.DTO
{
    public class PostBoardDTO
    {
        public int id { get; set; }
        public string boardName { get; set; }
        public bool isActive { get; set; } = false;
    }
}
