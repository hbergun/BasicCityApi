using CityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.Data
{
    public interface IAppRepository
    {
        T Add<T>(T Entity) where T : class;
        void Delete<T>(T Entity) where T : class;
        bool SaveAll();
        List<City> GetCities();
        List<Photo> GetPhotosByCity(int CityID);
        City GetCityByID(int CityID);
        Photo GetPhoto(int PhotoID);
        User GetUser(int UserID);
        List<City> GetCitiesByUserID(int UserID);
    }
}
