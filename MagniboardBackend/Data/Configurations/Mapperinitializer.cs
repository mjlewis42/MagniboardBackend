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
            CreateMap<Table, PutTableBoardIdDTO>().ReverseMap();

            CreateMap<Board, GetBoardDTO>().ReverseMap();
            CreateMap<Board, PostBoardDTO>().ReverseMap();
            CreateMap<Board, PutBoardDTO>().ReverseMap();
            //CreateMap<Board, GetBoardDTO>()
            //    .ForMember(dest => dest.templates, opt => opt.MapFrom(src => src.tables))
            //    .ReverseMap();


            CreateMap<Row, RowDTO>().ReverseMap();


            CreateMap<Cell, CellDTO>().ReverseMap();

        }
    }
}
