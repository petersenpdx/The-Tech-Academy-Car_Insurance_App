using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceApplication.Models
{
   public class Calculation
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CarYear { get; set; }
    public string CarMake { get; set; }
    public string CarModel { get; set; }
    public string DUI { get; set; }
    public int SpeedingTickets { get; set; }
    public string CoverageType { get; set; }

    public string CalculateQuote(string firstName, string lastName, string emailAddress, DateTime dateOfBirth,
                                    string carYear, string carMake, string carModel, string dui, int speedingTicketNum, string typeOfCoverage)
    {
        decimal quote = 50m;

        // If applicant is older than 100 or between 18 and 25 additional $25
        // If applicant is younger than 18 additional $100
        if ((DateTime.Today.Year - dateOfBirth.Year) > 100) { quote += 25; }
        else if ((DateTime.Today.Year - dateOfBirth.Year) < 25 && (DateTime.Today.Year - dateOfBirth.Year) > 18) { quote += 25; }
        else if ((DateTime.Today.Year - dateOfBirth.Year) < 18) { quote += 100; }

        // Adding $25 if car is older than 2000 OR newer than 2015
        if (Convert.ToInt32(carYear) < 2000 || (Convert.ToInt32(carYear) > 2015)) { quote += 25; }

        // Adding $25 is Make is Porsche and $50 if it is a Carrera
        if (carMake == "Porsche") { quote += 25; }
        if (carModel == "Carrera") { quote += 25; }

        // Adding $10 for every sppeding ticket 
        quote += speedingTicketNum * 10;

        // If applicant has DUI add 25%
        if (dui == "Yes") { quote *= Convert.ToDecimal(1.25); }

        // If full coverage, add 50%
        if (typeOfCoverage == "Full Coverage") { quote *= Convert.ToDecimal(1.5); }

        string result = String.Format("{0:C}", quote);
        return result;
    }
}
}