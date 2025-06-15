using AutoMapper;
using VolvoWebApp.Dtos;
using VolvoWebApp.Models;

namespace VolvoWebApp.Configurations
{
    public class VolvoProfile : Profile
    {
        public VolvoProfile()
        {
            //from dto object to database model, for UPDATING
            CreateMap<VehicleUpdateDTO, Vehicle>()
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));
        }
    }
}
