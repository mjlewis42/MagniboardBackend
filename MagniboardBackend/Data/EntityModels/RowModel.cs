using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.EntityModels
{
    public class Row
    {
        public int id { get; set; }

        public int rowOrder { get; set; }

        [ForeignKey("tableId")]
        public int tableId { get; set; }

        public virtual IList<Cell> cells { get; set; }
    }
}
