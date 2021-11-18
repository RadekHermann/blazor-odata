
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using Skeleton.Shared.Domain.Entity;

namespace Skeleton.Server.Api.Controllers;

public class PersonController : ODataController
{
    private static readonly List<Person> _persons = new()
    {
        new Person
        {
            Id = 1,
            Name = new Name
            {
                Id = 1,
                PersonId = 1,
                FirstName = "Radek",
                NormFirstName = "radek",
                LastName = "Hermann",
                NormLastName = "hermann",
            },
            DateOfBirth = new DateTime(1989, 8, 14,0,0,0, DateTimeKind.Utc),
            Age = 32,
        },
        new Person
        {
            Id = 2,
            Name = new Name
            {
                Id = 2,
                PersonId = 2,
                FirstName = "Šárka",
                NormFirstName = "sarka",
                LastName = "Ciglerová",
                NormLastName = "ciglerova",
            },
            DateOfBirth = new DateTime(1978, 8, 20,0,0,0, DateTimeKind.Utc),
            Age = 43
        }
    };

    [EnableQuery]
    public IQueryable<Person> Get()
    {
        return _persons.AsQueryable();
    }
}
