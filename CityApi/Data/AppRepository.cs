using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Data
{
    public class AppRepository : IAppRepository
    {
        private readonly CityContext _cityContext;
        public AppRepository(CityContext cityContext)
        {
            _cityContext = cityContext;
        }
        public T Add<T>(T Entity) where T : class
        {
            _cityContext.Add(Entity);
            return Entity;
        }

        public void Delete<T>(T Entity) where T : class
        {
            _cityContext.Remove(Entity);
        }

        public List<City> GetCities()
        {
            return _cityContext.Cities.Include(c => c.Photos).ToList();
        }

        public List<City> GetCitiesByUserID(int UserID)
        {
           return _cityContext.Cities.Where(c => c.UserID == UserID).ToList();
        }

        public City GetCityByID(int CityID)
        {
            return _cityContext.Cities.Include(c => c.Photos).FirstOrDefault(c => c.ID == CityID);
        }

        public Photo GetPhoto(int PhotoID)
        {
            return _cityContext.Photos.FirstOrDefault(p => p.ID == PhotoID);
        }

        public List<Photo> GetPhotosByCity(int CityID)
        {
            return _cityContext.Photos.Where(p => p.CityID == CityID).ToList();
        }

        public User GetUser(int UserID)
        {
            //return _cityContext.Users.Where(u => u.ID == UserID).FirstOrDefault();
            return _cityContext.Users.Include(u => u.Cities).Where(u => u.ID == UserID).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _cityContext.SaveChanges() > 0;
        }
    }
}
