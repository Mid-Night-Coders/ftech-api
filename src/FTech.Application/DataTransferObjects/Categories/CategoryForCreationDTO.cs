using Microsoft.AspNetCore.Http;

namespace FTech.Application.DataTransferObjects.Categories
{
    public class CategoryForCreationDTO
    {
        public string Model { get; set; }
        public IFormFile Icon { get; set; }
    }
}
