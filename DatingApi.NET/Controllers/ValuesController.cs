using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.NET.Data;
using DatingApi.NET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("/values/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetValues()
        {
            var value= await _context.Values.ToListAsync(); 
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            return Ok(await _context.Values.FirstOrDefaultAsync(x=>x.Id==id));
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm]Value value)
        {
           var oldValue = await _context.Values.FirstOrDefaultAsync(x=>x.Id == value.Id);
           _context.Values.Attach(oldValue);
           oldValue.Name = value.Name;
           await _context.SaveChangesAsync();
           return Ok(value);
        }
    }
}