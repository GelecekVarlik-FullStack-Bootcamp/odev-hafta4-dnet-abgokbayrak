using AutoMapper;
using BMS.Entity.Dto;
using BMS.Entity.Models;

namespace BMS.Entity.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee Related Dto Mapping
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, LoginUserDto>();
            CreateMap<Employee, AddEmployeeResponseDto>();
            #endregion


            #region Request Related Dto Mapping
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<Request, RequestForListDto>()
                .ForMember(dest => dest.RequesterName, opt => opt.MapFrom(src => $"{src.Requester.Name} {src.Requester.Surname}"))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.Name));
            CreateMap<Request, RequestDetailDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.PriorityName, opt => opt.MapFrom(src => src.Priority.Name))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee == null ? "" : $"{src.Employee.Name} {src.Employee.Surname}"))
                .ForMember(dest => dest.RequestSubjectName, opt => opt.MapFrom(src => src.RequestSubject.Name))
                .ForMember(dest => dest.RequesterName, opt => opt.MapFrom(src => $"{src.Requester.Name} {src.Requester.Surname}"))
                .ForMember(dest => dest.RequestMessages, opt => opt.Ignore());
            CreateMap<RequestMessage, RequestMessageForListDto>()
                .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender == null ? "" : $"{src.Sender.Name} {src.Sender.Surname}"));
            CreateMap<RequestMessage, RequestMessageDto>().ReverseMap();
            
            
            #endregion
        }
    }
}