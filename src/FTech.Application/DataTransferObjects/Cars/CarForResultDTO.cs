using FTech.Application.DataTransferObjects.Categories;

namespace FTech.Application.DataTransferObjects.Cars
{
    public class CarForResultDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string Image { get; set; }
        public CategoryForResultDTO Category { get; set; }
    }
}
