using FTech.Application.DataTransferObjects.Cars;

namespace FTech.Application.Services.Cars
{
    public interface ICarService
    {
        public ValueTask<CarForResultDTO> CreateAsync(CarForCreationDTO dto);
        public ValueTask<CarForResultDTO> UpdateAsync(long id, CarForCreationDTO dto);
        public ValueTask<bool> DeleteAsync(long id);
        public ValueTask<CarForResultDTO> GetByIdAsync(long id);
        public ValueTask<IEnumerable<CarForResultDTO>> GetAllAsync();
    }
}
