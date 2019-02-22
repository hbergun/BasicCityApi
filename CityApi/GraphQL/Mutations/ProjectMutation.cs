using CityApi.Data;
using CityApi.Dtos;
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
    public class ProjectMutation : ObjectGraphType<object>
    {
       public ProjectMutation(IAuthRepository authRepository)
        {
            Name = "RegisterUser";
            Field<UserType>(
                "RegisterUser",
                arguments: new QueryArguments(
                //new QueryArgument<NonNullGraphType<UserInputType>> { Name = "User" }
                new QueryArgument<UserInputType> { Name = "user" }
                ),
                resolve: context =>
                {
                var userforlogindto = context.GetArgument<UserForRegisterDto>("user");
                    //TODO : Use AutoMapper!
                    var user = new User { UserName = userforlogindto.UserName };
                    return authRepository.Register(user,userforlogindto.Password);
                });
        }
    }
}
