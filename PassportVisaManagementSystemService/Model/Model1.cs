using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PassportVisaManagementSystemService.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
<<<<<<< HEAD
            : base("name=Model1")
        {
        }
=======
            : base("name=PVMS")
        {
        }
        public DbSet<User> Users { set; get; }
        public DbSet<State> States { set; get; }
        public DbSet<HintQuestion> HintQuestions { set; get; }
        public DbSet<Country> Countries { set; get; }
        public DbSet<City> Cities { set; get; }
        public DbSet<ApplyVisa> ApplyVisas { set; get; }
        public DbSet<ApplyPassport> ApplyPassports { set; get; }

>>>>>>> 8e0c69e864a1313ebc0cf1e3826472d6f8a87dae


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
