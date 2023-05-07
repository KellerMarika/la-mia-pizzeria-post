using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
       //INDEX..../Pizza/index...................................................................
        [HttpGet]
        public IActionResult Index()
        {
            using(PizzeriaDbContext Pizzeria=new PizzeriaDbContext())
            {
                List<Pizza> Pizzas= Pizzeria.Pizzas.ToList<Pizza>();
                return View(Pizzas);

            }
        }

        //SHOW...../Pizza/ShowDetail?Id=<id>...........................................................
        [HttpGet]
        public IActionResult ShowDetails(int Id)
        {
            using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
            {
                try 
                {
                    Pizza Pizza = Pizzeria.Pizzas.First(p => p.Id == Id);
                    return View(Pizza);
                }
                catch 
                {
                    string message = $"nessuna pizza trovata con id= {Id}";
                    return View("Error", message);
                }     
            }
        }




    }
}
