using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
       //INDEX..../index...................................................................
        [HttpGet]
        public IActionResult Index()
        {
            using(PizzeriaDbContext Pizzeria=new PizzeriaDbContext())
            {
                List<Pizza> Pizzas= Pizzeria.Pizzas.ToList();
                return View(Pizzas);

            }
        }

        //SHOW...../ShowDetail?Id=<id>...........................................................
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
        //CREATE...../Create...........................................................
        [HttpGet]
        public IActionResult Create() => View("Create");

        //STORE...../Create...........................................................
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza Request)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",Request);
            }
            else
            {
                using (PizzeriaDbContext Pizzeria = new PizzeriaDbContext())
                {  
                    Pizzeria.Add(new Pizza(Request.Name, Request.Description, Request.Img, Request.Price, Request.Ingredients));
                    Pizzeria.SaveChanges();  
                    return RedirectToAction("Index");   
                }          
            }
        }








    }


}
