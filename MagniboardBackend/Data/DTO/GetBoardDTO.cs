using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class GetBoardDTO
    {
        public int id { get; set; }
        public string boardName { get; set; }
        public bool isActive { get; set; }
        public List<TemplateDTO> templates { get; set; }
    }
}
