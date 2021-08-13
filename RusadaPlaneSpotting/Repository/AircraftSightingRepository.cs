using Microsoft.EntityFrameworkCore;
using RusadaAircraftSpotting.Data;
using RusadaAircraftSpotting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RusadaAircraftSpotting.Repository
{
    public class AircraftSightingRepository : IAircraftSightingRepository
    {
        private readonly AircraftSightingContext _context = null;

        public AircraftSightingRepository(AircraftSightingContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewSpottingAsync(AircraftSightingModel model, string imageLocation)
        {

            var newSighting = new AircraftSightings()
            {
                AircraftMake = model.AircraftMake,
                AircraftModel = model.AircraftModel,
                AircraftRegistration = model.AircraftRegistration,
                LocationSpotted = model.LocationSpotted,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Notes = model.Notes,
                ImageLocation = imageLocation
            };
            await _context.AircraftSightings.AddAsync(newSighting);
            await _context.SaveChangesAsync();

            return newSighting.Id;
        }


        public async Task<List<AircraftSightingModel>> GetAllSpottingsAsync(string search, string searchBy)
        {
            List<AircraftSightingModel> records;
            IQueryable<AircraftSightings> tempRecords= null;

            if (searchBy != null && searchBy.ToLower() != "search by" && search != null && search.Length > 0)
            {
                if (searchBy.ToLower() == "make")
                {
                    tempRecords = _context.AircraftSightings.Where(y => y.AircraftMake.Contains(search));

                }
                else if (searchBy.ToLower() == "model")
                {
                    tempRecords = _context.AircraftSightings.Where(y => y.AircraftModel.Contains(search));

                }
                else if (searchBy.ToLower() == "reg")
                {
                    tempRecords = _context.AircraftSightings.Where(y => y.AircraftRegistration.Contains(search));
                }
            }
            else if (search != null && search.Length > 0)
            {
                tempRecords = _context.AircraftSightings.Where(y => y.AircraftMake.Contains(search)
                || y.AircraftModel.Contains(search)
                || y.AircraftRegistration.Contains(search)
                || y.LocationSpotted.Contains(search)
                || y.Notes.Contains(search));
            }

            if (search != null && search.Length > 0)
            {
                records = await tempRecords.Select(x => new AircraftSightingModel()
                {
                    Id = x.Id,
                    AircraftMake = x.AircraftMake,
                    AircraftModel = x.AircraftModel,
                    AircraftRegistration = x.AircraftRegistration,
                    LocationSpotted = x.LocationSpotted,
                    Notes = x.Notes,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified

                }).ToListAsync();
            }

            else
            {
                records = await _context.AircraftSightings.Select(x => new AircraftSightingModel()
                {
                    Id = x.Id,
                    AircraftMake = x.AircraftMake,
                    AircraftModel = x.AircraftModel,
                    AircraftRegistration = x.AircraftRegistration,
                    LocationSpotted = x.LocationSpotted,
                    Notes = x.Notes,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified

                }).ToListAsync();
            }


            return records;

        }

        public async Task<AircraftSightingModel> GetByIDAsync(int id)
        {
            var _sighting = await _context.AircraftSightings.FindAsync(id);

            if (_sighting != null)
            {
                var record = new AircraftSightingModel()
                {
                    Id = _sighting.Id,
                    AircraftMake = _sighting.AircraftMake,
                    AircraftModel = _sighting.AircraftModel,
                    AircraftRegistration = _sighting.AircraftRegistration,
                    LocationSpotted = _sighting.LocationSpotted,
                    Notes = _sighting.Notes,
                    DateCreated = _sighting.DateCreated,
                    DateModified = _sighting.DateModified,
                    ImageLocation = _sighting.ImageLocation

                };
                return record;

            }
            return null;


        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            try
            {
                var _sighting = new AircraftSightings() { Id = id };
                if (_sighting != null)
                {
                    _context.AircraftSightings.Remove(_sighting);

                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }


        public async Task<int> UpdateAsync(int id, AircraftSightingModel model)
        {
            try
            {
                var _record = await _context.AircraftSightings.FindAsync(id);
                if (_record != null)
                {
                    _record.Id = model.Id;
                    _record.AircraftMake = model.AircraftMake;
                    _record.AircraftModel = model.AircraftModel;
                    _record.LocationSpotted = model.LocationSpotted;
                    _record.AircraftRegistration = model.AircraftRegistration;
                    _record.Notes = model.Notes;
                    _record.DateModified = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return _record.Id;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }



    }//
}//
