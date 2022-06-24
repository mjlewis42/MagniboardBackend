using AutoMapper;
using MagniboardBackend.Data.DTO;
using MagniboardBackend.Data.EntityModels;

namespace MagniboardBackend.Data.Configurations
{
    public class Mapperinitializer : Profile
    {
        public Mapperinitializer()
        {
            CreateMap<Table, TableDTO>().ReverseMap(); ;
            CreateMap<TableDTO, Table>().ReverseMap(); ;
            //CreateMap<Table, CreateTableDTO>().ReverseMap();
           // CreateMap<Table, TableReadOnlyDTO>().ReverseMap();

            CreateMap<Row, RowDTO>().ReverseMap(); ;
            CreateMap<RowDTO, Row>().ReverseMap(); ;
            // CreateMap<Row, CreateRowDTO>().ReverseMap();
            // CreateMap<Row, RowReadOnlyDTO>().ReverseMap();

            CreateMap<Cell, CellDTO>().ReverseMap();
            CreateMap<CellDTO, Cell>().ReverseMap();

        }
    }
}
