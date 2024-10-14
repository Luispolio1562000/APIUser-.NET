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
        public DbSet<UserRole> UserRoles{ get; set; }
        public ApiAppContext(DbContextOptions<ApiAppContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<User> usersInitData = new List<User>();
            usersInitData.Add(new User { Name = "Luis", LastName = "Fernandez" });
            usersInitData.Add(new User { Name = "Gustavo", LastName = "Polio" });
            builder.Entity<User>().ToTable("User").HasData(usersInitData);
            builder.Entity<User>().HasKey(t => t.UserId);



            builder.Entity<UserRole>().ToTable("UserRole").HasKey(p => p.UserRoleId);
            List<UserRole> userRoles = new List<UserRole>();
            userRoles.Add(new UserRole() { Role = "Admin", UserId = usersInitData[0].UserId });
            userRoles.Add(new UserRole() { Role = "User", UserId = usersInitData[0].UserId });
            userRoles.Add(new UserRole() { Role = "Support", UserId = usersInitData[0].UserId });
            userRoles.Add(new UserRole() { Role = "Admin", UserId = usersInitData[1].UserId });

            builder.Entity<UserRole>().HasData(userRoles);


            /// Relacion uno a uno, cada userRole, tiene un elemento relacionado que es un USER, se especifica la propiedad.
            /// 
            builder.Entity<UserRole>().HasOne<User>("User");


        }
    }
}







// //Pndejo 
// protected void OnmodelCreating(ModelBuilder builder)
// {
//    
// }
