using AutoMapper;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Repositories;

namespace VolvoWebApp.Services
{
    public class BaseService<Data, ReadDto, CreateDto, UpdateDto> : IBaseService<Data, ReadDto, CreateDto, UpdateDto> where Data : BaseRecord
    {
        protected readonly IMapper _mapper;
        protected readonly IBaseRepository<Data> _baseRepository;

        public BaseService(IMapper mapper, IBaseRepository<Data> baseRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<ReadDto>> GetAllAsync()
        {
            IEnumerable<Data> result = await _baseRepository.GetAllAsync();
            result = result.OrderByDescending(x => x.LastUpdate);
            return _mapper.Map<IEnumerable<ReadDto>>(result);
        }

        public async Task<ReadDto?> GetByIdAsync(string id)
        {
            Data? result = await _baseRepository.GetByIdAsync(id);
            return _mapper.Map<ReadDto>(result);
        }

        public async Task<bool> CreateAsync(CreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            Data entity = _mapper.Map<CreateDto, Data>(dto);
            bool result = await _baseRepository.InsertAsync(entity);
            return result;
        }

        public async Task<bool> UpdateAsync(UpdateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));
            Data entity = _mapper.Map<UpdateDto, Data>(dto);
            bool result = await _baseRepository.UpdateAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Data? entity = await _baseRepository.GetByIdAsync(id);
            if (entity == null) return false;
            bool result = await _baseRepository.DeleteAsync(entity);
            return result;
        }
    }
}
