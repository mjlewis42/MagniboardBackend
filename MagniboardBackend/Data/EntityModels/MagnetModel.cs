namespace MagniboardBackend.Data.EntityModels
{
    public partial class Magnet
    {
        public int Id { get; set; }
        public string MagnetName { get; set; } = null!;
        public string MagnetColor { get; set; } = null!;
        public string TextColor { get; set; } = null!;
    }
}
