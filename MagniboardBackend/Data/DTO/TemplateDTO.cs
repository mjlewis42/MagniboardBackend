using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class TemplateDTO
    {
        public int id { get; set; }

        public bool isActive { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your Template name is too long!")]
        public string templateName { get; set; }

        public string templateHeader { get; set; }

        public bool showTemplateHeader { get; set; }

        public IList<RowDTO> rows { get; set; }
    }

}
