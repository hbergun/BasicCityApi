using CityApi.Data;
using CityApi.GraphQL.Types;
using CityApi.GraphQL.Types.InputType;
using CityApi.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Mutations
{
    public class ProjectMutation : ObjectGraphType
    {
        public ProjectMutation(IAppRepository appRepository)
        {
            Name = "CreateUserMutation";

            Field<UserType>(
                "createUser",
                arguments: new QueryArguments(
                //new QueryArgument<NonNullGraphType<UserInputType>> { Name = "player" }
               // new QueryArgument<UserInputType> { Name = "user" }
                ),
                resolve: context =>
                {
                    var user = context.GetArgument<User>("user");
                    return appRepository.Add<User>(user);
                });
        }
    }
}
