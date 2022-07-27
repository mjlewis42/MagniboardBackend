using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class PutTemplateBoardIdDTO
    {
        [Required]
        public int id { get; set; }
        public int boardId { get; set; }
    }
}
