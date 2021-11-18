using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Shared.Domain.Entity;

public class Person
{
    public int Id { get; set; }
	public Name Name { get; set; }
	public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
}

public class Name
{
	public int Id { get; set; }
	public int PersonId { get; set; }
	public string FirstName { get; set; }
    public string NormFirstName { get; set; }
    public string LastName { get; set; }
    public string NormLastName { get; set; }
}
