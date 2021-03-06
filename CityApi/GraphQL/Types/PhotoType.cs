﻿using CityApi.Data;
using CityApi.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Types
{
    public class PhotoType : ObjectGraphType<Photo>
    {
        public PhotoType(IAppRepository appRepository)
        {
            Field(p => p.ID);
            Field(p => p.CityID);
            Field(p => p.Url);
            Field(p => p.Description);
            Field<DateGraphType>(
                "DateAdded",
                //resolve:context => context.Source.DateAdded.ToString("G"),
                deprecationReason: "This Format Deprecated We Use FullDateTime "); //Classic Date Format 
            Field<DateTimeGraphType>(
                "DatTimeAdded",
                resolve: context => context.Source.DateAdded.ToString("G")
                //deprecationReason:"Bu Ar"
                );
            Field(p => p.IsMain);
            Field(p => p.PublicID);
            Field<CityType>(
                "City",
                 //arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "City ID" }),
                 resolve: context => appRepository.GetCityByID(context.Source.CityID),
                 description: "City"
                );
        }
    }
}
