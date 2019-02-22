using CityApi.Data;
using CityApi.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Types
{
    public class CityType : ObjectGraphType<City>
    {
        public CityType(IAppRepository appRepository)
        {
            Field(c => c.ID);
            Field(c => c.UserID);
            Field(c => c.Name);
            Field(c => c.Description);
            Field<ListGraphType<PhotoType>>(
                "Photos",
                //arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name="id" }),
                resolve: context => appRepository.GetPhotosByCity(context.Source.ID), description: "City's Photos"
                );
            Field<UserType>(
                "User",
                resolve: context => appRepository.GetUser(context.Source.UserID)
                );
        }
    }
}
