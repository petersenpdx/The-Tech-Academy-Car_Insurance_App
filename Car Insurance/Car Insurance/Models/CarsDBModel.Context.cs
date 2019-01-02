namespace CarInsuranceProjApp.Models
{

    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;


    public partial class CarsDBEntities1 : DbContext
    {
        public CarsDBEntities1()
            : base("name=CarsDBEntities1")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }


        public virtual DbSet<Car> Cars { get; set; }

    }

}