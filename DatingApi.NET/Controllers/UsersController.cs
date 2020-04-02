using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.NET.Dto;
using DatingApi.NET.Models;
using DatingApi.NET.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.NET.Controllers
{
    [Route("[controller]/{action}")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _repository;
        public UsersController(IDatingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> FindUsers()
        {
            var users = await _repository.FindUsersAsync(); 
            return Ok(users.Select(ToDto));    
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> FindUser(int id)
        {
            var user = await _repository.FindUserAsync(id); 
            return Ok(ToDto(user));    
        }

        private UserDto ToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName=user.UserName,
                BirthDate = user.BirthDate,
                KnownAs = user.KnownAs,
                LastActive = user.LastActive,
                Interests = user.Interests,
                Introduction = user.Introduction,
                LookingFor = user.LookingFor,
                City = user.City,
                Country = user.Country,
                Photos = user.Photos.Select(ToDtoPhoto)
            };
        }

        private PhotoDto ToDtoPhoto(Photo photo)
        {
            return new PhotoDto
            {
                Id = photo.Id,
                UserId = photo.User.Id,
                Description  = photo.Description,
                Url = photo.Url,
                IsMain = photo.IsMain
            };
        }
    }
}