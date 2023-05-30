using FinalExam_Bilet7.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalExam_Bilet7.DAL
{
	
	public class AppDbContext:IdentityDbContext<AppUser>
	{
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<OurTeam> Employees { get; set; }
        public DbSet<Setting> Settings { get; set; }


    }
}
