using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class GetTableDTO : TableDTO
    {
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your table name is too long!")]
        public string tableName { get; set; }

        [Required]
        public string tableHeader { get; set; }

        [Required]
        public bool showTableHeader { get; set; }

        [Required]
        public IList<RowDTO> rows { get; set; }
    }
}
