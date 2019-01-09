using System.Data.Entity;
using MTF_Services.Model;

namespace MTF_Services.DataAccess
{
    /// <summary>
    /// Класс контекста модели данных
    /// </summary>
    public class MTF_ServicesDbContext : DbContext
    {
        /// <summary>
        /// Конструктор класса контекста модели данных
        /// </summary>
        public MTF_ServicesDbContext() :base("MTF_ServicesBase")
        {
        }

        public virtual DbSet<CPU> CPU { get; set; }
        public virtual DbSet<CpuSocket> CpuSocket { get; set; }
        public virtual DbSet<IdleReason> IdleReason { get; set; }
        public virtual DbSet<IdleType> IdleType { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<PaasType> PaasType { get; set; }
        public virtual DbSet<Platform> Platform { get; set; }
        public virtual DbSet<Platform_StorageInt> Platform_StorageInt { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<RAM> RAM { get; set; }
        public virtual DbSet<RamType> RamType { get; set; }
        public virtual DbSet<RightsLevel> RightsLevel { get; set; }
        public virtual DbSet<SAN> SAN { get; set; }
        public virtual DbSet<SAN_MaintenanceShedule> SAN_MaintenanceShedule { get; set; }
        public virtual DbSet<SAN_Storage> SAN_Storage { get; set; }
        public virtual DbSet<SAN_StorageInt> SAN_StorageInt { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Server_MaintenanceShedule> Server_MaintenanceShedule { get; set; }
        public virtual DbSet<Server_RAM> Server_RAM { get; set; }
        public virtual DbSet<Server_Storage> Server_Storage { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceIdle> ServiceIdle { get; set; }
        public virtual DbSet<ServiceState> ServiceState { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<SoftType> SoftType { get; set; }
        public virtual DbSet<Software> Software { get; set; }
        public virtual DbSet<Strorage> Strorage { get; set; }
        public virtual DbSet<StrorageInterface> StrorageInterface { get; set; }
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// Вызывается при инициализации модели для производного контекста,
        /// в котором определены дополнительные действия при развертывании модели данных
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CPU>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CPU>()
                .HasMany(e => e.Server)
                .WithRequired(e => e.CPU)
                .HasForeignKey(e => e.CPU_Id);

            modelBuilder.Entity<Manufacturer>()
                .HasMany(e => e.CpuSocket)
                .WithRequired(e => e.Manufacturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaasType>()
                .HasMany(e => e.SAN)
                .WithMany(e => e.PaasType)
                .Map(m => m.ToTable("SANtoPaas").MapLeftKey("PaasTypeId"));

            modelBuilder.Entity<PaasType>()
                .HasMany(e => e.Server)
                .WithMany(e => e.PaasType)
                .Map(m => m.ToTable("ServerToPaas").MapLeftKey("PaasTypeId").MapRightKey("ServerId"));

            modelBuilder.Entity<Platform>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Position>()
                .Property(e => e.AvgSalary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RAM>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<RAM>()
                .HasMany(e => e.Server_RAM)
                .WithRequired(e => e.RAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RightsLevel>()
                .HasMany(e => e.User)
                .WithRequired(e => e.RightsLevel)
                .HasForeignKey(e => e.RightLevelId);

            modelBuilder.Entity<SAN>()
                .Property(e => e.AnnualMaintenance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SAN>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SAN>()
                .HasMany(e => e.SAN_MaintenanceShedule)
                .WithRequired(e => e.SAN)
                .HasForeignKey(e => e.SAN_Id);

            modelBuilder.Entity<SAN>()
                .HasMany(e => e.SAN_Storage)
                .WithRequired(e => e.SAN)
                .HasForeignKey(e => e.SAN_Id);

            modelBuilder.Entity<SAN>()
                .HasMany(e => e.SAN_StorageInt)
                .WithRequired(e => e.SAN)
                .HasForeignKey(e => e.SAN_Id);

            modelBuilder.Entity<Server>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Server>()
                .Property(e => e.AnnualMaintenance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .Property(e => e.CostPerHour)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Software)
                .WithMany(e => e.Service)
                .Map(m => m.ToTable("SoftService").MapLeftKey("ServiceId").MapRightKey("SoftwareId"));

            modelBuilder.Entity<Service>()
                .HasMany(e => e.User)
                .WithMany(e => e.Service)
                .Map(m => m.ToTable("UsingService").MapLeftKey("ServiceId").MapRightKey("UserTabNo"));

            modelBuilder.Entity<ServiceIdle>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ServiceIdle>()
                .HasMany(e => e.User)
                .WithMany(e => e.ServiceIdle)
                .Map(m => m.ToTable("AdminIdle").MapLeftKey("ServiceId").MapRightKey("UserTabNo"));

            modelBuilder.Entity<Software>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Strorage>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Strorage>()
                .HasMany(e => e.SAN_Storage)
                .WithRequired(e => e.Strorage)
                .HasForeignKey(e => e.StorageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Strorage>()
                .HasMany(e => e.Server_Storage)
                .WithRequired(e => e.Strorage)
                .HasForeignKey(e => e.StorageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StrorageInterface>()
                .HasMany(e => e.Platform_StorageInt)
                .WithRequired(e => e.StrorageInterface)
                .HasForeignKey(e => e.InterfaceId);

            modelBuilder.Entity<StrorageInterface>()
                .HasMany(e => e.SAN_StorageInt)
                .WithRequired(e => e.StrorageInterface)
                .HasForeignKey(e => e.InterfaceId);
        }
    }
}
