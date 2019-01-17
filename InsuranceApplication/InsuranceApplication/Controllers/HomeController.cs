using InsuranceApplication.Models;
using InsuranceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote (string firstName, string lastName, string emailAddress, DateTime dateOfBirth, string carYear,
                                    string carMake, string carModel, string dui, int speedingTicketNum, string typeOfCoverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
                {
                    var quote = new Insuree();
                    quote.FirstName = firstName;
                    quote.LastName = lastName;
                    quote.EmailAddress = emailAddress;
                    quote.DateOfBirth = Convert.ToDateTime(dateOfBirth);
                    quote.CarYear = carYear;
                    quote.CarMake = carMake;
                    quote.CarModel = carModel;
                    quote.DUI = dui;
                    quote.SpeedingTickets = speedingTicketNum.ToString();
                    quote.CoverageType = typeOfCoverage;
                    Calculation result = new Calculation();
                    quote.CustomerQuote = result.CalculateQuote(firstName, lastName, emailAddress, dateOfBirth, carYear, carMake, carModel, dui, speedingTicketNum, typeOfCoverage);

                    db.Insurees.Add(quote);
                    db.SaveChanges();

                    var displayQuoteVm = new DisplayQuoteVm();
                    displayQuoteVm.CustomerQuote = result.CalculateQuote(firstName, lastName, emailAddress, dateOfBirth, carYear, carMake, carModel, dui, speedingTicketNum, typeOfCoverage);


                    return View("QuoteDisplay", displayQuoteVm);
                }
            }
        }
    }
}