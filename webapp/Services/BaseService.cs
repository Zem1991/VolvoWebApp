using AutoMapper;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class BaseService<Data, ReadDto, CreateDto, UpdateDto> : IBaseService<Data, ReadDto, CreateDto, UpdateDto> where Data : BaseRecord
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<Data> _repository;

        public BaseService(IMapper mapper, IBaseRepository<Data> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ReadDto>> GetAllAsync()
        {
            IEnumerable<Data> result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadDto>>(result);
        }

        public async Task<ReadDto?> GetByIdAsync(string id)
        {
            Data? result = await _repository.GetByIdAsync(id);
            return _mapper.Map<ReadDto>(result);
        }

        public async Task<bool> CreateAsync(CreateDto vehicle)
        {
            Data entity = _mapper.Map<CreateDto, Data>(vehicle);
            bool result = await _repository.InsertAsync(entity);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateDto vehicle)
        {
            Data entity = _mapper.Map<UpdateDto, Data>(vehicle);
            bool result = await _repository.UpdateAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Data? entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;
            bool result = await _repository.DeleteAsync(entity);
            return result;
        }
    }
}
