﻿namespace Skeleton.Client.Blazor.Models;

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
