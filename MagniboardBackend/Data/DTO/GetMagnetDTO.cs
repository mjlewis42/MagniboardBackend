using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class GetMagnetDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your Template name is too long!")]
        public string MagnetName { get; set; } = null!;
        public string MagnetColor { get; set; } = null!;
        public string TextColor { get; set; } = null!;
    }
}
