using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.EntityModels
{
    public class Cell
    {
        public int id { get; set; }
        public int cellOrder { get; set; }
        public int colSpan { get; set; }
        public int rowSpan { get; set; }
        public bool header { get; set; }
        public bool droppable { get; set; }
        public string text { get; set; }

        [ForeignKey("rowId")]
        public int rowId { get; set; }
        public Magnet? magnet { get; set; }
    } 
}
