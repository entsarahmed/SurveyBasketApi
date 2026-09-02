using Microsoft.EntityFrameworkCore;

namespace SurveyBasket.Api.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        //Add Property for each entity in the project
        public DbSet<Poll> Polls { get; set; }
    }
}
