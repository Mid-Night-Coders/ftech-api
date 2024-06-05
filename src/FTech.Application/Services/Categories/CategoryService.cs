using AutoMapper;

using FTech.Application.DataTransferObjects.Categories;
using FTech.Domain.Entities.Categories;
using FTech.Domain.Exceptions;
using FTech.Infrastructure.Repositories.Categories;
using FTech.Infrastructure.Services.Files;

namespace FTech.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IFileService _fileService;
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public CategoryService(
            IMapper mapper,
            ICategoryRepository repository,
            IFileService fileService)
        {
            this.mapper = mapper;
            this._repository = repository;
            this._fileService = fileService;
        }

        public async ValueTask<CategoryForResultDTO> CreateAsync(CategoryForCreationDTO dto)
        {
            var iconPath = await _fileService.UploadImageAsync(dto.Icon);

            var mappedCategory = mapper.Map<Category>(dto);
            mappedCategory.Icon = iconPath;

            var result = await _repository.AddAsync(mappedCategory);

            return mapper.Map<CategoryForResultDTO>(result);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var category = await _repository.FindAsync(c => c.Id == id);
            if (category is null)
            {
                throw new ValidationException("category is not found");
            }

            await _repository.RemoveAsync(category);
            return true;
        }

        public async ValueTask<IEnumerable<CategoryForResultDTO>> GetAllAsync()
        {
            var Cars = await _repository.GetAllAsync();

            var mappedCars = mapper.Map<IEnumerable<CategoryForResultDTO>>(Cars);

            return mappedCars;
        }

        public async ValueTask<CategoryForResultDTO> GetByIdAsync(long id)
        {
            var category = await _repository.FindAsync(c => c.Id == id);
            if (category is null)
            {
                throw new ValidationException("category is not found");
            }

            return mapper.Map<CategoryForResultDTO>(category);
        }

        public async ValueTask<CategoryForResultDTO> UpdateAsync(long id, CategoryForCreationDTO dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) throw new ValidationException("category not found");

            string newImagePath = category.Icon;
            if (dto.Icon is not null)
            {
                // delete old image
                var deleteResult = await _fileService.DeleteImageAsync(category.Icon);
                if (deleteResult is false) throw new ValidationException("icon o'chirilmadi");

                newImagePath = await _fileService.UploadImageAsync(dto.Icon);
                // upload new image

                category.Icon = newImagePath;
                // parse new path to category
            }
            var mappedCategory = mapper.Map(dto, category);
            mappedCategory.Icon = newImagePath;
            mappedCategory.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(mappedCategory);

            return mapper.Map<CategoryForResultDTO>(result);
        }
    }
}
