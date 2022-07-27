using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class PostTemplateDTO
    {
        [Required]
        public int id { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Your Template name is too long!")]
        public string TemplateName { get; set; }

        public string TemplateHeader { get; set; }

        public bool showTemplateHeader { get; set; }

        [Required]
        public IList<RowDTO> rows { get; set; }
    }
}
