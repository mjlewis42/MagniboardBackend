using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class TableDTO
    {
        public int id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your table name is too long!")]
        public string tableName { get; set; }

        public string tableHeader { get; set; }

        public bool showTableHeader { get; set; }
        
        public int? boardId { get; set; }

        public IList<RowDTO> rows { get; set; }
    }

}
