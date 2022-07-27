using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class PutTemplateDTO
    {
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your Template name is too long!")]
        public string TemplateName { get; set; }

        [Required]
        public string TemplateHeader { get; set; }

        [Required]
        public bool showTemplateHeader { get; set; }

        [Required]
        public IList<RowDTO> rows { get; set; }
    }
}
