using Microsoft.AspNetCore.Mvc;
using PersonManagementWebAPI.Application.Dtos;
using PersonManagementWebAPI.Entities.Enums;
using PersonManagementWebAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PersonManagementWebAPI.Application.Interfaces
{
    public interface IPersonService
    {
        List<Person> GetAllPerson();
        Person GetPersonById(Guid Id);
        Person CreatePerson(PersonDto person);
        void EditPerson(Guid Id, PersonDto person);
        void DeletePerson(Person person);
        List<Person> FilterPeople( string? name,  GenderType? gender,  string? birthPlace);
    }
}
