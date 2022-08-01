using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class GetActiveBoardDTO
    {
        public List<MagnetDTO>? magnetPouch { get; set; }
        public TemplateDTO template { get; set; }
       
    }
}
