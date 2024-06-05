using AutoMapper;
using FTech.Application.DataTransferObjects.Cars;
using FTech.Domain.Entities.Cars;
using FTech.Domain.Exceptions;
using FTech.Infrastructure.Repositories.Cars;
using FTech.Infrastructure.Services.Files;
using Microsoft.EntityFrameworkCore;

namespace FTech.Application.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly IFileService _fileService;
        public CarService(ICarRepository carRepository,
            IMapper mapper,
            IFileService fileService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async ValueTask<CarForResultDTO> CreateAsync(CarForCreationDTO dto)
        {
            var existCar = await (await _carRepository.GetAllAsync())
                .Where(c => c.Number == dto.Number)
                .FirstOrDefaultAsync();

            if (existCar is not null)
                throw new ValidationException("Car already exist");

            var mappedCar = _mapper.Map<Car>(dto);
            if (dto.Image is not null)
            {
                mappedCar.Image = await _fileService.UploadImageAsync(dto.Image);
            }

            var result = await _carRepository.AddAsync(mappedCar);

            var car = await (await _carRepository.GetAllAsync())
                            .Where(c => c.Id == result.Id)
                            .Include(c => c.Category)
                            .FirstOrDefaultAsync();

            if (car == null)
                throw new ValidationException("Car not found");

            return _mapper.Map<CarForResultDTO>(car);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var car = await _carRepository.FindAsync(c => c.Id == id);
            if (car is null)
                throw new ValidationException($"Could not delete car");

            await _carRepository.RemoveAsync(car);

            return true;
        }

        public async ValueTask<IEnumerable<CarForResultDTO>> GetAllAsync()
        {
            var Cars = await (await _carRepository.GetAllAsync())
                .Include(c => c.Category)
                .ToListAsync();

            var mappedCars = _mapper.Map<IEnumerable<CarForResultDTO>>(Cars);

            return mappedCars;
        }

        public async ValueTask<CarForResultDTO> GetByIdAsync(long id)
        {
            var car = await (await _carRepository.GetAllAsync())
                            .Where(c => c.Id == id)
                            .Include(c => c.Category)
                            .FirstOrDefaultAsync();

            if (car == null)
                throw new ValidationException("Car not found");

            return _mapper.Map<CarForResultDTO>(car);
        }

        public async ValueTask<CarForResultDTO> UpdateAsync(long id, CarForCreationDTO dto)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car is null) throw new ValidationException("Car is not found");

            string newImagePath = car.Image;
            if (dto.Image is not null)
            {
                // delete old image
                var deleteResult = await _fileService.DeleteImageAsync(car.Image);
                if (deleteResult is false) throw new ValidationException("Image o'chirilamadi");

                // upload new image
                newImagePath = await _fileService.UploadImageAsync(dto.Image);

            }

            var mappedCar = _mapper.Map(dto, car);
            mappedCar.Image = newImagePath;
            mappedCar.UpdatedAt = DateTime.UtcNow;

            var updatedCar = await _carRepository.UpdateAsync(mappedCar);

            var carrr = await (await _carRepository.GetAllAsync())
                           .Where(c => c.Id == updatedCar.Id)
                           .Include(c => c.Category)
                           .FirstOrDefaultAsync();

            if (carrr == null)
                throw new ValidationException("Car not found");

            return _mapper.Map<CarForResultDTO>(carrr);
        }
    }
}
