using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Sales_App.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            
            return userIdentity;
        }
        public string Name { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Items> Items { get; set; }
        public System.Data.Entity.DbSet<Sales_App.Models.OrderMaster> OrderMaster { get; set; }
        public DbSet<OrderDetail> Order_Detail { get; set; }
      

        public System.Data.Entity.DbSet<Sales_App.Models.Sellers> Sellers { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.Costumers> Costumers { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.Townships> Townships { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.MeasureUnit> MeasureUnits { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.NewEntry> NewEntries { get; set; }

        public System.Data.Entity.DbSet<Sales_App.Models.Stock> Stocks { get; set; }
    }
}