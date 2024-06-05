using Microsoft.AspNetCore.Http;

namespace FTech.Application.DataTransferObjects.Cars
{
    public class CarForCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public IFormFile Image { get; set; }
        public long CategoryId { get; set; }
        public long DriverId { get; set; }
    }
}
