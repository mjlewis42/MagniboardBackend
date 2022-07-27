using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.DTO
{
    public class RowDTO 
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int rowOrder { get; set; }

        [Required]
        [ForeignKey("TemplateId")]
        public int TemplateId { get; set; }

        public IList<CellDTO> cells { get; set; }
    }

}
