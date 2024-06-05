using FTech.Domain.Entities.Cars;
using FTech.Infrastructure.Data;
using FTech.Infrastructure.Repositories.Base;

namespace FTech.Infrastructure.Repositories.Cars
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }

}
