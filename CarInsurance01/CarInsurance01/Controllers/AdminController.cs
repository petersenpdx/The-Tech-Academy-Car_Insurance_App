using CarInsurance01.Models;
using CarInsurance01.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance01.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin Where the stored data is at
        public ActionResult Index()
        {
            using (CarInsurance01Entities db = new CarInsurance01Entities())
            {
                var carinfo01 = db.CarInfo01.Where(x => x.removed == DateTime.Now).ToList();
                var carinfo01Vms = new List<CarInfo01Vm>();

                foreach (var _carinfo01 in carinfo01)
                {
                    var carinfo01vm = new CarInfo01Vm();
                    carinfo01vm.id = _carinfo01.id;
                    carinfo01vm.FirstName = _carinfo01.firstName;
                    carinfo01vm.LastName = _carinfo01.lastName;
                    carinfo01vm.EmailAddress = _carinfo01.emailAddress;
                    carinfo01vm.CarTotal = _carinfo01.carTotal;
                    carinfo01Vms.Add(carinfo01vm);

                }
                return View(carinfo01Vms);
            }

        }
        public ActionResult Unsubscribe(int id)
        {
            using (CarInsurance01Entities db = new CarInsurance01Entities())
            {
                var _carinfo01 = db.CarInfo01.Find(id);
                _carinfo01.removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

    }

}