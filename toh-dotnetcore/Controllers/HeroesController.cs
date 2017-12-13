﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tohdotnetcore;
using tohdotnetcore.Models;

namespace toh_dotnetcore.Controllers
{
    [Produces("application/json")]
    [Route("api/Heroes")]
    public class HeroesController : Controller
    {
        private readonly tohdotnetcoreContext _context;

        public HeroesController(tohdotnetcoreContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public IEnumerable<Hero> GetHero(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _context.Hero;
            }
            else
            {
                throw new NullReferenceException();
                return _context.Hero.Where(m => m.Name.Contains(name)).ToList();
            }
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hero = await _context.Hero.SingleOrDefaultAsync(m => m.Id == id + 2);

            if (hero.Name.Contains("Ken"))
            {
                return Ok(hero);
            }

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        //// GET: api/Heroes/5
        //[HttpGet("?name={name}")]
        //public async Task<IActionResult> SearchHeroes(string name)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var heros = await _context.Hero.Where(m => m.Name == name).ToListAsync();

        //    return Ok(heros);
        //}


        // PUT: api/Heroes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero([FromRoute] int id, [FromBody] Hero hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Heroes
        [HttpPost]
        public async Task<IActionResult> PostHero([FromBody] Hero hero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Hero.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hero = await _context.Hero.SingleOrDefaultAsync(m => m.Id == id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Hero.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(hero);
        }

        private bool HeroExists(int id)
        {
            return _context.Hero.Any(e => e.Id == id);
        }
    }
}