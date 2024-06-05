using FTech.Domain.Entities.Cars;
using FTech.Domain.Entities.Common;

namespace FTech.Domain.Entities.Categories
{
    public class Category : Auditable<long>
    {
        public string Model { get; set; }
        public string Icon { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
