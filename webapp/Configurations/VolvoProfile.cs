using AutoMapper;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;

namespace VolvoWebApp.Configurations
{
    public class VolvoProfile : Profile
    {
        public VolvoProfile()
        {
            CreateMap<Vehicle, VehicleReadDTO>();
            CreateMap<VehicleReadDTO, VehicleModel>();
            CreateMap<VehicleModel, VehicleCreateDTO>();
            CreateMap<VehicleCreateDTO, Vehicle>();
            CreateMap<VehicleModel, VehicleUpdateDTO>();
            CreateMap<VehicleUpdateDTO, Vehicle>()
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
        }
    }
}
