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
    public class BurritoController : ControllerBase
    {
        private TacoBellDbContext dbContext = new TacoBellDbContext();

        //api/burrito
        [HttpGet]
        public List<Burrito> GetBurritos()
        {
            return dbContext.Burritos.ToList();
        }

        [HttpGet("{Id}")]
        public Burrito GetById(int Id)
        {
            return dbContext.Burritos.FirstOrDefault(b => b.Id == Id);
        }

        //api/burritos/vegetarian
        [HttpGet("vegetarian")]
        public List<Burrito> GetVegetarianBurritos()
        {
            List<Burrito> result = dbContext.Burritos.Where(b => b.Bean == true).ToList();
            return result;
        }

        [HttpPost]
        public Burrito AddBurrito(string name, float cost, bool bean)
        {
            Burrito newBurrito = new Burrito();
            newBurrito.Name = name;
            newBurrito.Cost = cost;
            newBurrito.Bean = bean;
            dbContext.Burritos.Add(newBurrito);
            dbContext.SaveChanges();

            return newBurrito;
        }

        //api/Burrito/Delete/1
        //custom route name + variable plugin
        [HttpDelete("{Id}")]
        public Burrito DeleteBurrito(int Id)
        {
            Burrito b = dbContext.Burritos.FirstOrDefault(b => b.Id == Id);
            dbContext.Burritos.Remove(b);
            dbContext.SaveChanges();

            return b;
        }

        //Patch to update age
        //api/Burritos/1?cost=5.00
        [HttpPatch("{Id}")]
        public Burrito UpdateCost(int Id, float Cost)
        {
            Burrito b = dbContext.Burritos.FirstOrDefault(b => b.Id == Id);
            b.Cost = Cost;
            dbContext.Burritos.Update(b);
            dbContext.SaveChanges();

            return b;

        }
    }
}

