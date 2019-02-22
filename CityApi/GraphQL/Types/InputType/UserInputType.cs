using CityApi.Dtos;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Types.InputType
{
    public class UserInputType : InputObjectGraphType<UserForRegisterDto>
    {
        public UserInputType()
        {
            Name = "UserInput";
            //Field<IntGraphType>("id");
            //Field<StringGraphType>("username");
            Field<NonNullGraphType<StringGraphType>>("UserName");
            Field<StringGraphType>("Password");
        }
    }
}
