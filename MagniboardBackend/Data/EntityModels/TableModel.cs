using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniboardBackend.Data.EntityModels
{
    public class Template
    {
        public int id { get; set; }

        public string templateName { get; set; }

        public string templateHeader { get; set; }

        public bool showTemplateHeader { get; set; }

        public int? boardId { get; set; }

        public virtual IList<Row> rows { get; set; }
    }
}
