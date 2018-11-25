using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SistemaParqueo.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int? EmpresaId { get; set; }
        public virtual Empresa Empresa{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=ParqueoContext", throwIfV1Schema: false)
        {
            
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<ClienteEstado> ClienteEstado { get; set; }
        public virtual DbSet<Cochera> Cochera { get; set; }
        public virtual DbSet<CocheraEstado> CocheraEstado { get; set; }
        public virtual DbSet<CocheraUsuario> CocheraUsuario { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Espacio> Espacio { get; set; }
        public virtual DbSet<EspacioEstado> EspacioEstado { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<PersonaEstado> PersonaEstado { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<ReservaEstado> ReservaEstado { get; set; }
        public virtual DbSet<ReservaServicios> ReservaServicios { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public virtual DbSet<Vehiculo> Vehiculo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ClienteEstado>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ClienteEstado>()
                .HasMany(e => e.Cliente)
                .WithOptional(e => e.ClienteEstado)
                .HasForeignKey(e => e.PersonaEstadoId);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Longitud)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Latitud)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .Property(e => e.Foto)
                .IsUnicode(false);

            modelBuilder.Entity<Cochera>()
                .HasMany(e => e.CocheraUsuario)
                .WithRequired(e => e.Cochera)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cochera>()
                .HasMany(e => e.Servicio)
                .WithRequired(e => e.Cochera)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CocheraEstado>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Empresa>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<EspacioEstado>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Persona>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<PersonaEstado>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Reserva>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Reserva>()
                .HasMany(e => e.ReservaServicios)
                .WithRequired(e => e.Reserva)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReservaEstado>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ReservaServicios>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Servicio>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Servicio>()
                .HasMany(e => e.Reserva)
                .WithRequired(e => e.Servicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Servicio>()
                .HasMany(e => e.ReservaServicios)
                .WithRequired(e => e.Servicio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoVehiculo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .Property(e => e.Placa)
                .IsUnicode(false);

            modelBuilder.Entity<Vehiculo>()
                .HasMany(e => e.Reserva)
                .WithRequired(e => e.Vehiculo)
                .WillCascadeOnDelete(false);
        }

        //public System.Data.Entity.DbSet<SistemaParqueo.Models.ApplicationUser> ApplicationUsers { get; set; }
        
    }
}