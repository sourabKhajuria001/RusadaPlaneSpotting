using RusadaAircraftSpotting.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Repository
{
    public interface IAircraftSightingRepository
    {
        Task<int> AddNewSpottingAsync(AircraftSightingModel model, string imageLocation);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<AircraftSightingModel>> GetAllSpottingsAsync(string search, string searchBy);
        Task<AircraftSightingModel> GetByIDAsync(int id);
        Task<int> UpdateAsync(int id, AircraftSightingModel model);
    }
}