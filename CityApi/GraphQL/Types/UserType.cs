using CityApi.Data;
using CityApi.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IAppRepository appRepository)
        {
            Field(u => u.ID); //UserID
            Field(u => u.UserName);
            Field<ListGraphType<CityType>>(
                "Cities",
                //arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => appRepository.GetCitiesByUserID(context.Source.ID),description: "These Cities Were Visited By The User"
                );

        }
    }
}
