using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.EntityModels
{
    public class Table
    {
        public int id { get; set; }

        public string tableName { get; set; }

        public string tableHeader { get; set; }

        public bool showTableHeader { get; set; }

        public int? boardId { get; set; }

        public virtual IList<Row> rows { get; set; }
    }
}
