using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using PhoneBook.Models;
using PhoneBook.SQLManager;

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            SourceManager sourceManager = new SourceManager();
            sourceManager.show();
            ViewBag.TotalNumberOfPerson = sourceManager.Persons.Count();

            ViewBag.CurvenPage = page;

            var listOfPerson = sourceManager.Persons;
            var numberOfPerson = sourceManager.Persons.Count();

            if (page == 1)
            {
                return View(listOfPerson.Take(10).ToList());
            }
            else if (page == 2)
            {
                return View(listOfPerson.Skip(10).Take(10).ToList());
            }
            else if (page == 3)
            {
                return View(listOfPerson.Skip(20).Take(10).ToList());
            }

            return View(listOfPerson);
        }

        [HttpPost]
        public ActionResult Index(string name)
        {

            SourceManager sourceManager = new SourceManager();
            
            var listOfPerson = sourceManager.Persons;
            if (name != "")
            {
                sourceManager.search(name);
                listOfPerson = sourceManager.Persons;
                if (sourceManager.Persons.Count == 0)
                {
                    //brak takiej osoby
                    //ViewBag.BrakTakiejOsoby = "NIE MA TAKIEJ OSOBY";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TotalNumberOfPerson = sourceManager.Persons.Count();
                    return View(listOfPerson);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                SourceManager sourceManager = new SourceManager();
                sourceManager.add(personModel);
                return RedirectToAction("Index");
            }

            return View(personModel);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            SourceManager sourceManager = new SourceManager();
            var personModel = sourceManager.GetPersonByID(id);

            return View(personModel);
        }

        [HttpPost]
        public ActionResult Update(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                SourceManager sourceManager = new SourceManager();
                sourceManager.update(personModel);
                //sourceManager.update(personToUpdate);
                return RedirectToAction("Index");
            }

            return View(personModel);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            SourceManager sourceManager = new SourceManager();
            var personModel = sourceManager.GetPersonByID(id);
            return View(personModel);
        }

        [HttpPost]
        public ActionResult Remove(PersonModel personModel)
        {
            SourceManager sourceManager = new SourceManager();
            sourceManager.remove(personModel);
            return RedirectToAction("Index");
        }
    }
}

