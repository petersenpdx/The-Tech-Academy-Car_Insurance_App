using CarInsurance01.Models;
using CarInsurance01.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance01.Controllers
{
    public class HomeController : Controller
    {
        public decimal carTotal;
        //The logic for if the customer filled out the questionare right

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CarInfo01(string firstName, string lastName, string emailAddress, string carModel, string carMake, int tickets, string dui, string coverage, DateTime dateOfBirth, int carYear = 0)
        {
            
            if (dateOfBirth == null) { dateOfBirth = DateTime.Now; }
            
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(carModel)
                || string.IsNullOrEmpty(carMake) || (carYear == 0) || DateTime.Equals(dateOfBirth, DateTime.Now) || string.IsNullOrEmpty(dui) || string.IsNullOrEmpty(coverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                CarInsurance01Entities db = new CarInsurance01Entities();
                CarInfo01 carinfo01 = new CarInfo01();



                carTotal = 50;
                carinfo01.firstName = firstName;
                carinfo01.lastName = lastName;
                carinfo01.emailAddress = emailAddress;
                carinfo01.carModel = carModel;
                carinfo01.carMake = carMake;
                carinfo01.tickets = tickets;
                carinfo01.dui = dui;
                carinfo01.coverage = coverage;
                carinfo01.carYear = carYear;
                carinfo01.dateOfBirth = dateOfBirth;
                carinfo01.carTotal = CalcQuote(carModel, carMake, carYear, tickets, dui, coverage, dateOfBirth);
                db.CarInfo01.Add(carinfo01);
                db.SaveChanges();


                return View("Success", db.CarInfo01.OrderByDescending(x => x.id).Take(1).ToList());
            }
        }
        public decimal CalcQuote(string CarModel, string CarMake, int CarYear, int Tickets, string Dui, string Coverage, DateTime DateOfBirth)
        {
            // math here

            decimal carTotal = 50;
            {

                if (DateTime.Now.Year - DateOfBirth.Year < 25)

                {

                    carTotal = carTotal + 25;

                }

                if (DateTime.Now.Year - DateOfBirth.Year < 18)

                {

                    carTotal = carTotal + 100;

                }

                if (DateTime.Now.Year - DateOfBirth.Year > 100)
                {
                    carTotal = carTotal + 25;
                }
                if (CarYear < 2000)
                {
                    carTotal = carTotal + 25;
                }

                if (CarYear > 2015)
                {
                    carTotal = carTotal + 25;
                }

                if (CarMake.ToLower() == "porsche")
                {
                    carTotal = carTotal + 25;
                }
                if (CarMake.ToLower() == "porsche" && CarModel.ToLower() == "911 carrera")
                {
                    carTotal = carTotal + 25;

                }

                if (Tickets > 4)
                {
                    carTotal = carTotal + (Tickets * 10);
                }

                if (Dui.ToLower() == "yes")
                {
                    carTotal = carTotal + (Decimal.Multiply(carTotal, .25M));

                }
                if (Coverage.ToLower() == "full")
                {
                    carTotal = carTotal + (Decimal.Multiply(carTotal, .25M));
                    Convert.ToString(carTotal);
                }
                return (carTotal);
            }
        }
    }




}