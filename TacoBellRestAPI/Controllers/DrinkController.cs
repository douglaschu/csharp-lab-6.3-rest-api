using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TacoBellRestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TacoBellRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class DrinkController : ControllerBase
    {
        private TacoBellDbContext dbContext = new TacoBellDbContext();

        [HttpGet]
        public List<Drink> GetDrinks()
        {
            return dbContext.Drinks.ToList();

        }

        [HttpGet]
        public List<Drink> GetSlushies()
        {
            List<Drink> result = dbContext.Drinks.Where(d => d.Slushie == true).ToList();
            return result;
        }

        //Patch to update age
        //api/Drink/1/
        [HttpPatch("{Id}")]
        public Drink UpdateName(int Id, string Name)
        {
            Drink d = dbContext.Drinks.FirstOrDefault(d => d.Id == Id);
            d.Name = Name;
            dbContext.Drinks.Update(d);
            dbContext.SaveChanges();

            return d;

        }
    }
}

