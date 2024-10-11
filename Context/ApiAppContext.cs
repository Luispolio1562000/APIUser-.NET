using APIDEMO_.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDEMO_.Context
{
    public class ApiAppContext : DbContext

    {

        /// <summary>
        /// Representa la coleccion de todos los usuarios de la DB
        /// </summary>
        
        public DbSet<User> Users { get; set; }
        public ApiAppContext(DbContextOptions<ApiAppContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<User> usersInitData = new List<User>();
            usersInitData.Add(new User{Name = "Luis",LastName = "Fernandez"});
            usersInitData.Add(new User { Name = "Luis", LastName = "Fernandez" });
            builder.Entity<User>().ToTable("User").HasData(usersInitData);
            builder.Entity<User>().HasKey(t => t.UserId);
        }

   

    }
}







// //Pndejo 
// protected void OnmodelCreating(ModelBuilder builder)
// {
//    
// }
