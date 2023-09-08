using DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public partial class ApplicationDBContext : DbContext 
    {
        public ApplicationDBContext()
        {
                
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base (options)
        {
                
        }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//    => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Excel;Integrated Security=True;Encrypt=False;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trades>()
           .HasKey(t => t.TradeId);

            base.OnModelCreating(modelBuilder);
            //OnModelCreatingPartial(modelBuilder);
        }

        public DbSet<Trades> Trades { get; set; }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
