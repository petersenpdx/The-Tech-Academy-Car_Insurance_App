
using InsuranceApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {
                var quotes = db.Insurees.ToList();
                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.Id = quoteVm.Id;
                    quoteVm.FirstName = quote.FirstName;
                    quoteVm.LastName = quote.LastName;
                    quoteVm.EmailAddress = quote.EmailAddress;
                    quoteVm.DateOfBirth = quote.DateOfBirth;
                    quoteVm.CarYear = quote.CarYear;
                    quoteVm.CarMake = quote.CarMake;
                    quoteVm.CarModel = quote.CarModel;
                    quoteVm.DUI = quote.DUI;
                    quoteVm.SpeedingTickets = quote.SpeedingTickets;
                    quoteVm.CoverageType = quote.CoverageType;
                    quoteVm.CustomerQuote = quote.CustomerQuote;
                    quoteVms.Add(quoteVm);

                }
                return View(quoteVms);
            }
        }
    }
}