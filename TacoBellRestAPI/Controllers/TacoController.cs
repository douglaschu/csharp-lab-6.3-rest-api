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
    public class TacoController : ControllerBase
    {
        private TacoBellDbContext dbContext = new TacoBellDbContext();

        //api/Tacos
        [HttpGet]
        public List<Taco> GetTacos()
        {
            return dbContext.Tacos.ToList();
        }

        [HttpGet]
        public List<Taco> GetDoritosLocos()
        {
            List<Taco> result = dbContext.Tacos.Where(t => t.Dorito == true).ToList();
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

        //api/Taco/Delete/1
        //custom route name + variable plugin
        [HttpDelete("Delete/{Id}")]
        public Taco DeleteTaco(int Id)
        {
            Taco t = dbContext.Tacos.FirstOrDefault(t => t.Id == Id);
            dbContext.Tacos.Remove(t);
            dbContext.SaveChanges();

            return t;
        }

        //Patch to update cost
        //api/Tacos/1?Cost=2.00
        [HttpPatch("{Id}")]
        public Taco UpdateCost(int Id, float Cost)
        {
            Taco t = dbContext.Tacos.FirstOrDefault(t => t.Id == Id);
            t.Cost = Cost;
            dbContext.Tacos.Update(t);
            dbContext.SaveChanges();

            return t;

        }

    }
}

