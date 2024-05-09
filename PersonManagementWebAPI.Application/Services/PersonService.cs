using Microsoft.AspNetCore.Mvc;
using PersonManagementWebAPI.Application.Dtos;
using PersonManagementWebAPI.Application.Interfaces;
using PersonManagementWebAPI.Entities.Enums;
using PersonManagementWebAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagementWebAPI.Application.Services
{
    public class PersonService : IPersonService
    {
        private static List<Person> _persons = new List<Person>()
        {
            new Person{Id = Guid.NewGuid(), FirstName = "John", DateOfBirth = new DateTime(1999-1-1), LastName="Doe 1",BirthPlace="Ha Noi",Gender=GenderType.Male },
            new Person{Id = Guid.NewGuid(), FirstName = "John", DateOfBirth = new DateTime(1999-1-1), LastName="Doe 2",BirthPlace="Ha Noi",Gender=GenderType.Female },
            new Person{Id = Guid.NewGuid(), FirstName = "John", DateOfBirth = new DateTime(1999-1-1), LastName="Doe 3",BirthPlace="Ha Noi",Gender=GenderType.Other },

        };
        public List<Person> GetAllPerson()
        {
            return(_persons);
        }

        public Person GetPersonById(Guid Id)
        {
            return _persons.FirstOrDefault(x => x.Id == Id);
            
        }
        public Person CreatePerson(PersonDto person)
        {
            var newPerson = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                BirthPlace = person.BirthPlace,
                Gender = person.Gender
            };
            _persons.Add(newPerson);
            return newPerson;
        }

        public void DeletePerson(Person person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Id == person.Id);
            if (existingPerson != null) 
            {
                _persons.Remove(existingPerson);
            }
        }

        public void EditPerson(Guid Id, PersonDto person)
        {
            var existingPerson = _persons.FirstOrDefault(x => x.Id == Id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Gender = person.Gender;
                existingPerson.DateOfBirth = person.DateOfBirth;
                existingPerson.BirthPlace = person.BirthPlace;
            }
        }

        public List<Person> FilterPeople( string? name,  GenderType? gender, string? birthPlace)
        {
            var filteredList = _persons.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                filteredList =  filteredList.Where(x => x.FullName.Contains(name));
            }

            if ( gender != null)
            {
                filteredList = filteredList.Where(x => x.Gender == gender);
            }

            if (!string.IsNullOrEmpty(birthPlace))
            {
                filteredList = filteredList.Where(x => x.BirthPlace.Contains(birthPlace));
            }

            return filteredList.ToList();
        }
    }
}
