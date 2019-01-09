using MTF_Services.Model;

namespace MTF_Services.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// ����� ������������ ������ ������
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<MTF_Services.DataAccess.MTF_ServicesDbContext>
    {
        /// <summary>
        /// ����������� ������ ������������ ������ ������
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// ����������� ����� ������������� ��������� ��������,
        /// �������� ������ � ��������
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MTF_Services.DataAccess.MTF_ServicesDbContext context)
        {
            context.RightsLevel.AddOrUpdate(
                rl => rl.Name,
                new RightsLevel { Id = 1, Name = "��������" },
                new RightsLevel { Id = 2, Name = "��������� �������������" },
                new RightsLevel { Id = 3, Name = "���������" });

            context.Position.AddOrUpdate(
                p => p.Name,
                new Position
                {
                    Name = "��������� �������������",
                    AvgSalary = new decimal(54692.50),
                    WorkHours = 59
                },
                new Position
                {
                    Name = "����������� ��������",
                    AvgSalary = new decimal(125680.90),
                    WorkHours = 45
                },
                new Position
                {
                    Name = "���������",
                    AvgSalary = new decimal(75930.00),
                    WorkHours = 51
                });

            context.SaveChanges();

            context.User.AddOrUpdate(
                u => u.Login,
                new User
                {
                    Login = "Director",
                    Password = "123Dir",
                    RightsLevel = context.RightsLevel.Single(rl => rl.Name.Equals("��������")),
                    Position = context.Position.Single(p => p.Name.Equals("����������� ��������")),
                    Fio = "������� ����� ����������"
                },
                new User
                {
                    Login = "Admin",
                    Password = "0000",
                    RightsLevel = context.RightsLevel.Single(rl => rl.Name.Equals("��������� �������������")),
                    Position = context.Position.Single(p => p.Name.Equals("��������� �������������")),
                    Fio = "������� ��������� ����������"
                });

            context.Manufacturer.AddOrUpdate(
                m => m.Name,
                new Manufacturer { Name = "AMD" },
                new Manufacturer { Name = "Intel" },
                new Manufacturer { Name = "Asus" },
                new Manufacturer { Name = "HP" },
                new Manufacturer { Name = "WD" },
                new Manufacturer { Name = "Kingston" },
                new Manufacturer { Name = "Seagate" },
                new Manufacturer { Name = "Fujitsu" },
                new Manufacturer { Name = "HGST" },
                new Manufacturer { Name = "Supermicro" },
                new Manufacturer { Name = "Dell" }
                );

            context.SaveChanges();

            var amd = context.Manufacturer.Single(m => m.Name.Equals("AMD"));
            var intel = context.Manufacturer.Single(m => m.Name.Equals("Intel"));

            context.CpuSocket.AddOrUpdate(
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 1366/Socket B"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 1156/Socket H"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 1567/Socket LS"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 1155/Socket H2"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 2011/Socket R"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 1356/Socket B2"
                },
                new CpuSocket
                {
                    Manufacturer = intel,
                    Name = "LGA 3647"
                },
                new CpuSocket
                {
                    Manufacturer = amd,
                    Name = "G34"
                },
                new CpuSocket
                {
                    Manufacturer = amd,
                    Name = "C32"
                },
                new CpuSocket
                {
                    Manufacturer = amd,
                    Name = "SP3"
                }
                );

            context.RamType.AddOrUpdate(
                rt => rt.Name,
                new RamType { Name = "DDD3" },
                new RamType { Name = "DDD4" }
                );

            context.StrorageInterface.AddOrUpdate(
                si => si.Name,
                new StrorageInterface { Name = "SATA3" },
                new StrorageInterface { Name = "SAS 300" },
                new StrorageInterface { Name = "SAS 600" }
                );

            context.PaasType.AddOrUpdate(
                pt => pt.Name,
                new PaasType { Name = "1�" },
                new PaasType { Name = "���. ���������" },
                new PaasType { Name = "SQL" },
                new PaasType { Name = "Cloud" },
                new PaasType { Name = "Chat" },
                new PaasType { Name = "Mail" }
                );

            context.SoftType.AddOrUpdate(
                st => st.Name,
                new SoftType { Name = "���������" },
                new SoftType { Name = "����������" },
                new SoftType { Name = "������ ���������� ��������" },
                new SoftType { Name = "��������" },
                new SoftType { Name = "����������������" },
                new SoftType { Name = "SDK" }
                );

            context.ServiceState.AddOrUpdate(
                ss => ss.Name,
                new ServiceState { Name = "�������" },
                new ServiceState { Name = "���������" },
                new ServiceState { Name = "���������� ������" },
                new ServiceState { Name = "��������" }
                );

            context.IdleType.AddOrUpdate(
                it => it.Name,
                new IdleType { Name = "��������" },
                new IdleType { Name = "�����������" }
                );

            context.IdleReason.AddOrUpdate(
                ir => ir.Name,
                new IdleReason { Name = "��������� �����������" },
                new IdleReason { Name = "���������� ������������" },
                new IdleReason { Name = "���� �������" },
                new IdleReason { Name = "����� ������������ �� �����" },
                new IdleReason { Name = "������������ ������" },
                new IdleReason { Name = "�������� ������������" }
                );
        }
    }
}
