using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.DTO
{
    public class CellDTO
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int cellOrder { get; set; }
        [Required]
        public int colSpan { get; set; }
        [Required]
        public int rowSpan { get; set; }
        [Required]
        public bool header { get; set; }
        [Required]
        public bool droppable { get; set; }
        [Required]
        public string text { get; set; }

        [Required]
        [ForeignKey("tableId")]
        public int rowId { get; set; }
    }
}
