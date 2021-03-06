using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DatingApi.NET.Enumerations;

namespace DatingApi.NET.Models
{
    public class User
    {
        public int Id{get; set;}
        public string UserName{get; set;}
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        public Gender Gender{get; set;}
        public DateTime BirthDate {get; set;}
        public string KnownAs {get; set;}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn {get; set;}=DateTime.UtcNow;
        public DateTime LastActive {get; set;}
        public string Introduction {get; set;}
        public Gender LookingFor{get; set;}
        public string Interests{get; set;}
        public string City {get; set;}
        public string Country {get; set;}
        public ICollection<Photo> Photos {get; set;}
    }
}