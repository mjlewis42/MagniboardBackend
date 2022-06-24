namespace MagniboardBackend.Data.EntityModels
{
    public partial class Magnets
    {
        public int Id { get; set; }
        public string MagnetName { get; set; } = null!;
        public string MagnetColor { get; set; } = null!;
    }
}
