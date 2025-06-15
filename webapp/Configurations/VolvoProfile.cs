using AutoMapper;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;

namespace VolvoWebApp.Configurations
{
    public class VolvoProfile : Profile
    {
        public VolvoProfile()
        {
            CreateMap<Vehicle, VehicleReadDTO>();
            CreateMap<VehicleReadDTO, VehicleUpdateDTO>();
            CreateMap<VehicleCreateDTO, Vehicle>();
            CreateMap<VehicleUpdateDTO, Vehicle>()
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
        }
    }
}
