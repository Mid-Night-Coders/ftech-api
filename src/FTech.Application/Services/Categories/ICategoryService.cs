using FTech.Application.DataTransferObjects.Categories;

namespace FTech.Application.Services.Categories
{
    public interface ICategoryService
    {
        ValueTask<CategoryForResultDTO> CreateAsync(CategoryForCreationDTO dto);
        ValueTask<CategoryForResultDTO> UpdateAsync(long id, CategoryForCreationDTO dto);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<CategoryForResultDTO> GetByIdAsync(long id);
        ValueTask<IEnumerable<CategoryForResultDTO>> GetAllAsync();
    }
}
