using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string firstName, string lastName, string middleName, string company, string country, string street, string city, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Company = company;
            Country = country;
            Street = street;
            City = city;
            State = state;
            Zipcode = zipcode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
    }
}