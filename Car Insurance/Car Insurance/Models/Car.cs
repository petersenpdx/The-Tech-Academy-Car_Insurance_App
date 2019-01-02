namespace CarInsuranceProjApp.Models
{

    using System;
    using System.Collections.Generic;

    public partial class Car
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public System.DateTime DateOfBirth { get; set; }

        public int CarYear { get; set; }

        public string CarMake { get; set; }

        public string CarModel { get; set; }

        public int Tickets { get; set; }

        public string Dui { get; set; }

        public string Coverage { get; set; }

        public Nullable<decimal> QuoteTotal { get; set; } // CD: gen'd from new column in db
                                                          // had to add new column to db to store quote val
                                                          // ALTER TABLE Cars
                                                          // ADD QuoteTotal decimal(8,2) null
                                                          // then updated the edmx from the database
                                                          // set new QuoteTotal prop to Nullable (.edmx props)

    }

}