using System.Text.Json.Serialization;

namespace MagniboardBackend.Data.EntityModels
{
    public class Board
    {
        public int id { get; set; }
        public string boardName { get; set; }
        public bool isActive { get; set; }
        //[JsonPropertyName("templates")]
        //public List<Template> templates { get; set; }
    }
}
