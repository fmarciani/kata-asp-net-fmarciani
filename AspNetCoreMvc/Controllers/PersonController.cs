using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            var peopleList = new List<Person>
            {
                new Person {Name = "Crying"},
                new Person {Name = "Screaming"},
                new Person {Name = "Despair"}
            };

            return View(peopleList);
        }
    }
}