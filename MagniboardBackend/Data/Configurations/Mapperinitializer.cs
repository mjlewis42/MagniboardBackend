using AutoMapper;
using MagniboardBackend.Data.DTO;
using MagniboardBackend.Data.EntityModels;

namespace MagniboardBackend.Data.Configurations
{
    public class Mapperinitializer : Profile
    {
        public Mapperinitializer()
        {
            CreateMap<Magnets, MagnetDTO>().ReverseMap();
            CreateMap<Magnets, GetMagnetDTO>().ReverseMap(); 
            CreateMap<Magnets, PutMagnetDTO>().ReverseMap();
            CreateMap<Magnets, PostMagnetDTO>().ReverseMap();


            CreateMap<Table, TableDTO>().ReverseMap();
            CreateMap<Table, GetTableDTO>().ReverseMap();
            CreateMap<Table, PutTableDTO>().ReverseMap();
            CreateMap<Table, PostTableDTO>().ReverseMap();


            CreateMap<Row, RowDTO>().ReverseMap(); ;


            CreateMap<Cell, CellDTO>().ReverseMap();

        }
    }
}
