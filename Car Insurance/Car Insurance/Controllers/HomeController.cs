using Car_Insurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car_Insurance.Controllers
{
    public class HomeController : Controller
    {
        public CarsDBEntities1 _db = new CarsDBEntities1();


        Car c = new Car();

        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Cars.ToList());
        }

        // ADMIN page
        public ActionResult Admin()
        {
            return View(_db.Cars.ToList());
        }

        // CD: built nav to success
        public ActionResult Success()
        {
            return View(_db.Cars.OrderByDescending(x => x.QuoteTotal).Take(1).ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Car carToCreate)
        {
            if (!ModelState.IsValid)
                return View();

            SetProps(carToCreate); // CD: ensure props are set
            carToCreate.QuoteTotal = Quote(); // CD: set gen'd quote

            _db.Cars.Add(carToCreate);
            _db.SaveChanges();

            return View("Success", _db.Cars.OrderByDescending(x => x.QuoteTotal).Take(1).ToList()); // CD nav to success
            //return RedirectToAction("Index"); // CD removed

            #region unused
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            #endregion
        }

        public void SetProps([Bind(Exclude = "Id")] Car _pass) // CD: doubly ensure all props set
        {
            c.FirstName = _pass.FirstName;
            c.LastName = _pass.LastName;
            c.DateOfBirth = _pass.DateOfBirth;
            c.CarYear = _pass.CarYear;
            c.CarMake = _pass.CarMake;
            c.CarModel = _pass.CarModel;
            c.Tickets = _pass.Tickets;
            c.DUI = _pass.DUI;
            c.EmailAddress = _pass.EmailAddress;
            c.Coverage = _pass.Coverage;
        }


        //Function to create Quote Calculation
        decimal quoteTotal = 50; // put in global

        public decimal Quote()
        {
            if (DateTime.Now.Year - c.DateOfBirth.Year > 25)
            {
                quoteTotal = quoteTotal + 25;
            }
            if (DateTime.Now.Year - c.DateOfBirth.Year < 18)
            {
                quoteTotal = quoteTotal + 100;
            }
            if (DateTime.Now.Year - c.DateOfBirth.Year > 100)
            {
                quoteTotal = quoteTotal + 25;
            }

            if (c.CarYear < 2000)
            {
                quoteTotal = quoteTotal + 25;
            }
            if (c.CarYear > 2015)
            {
                quoteTotal = quoteTotal + 25;
            }
            if (c.CarMake.ToLower() == "porsche")
            {
                quoteTotal = quoteTotal + 25;
            }
            if (c.CarMake.ToLower() == "porsche" && c.CarModel.ToLower() == "911 carrera")
            {
                quoteTotal = quoteTotal + 25;
            }
            if (c.Tickets > 4)
            {
                quoteTotal = quoteTotal + (c.Tickets * 10);
            }
            if (c.DUI.ToLower() == "yes")
            {
                quoteTotal = quoteTotal + (Decimal.Multiply(quoteTotal, .25M));
            }
            if (c.Coverage.ToLower() == "full") // CD: fixed issue with calc on full (was converting ToLower() but comapring to Full)
            {
                quoteTotal = quoteTotal + (Decimal.Multiply(quoteTotal, .25M));
            }

            return quoteTotal; // CD return val
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var carToEdit = (from c in _db.Cars
                             where c.Id == id
                             select c).First();

            return View(carToEdit);
        }



        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Car carToEdit)
        {
            var originalCar = (from c in _db.Cars
                               where c.Id == carToEdit.Id
                               select c).First();
            if (!ModelState.IsValid)
                return View(originalCar);

            _db.Entry(originalCar).CurrentValues.SetValues(carToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Car deletedCar = _db.Cars.Find(id);
            if (deletedCar == null)
            {
                return HttpNotFound();
            }
            return View(deletedCar);


        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car deletedCar = _db.Cars.Find(id);
            _db.Cars.Remove(deletedCar);
            _db.SaveChanges();



            return RedirectToAction("Index");



        }
    }
}