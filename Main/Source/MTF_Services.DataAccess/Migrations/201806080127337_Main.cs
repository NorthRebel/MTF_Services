namespace MTF_Services.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Main : DbMigration
    {
        /// <summary>
        /// Перечень выполняемых операций с сущностями при обновлении базы данных
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.CPU",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Short(nullable: false),
                        Model = c.String(nullable: false, maxLength: 50),
                        CpuSocketId = c.Byte(nullable: false),
                        CoreCount = c.Short(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Frequency = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CpuSocket", t => t.CpuSocketId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.CpuSocketId);
            
            CreateTable(
                "dbo.CpuSocket",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ManufacturerId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Platform",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Short(nullable: false),
                        Model = c.String(nullable: false, maxLength: 100),
                        CpuSocketId = c.Byte(nullable: false),
                        CPUCount = c.Byte(nullable: false),
                        RamTypeId = c.Byte(nullable: false),
                        RamVolumeMax = c.Int(nullable: false),
                        RamSocketCount = c.Byte(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CpuSocket", t => t.CpuSocketId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.RamType", t => t.RamTypeId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.CpuSocketId)
                .Index(t => t.RamTypeId);
            
            CreateTable(
                "dbo.Platform_StorageInt",
                c => new
                    {
                        PlatformId = c.Int(nullable: false),
                        InterfaceId = c.Byte(nullable: false),
                        SlotCount = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlatformId, t.InterfaceId })
                .ForeignKey("dbo.Platform", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.StrorageInterface", t => t.InterfaceId, cascadeDelete: true)
                .Index(t => t.PlatformId)
                .Index(t => t.InterfaceId);
            
            CreateTable(
                "dbo.StrorageInterface",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SAN_StorageInt",
                c => new
                    {
                        SAN_Id = c.Int(nullable: false),
                        InterfaceId = c.Byte(nullable: false),
                        SlotCount = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.SAN_Id, t.InterfaceId })
                .ForeignKey("dbo.SAN", t => t.SAN_Id, cascadeDelete: true)
                .ForeignKey("dbo.StrorageInterface", t => t.InterfaceId, cascadeDelete: true)
                .Index(t => t.SAN_Id)
                .Index(t => t.InterfaceId);
            
            CreateTable(
                "dbo.SAN",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Short(nullable: false),
                        Model = c.String(nullable: false, maxLength: 50),
                        AnnualMaintenance = c.Decimal(storeType: "money"),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Active = c.Boolean(nullable: false),
                        OnMaintenance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.PaasType",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Server",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlatformId = c.Int(nullable: false),
                        CPU_Id = c.Int(nullable: false),
                        Price = c.Decimal(storeType: "money"),
                        AnnualMaintenance = c.Decimal(storeType: "money"),
                        Active = c.Boolean(nullable: false),
                        OnMaintenance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Platform", t => t.PlatformId, cascadeDelete: true)
                .ForeignKey("dbo.CPU", t => t.CPU_Id, cascadeDelete: false)
                .Index(t => t.PlatformId)
                .Index(t => t.CPU_Id);
            
            CreateTable(
                "dbo.Server_MaintenanceShedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServerID = c.Int(nullable: false),
                        BeginPeriod = c.DateTime(nullable: false),
                        EndPeriod = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Server", t => t.ServerID, cascadeDelete: true)
                .Index(t => t.ServerID);
            
            CreateTable(
                "dbo.Server_RAM",
                c => new
                    {
                        ServerId = c.Int(nullable: false),
                        RamId = c.Int(nullable: false),
                        Count = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServerId, t.RamId })
                .ForeignKey("dbo.RAM", t => t.RamId)
                .ForeignKey("dbo.Server", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId)
                .Index(t => t.RamId);
            
            CreateTable(
                "dbo.RAM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Short(nullable: false),
                        Model = c.String(nullable: false, maxLength: 100),
                        RamTypeId = c.Byte(nullable: false),
                        Volume = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.RamType", t => t.RamTypeId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.RamTypeId);
            
            CreateTable(
                "dbo.RamType",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Server_Storage",
                c => new
                    {
                        ServerId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        Count = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServerId, t.StorageId })
                .ForeignKey("dbo.Server", t => t.ServerId, cascadeDelete: true)
                .ForeignKey("dbo.Strorage", t => t.StorageId)
                .Index(t => t.ServerId)
                .Index(t => t.StorageId);
            
            CreateTable(
                "dbo.Strorage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Short(nullable: false),
                        Model = c.String(nullable: false, maxLength: 100),
                        StrorageInterfaceId = c.Byte(nullable: false),
                        Volume = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.StrorageInterface", t => t.StrorageInterfaceId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.StrorageInterfaceId);
            
            CreateTable(
                "dbo.SAN_Storage",
                c => new
                    {
                        SAN_Id = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        Count = c.Short(),
                    })
                .PrimaryKey(t => new { t.SAN_Id, t.StorageId })
                .ForeignKey("dbo.Strorage", t => t.StorageId)
                .ForeignKey("dbo.SAN", t => t.SAN_Id, cascadeDelete: true)
                .Index(t => t.SAN_Id)
                .Index(t => t.StorageId);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaasTypeId = c.Short(nullable: false),
                        ServiceTypeId = c.Byte(nullable: false),
                        CoreCount = c.Byte(),
                        RamCount = c.Double(),
                        HDDVolume = c.Double(),
                        CreateDate = c.DateTime(),
                        DeleteDate = c.DateTime(),
                        ServiceStateId = c.Byte(nullable: false),
                        CostPerHour = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaasType", t => t.PaasTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceState", t => t.ServiceStateId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.PaasTypeId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.ServiceStateId);
            
            CreateTable(
                "dbo.ServiceIdle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        BeginPeriod = c.DateTime(nullable: false),
                        EndPeriod = c.DateTime(nullable: false),
                        IdleTypeId = c.Byte(nullable: false),
                        IdleReasonId = c.Byte(nullable: false),
                        Cost = c.Decimal(storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdleReason", t => t.IdleReasonId, cascadeDelete: true)
                .ForeignKey("dbo.IdleType", t => t.IdleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.IdleTypeId)
                .Index(t => t.IdleReasonId);
            
            CreateTable(
                "dbo.IdleReason",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdleType",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        TabNo = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Fio = c.String(maxLength: 100),
                        RightLevelId = c.Byte(nullable: false),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TabNo)
                .ForeignKey("dbo.Position", t => t.PositionId, cascadeDelete: true)
                .ForeignKey("dbo.RightsLevel", t => t.RightLevelId, cascadeDelete: true)
                .Index(t => t.RightLevelId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        AvgSalary = c.Decimal(nullable: false, storeType: "money"),
                        WorkHours = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RightsLevel",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceState",
                c => new
                    {
                        ID = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ServiceType",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Software",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        SoftTypeId = c.Byte(nullable: false),
                        Cost = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SoftType", t => t.SoftTypeId, cascadeDelete: true)
                .Index(t => t.SoftTypeId);
            
            CreateTable(
                "dbo.SoftType",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SAN_MaintenanceShedule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SAN_Id = c.Int(nullable: false),
                        BeginPeriod = c.DateTime(nullable: false),
                        EndPeriod = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SAN", t => t.SAN_Id, cascadeDelete: true)
                .Index(t => t.SAN_Id);
            
            CreateTable(
                "dbo.SANtoPaas",
                c => new
                    {
                        PaasTypeId = c.Short(nullable: false),
                        SAN_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PaasTypeId, t.SAN_Id })
                .ForeignKey("dbo.PaasType", t => t.PaasTypeId, cascadeDelete: true)
                .ForeignKey("dbo.SAN", t => t.SAN_Id, cascadeDelete: true)
                .Index(t => t.PaasTypeId)
                .Index(t => t.SAN_Id);
            
            CreateTable(
                "dbo.ServerToPaas",
                c => new
                    {
                        PaasTypeId = c.Short(nullable: false),
                        ServerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PaasTypeId, t.ServerId })
                .ForeignKey("dbo.PaasType", t => t.PaasTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Server", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.PaasTypeId)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.AdminIdle",
                c => new
                    {
                        ServiceId = c.Int(nullable: false),
                        UserTabNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceId, t.UserTabNo })
                .ForeignKey("dbo.ServiceIdle", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserTabNo, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.UserTabNo);
            
            CreateTable(
                "dbo.SoftService",
                c => new
                    {
                        ServiceId = c.Int(nullable: false),
                        SoftwareId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceId, t.SoftwareId })
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Software", t => t.SoftwareId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.SoftwareId);
            
            CreateTable(
                "dbo.UsingService",
                c => new
                    {
                        ServiceId = c.Int(nullable: false),
                        UserTabNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceId, t.UserTabNo })
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserTabNo, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.UserTabNo);
            
        }

        /// <summary>
        /// Перечень выполняемых операций с сущностями при откате изменений базы данных
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Server", "CPU_Id", "dbo.CPU");
            DropForeignKey("dbo.SAN_StorageInt", "InterfaceId", "dbo.StrorageInterface");
            DropForeignKey("dbo.SAN_StorageInt", "SAN_Id", "dbo.SAN");
            DropForeignKey("dbo.SAN_Storage", "SAN_Id", "dbo.SAN");
            DropForeignKey("dbo.SAN_MaintenanceShedule", "SAN_Id", "dbo.SAN");
            DropForeignKey("dbo.UsingService", "UserTabNo", "dbo.User");
            DropForeignKey("dbo.UsingService", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.SoftService", "SoftwareId", "dbo.Software");
            DropForeignKey("dbo.SoftService", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Software", "SoftTypeId", "dbo.SoftType");
            DropForeignKey("dbo.Service", "ServiceTypeId", "dbo.ServiceType");
            DropForeignKey("dbo.Service", "ServiceStateId", "dbo.ServiceState");
            DropForeignKey("dbo.AdminIdle", "UserTabNo", "dbo.User");
            DropForeignKey("dbo.AdminIdle", "ServiceId", "dbo.ServiceIdle");
            DropForeignKey("dbo.User", "RightLevelId", "dbo.RightsLevel");
            DropForeignKey("dbo.User", "PositionId", "dbo.Position");
            DropForeignKey("dbo.ServiceIdle", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.ServiceIdle", "IdleTypeId", "dbo.IdleType");
            DropForeignKey("dbo.ServiceIdle", "IdleReasonId", "dbo.IdleReason");
            DropForeignKey("dbo.Service", "PaasTypeId", "dbo.PaasType");
            DropForeignKey("dbo.ServerToPaas", "ServerId", "dbo.Server");
            DropForeignKey("dbo.ServerToPaas", "PaasTypeId", "dbo.PaasType");
            DropForeignKey("dbo.Strorage", "StrorageInterfaceId", "dbo.StrorageInterface");
            DropForeignKey("dbo.Server_Storage", "StorageId", "dbo.Strorage");
            DropForeignKey("dbo.SAN_Storage", "StorageId", "dbo.Strorage");
            DropForeignKey("dbo.Strorage", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Server_Storage", "ServerId", "dbo.Server");
            DropForeignKey("dbo.Server_RAM", "ServerId", "dbo.Server");
            DropForeignKey("dbo.Server_RAM", "RamId", "dbo.RAM");
            DropForeignKey("dbo.RAM", "RamTypeId", "dbo.RamType");
            DropForeignKey("dbo.Platform", "RamTypeId", "dbo.RamType");
            DropForeignKey("dbo.RAM", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Server_MaintenanceShedule", "ServerID", "dbo.Server");
            DropForeignKey("dbo.Server", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.SANtoPaas", "SAN_Id", "dbo.SAN");
            DropForeignKey("dbo.SANtoPaas", "PaasTypeId", "dbo.PaasType");
            DropForeignKey("dbo.SAN", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Platform_StorageInt", "InterfaceId", "dbo.StrorageInterface");
            DropForeignKey("dbo.Platform_StorageInt", "PlatformId", "dbo.Platform");
            DropForeignKey("dbo.Platform", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.Platform", "CpuSocketId", "dbo.CpuSocket");
            DropForeignKey("dbo.CpuSocket", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.CPU", "ManufacturerId", "dbo.Manufacturer");
            DropForeignKey("dbo.CPU", "CpuSocketId", "dbo.CpuSocket");
            DropIndex("dbo.UsingService", new[] { "UserTabNo" });
            DropIndex("dbo.UsingService", new[] { "ServiceId" });
            DropIndex("dbo.SoftService", new[] { "SoftwareId" });
            DropIndex("dbo.SoftService", new[] { "ServiceId" });
            DropIndex("dbo.AdminIdle", new[] { "UserTabNo" });
            DropIndex("dbo.AdminIdle", new[] { "ServiceId" });
            DropIndex("dbo.ServerToPaas", new[] { "ServerId" });
            DropIndex("dbo.ServerToPaas", new[] { "PaasTypeId" });
            DropIndex("dbo.SANtoPaas", new[] { "SAN_Id" });
            DropIndex("dbo.SANtoPaas", new[] { "PaasTypeId" });
            DropIndex("dbo.SAN_MaintenanceShedule", new[] { "SAN_Id" });
            DropIndex("dbo.Software", new[] { "SoftTypeId" });
            DropIndex("dbo.User", new[] { "PositionId" });
            DropIndex("dbo.User", new[] { "RightLevelId" });
            DropIndex("dbo.ServiceIdle", new[] { "IdleReasonId" });
            DropIndex("dbo.ServiceIdle", new[] { "IdleTypeId" });
            DropIndex("dbo.ServiceIdle", new[] { "ServiceId" });
            DropIndex("dbo.Service", new[] { "ServiceStateId" });
            DropIndex("dbo.Service", new[] { "ServiceTypeId" });
            DropIndex("dbo.Service", new[] { "PaasTypeId" });
            DropIndex("dbo.SAN_Storage", new[] { "StorageId" });
            DropIndex("dbo.SAN_Storage", new[] { "SAN_Id" });
            DropIndex("dbo.Strorage", new[] { "StrorageInterfaceId" });
            DropIndex("dbo.Strorage", new[] { "ManufacturerId" });
            DropIndex("dbo.Server_Storage", new[] { "StorageId" });
            DropIndex("dbo.Server_Storage", new[] { "ServerId" });
            DropIndex("dbo.RAM", new[] { "RamTypeId" });
            DropIndex("dbo.RAM", new[] { "ManufacturerId" });
            DropIndex("dbo.Server_RAM", new[] { "RamId" });
            DropIndex("dbo.Server_RAM", new[] { "ServerId" });
            DropIndex("dbo.Server_MaintenanceShedule", new[] { "ServerID" });
            DropIndex("dbo.Server", new[] { "CPU_Id" });
            DropIndex("dbo.Server", new[] { "PlatformId" });
            DropIndex("dbo.SAN", new[] { "ManufacturerId" });
            DropIndex("dbo.SAN_StorageInt", new[] { "InterfaceId" });
            DropIndex("dbo.SAN_StorageInt", new[] { "SAN_Id" });
            DropIndex("dbo.Platform_StorageInt", new[] { "InterfaceId" });
            DropIndex("dbo.Platform_StorageInt", new[] { "PlatformId" });
            DropIndex("dbo.Platform", new[] { "RamTypeId" });
            DropIndex("dbo.Platform", new[] { "CpuSocketId" });
            DropIndex("dbo.Platform", new[] { "ManufacturerId" });
            DropIndex("dbo.CpuSocket", new[] { "ManufacturerId" });
            DropIndex("dbo.CPU", new[] { "CpuSocketId" });
            DropIndex("dbo.CPU", new[] { "ManufacturerId" });
            DropTable("dbo.UsingService");
            DropTable("dbo.SoftService");
            DropTable("dbo.AdminIdle");
            DropTable("dbo.ServerToPaas");
            DropTable("dbo.SANtoPaas");
            DropTable("dbo.SAN_MaintenanceShedule");
            DropTable("dbo.SoftType");
            DropTable("dbo.Software");
            DropTable("dbo.ServiceType");
            DropTable("dbo.ServiceState");
            DropTable("dbo.RightsLevel");
            DropTable("dbo.Position");
            DropTable("dbo.User");
            DropTable("dbo.IdleType");
            DropTable("dbo.IdleReason");
            DropTable("dbo.ServiceIdle");
            DropTable("dbo.Service");
            DropTable("dbo.SAN_Storage");
            DropTable("dbo.Strorage");
            DropTable("dbo.Server_Storage");
            DropTable("dbo.RamType");
            DropTable("dbo.RAM");
            DropTable("dbo.Server_RAM");
            DropTable("dbo.Server_MaintenanceShedule");
            DropTable("dbo.Server");
            DropTable("dbo.PaasType");
            DropTable("dbo.SAN");
            DropTable("dbo.SAN_StorageInt");
            DropTable("dbo.StrorageInterface");
            DropTable("dbo.Platform_StorageInt");
            DropTable("dbo.Platform");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.CpuSocket");
            DropTable("dbo.CPU");
        }
    }
}
