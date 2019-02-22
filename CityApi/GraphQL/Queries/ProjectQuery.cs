using CityApi.Data;
using CityApi.GraphQL.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Queries
{
    public class ProjectQuery : ObjectGraphType
    {
        public ProjectQuery(IAppRepository _appRepository)
        {
            Field<UserType>(
               "User",
               arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
               resolve: context => _appRepository.GetUser(context.GetArgument<int>("id")));

           // Field<PlayerType>(
           //     "randomPlayer",
           //     resolve: context => playerRepository.GetRandom());
           //
           // Field<ListGraphType<PlayerType>>(
           //     "players",
           //     resolve: context => playerRepository.All());
        }
    }
}
