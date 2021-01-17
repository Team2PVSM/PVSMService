using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PassportVisaManagementSystemService.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
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
        public DbSet<OldPassportData> OldPassportDatas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
