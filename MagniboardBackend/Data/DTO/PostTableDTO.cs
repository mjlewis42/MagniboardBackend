using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class PostTableDTO
    {
        [Required]
        public int id { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "Your table name is too long!")]
        public string tableName { get; set; }

        public string tableHeader { get; set; }

        public bool showTableHeader { get; set; }

        [Required]
        public IList<RowDTO> rows { get; set; }
    }
}
