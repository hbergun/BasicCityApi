using CityApi.GraphQL.Mutations;
using CityApi.GraphQL.Queries;
using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityApi.GraphQL.Schemas
{
    public class ProjectSchema : Schema
    {
        public ProjectSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ProjectQuery>();
            Mutation = resolver.Resolve<ProjectMutation>();
        }
    }
}
