using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.DTO
{
    public class CellDTO
    {
        [Required]
        public int id { get; set; }
        public int cellOrder { get; set; }
        public int colSpan { get; set; }
        public int rowSpan { get; set; }
        public bool header { get; set; }
        public bool droppable { get; set; }
        public string text { get; set; }

        [Required]
        [ForeignKey("tableId")]
        public int rowId { get; set; }
    }
}
