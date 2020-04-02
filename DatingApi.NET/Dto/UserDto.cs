using System;
using System.Collections.Generic;
using DatingApi.NET.Enumerations;

namespace DatingApi.NET.Dto
{
    public class UserDto
    {
        public int Id{get; set;}
        public string UserName{get; set;}
        public Gender Gender{get; set;}
        public DateTime BirthDate {get; set;}
        public string KnownAs {get; set;}
        public DateTime LastActive {get; set;}
        public string Introduction {get; set;}
        public Gender LookingFor{get; set;}
        public string Interests{get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public IEnumerable<PhotoDto> Photos {get; set;}
    }
}