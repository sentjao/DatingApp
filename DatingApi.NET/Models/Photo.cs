using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DatingApi.NET.Models
{
    public class Photo
    {
        public int Id{get; set;}
        public string Url{get; set;}
        public string Description {get; set;}
        public bool IsMain{get; set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn{get; set;} = DateTime.UtcNow;

        [ForeignKey("Users")]
        [JsonIgnore]
        public User User{get; set;}
    }
}