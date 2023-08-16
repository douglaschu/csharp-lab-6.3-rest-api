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

        public List<Burrito> GetBurritos()
        {
            return dbContext.Burritos.ToList();
        }

        [HttpGet]
        public List<Burrito> GetVegetarianBurritos()
        {
            List<Burrito> result = dbContext.Burritos.Where(b => b.Bean == true).ToList();
            return result;
        }


        //api/Taco?name=BajaBlastTaco&cost=1.25&false&true
        [HttpPost]
        public Taco AddTaco(string name, float cost, bool softshell, bool dorito)
        {
            Taco newTaco = new Taco();
            newTaco.Name = name;
            newTaco.Cost = cost;
            newTaco.SoftShell = softshell;
            newTaco.Dorito = dorito;

            return newTaco;
        }

        //api/Burrito/Delete/1
        //custom route name + variable plugin
        [HttpDelete("Delete/{Id}")]
        public Taco DeleteTaco(int Id)
        {
            Taco t = dbContext.Tacos.FirstOrDefault(t => t.Id == Id);
            dbContext.Tacos.Remove(t);
            dbContext.SaveChanges();

            return t;
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

