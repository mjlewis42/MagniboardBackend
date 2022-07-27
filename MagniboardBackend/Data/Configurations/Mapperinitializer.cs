using AutoMapper;
using MagniboardBackend.Data.DTO;
using MagniboardBackend.Data.EntityModels;

namespace MagniboardBackend.Data.Configurations
{
    public class Mapperinitializer : Profile
    {
        public Mapperinitializer()
        {
            CreateMap<Magnet, MagnetDTO>().ReverseMap();
            CreateMap<Magnet, GetMagnetDTO>().ReverseMap(); 
            CreateMap<Magnet, PutMagnetDTO>().ReverseMap();
            CreateMap<Magnet, PostMagnetDTO>().ReverseMap();


            CreateMap<Template, TemplateDTO>().ReverseMap();
            CreateMap<Template, GetTemplateDTO>().ReverseMap();
            CreateMap<Template, PutTemplateDTO>().ReverseMap();
            CreateMap<Template, PostTemplateDTO>().ReverseMap();
            CreateMap<Template, PutTemplateBoardIdDTO>().ReverseMap();
            CreateMap<Template, PutUnlinkTemplateDTO>().ReverseMap();

            CreateMap<Board, GetBoardDTO>().ReverseMap();
            CreateMap<Board, PostBoardDTO>().ReverseMap();
            CreateMap<Board, PutBoardDTO>().ReverseMap();
            //CreateMap<Board, GetBoardDTO>()
            //    .ForMember(dest => dest.templates, opt => opt.MapFrom(src => src.Templates))
            //    .ReverseMap();


            CreateMap<Row, RowDTO>().ReverseMap();


            CreateMap<Cell, CellDTO>().ReverseMap();

        }
    }
}
