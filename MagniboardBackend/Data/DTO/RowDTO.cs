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
        [ForeignKey("tableId")]
        public int tableId { get; set; }

        public IList<CellDTO> cells { get; set; }
    }

}
