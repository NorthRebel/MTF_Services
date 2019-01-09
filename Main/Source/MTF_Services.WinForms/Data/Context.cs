using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MTF_Services.DataAccess;
using MTF_Services.Model;
using MTF_Services.Model.Views;

namespace MTF_Services.WinForms.Data
{
    /// <summary>
    /// Контекст для работы с базой данных.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Модель данных.
        /// </summary>
        private MTF_ServicesDbContext _ctx = new MTF_ServicesDbContext();

        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public static User CurrentUser { get; private set; }

        /// <summary>
        /// Проверка наличия ссылки на контекст данных.
        /// </summary>
        /// <returns>Наличие ссылки на контекст данных</returns>
        public bool TestConnection()
        {
            return _ctx != null;
        }

        /// <summary>
        /// Сохранение изменений в текущем контексте (ассинхронный)
        /// </summary>
        /// <returns>Число затронутых записей</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Отмена изменений существующей сущности
        /// </summary>
        /// <typeparam name="T">Тип измененной сущности</typeparam>
        /// <param name="entity">Измененная сущность</param>
        /// <returns>Исходный экземпляр сущности</returns>
        public T CancelChanges<T>(T entity) where T : class
        {
            var changedEntity = _ctx.ChangeTracker.Entries().SingleOrDefault(ent => ent.Entity.Equals(entity));
            if (changedEntity != null)
            {
                changedEntity.CurrentValues.SetValues(changedEntity.OriginalValues);
                changedEntity.State = EntityState.Unchanged;
            }

            return changedEntity?.Entity as T;
        }

        /// <summary>
        /// Сохранение изменений в текущем контексте
        /// </summary>
        /// <returns>Число затронутых записей</returns>
        public int SaveChanges()
        {
            return _ctx.SaveChanges();
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns>Наличие ссылки на объект текущего пользователя</returns>
        public async Task<bool> Login(string login, string password)
        {
            CurrentUser = await _ctx.User.SingleOrDefaultAsync(u => u.Login.Equals(login) && u.Password.Equals(password));
            return CurrentUser != null;
        }

        /// <summary>
        /// Выход из учетной записи.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }

        #region Views

        /// <summary>
        /// Получение списка с информацией о платформах.
        /// </summary>
        /// <returns>Коллекция с информацией о платформах</returns>
        public async Task<BindingList<PaasInfo>> GetPaasInfo()
        {
            var qry = from paasType in _ctx.PaasType
                      join service in _ctx.Service on paasType.Id equals service.PaasTypeId into se
                      from subServ in se.DefaultIfEmpty()
                      select new PaasInfo
                      {
                          Name = paasType.Name,
                          SANCount = paasType.SAN.Count,
                          ServerCount = paasType.Server.Count,
                          ServiceCount = paasType.Service.Count,
                          UserCount = subServ.User.Count
                      };

            var paasInfos = await qry.ToListAsync();

            var xx = paasInfos.GroupBy(q => q.Name)
                .Select(q => q.First())
                .ToList();

            return new BindingList<PaasInfo>(xx);
        }

        /// <summary>
        /// Получение списка с информацией о сервисах.
        /// </summary>
        /// <returns>Коллекция с информацией о сервисах</returns>
        public BindingList<ServiceInfo> GetServiceInfo()
        {
            var qry = from serviceType in _ctx.ServiceType
                      join service in _ctx.Service on serviceType.Id equals service.ServiceTypeId into se
                      from subServ in se.DefaultIfEmpty()
                      select new ServiceInfo
                      {
                          Name = serviceType.Name,
                          Total = serviceType.Service.Count,
                          Active = serviceType.Service.Count(x => x.ServiceState.Name == "Активен"),
                          UnActive = serviceType.Service.Count(x =>
                              x.ServiceState.Name == "Неактивен" || x.ServiceState.Name == "Отклонен"),
                          Rejected = serviceType.Service.Count(x => x.ServiceState.Name == "Отклонен")
                      };

            var xx = qry.ToList().GroupBy(q => q.Name)
                .Select(q => q.First())
                .ToList();

            return new BindingList<ServiceInfo>(xx);
        }

        /// <summary>
        /// Получение объекта, который описывает краткое состояние серверов.
        /// </summary>
        /// <returns>Краткое описание состояния серверов</returns>
        public async Task<EquipmentState> GetServersEquipmentState()
        {
            var es = new EquipmentState();
            var servers = await _ctx.Server.ToListAsync();

            es.Total = servers.Count;
            foreach (var serv in servers)
            {
                if (serv.Active)
                    es.Active++;
                else
                    es.UnActive++;
                if (serv.OnMaintenance)
                    es.OnMaintenace++;

            }

            return es;
        }

        /// <summary>
        /// Получение объекта, который описывает краткое состояние систем хранения данных.
        /// </summary>
        /// <returns>Краткое описание состояния систем хранения данных</returns>
        public async Task<EquipmentState> GetSanEquipmentState()
        {
            var es = new EquipmentState();
            var sans = await _ctx.SAN.ToListAsync();

            es.Total = sans.Count;
            foreach (var s in sans)
            {
                if (s.Active)
                    es.Active++;
                else
                    es.UnActive++;
                if (s.OnMaintenance)
                    es.OnMaintenace++;
            }

            return es;
        }

        /// <summary>
        /// Получение списка краткого описания конфигураций серверов
        /// </summary>
        /// <returns>Коллекция с кратким описнием конфигураций серверов</returns>
        public BindingList<ServerInfo> GetServerInfo()
        {
            var qry = from serv in _ctx.Server
                      join cpu in _ctx.CPU on serv.CPU_Id equals cpu.Id into subCpu
                      from cp in subCpu.DefaultIfEmpty()
                      select new ServerInfo
                      {
                          Id = serv.Id,
                          CPU = cp.Manufacturer.Name + " " + cp.Model,
                          CpuCount = cp.CoreCount * serv.Platform.CPUCount,
                          RamSlotCount = serv.Server_RAM.Sum(sr => sr.Count),
                          RamVolume = serv.Server_RAM.Sum(sr => sr.Count * sr.RAM.Volume),
                          StorageCount = serv.Server_Storage.Sum(ss => ss.Count),
                          StorageVolume = serv.Server_Storage.Sum(ss => ss.Count * ss.Strorage.Volume),
                          Platform = serv.Platform.Manufacturer.Name + " " + serv.Platform.Model,
                          ServiceCount = serv.PaasType.Count > 0 ? serv.PaasType.Select(x => x.Service.Count).Sum() : 0,
                          PaasCount = serv.PaasType.Count
                      };

            return new BindingList<ServerInfo>(qry.ToList());
        }

        /// <summary>
        /// Получение списка краткого описания конфигураций систем хранения данных
        /// </summary>
        /// <returns>Коллекция с кратким описнием конфигураций систем хранения данных</returns>
        public BindingList<SAN_Info> GetSanInfo()
        {
            var qry = from san in _ctx.SAN
                      select new SAN_Info
                      {
                          ID = san.Id,
                          Manufacturer = san.Manufacturer.Name,
                          Model = san.Model,
                          StorageCount = san.SAN_Storage.Sum(ss => ss.Count.Value),
                          Volume = san.SAN_Storage.Sum(ss => ss.Strorage.Volume * ss.Count.Value),
                          PaasCount = san.PaasType.Count,
                          ServiceCount = san.PaasType.Count > 0 ? san.PaasType.Select(x => x.Service.Count).Sum() : 0
                      };

            return new BindingList<SAN_Info>(qry.ToList());
        }

        #endregion

        #region SysAdmin_statusInfo

        /// <summary>
        /// Получение количества используемых платформ.
        /// </summary>
        /// <param name="paasInfos">Список с информацией о платформах</param>
        /// <returns>Количество используемых платформ</returns>
        public int UsedPlatformsCount(BindingList<PaasInfo> paasInfos)
        {
            return paasInfos.Count(pi => pi.ServiceCount > 0);
        }

        /// <summary>
        /// Получение количества используемых сервисов.
        /// </summary>
        /// <param name="serviceInfos">Список с информацией о предоставляемых сервисах</param>
        /// <returns>Количество используемых сервисов</returns>
        public int UsedServiceCount(BindingList<ServiceInfo> serviceInfos)
        {
            return serviceInfos.Count;
        }

        /// <summary>
        /// Получение количества используемых конфигураций сервисов.
        /// </summary>
        /// <param name="serviceInfos">Список с информацией о предоставляемых сервисах</param>
        /// <returns>Количество используемых конфигураций сервисов</returns>
        public int ServiceConfigsCount(BindingList<ServiceInfo> serviceInfos)
        {
            return serviceInfos.Sum(si => si.Active);
        }

        /// <summary>
        /// Проверка наличия платформ, на которые распределены ресурсы
        /// </summary>
        /// <returns>Количество платформ</returns>
        public int CheckActivePaas()
        {
            return _ctx.PaasType.Count(p => p.Server.Count > 0 && p.SAN.Count > 0);
        }

        /// <summary>
        /// Получение экземпляра типа сервиса по его наименованию
        /// </summary>
        /// <param name="name">Наименование типа сервиса</param>
        /// <returns>Экземпляр типа сервиса</returns>
        public async Task<ServiceType> GetPaasTypeByName(string name)
        {
            return await _ctx.ServiceType.SingleAsync(st => st.Name.Equals(name));
        }

        /// <summary>
        /// Удаление выбранного типа сервиса
        /// </summary>
        /// <param name="serviceTypeToDel">Тип сервиса, который следует удалить</param>
        public async Task DeleteServiceType(ServiceType serviceTypeToDel)
        {
            _ctx.ServiceType.Remove(serviceTypeToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление выбранного типа платформы
        /// </summary>
        /// <param name="paasTypeToDel">Тип платформы, который следует удалить</param>
        public async Task DeletePaasType(PaasType paasTypeToDel)
        {
            _ctx.PaasType.Remove(paasTypeToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Проверка наличие экземпляров конфигураций серверов и хранилищ данных
        /// </summary>
        public bool CheckInfrastructureToCreatePlatform()
        {
            return _ctx.Server.Any() && _ctx.SAN.Any();
        }

        #endregion

        #region ServerConfiguration

        /// <summary>
        /// Удаление выбранной конфигурации сервера
        /// </summary>
        /// <param name="serverToDel">Конфигурация сервера, которую следует удалить</param>
        public async Task DeleteServer(Server serverToDel)
        {
            _ctx.Server.Remove(serverToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение списка платформ
        /// </summary>
        /// <returns>Список платформ</returns>
        public BindingList<Platform> GetPlatformsList()
        {
            return new BindingList<Platform>(_ctx.Platform.ToList());
        }

        /// <summary>
        /// Получение списка процессоров по их разъему
        /// </summary>
        /// <param name="socketName">Разъем процессора</param>
        /// <returns>Список доступных процессоров по указанному разъему</returns>
        public BindingList<CPU> GetCpusOfSocket(string socketName)
        {
            var qry = from cpu in _ctx.CPU
                      where cpu.CpuSocket.Name.Equals(socketName)
                      select cpu;
            return new BindingList<CPU>(qry.ToList());
        }

        /// <summary>
        /// Получение списка памяти по ее типу.
        /// </summary>
        /// <param name="ramType">Тип памяти</param>
        /// <returns>Спискок доступной памяти по указанному типу</returns>
        public BindingList<RAM> GetRamsOfRamType(string ramType)
        {
            var qry = from ram in _ctx.RAM
                      where ram.RamType.Name.Equals(ramType)
                      select ram;
            return new BindingList<RAM>(qry.ToList());
        }

        /// <summary>
        /// Получение списка доступных накопителей по указанным интерфейсам
        /// </summary>
        /// <param name="avalibleInterfaces">Список доступных интерфейсов</param>
        /// <returns>Список доступных накопителей по укзанным интерфейсам</returns>
        public BindingList<Strorage> GetStoragesByAvaliblePlatformInterfaces(ICollection<Platform_StorageInt> avalibleInterfaces)
        {
            var avInt = avalibleInterfaces.ToList();
            var listOfAvalibleStorages =
                _ctx.Strorage.ToList().Where(s => avInt.Exists(nterf => nterf.StrorageInterface.Equals(s.StrorageInterface)));

            return new BindingList<Strorage>(listOfAvalibleStorages.ToList());
        }

        /// <summary>
        /// Получение преобразованного списка доступных интерфейсов накопителей для платформы
        /// </summary>
        /// <param name="storageInterfaces">Интерфейсы накопителей платформы</param>
        /// <returns>Преобразованный список доступных интерфейсов накопителей</returns>
        public BindingList<AvalibleInterface> GetAvalibleInterfacesOfPlarformBS(ICollection<Platform_StorageInt> storageInterfaces)
        {
            return new BindingList<AvalibleInterface>(GetAvalibleInterfacesOfPlarform(storageInterfaces));
        }

        /// <summary>
        /// Получение обратно преобразованного списка доступных интерфейсов накопителей для платформы
        /// </summary>
        /// <param name="platform">Платформа</param>
        /// <param name="avalibleInterfaces">Преобразованный список доступных интерфейсов накопителей</param>
        /// <returns>Обработно преобразованный список доступных интерфейсов накопителей</returns>
        public List<Platform_StorageInt> GetPlatformStorageIntFromAvalible(Platform platform,
            List<AvalibleInterface> avalibleInterfaces)
        {
            var plStInt = new List<Platform_StorageInt>();
            avalibleInterfaces.ForEach(ai => plStInt.Add(new Platform_StorageInt
            {
                StrorageInterface = GetInterfaceIdByName(ai.Name),
                Platform = platform,
                SlotCount = ai.Slot_Count
            }));

            return plStInt;
        }

        /// <summary>
        /// Получение кода интерфейса по его наименованию
        /// </summary>
        /// <param name="intName">Наименование интерфейса</param>
        /// <returns>Код интерфейса</returns>
        private StrorageInterface GetInterfaceIdByName(string intName)
        {
            return _ctx.StrorageInterface.Single(si => si.Name.Equals(intName));
        }

        /// <summary>
        /// Получение списка доступных интерфейсов выбранной платформы
        /// </summary>
        private List<AvalibleInterface> GetAvalibleInterfacesOfPlarform(ICollection<Platform_StorageInt> storageInterfaces)
        {
            var storInt = storageInterfaces.ToList();
            var avInt = new List<AvalibleInterface>();
            storInt.ForEach(si => avInt.Add(new AvalibleInterface
            {
                Name = si.StrorageInterface.Name,
                Slot_Count = si.SlotCount
            }));
            return avInt;
        }

        /// <summary>
        /// Сохранение новой конфигурации сервера
        /// </summary>
        /// <param name="newServer">Новая конфигурация сервера</param>
        public async Task AddNewServerConfiguration(Server newServer)
        {
            _ctx.Server.Add(newServer);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Сохранение изменений в редактируемой сервера
        /// </summary>
        /// <param name="editedServer">Редактируемуя сервера</param>
        public async Task EditServerConfiguration(Server editedServer)
        {
            _ctx.Server.Attach(editedServer);
            _ctx.Entry(editedServer).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение ссылки на экземпляр сервера по его подробной информации
        /// </summary>
        /// <param name="selectedServer">Подробная информация о сервере</param>
        /// <returns>Ссылка на экземпляр сервера</returns>
        public async Task<Server> GetServerByServerInfo(ServerInfo selectedServer)
        {
            return await _ctx.Server
                    .Include(s => s.Server_RAM)
                    .Include(s => s.Server_Storage)
                    .Include(s => s.CPU)
                    .Include(s => s.Platform)
                .SingleAsync(s => s.Id == selectedServer.Id);
        }

        /// <summary>
        /// Получение экземпляра текущей платформы с коллекцией поддерживаемых интерфейсов
        /// </summary>
        /// <param name="currentPlatform">Текущая платформа</param>
        /// <returns>Экземпляр текущей платформы с коллекцией поддерживаемых интерфейсов</returns>
        public Platform GetPlatformInclude(Platform currentPlatform)
        {
            return _ctx.Platform
                .Include(p => p.Platform_StorageInt)
                .Single(p => p.Id == currentPlatform.Id);
        }

        #endregion

        #region SelectPlatform

        /// <summary>
        /// Получение списка платформ с подробным описанием.
        /// </summary>
        /// <returns>Список платформ с подробным описанием</returns>
        public BindingList<PlatformInfo> GetPlatformsInfo()
        {
            var qry = from platform in _ctx.Platform
                      select new PlatformInfo
                      {
                          Model = platform.Model,
                          Manufacturer = platform.Manufacturer.Name,
                          CPUSocket = platform.CpuSocket.Name,
                          CPU_Count = platform.CPUCount,
                          RAMType = platform.RamType.Name,
                          RamSocketCount = platform.RamSocketCount,
                          RamVolume_Max = platform.RamVolumeMax,
                          Price = platform.Price
                      };
            return new BindingList<PlatformInfo>(qry.ToList());
        }

        /// <summary>
        /// Получение экземпляра платформы из подробного описания платформы
        /// </summary>
        /// <param name="selectedPlatformInfo">Подробное описание платформы</param>
        /// <returns>Экземпляр платформы</returns>
        public Platform GetPlatformByPlatformInfo(PlatformInfo selectedPlatformInfo)
        {
            return _ctx.Platform
                .Include(pl => pl.Platform_StorageInt)
                .Include(pl => pl.Manufacturer)
                .Include(pl => pl.CpuSocket)
                .Include(pl => pl.RamType)
                .SingleOrDefault(p =>
                p.Model.Equals(selectedPlatformInfo.Model) &&
                p.Manufacturer.Name.Equals(selectedPlatformInfo.Manufacturer));
        }

        /// <summary>
        /// Удаление выбранной платформы
        /// </summary>
        /// <param name="platformToDel">Платформа, которую следует удалить</param>
        public async Task DeletePlatform(Platform platformToDel)
        {
            _ctx.Platform.Remove(platformToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение подробного описания платформы по ее экземпляру
        /// </summary>
        /// <param name="platform">Экземпляр платформы</param>
        /// <returns>Подробное описание платформы</returns>
        public PlatformInfo GetPlatformInfoByPlatform(Platform platform)
        {
            return new PlatformInfo
            {
                Model = platform.Model,
                Manufacturer = platform.Manufacturer.Name,
                CPUSocket = platform.CpuSocket.Name,
                CPU_Count = platform.CPUCount,
                RAMType = platform.RamType.Name,
                RamSocketCount = platform.RamSocketCount,
                RamVolume_Max = platform.RamVolumeMax,
                Price = platform.Price
            };
        }

        #endregion

        #region Manufacturers

        /// <summary>
        /// Получение списка производителей с привязкой
        /// </summary>
        /// <returns>Список производителей</returns>
        public BindingList<Manufacturer> GetManufacturers()
        {
            _ctx.Manufacturer.Load();
            return _ctx.Manufacturer.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования наименования производителя
        /// </summary>
        /// <param name="name">Наименование производителя</param>
        /// <returns>Наличие производителя с таким наименованием</returns>
        public async Task<bool> CheckManufacturerForDublicate(string name)
        {
            return await _ctx.Manufacturer.SingleOrDefaultAsync(m => m.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        #endregion

        #region CPU_Sockets

        /// <summary>
        /// Получение списка разъемов процессора с наименованием производителя
        /// </summary>
        /// <returns>Списиок разъемов процессора</returns>
        public BindingList<CPUSocketInfo> GetCPUSocketsInfo()
        {
            var qry = from cpuSocket in _ctx.CpuSocket
                      select new CPUSocketInfo
                      {
                          Manufacturer = cpuSocket.Manufacturer.Name,
                          Socket = cpuSocket.Name
                      };
            return new BindingList<CPUSocketInfo>(qry.ToList());
        }

        /// <summary>
        /// Получение разъема процессора по его информации
        /// </summary>
        /// <param name="selectedItem">Информация о разъеме</param>
        /// <returns>Разъем процессора</returns>
        public async Task<CpuSocket> GetCPUSocketByInfo(CPUSocketInfo selectedItem)
        {
            return await _ctx.CpuSocket.SingleAsync(cs =>
                cs.Name.Equals(selectedItem.Socket) && cs.Manufacturer.Name.Equals(selectedItem.Manufacturer));
        }

        /// <summary>
        /// Проверка дублирования разъема процессора
        /// </summary>
        /// <param name="name">Наименование разъемв</param>
        /// <param name="manufacturerName">Наименование производителя</param>
        /// <returns>Наличие разъема у производителя с таким наименованием</returns>
        public async Task<bool> CheckCpuSocketForDublicate(string name, string manufacturerName)
        {
            return await _ctx.CpuSocket.SingleOrDefaultAsync(cs =>
                       cs.Name.ToUpper().Equals(name.ToUpper()) && cs.Manufacturer.Name.Equals(manufacturerName)) != null;
        }

        /// <summary>
        /// Добавление нового разъема процессора
        /// </summary>
        /// <param name="newSocket">Новый разъем процессора</param>
        public async Task AddNewCPUSocket(CpuSocket newSocket)
        {
            _ctx.CpuSocket.Add(newSocket);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующего разъема процессора
        /// </summary>
        /// <param name="newSocket">Существующий разъем процессора</param>
        public async Task EditCPUSocket(CpuSocket editedSocket)
        {
            _ctx.CpuSocket.Attach(editedSocket);
            _ctx.Entry(editedSocket).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region EditPlatform

        /// <summary>
        /// Получение списка разъемов процессоров
        /// </summary>
        /// <returns>Список разъемов процессоров</returns>
        public BindingList<CpuSocket> GetCPUSockets()
        {
            return new BindingList<CpuSocket>(_ctx.CpuSocket.ToList());
        }

        /// <summary>
        /// Получение списка типов ОЗУ
        /// </summary>
        /// <returns>Список типов ОЗУ</returns>
        public BindingList<RamType> GetRamsTypes()
        {
            return new BindingList<RamType>(_ctx.RamType.ToList());
        }

        /// <summary>
        /// Получение списка интерфейсов храненения
        /// </summary>
        /// <returns>Список интерфейсов храненения</returns>
        public BindingList<StrorageInterface> GetStorageInterfaces()
        {
            return new BindingList<StrorageInterface>(_ctx.StrorageInterface.ToList());
        }

        /// <summary>
        /// Проверка дублирования платформы
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="manufacturer">Производитель</param>
        /// <returns>Наличие такой модели платформы у производителя</returns>
        public async Task<bool> CheckPlatformForDublicate(string model, string manufacturer)
        {
            return await _ctx.Platform.SingleOrDefaultAsync(pl =>
                       pl.Model.ToUpper().Equals(model.ToUpper()) && pl.Manufacturer.Name.Equals(manufacturer)) != null;
        }

        /// <summary>
        /// Добавление новой платформы
        /// </summary>
        /// <param name="newPlatform">Новая платформа</param>
        public async Task AddNewPlatform(Platform newPlatform)
        {
            _ctx.Platform.Add(newPlatform);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующей платформы
        /// </summary>
        /// <param name="editedPlatform">Существующая платформа</param>
        public async Task EditPlatform(Platform editedPlatform)
        {
            _ctx.Platform.Attach(editedPlatform);
            _ctx.Entry(editedPlatform).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }


        #endregion

        #region EditUsers

        /// <summary>
        /// Получение списка уровней привелегий пользователя
        /// </summary>
        /// <returns>Список уровней привелегий пользователя</returns>
        public BindingList<RightsLevel> GetRightsLevels()
        {
            return new BindingList<RightsLevel>(_ctx.RightsLevel.ToList());
        }

        /// <summary>
        /// Получение списка должностей
        /// </summary>
        /// <returns>Список должностей</returns>
        public BindingList<Position> GetPositions()
        {
            _ctx.Position.Load();
            return _ctx.Position.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования логина пользователя
        /// </summary>
        /// <param name="login">Новый логин</param>
        /// <returns>Наличие уже существующего логина</returns>
        public async Task<bool> CheckUserLoginForDublicate(string login)
        {
            return await _ctx.User.SingleOrDefaultAsync(u => u.Login.ToUpper().Equals(login.ToUpper())) != null;
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        public async Task AddNewUser(User user)
        {
            _ctx.User.Add(user);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="user">Новый пользователь</param>
        public async Task EditUser(User user)
        {
            _ctx.User.Attach(user);
            _ctx.Entry(user).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region EditPosition

        /// <summary>
        /// Проверка дублирования наименования должности
        /// </summary>
        /// <param name="currentPositionName">Наименование должности</param>
        /// <returns>Наличие должности с таким наименованием</returns>
        public async Task<bool> CheckPositionForDublicate(string positionName)
        {
            return await _ctx.Position.SingleOrDefaultAsync(p => p.Name.ToUpper().Equals(positionName.ToUpper())) != null;
        }


        #endregion

        #region UserList

        /// <summary>
        /// Получение списка пользователей с подробным описанием
        /// </summary>
        /// <returns>Список пользователей с подробным описанием</returns>
        public BindingList<UserInfo> GetUsersInfo()
        {
            var usr = _ctx.User
                .Include(u => u.Position)
                .Include(u => u.RightsLevel)
                .ToList();
            var usrInfo = new List<UserInfo>();
            usr.ForEach(u => usrInfo.Add(new UserInfo
            {
                TabNo = u.TabNo,
                Login = u.Login,
                Position = u.Position.Name,
                RightsLevel = u.RightsLevel.Name,
                Fio = u.Fio
            }));

            return new BindingList<UserInfo>(usrInfo);
        }

        /// <summary>
        /// Получение ссылки на пользователя по его подробному описанию
        /// </summary>
        /// <param name="currentUser">Текущая информация о пользователе</param>
        /// <returns>Ссылка на пользователя</returns>
        public async Task<User> GetUserByUserInfo(UserInfo currentUser)
        {
            if (currentUser == null)
                return null;
            return await _ctx.User.SingleOrDefaultAsync(u => u.TabNo == currentUser.TabNo);
        }

        #endregion

        #region SelectStorage

        /// <summary>
        /// Получение списка накопителей с подробным описанием
        /// </summary>
        /// <returns>Список накопителей с подробным описанием</returns>
        public BindingList<StorageInfo> GetStoragesInfo()
        {
            var stor = from strorage in _ctx.Strorage
                       select new StorageInfo
                       {
                           Manufacturer = strorage.Manufacturer.Name,
                           Model = strorage.Model,
                           Price = strorage.Price,
                           StrorageInterface = strorage.StrorageInterface.Name,
                           Volume = strorage.Volume
                       };
            return new BindingList<StorageInfo>(stor.ToList());
        }

        /// <summary>
        /// Получение ссылки на накопитель по его подробной информации
        /// </summary>
        /// <param name="currentStorage">Информация об накопителе</param>
        /// <returns>Ссылка на накопитель</returns>
        public async Task<Strorage> GetStorageByStorageInfo(StorageInfo currentStorage)
        {
            if (currentStorage == null)
                return null;

            return await _ctx.Strorage.SingleOrDefaultAsync(s =>
                s.Model.Equals(currentStorage.Model) && s.Manufacturer.Name.Equals(currentStorage.Manufacturer));
        }

        /// <summary>
        /// Удаление выбранного накопителя
        /// </summary>
        /// <param name="strorageToDel">Накопитель, который следует удалить</param>
        public async Task DeleteStrorage(Strorage strorageToDel)
        {
            _ctx.Strorage.Remove(strorageToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение экземпляра накопителя по ее модели и производителю
        /// </summary>
        /// <param name="model">Наименование модели</param>
        /// <param name="manufacturer">Наименование производителя</param>
        /// <returns>Экземпляр накопителя</returns>
        public Strorage GetStorageByModelManufacturer(string model, string manufacturer)
        {
            return _ctx.Strorage.Single(r => r.Model.Equals(model) && r.Manufacturer.Name.Equals(manufacturer));
        }

        /// <summary>
        /// Удаление выбранного пользователя
        /// </summary>
        /// <param name="userToDel">Пользователь, которого следует удалить</param>
        public async Task DeleteUser(User userToDel)
        {
            _ctx.User.Remove(userToDel);
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region EditStorage

        /// <summary>
        /// Проверка дублирования накопителя
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="manufacturer">Производитель</param>
        /// <returns>Наличие существующего накопителя с такой моделью и производителем</returns>
        public async Task<bool> CheckStorageForDublicate(string model, string manufacturer)
        {
            return await _ctx.Strorage.SingleOrDefaultAsync(s =>
                       s.Model.ToUpper().Equals(model.ToUpper()) && s.Manufacturer.Name.Equals(manufacturer)) != null;
        }

        /// <summary>
        /// Добавление нового накопителя
        /// </summary>
        /// <param name="newStorage">Новый накопитель</param>
        public async Task AddNewStorage(Strorage newStorage)
        {
            _ctx.Strorage.Add(newStorage);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующего накопителя
        /// </summary>
        /// <param name="editedStrorage">Существующий накопитель</param>
        public async Task EditStorage(Strorage editedStrorage)
        {
            _ctx.Strorage.Attach(editedStrorage);
            _ctx.Entry(editedStrorage).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region StorageInterfaces

        /// <summary>
        /// Получение списка интерфейсов накопителей с привязкой
        /// </summary>
        /// <returns>Список интерфейсов накопителей</returns>
        public BindingList<StrorageInterface> GetStorageInterfacesBS()
        {
            _ctx.StrorageInterface.Load();
            return _ctx.StrorageInterface.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования наименования интерфейса накопителя
        /// </summary>
        /// <param name="name">Наименование интерфейса накопителя</param>
        /// <returns>Наличие интерфейса накопителя с таким наименованием</returns>
        public async Task<bool> CheckStorageInterfaceForDublicate(string name)
        {
            return await _ctx.StrorageInterface.SingleOrDefaultAsync(m => m.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        #endregion

        #region SelectCPU

        /// <summary>
        /// Получение списка процессоров с подробным описанием
        /// </summary>
        /// <returns>Список процессоров с подробным описанием</returns>
        public BindingList<CPUInfo> GetCPUsInfo()
        {
            var cp = from cpu in _ctx.CPU
                     select new CPUInfo
                     {
                         Model = cpu.Model,
                         Manufacturer = cpu.Manufacturer.Name,
                         CpuSocket = cpu.CpuSocket.Name,
                         Core_Count = cpu.CoreCount,
                         Price = cpu.Price,
                         Frequency = cpu.Frequency
                     };

            return new BindingList<CPUInfo>(cp.ToList());
        }

        /// <summary>
        /// Получение ссылки на процессор по его подробной информации
        /// </summary>
        /// <param name="currentCPU">Информация об процессоре</param>
        /// <returns>Ссылка на процессор</returns>
        public async Task<CPU> GetCPUByCPUInfo(CPUInfo currentCPU)
        {
            if (currentCPU == null)
                return null;

            return await _ctx.CPU.SingleOrDefaultAsync(s =>
                s.Model.Equals(currentCPU.Model) && s.Manufacturer.Name.Equals(currentCPU.Manufacturer));
        }

        /// <summary>
        /// Удаление выбранного процессора
        /// </summary>
        /// <param name="cpuToDel">Процессор, который следует удалить</param>
        public async Task DeleteCPU(CPU cpuToDel)
        {
            _ctx.CPU.Remove(cpuToDel);
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region EditCPU

        /// <summary>
        /// Проверка дублирования процессора
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="manufacturer">Производитель</param>
        /// <returns>Наличие существующего процессора с такой моделью и производителем</returns>
        public async Task<bool> CheckCPUForDublicate(string model, string manufacturer)
        {
            return await _ctx.CPU.SingleOrDefaultAsync(s =>
                       s.Model.ToUpper().Equals(model.ToUpper()) && s.Manufacturer.Name.Equals(manufacturer)) != null;
        }

        /// <summary>
        /// Добавление нового процессора
        /// </summary>
        /// <param name="newCPU">Новый процессор</param>
        public async Task AddNewCPU(CPU newCPU)
        {
            _ctx.CPU.Add(newCPU);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующего процессора
        /// </summary>
        /// <param name="editedCPU">Существующий процессор</param>
        public async Task EditCPU(CPU editedCPU)
        {
            _ctx.CPU.Attach(editedCPU);
            _ctx.Entry(editedCPU).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region SelectRAM

        /// <summary>
        /// Получение списка оперативной памяти с подробным описанием
        /// </summary>
        /// <returns>Список оперативной памяти с подробным описанием</returns>
        public BindingList<RAMInfo> GetRAMsInfo()
        {
            var cp = from ram in _ctx.RAM
                     select new RAMInfo
                     {
                         Model = ram.Model,
                         Manufacturer = ram.Manufacturer.Name,
                         RamType = ram.RamType.Name,
                         Volume = ram.Volume,
                         Price = ram.Price
                     };

            return new BindingList<RAMInfo>(cp.ToList());
        }

        /// <summary>
        /// Получение ссылки на оперативную память по его подробной информации
        /// </summary>
        /// <param name="currentRAM">Информация об оперативной памяти</param>
        /// <returns>Ссылка на оперативную память</returns>
        public async Task<RAM> GetRAMByRAMInfo(RAMInfo currentRAM)
        {
            if (currentRAM == null)
                return null;

            return await _ctx.RAM.SingleOrDefaultAsync(s =>
                s.Model.Equals(currentRAM.Model) && s.Manufacturer.Name.Equals(currentRAM.Manufacturer));
        }

        /// <summary>
        /// Удаление выбранной оперативной памяти
        /// </summary>
        /// <param name="ramToDel">Оперативная память, которую следует удалить</param>
        public async Task DeleteRAM(RAM ramToDel)
        {
            _ctx.RAM.Remove(ramToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение экземпляра оперативной памяти по ее модели и производителю
        /// </summary>
        /// <param name="model">Наименование модели</param>
        /// <param name="manufacturer">Наименование производителя</param>
        /// <returns>Экземпляр оперативной памяти</returns>
        public async Task<RAM> GetRAMByModelManufacturer(string model, string manufacturer)
        {
            return await _ctx.RAM.SingleAsync(r => r.Model.Equals(model) && r.Manufacturer.Name.Equals(manufacturer));
        }

        #endregion

        #region EditRAM

        /// <summary>
        /// Проверка дублирования оперативной памяти
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="manufacturer">Производитель</param>
        /// <returns>Наличие существующей оперативной памяти с такой моделью и производителем</returns>
        public async Task<bool> CheckRAMForDublicate(string model, string manufacturer)
        {
            return await _ctx.RAM.SingleOrDefaultAsync(s =>
                       s.Model.ToUpper().Equals(model.ToUpper()) && s.Manufacturer.Name.Equals(manufacturer)) != null;
        }

        /// <summary>
        /// Добавление новой оперативной памяти
        /// </summary>
        /// <param name="newRAM">Новая оперативная память</param>
        public async Task AddNewRAM(RAM newRAM)
        {
            _ctx.RAM.Add(newRAM);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующей оперативной памяти
        /// </summary>
        /// <param name="editedRAM">Существующая оперативная память</param>
        public async Task EditRAM(RAM editedRAM)
        {
            _ctx.RAM.Attach(editedRAM);
            _ctx.Entry(editedRAM).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region RAM_Types

        /// <summary>
        /// Получение списка типов оперативной памяти с привязкой
        /// </summary>
        /// <returns>Список типов оперативной памяти</returns>
        public BindingList<RamType> GetRAMTypesBS()
        {
            _ctx.RamType.Load();
            return _ctx.RamType.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования наименования типа оперативной памяти
        /// </summary>
        /// <param name="name">Наименование типа оперативной памяти</param>
        /// <returns>Наличие типа оперативной памяти с таким наименованием</returns>
        public async Task<bool> CheckRAMTypeForDublicate(string name)
        {
            return await _ctx.RamType.SingleOrDefaultAsync(m => m.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        #endregion

        #region EditSAN

        /// <summary>
        /// Получение списка доступных накопителей по укзанным интерфейсам хранилища данных
        /// </summary>
        /// <param name="avalibleInterfaces">Список доступных интерфейсов</param>
        /// <returns>Список доступных накопителей по укзанным интерфейсам хранилища данных</returns>
        public BindingList<Strorage> GetStoragesByAvalibleSANInterfaces(ICollection<SAN_StorageInt> avalibleInterfaces)
        {
            var avInt = avalibleInterfaces.ToList();
            var listOfAvalibleStorages =
                _ctx.Strorage.ToList().Where(s => avInt.Exists(nterf => nterf.StrorageInterface.Equals(s.StrorageInterface)));
            return new BindingList<Strorage>(listOfAvalibleStorages.ToList());
        }

        /// <summary>
        /// Получение списка доступных накопителей по укзанным интерфейсам
        /// </summary>
        /// <param name="avalibleInterfaces">Список доступных интерфейсов</param>
        /// <returns>Список доступных накопителей по укзанным интерфейсам</returns>
        public BindingList<Strorage> GetStoragesByAvalibleInterfaces(ICollection<AvalibleInterface> avalibleInterfaces)
        {
            var avInt = avalibleInterfaces.ToList();
            var listOfAvalibleStorages =
                _ctx.Strorage.ToList().Where(s => avInt.Exists(x => x.Name.Equals(s.StrorageInterface.Name)));
            return new BindingList<Strorage>(listOfAvalibleStorages.ToList());
        }

        /// <summary>
        /// Получение преобразованного списка доступных интерфейсов накопителей для хранилища данных с привязкой
        /// </summary>
        /// <param name="storageInterfaces">Интерфейсы накопителей хранилища данных</param>
        /// <returns>Преобразованный список доступных интерфейсов накопителей</returns>
        public BindingList<AvalibleInterface> GetAvalibleInterfacesOfSANBS(ICollection<SAN_StorageInt> storageInterfaces)
        {
            return new BindingList<AvalibleInterface>(GetAvalibleInterfacesOfSAN(storageInterfaces));
        }

        /// <summary>
        /// Получение преобразованного списка доступных интерфейсов накопителей для хранилища данных
        /// </summary>
        /// <param name="storageInterfaces">Интерфейсы накопителей хранилища данных</param>
        /// <returns>Преобразованный список доступных интерфейсов накопителей</returns>
        private List<AvalibleInterface> GetAvalibleInterfacesOfSAN(ICollection<SAN_StorageInt> storageInterfaces)
        {
            var storInt = storageInterfaces.ToList();
            var avInt = new List<AvalibleInterface>();
            storInt.ForEach(si => avInt.Add(new AvalibleInterface
            {
                Name = si.StrorageInterface.Name,
                Slot_Count = si.SlotCount
            }));
            return avInt;
        }

        /// <summary>
        /// Получение обратно преобразованного списка доступных интерфейсов накопителей для хранилища данных
        /// </summary>
        /// <param name="san">Хранилище данных</param>
        /// <param name="avalibleInterfaces">Преобразованный список доступных интерфейсов накопителей</param>
        /// <returns>Обработно преобразованный список доступных интерфейсов накопителей</returns>
        public List<SAN_StorageInt> GetSANStorageIntFromAvalible(SAN san,
            List<AvalibleInterface> avalibleInterfaces)
        {
            var sanStInt = new List<SAN_StorageInt>();
            avalibleInterfaces.ForEach(ai => sanStInt.Add(new SAN_StorageInt
            {
                StrorageInterface = GetInterfaceIdByName(ai.Name),
                SAN = san,
                SlotCount = ai.Slot_Count
            }));

            return sanStInt;
        }

        /// <summary>
        /// Получение ссылки на экземпляр интерфейса накопителя по его наименованию
        /// </summary>
        /// <param name="aiName">Наименование интерфейса накопителя</param>
        /// <returns>Ссылка на экземпляр интерфейса</returns>
        public StrorageInterface GetStorageInterfaceByName(string aiName)
        {
            return _ctx.StrorageInterface.Single(si => si.Name.Equals(aiName));
        }

        /// <summary>
        /// Сохранение нового хранилища данных
        /// </summary>
        /// <param name="newSAN">Новое хранилище данных</param>
        public async Task AddNewSAN(SAN newSAN)
        {
            _ctx.SAN.Add(newSAN);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Сохранение изменений в редактируемом хранилище данных
        /// </summary>
        /// <param name="editedSAN">Редактируемое хранилище данных</param>
        public async Task EditSAN(SAN editedSAN)
        {
            _ctx.SAN.Attach(editedSAN);
            _ctx.Entry(editedSAN).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление выбранной конфигурации хранилища данных
        /// </summary>
        /// <param name="sanToDel">Конфигурация хранилища данных, которую следует удалить</param>
        public async Task DeleteSAN(SAN sanToDel)
        {
            _ctx.SAN.Remove(sanToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение ссылки на экземпляр файлового хранилища по его подробной информации
        /// </summary>
        /// <param name="selectedSAN">Подробная информация о файловом хралилище</param>
        /// <returns>Ссылка на экземпляр файлового хранилища</returns>
        public async Task<SAN> GetSANBySANInfo(SAN_Info selectedSAN)
        {
            return await _ctx.SAN
                .Include(s => s.SAN_Storage)
                .Include(s => s.SAN_StorageInt)
                .SingleAsync(s => s.Id == selectedSAN.ID);
        }

        #endregion

        #region EditPaas

        /// <summary>
        /// Получение списка сервисов, которые используют выбранный сервер
        /// </summary>
        /// <param name="s">Выбранный сервер</param>
        /// <returns>Список сервисов, которые используют выбранный сервер</returns>
        public List<Service> GetServicesByServer(Server s)
        {
            var paasTypes = s.PaasType.ToList();
            return _ctx.Service
                .Include(serv => serv.ServiceIdle)
                .ToList().Where(x => paasTypes.Contains(x.PaasType)).ToList();
        }

        /// <summary>
        /// Получение списка используемых серверов выбранного сервиса
        /// </summary>
        /// <param name="s">Выбранный сервис</param>
        /// <returns>Список используемых серверов выбранного сервиса</returns>
        public List<Server> GetServersByService(Service s)
        {
            return _ctx.Server.ToList().Where(x => x.PaasType.ToList().Contains(s.PaasType)).ToList();
        }

        /// <summary>
        /// Получение списка используемых хранилищ данных выбранного сервиса 
        /// </summary>
        /// <param name="s">Выбранный сервис</param>
        /// <returns>Список используемых хранилищ данных выбранного сервиса </returns>
        public List<SAN> GetSANByService(Service s)
        {
            return _ctx.SAN.ToList().Where(x => x.PaasType.ToList().Contains(s.PaasType)).ToList();
        }

        /// <summary>
        /// Получение списка сервисов, которые используют выбранное хранилище данных
        /// </summary>
        /// <param name="s">Выбранное хранилище данных</param>
        /// <returns>Список сервисов, которые используют выбранное хранилище данных</returns>
        public List<Service> GetServicesBySAN(SAN s)
        {
            var paasTypes = s.PaasType.ToList();
            return _ctx.Service.ToList().Where(x => paasTypes.Contains(x.PaasType)).ToList();
        }

        /// <summary>
        /// Получение списка конфигураций серверов
        /// </summary>
        /// <returns>Список конфигураций серверов</returns>
        public async Task<List<Server>> GetServerConfigs()
        {
            return await _ctx.Server.ToListAsync();
        }

        /// <summary>
        /// Получение списка используемых конфигураций серверов по выбранному типу плфатформы
        /// </summary>
        /// <returns>Список используемых конфигураций серверов выбранной платформы</returns>
        public List<Server> GetServerConfigsByPaas(PaasType paasType)
        {
            var servers = _ctx.Server
                .Include(pt => pt.PaasType)
                .ToList();
            return servers.Where(pt => pt.PaasType.Contains(paasType)).ToList();
        }

        /// <summary>
        /// Получение списка хранилищ данных
        /// </summary>
        /// <returns>Список хранилищ данных</returns>
        public async Task<List<SAN>> GetSANsList()
        {
            return await _ctx.SAN.ToListAsync();
        }

        /// <summary>
        /// Получение списка используемых хранилищ данных по выбранному типу платформы
        /// </summary>
        /// <returns>Список используемых хранилищ данных по выбранному типу платформы</returns>
        public List<SAN> GetSANsListByPaas(PaasType paasType)
        {
            var sans = _ctx.SAN
                .Include(pt => pt.PaasType)
                .ToList();

            return sans.Where(pt => pt.PaasType.Contains(paasType)).ToList();
        }

        /// <summary>
        /// Получение информации о доступных/используемых ресурсов выбранного сервера
        /// </summary>
        /// <param name="selectedServer">Выбранный сервер</param>
        /// <returns>Информация о доступных/используемых ресурсов выбранного сервера</returns>
        public ServerPaasInfo GetServerPaasInfoByServer(Server selectedServer)
        {
            var sInfo = GetServerInfo();
            var servInfo = sInfo.Single(si => si.Id == selectedServer.Id);
            var servicesByServer = GetServicesByServer(selectedServer);

            var usedCoreCount = servicesByServer.Sum(cp => cp.CoreCount.Value);
            var usedRAMVolume = servicesByServer.Sum(rv => rv.RamCount.Value);
            var usedStorageVolume = (servicesByServer.Sum(sv => sv.HDDVolume.Value) / 10);
            return new ServerPaasInfo
            {
                Id = selectedServer.Id,
                CPU = servInfo.CPU,
                Platform = servInfo.Platform,
                AvalibleCoreCount = servInfo.CpuCount - usedCoreCount,
                AvalibleRAMVolume = servInfo.RamVolume - usedRAMVolume,
                AvalibleStorageVolume = servInfo.StorageVolume - usedStorageVolume,
                UsedCoreCount = usedCoreCount,
                UsedRAMVolume = usedRAMVolume,
                UsedStorageVolume = usedStorageVolume
            };
        }

        /// <summary>
        /// Получение информации о доступных/используемых ресурсов выбранного хранилища данных
        /// </summary>
        /// <param name="selectedSAN">Выбранное хранилище данных</param>
        /// <returns>Информация о доступных/используемых ресурсов выбранного хранилища данных</returns>
        public SANPaasInfo GetSANPaasInfoBySAN(SAN selectedSAN)
        {
            var sInfo = GetSanInfo();
            var sanInfo = sInfo.Single(si => si.ID == selectedSAN.Id);
            var servicesBySAN = GetServicesBySAN(selectedSAN);

            var usedStorageVolume = (servicesBySAN.Sum(sv => sv.HDDVolume.Value));
            return new SANPaasInfo
            {
                Id = selectedSAN.Id,
                Manufacturer = sanInfo.Manufacturer,
                Model = sanInfo.Model,
                AvalibleVolume = sanInfo.Volume - usedStorageVolume,
                UsedVolume = usedStorageVolume
            };
        }

        /// <summary>
        /// Проверка дублирования наименования платформы
        /// </summary>
        /// <param name="name">Наименование платформы</param>
        /// <returns>Наличие платформы с таким наименованием</returns>
        public async Task<bool> CheckPaasForDublicate(string name)
        {
            return await _ctx.PaasType.SingleOrDefaultAsync(s => s.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        /// <summary>
        /// Получение ссылки на экземпляр сервера по его порядковому номеру
        /// </summary>
        /// <param name="id">Порядковый номер сервера</param>
        /// <returns>Ссылка на экземпляр сервера</returns>
        public async Task<Server> GetServerByID(int id)
        {
            return await _ctx.Server.SingleAsync(s => s.Id == id);
        }

        /// <summary>
        /// Получение ссылки на экземпляр хранилища данных по его порядковому номеру
        /// </summary>
        /// <param name="id">Порядковый номер сервера</param>
        /// <returns>Ссылка на экземпляр хранилища данных</returns>
        public async Task<SAN> GetSANByID(int id)
        {
            return await _ctx.SAN.SingleAsync(s => s.Id == id);
        }

        /// <summary>
        /// Сохранение новой платформы
        /// </summary>
        /// <param name="newPaas">Новая платформа</param>
        public async Task AddNewPaas(PaasType newPaas)
        {
            _ctx.PaasType.Add(newPaas);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Сохранение изменений в редактируемой платформе
        /// </summary>
        /// <param name="editedPaas">Редактируемая платформа</param>
        public async Task EditPaas(PaasType editedPaas)
        {
            _ctx.PaasType.Attach(editedPaas);
            _ctx.Entry(editedPaas).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение ссылки на экземпляр платформы по ее подробному описанию
        /// </summary>
        /// <param name="selectedPlatform">Выбранное подробное описание платформы</param>
        /// <returns>Ссылка на экземпляр платформы </returns>
        public async Task<PaasType> GetPaasFromPaasInfo(PaasInfo selectedPlatform)
        {
            return await _ctx.PaasType
                    .Include(pt => pt.Service)
                .SingleAsync(pt => pt.Name.Equals(selectedPlatform.Name));
        }

        #endregion

        #region EditService

        /// <summary>
        /// Получение списка типов платформ
        /// </summary>
        /// <returns>Cписок типов платформ</returns>
        public BindingList<PaasType> GetPaasTypes()
        {
            var lst = _ctx.PaasType.Where(pt => pt.Server.Count > 0 && pt.SAN.Count > 0).ToList();
            return new BindingList<PaasType>(lst);
        }

        /// <summary>
        /// Получение списка типов сервиса
        /// </summary>
        /// <returns>Список типов сервиса</returns>
        public BindingList<ServiceType> GetServiceTypes()
        {
            var lst = _ctx.ServiceType.ToList();
            return new BindingList<ServiceType>(lst);
        }

        /// <summary>
        /// Получение списка серверов выбранной платформы
        /// </summary>
        /// <param name="currentPaasType">Выбранная платформа</param>
        /// <returns>Список серверов выбранной платформы</returns>
        public BindingList<PlatformServerItem> GetServersByPlatform(PaasType currentPaasType)
        {
            var psi = new List<PlatformServerItem>();
            _ctx.Server.ToList().Where(s => s.PaasType.ToList().Exists(p => p.Id == currentPaasType.Id)).ToList()
                .ForEach(x => psi.Add(new PlatformServerItem(x)));
            return new BindingList<PlatformServerItem>(psi);
        }

        /// <summary>
        /// Получение списка хранилищ данных выбранной платформы
        /// </summary>
        /// <param name="currentPaasType">Выбранная платформа</param>
        /// <returns>Список хранилищ данных выбранной платформы</returns>
        public BindingList<PlatfromSANItem> GetSANsByPlatform(PaasType currentPaasType)
        {
            var psi = new List<PlatfromSANItem>();
            _ctx.SAN.ToList().Where(s => s.PaasType.ToList().Exists(p => p.Id == currentPaasType.Id)).ToList().ForEach(x => psi.Add(new PlatfromSANItem(x)));
            return new BindingList<PlatfromSANItem>(psi);
        }

        /// <summary>
        /// Получение детальной информации о сервере по его порядковому номеру
        /// </summary>
        /// <param name="id">Порядковый номер сервера</param>
        /// <returns>Детальная информация о сервере</returns>
        public Server GetServerDetailsByID(int id)
        {
            return _ctx.Server
                .Include(cp => cp.CPU)
                .Include(pl => pl.Platform)
                .Include(ss => ss.Server_Storage)
                .Include(sr => sr.Server_RAM)
                .Include(srv => srv.PaasType)
                .Single(s => s.Id == id);
        }

        /// <summary>
        /// Получение детальной информации о хранилище данных по его порядковому номеру
        /// </summary>
        /// <param name="id">Порядковый номер хранилища данных</param>
        /// <returns>Детальная информация о хранилище данных</returns>
        public SAN GetSANDetailsByID(int id)
        {
            return _ctx.SAN
                .Include(s => s.SAN_Storage)
                .Single(s => s.Id == id);
        }

        /// <summary>
        /// Получение информации о доступных ресурсах сервера
        /// </summary>
        /// <param name="selectedServer">Исходный экземпляр сервера</param>
        /// <returns>Информация о доступных ресурсах сервера</returns>
        public ServerPlarformInfo GetServerPlatformInfoByServer(Server selectedServer)
        {
            var servicesByServer = GetServicesByServer(selectedServer);

            var usedCoreCount = servicesByServer.Sum(cp => cp.CoreCount.Value);
            var usedRAMVolume = servicesByServer.Sum(rv => rv.RamCount.Value);

            return new ServerPlarformInfo
            {
                SumFrequency = (selectedServer.CPU.Frequency * selectedServer.Platform.CPUCount) * selectedServer.CPU.CoreCount,
                AvalibleCoreCount = (selectedServer.CPU.CoreCount * selectedServer.Platform.CPUCount) - usedCoreCount,
                CoreCount = selectedServer.CPU.CoreCount * selectedServer.Platform.CPUCount,
                AvalibleRAMVolume = selectedServer.Server_RAM.Sum(sr => sr.RAM.Volume * sr.Count) - usedRAMVolume,
                FrequencyPerCore = selectedServer.CPU.Frequency,
                SumRAMVolume = selectedServer.Server_RAM.Sum(sr => sr.RAM.Volume * sr.Count)
            };
        }

        /// <summary>
        /// Получение информации о доступных ресурсах хранилища данных
        /// </summary>
        /// <param name="selectedSAN">Исходный экземпляр хранилища данных</param>
        /// <returns>Информация о доступных ресурсах хранилища данных</returns>
        public SANPlatformInfo GetSanPlatformInfoBySAN(SAN selectedSAN)
        {
            var servicesBySAN = GetServicesBySAN(selectedSAN);

            var usedStorageVolume = (servicesBySAN.Sum(sv => sv.HDDVolume.Value));

            return new SANPlatformInfo
            {
                SummaryVolume = selectedSAN.SAN_Storage.Sum(ss => ss.Strorage.Volume * ss.Count.Value),
                AvalibleVolume = selectedSAN.SAN_Storage.Sum(ss => ss.Strorage.Volume * ss.Count.Value) - usedStorageVolume
            };
        }

        /// <summary>
        /// Создание нового сервиса
        /// </summary>
        /// <param name="newService">Экземпляр нового сервиса</param>
        public async Task AddNewService(Service newService)
        {
            newService.CreateDate = DateTime.Now;
            newService.ServiceState = await GetServiceStateByName("Отправлена заявка");
            _ctx.Service.Add(newService);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование выбранного сервиса
        /// </summary>
        /// <param name="editedService">Экземпляр выбранного сервиса</param>
        public async Task EditService(Service editedService)
        {
            editedService.ServiceState = await GetServiceStateByName("Неактивен");
            _ctx.ServiceIdle.Add(new ServiceIdle
            {
                Date = DateTime.Now,
                BeginPeriod = DateTime.Now,
                EndPeriod = DateTime.Now.AddDays(1),
                Service = editedService,
                IdleType = await GetIdleTypeByName("Внеплановый"),
                IdleReason = await GetIdleReasonByName("Обновление конфигурации")
            });
            _ctx.Service.Attach(editedService);
            _ctx.Entry(editedService).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Получение экземпляра состояния сервиса по его наименованию
        /// </summary>
        /// <param name="name">Наименование состояния сервиса</param>
        /// <returns>Экземпляр состояния сервиса</returns>
        private async Task<ServiceState> GetServiceStateByName(string name)
        {
            return await _ctx.ServiceState.SingleAsync(ss => ss.Name.Equals(name));
        }

        /// <summary>
        /// Получение экземпляра типа простоя сервиса по его наименованию
        /// </summary>
        /// <param name="name">Наименование типа простоя сервиса</param>
        /// <returns>Экземпляр типа простоя сервиса</returns>
        private async Task<IdleType> GetIdleTypeByName(string name)
        {
            return await _ctx.IdleType.SingleAsync(it => it.Name.Equals(name));
        }

        /// <summary>
        /// Получение экземпляра причины простоя сервиса по его наименованию
        /// </summary>
        /// <param name="name">Наименование причины простоя сервиса</param>
        /// <returns>Экземпляр причины простоя сервиса</returns>
        private async Task<IdleReason> GetIdleReasonByName(string name)
        {
            return await _ctx.IdleReason.SingleAsync(ir => ir.Name.Equals(name));
        }

        /// <summary>
        /// Получение экземляра программного обеспечения по его ID
        /// </summary>
        /// <param name="id">ID программного обеспечения</param>
        /// <returns>Экземляр программного обеспечения</returns>
        public async Task<Software> GetSoftwareById(int id)
        {
            return await _ctx.Software.SingleAsync(s => s.Id == id);
        }

        #endregion

        #region SelectSoftware

        /// <summary>
        /// Получение списка программного обеспечения с подробным описанием
        /// </summary>
        /// <returns>Список программного обеспечения с подробным описанием</returns>
        public BindingList<SoftwareInfo> GetSoftwaresInfo()
        {
            var soft = from software in _ctx.Software
                       select new SoftwareInfo
                       {
                           Id = software.Id,
                           Software = software.Name,
                           SoftType = software.SoftType.Name,
                           Cost = software.Cost
                       };
            return new BindingList<SoftwareInfo>(soft.ToList());
        }

        /// <summary>
        /// Получение списка типов программного обеспечения
        /// </summary>
        /// <returns>Список типов программного обеспечения</returns>
        public BindingList<SoftType> GetSoftwareTypes()
        {
            return new BindingList<SoftType>(_ctx.SoftType.ToList());
        }

        /// <summary>
        /// Получение программного обеспечения по его информации
        /// </summary>
        /// <param name="selectedItem">Информация о программном обеспечении</param>
        /// <returns>Экземпляр программного обеспечения</returns>
        public Software GetSoftwareByInfo(SoftwareInfo selectedItem)
        {
            return _ctx.Software.Single(cs =>
               cs.Name.Equals(selectedItem.Software) && cs.SoftType.Name.Equals(selectedItem.SoftType));
        }

        /// <summary>
        /// Удаление выбранного программного обеспечения
        /// </summary>
        /// <param name="softwareToDel">Программное обеспечение, которую следует удалить</param>
        public async Task DeleteSoftware(Software softwareToDel)
        {
            _ctx.Software.Remove(softwareToDel);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Проверка дублирования программного обеспечения
        /// </summary>
        /// <param name="name">Наименование программного обеспечения</param>
        /// <param name="softTypeName">Наименование типа программного обеспечения</param>
        /// <returns>Наличие программного обеспечения с таким наименованием и типом</returns>
        public async Task<bool> CheckSoftwareForDublicate(string name, string softTypeName)
        {
            return await _ctx.Software.SingleOrDefaultAsync(cs =>
                       cs.Name.ToUpper().Equals(name.ToUpper()) && cs.SoftType.Name.Equals(softTypeName)) != null;
        }

        /// <summary>
        /// Добавление нового программного обеспечения
        /// </summary>
        /// <param name="newSoftware">Новое программное обеспечение</param>
        public async Task AddNewSoftware(Software newSoftware)
        {
            _ctx.Software.Add(newSoftware);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Редактирование существующего программного обеспечения
        /// </summary>
        /// <param name="editedSoftware">Существующее программное обеспечение</param>
        public async Task EditSoftware(Software editedSoftware)
        {
            _ctx.Software.Attach(editedSoftware);
            _ctx.Entry(editedSoftware).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region SoftTypes

        /// <summary>
        /// Получение списка типов программного обеспечения с привязкой
        /// </summary>
        /// <returns>Список типов программного обеспечения</returns>
        public BindingList<SoftType> GetSoftTypesBS()
        {
            _ctx.SoftType.Load();
            return _ctx.SoftType.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования наименования типа программного обеспечения
        /// </summary>
        /// <param name="name">Наименование типа программного обеспечения</param>
        /// <returns>Наличие типа программного обеспечения с таким наименованием</returns>
        public async Task<bool> CheckSoftTypeForDublicate(string name)
        {
            return await _ctx.SoftType.SingleOrDefaultAsync(m => m.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        #endregion

        #region ServiceTypes

        /// <summary>
        /// Получение списка типов сервиса с привязкой
        /// </summary>
        /// <returns>Список типов сервиса</returns>
        public BindingList<ServiceType> GetServiceTypesBS()
        {
            _ctx.ServiceType.Load();
            return _ctx.ServiceType.Local.ToBindingList();
        }

        /// <summary>
        /// Проверка дублирования наименования типа сервиса
        /// </summary>
        /// <param name="name">Наименование типа сервиса</param>
        /// <returns>Наличие типа сервиса с таким наименованием</returns>
        public async Task<bool> CheckServiceTypeForDublicate(string name)
        {
            return await _ctx.ServiceType.SingleOrDefaultAsync(m => m.Name.ToUpper().Equals(name.ToUpper())) != null;
        }

        #endregion

        #region ListOfServices

        /// <summary>
        /// Получение списка платформ с информацией о количестве пользователей и сервисов
        /// </summary>
        /// <returns>Список платформ с информацией о количестве пользователей и сервисов</returns>
        public BindingList<PlatformServiceUser> GetPlatformServiceUsers()
        {
            var qry = from paasType in _ctx.PaasType
                      select new PlatformServiceUser
                      {
                          Name = paasType.Name,
                          ServiceCount = paasType.Service.Count,
                          UserCount = paasType.Service.Count > 0 ? paasType.Service.Sum(uc => uc.User.Count) : 0
                      };
            return new BindingList<PlatformServiceUser>(qry.ToList());
        }

        /// <summary>
        /// Получение полного списка конфигураций сервисов с детальным описанием
        /// </summary>
        /// <returns>Полный список конфигураций сервисов с детальным описанием</returns>
        public BindingList<ServiceDetailInfo> GetServicesDetailInfo()
        {
            var qry = from service in _ctx.Service
                      select new ServiceDetailInfo
                      {
                          ID = service.Id,
                          PaasType = service.PaasType.Name,
                          ServiceType = service.ServiceType.Name,
                          ServiceState = service.ServiceState.Name,
                          UserCount = service.User.Count,
                          CostPerHour = service.CostPerHour ?? 0,
                          Create_Date = service.CreateDate,
                          Delete_Date = service.DeleteDate
                      };

            return new BindingList<ServiceDetailInfo>(qry.ToList());
        }

        /// <summary>
        /// Получение полного списка конфигураций сервисов с детальным описанием
        /// </summary>
        /// <returns>Полный список конфигураций сервисов с детальным описанием</returns>
        public BindingList<ServiceDetailInfo> GetServicesCost()
        {
            var qry = from service in _ctx.Service
                      where !service.ServiceState.Name.Equals("Отклонен") && !service.ServiceState.Name.Equals("Отправлена заявка")
                      select new ServiceDetailInfo
                      {
                          ID = service.Id,
                          PaasType = service.PaasType.Name,
                          ServiceType = service.ServiceType.Name,
                          ServiceState = service.ServiceState.Name,
                          UserCount = service.User.Count,
                          CostPerHour = service.CostPerHour ?? 0,
                          Create_Date = service.CreateDate
                      };

            return new BindingList<ServiceDetailInfo>(qry.ToList());
        }

        /// <summary>
        /// Получение экземпляра платформы по ее подробной информации
        /// </summary>
        /// <param name="platformServiceUser">Подробная информация о платформе</param>
        /// <returns>Экземпляр платформы</returns>
        public async Task<PaasType> GetPlatformByPlatformServiceUser(PlatformServiceUser platformServiceUser)
        {
            return await _ctx.PaasType.SingleAsync(p => p.Name.Equals(platformServiceUser.Name));
        }

        /// <summary>
        /// Получение экземпляра сервиса по ее подробной информации
        /// </summary>
        /// <param name="serviceDetailInfo">Подробная информация о сервисе</param>
        /// <returns>Экземпляр сервиса</returns>
        public async Task<Service> GetServiceByServiceDetailInfo(ServiceDetailInfo serviceDetailInfo)
        {
            return await _ctx.Service.SingleAsync(s => s.Id == serviceDetailInfo.ID);
        }

        /// <summary>
        /// Удаление выбранного сервиса
        /// </summary>
        /// <param name="serviceToDel">Сервис, которую следует удалить</param>
        public async Task DeleteService(Service serviceToDel)
        {
            _ctx.Service.Remove(serviceToDel);
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region EquipmentIdleMenu

        /// <summary>
        /// Получение списка с информацией о простое серверов
        /// </summary>
        /// <returns>Список с информацией о простое серверов</returns>
        public BindingList<ServerIdleItem> GetServerIdleItems()
        {

            var qry = from server in _ctx.Server
                      from service in _ctx.Service
                      join serviceIdle in _ctx.ServiceIdle on service.Id equals serviceIdle.ServiceId into si
                      from subServ in si.DefaultIfEmpty()
                      join platform in _ctx.Platform on server.PlatformId equals platform.Id
                      join cpu in _ctx.CPU on server.CPU_Id equals cpu.Id
                      where server.PaasType.Contains(service.PaasType) || subServ == null
                      select new ServerIdleItem
                      {
                          ID = server.Id,
                          Plarform = platform.Manufacturer.Name + " " + platform.Model,
                          CPU = cpu.Manufacturer.Name + " " + cpu.Model + " x" + platform.CPUCount,
                          RAM = server.Server_RAM.Sum(sr => sr.RAM.Volume * sr.Count),
                          OnMaintenance = server.OnMaintenance,
                          Active = server.Active,
                          CompleteIdleCount = si.Count(s => s.EndPeriod <= DateTime.Now),
                          SheduledIdleCount = server.Server_MaintenanceShedule.Count(sms => sms.BeginPeriod >= DateTime.Now)
                      };

            var xx = qry.ToList().GroupBy(q => q.ID)
                .Select(q => q.First())
                .ToList();

            return new BindingList<ServerIdleItem>(xx);
        }

        /// <summary>
        /// Получение списка с информацией о простое хранилищ данных
        /// </summary>
        /// <returns>Список с информацией о простое хранилищ данных</returns>
        public BindingList<SANIdleItem> GetSanIdleItems()
        {
            var qry = from san in _ctx.SAN
                      from service in _ctx.Service
                      join serviceIdle in _ctx.ServiceIdle on service.Id equals serviceIdle.ServiceId into si
                      from subServ in si.DefaultIfEmpty()
                      where san.PaasType.Contains(service.PaasType) || subServ == null
                      select new SANIdleItem
                      {
                          ID = san.Id,
                          Manufacturer = san.Manufacturer.Name,
                          Model = san.Model,
                          StorageCount = san.SAN_Storage.Sum(ss => ss.Count.Value),
                          StorageVolume = san.SAN_Storage.Sum(ss => ss.Count.Value * ss.Strorage.Volume),
                          OnMaintenance = san.OnMaintenance,
                          Active = san.Active,
                          CompleteIdleCount = si.Count(s => s.EndPeriod <= DateTime.Now),
                          SheduledIdleCount = san.SAN_MaintenanceShedule.Count(sms => sms.BeginPeriod >= DateTime.Now)
                      };

            var xx = qry.ToList().GroupBy(q => q.ID)
                .Select(q => q.First())
                .ToList();

            return new BindingList<SANIdleItem>(xx);
        }

        /// <summary>
        /// Получение списка с информацией о простое сервисов
        /// </summary>
        /// <returns>Список с информацией о простое сервисов</returns>
        public BindingList<ServiceIdleItem> GetServiceIdleItems()
        {
            var qry = from service in _ctx.Service
                      from server in _ctx.Server
                      from san in _ctx.SAN
                      where san.PaasType.Contains(service.PaasType) && server.PaasType.Contains(service.PaasType)
                      select new ServiceIdleItem
                      {
                          ID = service.Id,
                          Name = service.ServiceType.Name,
                          PaasTypeName = service.PaasType.Name,
                          CurrentState = service.ServiceState.Name,
                          UsersCount = service.User.Count,
                          CompleteIdleCount = san.SAN_MaintenanceShedule.Count(sms => sms.EndPeriod <= DateTime.Now) + server.Server_MaintenanceShedule.Count(sms => sms.EndPeriod <= DateTime.Now),
                          SheduledIdleCount = san.SAN_MaintenanceShedule.Count(sms => sms.BeginPeriod >= DateTime.Now) + server.Server_MaintenanceShedule.Count(sms => sms.BeginPeriod >= DateTime.Now)
                      };

            return new BindingList<ServiceIdleItem>(qry.ToList());
        }

        #endregion

        #region EditScheduleItem

        /// <summary>
        /// Добавление новой позиции в расписании обслуживания сервера
        /// </summary>
        /// <param name="serverMaintenanceShedule">Новая позиции в расписании обслуживания сервера</param>
        public async Task AddNewServerSchedulePosition(Server_MaintenanceShedule serverMaintenanceShedule)
        {
            _ctx.Server_MaintenanceShedule.Add(serverMaintenanceShedule);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Добавление новой позиции в расписании обслуживания хранилища данных
        /// </summary>
        /// <param name="sanMaintenanceShedule">Новая позиции в расписании обслуживания хранилища данных</param>
        public async Task AddNewSANSchedulePosition(SAN_MaintenanceShedule sanMaintenanceShedule)
        {
            _ctx.SAN_MaintenanceShedule.Add(sanMaintenanceShedule);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Изменение позиции в расписании обслуживания сервера
        /// </summary>
        /// <param name="serverMaintenanceShedule">Измененная позиция в расписании обслуживания сервера</param>
        public async Task EditServerSchedulePosition(Server_MaintenanceShedule serverMaintenanceShedule)
        {
            _ctx.Server_MaintenanceShedule.Attach(serverMaintenanceShedule);
            _ctx.Entry(serverMaintenanceShedule).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Изменений позиции в расписании обслуживания хранилища данных
        /// </summary>
        /// <param name="sanMaintenanceShedule">Измененная позиция в расписании обслуживания хранилища данных</param>
        public async Task EditSANSchedulePosition(SAN_MaintenanceShedule sanMaintenanceShedule)
        {
            _ctx.SAN_MaintenanceShedule.Attach(sanMaintenanceShedule);
            _ctx.Entry(sanMaintenanceShedule).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Проверка уже запланированных позиций обсулживания в установленном диапазоне дат
        /// </summary>
        /// <param name="positionId">Номер редактируемой записи</param>
        /// <param name="id">Код выбранного сервера или хранилища данных</param>
        /// <param name="isServer">Флаг, который укзаывает какую таблицу проверять</param>
        /// <param name="beginDateTime">Дата начала обсулживания</param>
        /// <param name="endDateTime">Дата окончания обсулживания</param>
        public async Task<bool> CheckMaintenanceDateRange(int positionId, int id, bool isServer, DateTime beginDateTime, DateTime endDateTime)
        {
            if (isServer)
            {
                if (positionId < 0)
                    return await _ctx.Server_MaintenanceShedule
                        .AnyAsync(sms => sms.ServerID == id && sms.BeginPeriod >= beginDateTime && sms.EndPeriod <= endDateTime);
                else
                    return await _ctx.Server_MaintenanceShedule
                        .AnyAsync(sms => sms.ServerID == id && sms.BeginPeriod >= beginDateTime && sms.EndPeriod <= endDateTime && sms.Id != positionId);

            }
            else
            {
                if (positionId < 0)
                    return await _ctx.SAN_MaintenanceShedule
                        .AnyAsync(sms => sms.SAN_Id == id && sms.BeginPeriod >= beginDateTime && sms.EndPeriod <= endDateTime);
                else
                    return await _ctx.SAN_MaintenanceShedule
                        .AnyAsync(sms => sms.SAN_Id == id && sms.BeginPeriod >= beginDateTime && sms.EndPeriod <= endDateTime && sms.Id != positionId);
            }
        }

        #endregion

        #region PlanningSchedule

        /// <summary>
        /// Получение списка позиций обслуживания для выбранного сервера
        /// </summary>
        /// <param name="selectedServer">Выбранный сервер</param>
        /// <returns>Список позиций обсулуживания для выбранного сервера</returns>
        public BindingList<ScheduleItem> GetScheduleItemsOfServer(Server selectedServer)
        {
            var serverMaintenanceShedules = _ctx.Server_MaintenanceShedule.Where(sms => sms.ServerID == selectedServer.Id).ToList();
            var scheduleItems = new List<ScheduleItem>();

            serverMaintenanceShedules.ForEach(sms => scheduleItems.Add(new ScheduleItem
            {
                Id = sms.Id,
                BeginDate = sms.BeginPeriod,
                EndDate = sms.EndPeriod,
                Duration = (sms.EndPeriod - sms.BeginPeriod).TotalMinutes.ToString()
            }));

            return new BindingList<ScheduleItem>(scheduleItems);
        }

        /// <summary>
        /// Получение списка позиций обслуживания для выбранного хранилища данных
        /// </summary>
        /// <param name="selectedSAN">Выбранное хранилище данных</param>
        /// <returns>Список позиций обсулуживания для выбранного хранилища данных</returns>
        public BindingList<ScheduleItem> GetScheduleItemsOfSAN(SAN selectedSAN)
        {
            var serverMaintenanceShedules = _ctx.SAN_MaintenanceShedule.Where(sms => sms.SAN_Id == selectedSAN.Id).ToList();
            var scheduleItems = new List<ScheduleItem>();

            serverMaintenanceShedules.ForEach(sms => scheduleItems.Add(new ScheduleItem
            {
                Id = sms.Id,
                BeginDate = sms.BeginPeriod,
                EndDate = sms.EndPeriod,
                Duration = (sms.EndPeriod - sms.BeginPeriod).TotalMinutes.ToString()
            }));

            return new BindingList<ScheduleItem>(scheduleItems);
        }

        /// <summary>
        /// Полулчение ссылки на экземпляр позиции расписания обслуживания сервера
        /// </summary>
        /// <param name="id">Код позиции</param>
        /// <returns>Ссылка на экземпляр позиции расписания обслуживания сервера</returns>
        public async Task<Server_MaintenanceShedule> GetServerMaintenanceSheduleByID(int id)
        {
            return await _ctx.Server_MaintenanceShedule.SingleAsync(sms => sms.Id == id);
        }

        /// <summary>
        /// Полулчение ссылки на экземпляр позиции расписания обслуживания хранилища данных
        /// </summary>
        /// <param name="id">Код позиции</param>
        /// <returns>Ссылка на экземпляр позиции расписания обслуживания хранилища данных</returns>
        public async Task<SAN_MaintenanceShedule> GetSANMaintenanceSheduleByID(int id)
        {
            return await _ctx.SAN_MaintenanceShedule.SingleAsync(sms => sms.Id == id);
        }

        /// <summary>
        /// Удаление выбранной позиции расписания обслуживания сервера
        /// </summary>
        /// <param name="selectedServerMaintenanceShedule">Удаляемая позиция</param>
        public async Task DeleteSelectedServiceScheduleMaintenance(Server_MaintenanceShedule selectedServerMaintenanceShedule)
        {
            _ctx.Server_MaintenanceShedule.Remove(selectedServerMaintenanceShedule);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление выбранной позиции расписания обслуживания хранилища данных
        /// </summary>
        /// <param name="selectedServerMaintenanceShedule">Удаляемая позиция</param>
        public async Task DeleteSelectedSANScheduleMaintenance(SAN_MaintenanceShedule selectedSanMaintenanceShedule)
        {
            _ctx.SAN_MaintenanceShedule.Remove(selectedSanMaintenanceShedule);
            await _ctx.SaveChangesAsync();
        }

        #endregion

        #region RegisterNewIdle

        /// <summary>
        /// Получения списка пользователей, которые относятся к администраторам
        /// </summary>
        /// <returns>Список пользователей, которые относятся к администраторам</returns>
        public List<User> GetAdminsList()
        {
            return _ctx.User.Where(usr => usr.RightsLevel.Name.Equals("Системный администратор")).ToList();
        }

        /// <summary>
        /// Получение списка типов простоя
        /// </summary>
        /// <returns>Список типов простоя</returns>
        public BindingList<IdleType> GetIdleTypes()
        {
            return new BindingList<IdleType>(_ctx.IdleType.ToList());
        }

        /// <summary>
        /// Получение списка причин простоя
        /// </summary>
        /// <returns>Список причин простоя</returns>
        public BindingList<IdleReason> GetIdleReasons()
        {
            return new BindingList<IdleReason>(_ctx.IdleReason.ToList());
        }

        /// <summary>
        /// Регистрация нового простоя сервисов
        /// </summary>
        /// <param name="services">Список сервисов на простой</param>
        /// <param name="usedPersonal">Задействованный персонал</param>
        /// <param name="beginDateTime">Дата начала простоя</param>
        /// <param name="endDateTime">Дата окончания простоя</param>
        /// <param name="selectedIdleType">Тип простоя</param>
        /// <param name="selectedIdleReason">Причина простоя</param>
        public void RegisterNewIdle(List<Service> services, List<User> usedPersonal, DateTime beginDateTime, DateTime endDateTime, IdleType selectedIdleType, IdleReason selectedIdleReason)
        {
            var qry = from sb in _ctx.Service.AsEnumerable()
                      where services.Exists(srv => srv.Id == sb.Id) && sb.CreateDate.Value < beginDateTime
                        && !(sb.DeleteDate != null && sb.DeleteDate <= endDateTime)
                      select new ServiceIdle
                      {
                          ServiceId = sb.Id,
                          Date = DateTime.Now,
                          BeginPeriod = beginDateTime,
                          EndPeriod = endDateTime,
                          IdleType = selectedIdleType,
                          IdleReason = selectedIdleReason,
                          User = usedPersonal
                      };

            _ctx.ServiceIdle.AddRange(qry.AsEnumerable());
            _ctx.SaveChanges();
        }
        #endregion

        #region DirectorForm


        /// <summary>
        /// Получение списка заявок на создание сервисов
        /// </summary>
        /// <returns>Список заявок на создание сервисов</returns>
        public BindingList<ServiceRequestItem> GetServiceReuqests()
        {
            var qry = from service in _ctx.Service
                      where service.ServiceState.Name.Equals("Отправлена заявка")
                      select new ServiceRequestItem
                      {
                          Id = service.Id,
                          Name = service.ServiceType.Name,
                          PaasTypeName = service.PaasType.Name,
                          CreateDate = service.CreateDate.Value,
                          CostPerHour = service.CostPerHour.Value
                      };

            return new BindingList<ServiceRequestItem>(qry.ToList());
        }

        /// <summary>
        /// Получение экземпляра сервиса по его порядковому номеру
        /// </summary>
        /// <param name="id">Порядковый номер сервиса</param>
        /// <returns>Экземпляр сервиса по его порязковому номеру</returns>
        public Service GetServiceByID(int id)
        {
            return _ctx.Service.Single(s => s.Id == id);
        }

        /// <summary>
        /// Получение списка с информацией о простое сервиса
        /// </summary>
        /// <returns>Список с информацией о простое сервиса</returns>
        public BindingList<IdleItem> GetIdleItems()
        {
            var idleItems = _ctx.ServiceIdle
                .Include(si => si.Service)
                .Include(si => si.Service.ServiceType)
                .Include(si => si.Service.PaasType)
                .AsEnumerable();

            var qry = from serviceIdle in idleItems
                      select new IdleItem
                      {
                          Id = serviceIdle.Id,
                          ServiceName = serviceIdle.Service.ServiceType.Name,
                          PaasName = serviceIdle.Service.PaasType.Name,
                          BeginDate = serviceIdle.BeginPeriod,
                          EndDate = serviceIdle.EndPeriod,
                          Duration = (serviceIdle.EndPeriod - serviceIdle.BeginPeriod).TotalMinutes.ToString(),
                          Cost = serviceIdle.Cost != null ? serviceIdle.Cost.Value.ToString("F") : "Не расчитана"
                      };
            return new BindingList<IdleItem>(qry.ToList());
        }

        #endregion

        #region Расчет стоимости простоя

        /// <summary>
        /// Фоновый расчет стоимости простоя сервисов
        /// </summary>
        public void CalculateServicesIdleBackground()
        {
            // Нерасчитанные экземпляры простоя сервисов
            var idlesToCalc = _ctx.ServiceIdle.Where(si => si.Cost == null).ToList();
            if (idlesToCalc.Count == 0)
                return;

            // Годовой доход компанни
            CostIdleParams idleParams = null;
            // Минут в году
            int minutesInYear = 525600;

            try
            {
                // Попытка загрузки из файла
                idleParams = CostIdleParams.Load();
                // При отсутсвтсвии файла расчет обрывается
                if (idleParams == null)
                    return;
            }
            catch
            {
                // в случае ошибки расчет обрывается
                return;
            }

            // Доход в минуту
            decimal incomeInMinute = idleParams.YearIncome / minutesInYear;
            // Средний доход администратора в минуту
            decimal avgAdminSalaryPerMin = idleParams.AvgAdminSalary / 60;
            // Средний доход работника в минуту
            decimal avgEmployeeSalaryPerMin = idleParams.AvgEmployeeSalary / 60;
            // Доход на каждого работника (руб./мин)
            decimal incomePerEmployee = incomeInMinute / idleParams.TotalEmployeeCount;

            foreach (ServiceIdle serviceIdle in idlesToCalc)
            {
                // Продолжительность (мин.)
                double duration = (serviceIdle.EndPeriod - serviceIdle.BeginPeriod).TotalMinutes;
                // количество пользователей, отключенных при этом
                int disEmployesCount = serviceIdle.Service.User.Count;
                // количество администраторов, задействованных для этого
                int usedAdminsCount = serviceIdle.User.Count;
                // Плановые расходы на администраторов (руб./мин.)
                decimal adminPlannedCost = (decimal)duration * usedAdminsCount * avgAdminSalaryPerMin;
                // Плановые расходы на конечных пользователей (руб./мин.)
                decimal employeePlannedCost = (decimal)duration * disEmployesCount * avgEmployeeSalaryPerMin;
                // Плановые расходы на отключение сервера
                decimal servicePlannedCost = adminPlannedCost + employeePlannedCost;
                // Потерянный доход (руб./мин.)
                decimal brokenIncome = incomePerEmployee * (decimal)duration;

                // Итоговая стоимость простоя сервиса
                serviceIdle.Cost = (servicePlannedCost + brokenIncome);
            }

            idlesToCalc.ForEach(itc => _ctx.ServiceIdle.Single(si => si.Id == itc.Id).Cost = itc.Cost);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Получение экземпляра простоя сервиса по его номеру
        /// </summary>
        /// <param name="id">Номер записи</param>
        /// <returns>Экземпляр простоя сервиса</returns>
        public ServiceIdle GetServiceIdleByID(int id)
        {
            return _ctx.ServiceIdle
                    .Include(si => si.Service)
                    .Include(si => si.Service.ServiceState)
                    .Include(si => si.Service.PaasType)
                    .Include(si => si.User)
                .Single(si => si.Id == id);
        }

        #endregion

        #region ServiceRequestTreatment

        /// <summary>
        /// Отмена заявки на создание сервиса
        /// </summary>
        /// <param name="selectedService">Отклоняемый сервис</param>
        public void CancelServiceRequest(Service selectedService)
        {
            var service = _ctx.Service.Single(s => s.Id == selectedService.Id);
            service.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Отклонен"));
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Принятие заявки на создание сервиса
        /// </summary>
        /// <param name="selectedService">Принемаемый сервис</param>
        public void ConfirmServiceRequest(Service selectedService)
        {
            var service = _ctx.Service.Single(s => s.Id == selectedService.Id);
            service.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Активен"));

            //todo Исправить случай с плановым обсуживанием
            var serversByService = GetServersByService(selectedService);
            var sanByService = GetSANByService(selectedService);

            serversByService.ForEach(s => _ctx.Server.Single(serv => serv.Id == s.Id).Active = true);
            sanByService.ForEach(s => _ctx.SAN.Single(san => san.Id == s.Id).Active = true);

            _ctx.SaveChanges();
        }

        #endregion

        #region EmployeeForm

        /// <summary>
        /// Получение списка используемых сервисов текущим пользователем
        /// </summary>
        /// <returns>Список используемых сервисов текущим пользователем</returns>
        public BindingList<UserService> GetUsedServices()
        {
            var xx = _ctx.Service
                .Include(s => s.User)
                .ToList()
                .Where(s => s.User.Contains(CurrentUser))
                .GroupBy(s => s.ServiceType);

            var x = xx.Select(g => new UserService
            {
                ServiceTypeId = g.Key.Id,
                Name = g.Key.Name,
                PaasName = g.Select(m => m.PaasType.Name).First(),
                State = g.All(s => s.ServiceState.Name.Equals("Неактивен")) ? "Неактивен" : "Активен"
            });

            return new BindingList<UserService>(x.ToList());
        }

        /// <summary>
        /// Отписка от выбранного сервиса
        /// </summary>
        /// <param name="selectedServiceType">Выбранный тип сервиса</param>
        public void UnSubscribeSelectedService(ServiceType selectedServiceType)
        {
            var qry = from service in _ctx.Service.AsEnumerable()
                      where service.User.Contains(CurrentUser) && service.ServiceType.Id == selectedServiceType.Id
                      select service;

            var services = qry.ToList();
            User currentUser = _ctx.User.Single(usr => usr.TabNo == CurrentUser.TabNo);
            services.ForEach(s => currentUser.Service.Remove(s));
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Получение ссылки на экземпляр типа сервиса по его порядковому номеру
        /// </summary>
        /// <param name="serviceTypeId">Порядковый номер сервиса</param>
        /// <returns>Ссылка на экземпляр типа сервиса</returns>
        public ServiceType GetServiceTypeById(int serviceTypeId)
        {
            return _ctx.ServiceType.Single(st => st.Id == serviceTypeId);
        }

        #endregion

        #region SubscribeServicesForm

        /// <summary>
        /// Получение списка доступных типов сервисов
        /// </summary>
        /// <returns></returns>
        public BindingList<ServiceType> GetAvalibleServiceTypes()
        {
            var qry = _ctx.Service
                .Include(s => s.User)
                .ToList()
                .Where(s => !s.User.Contains(CurrentUser))
                .GroupBy(s => s.ServiceType);

            var serviceTypes = new List<ServiceType>();
            qry.ToList().ForEach(st =>
            {
                if (st.All(s => s.ServiceState.Name.Equals("Активен") || s.ServiceState.Name.Equals("Неактивен")))
                    serviceTypes.Add(st.Key);
            });

            return new BindingList<ServiceType>(serviceTypes);
        }

        /// <summary>
        /// Подписка на выбранные сервисы
        /// </summary>
        /// <param name="selectedServiceTypes">Выбранные категории сервисов</param>
        public void SubscribeToSelectedServices(List<ServiceType> selectedServiceTypes)
        {
            var qry = from service in _ctx.Service.AsEnumerable()
                      where selectedServiceTypes.Exists(z => z.Id == service.ServiceType.Id) &&
                            (service.ServiceState.Name.Equals("Активен") || service.ServiceState.Name.Equals("Неактивен"))
                      select service;

            var services = qry.ToList();
            User currentUser = _ctx.User.Single(usr => usr.TabNo == CurrentUser.TabNo);

            services.ForEach(s => currentUser.Service.Add(s));
            _ctx.SaveChanges();
        }

        #endregion

        #region EditIdleItemForm

        /// <summary>
        /// Получение параметров для расчета стоимости простоя
        /// </summary>
        /// <returns>Параметры для расчета стоимости простоя</returns>
        public CostIdleParams GetIdleParamsFormDatabase()
        {
            var costIdleParams = new CostIdleParams();
            var qryAdmin = from position in _ctx.Position
                           join user in _ctx.User on position.Id equals user.PositionId
                           join rightsLevel in _ctx.RightsLevel on user.RightLevelId equals rightsLevel.Id
                           where rightsLevel.Name.Equals("Системный администратор")
                           select position.AvgSalary;

            var qryEmployee = from position in _ctx.Position
                              join user in _ctx.User on position.Id equals user.PositionId
                              join rightsLevel in _ctx.RightsLevel on user.RightLevelId equals rightsLevel.Id
                              where rightsLevel.Name.Equals("Сотрудник")
                              select position.AvgSalary;

            var adm = qryAdmin.ToList();
            var empl = qryEmployee.ToList();

            costIdleParams.AvgAdminSalary = adm.Count > 0 ? adm.Average(x => x / 720) : 0;
            costIdleParams.AvgEmployeeSalary = empl.Count > 0 ? empl.Average(x => x / 720) : 0;
            costIdleParams.TotalEmployeeCount = _ctx.User.Count();

            return costIdleParams;
        }

        /// <summary>
        /// Обновление стоимости простоя сервиса
        /// </summary>
        /// <param name="serviceIdle">Обновленный экземпляр простоя сервиса</param>
        public void UpdateServiceIdle(ServiceIdle serviceIdle)
        {
            _ctx.ServiceIdle.Attach(serviceIdle);
            _ctx.Entry(serviceIdle).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        #endregion

        #region Timer

        /// <summary>
        /// Проверка обслуживания конфигураций серверов
        /// </summary>
        public void CheckServerMaintenance()
        {
            #region Перевод на облуживание

            var activeTasks = _ctx.Server_MaintenanceShedule
                .Include(sms => sms.Server)
                .Where(sms => sms.BeginPeriod <= DateTime.Now && sms.EndPeriod > DateTime.Now)
                .ToList();

            foreach (Server_MaintenanceShedule activeTask in activeTasks)
            {
                var ctxServer = _ctx.Server.Single(s => s.Id == activeTask.ServerID);
                ctxServer.Active = false;
                ctxServer.OnMaintenance = true;
                _ctx.SaveChanges();

                List<Service> services = GetServicesByServer(activeTask.Server);

                foreach (Service service in services)
                {
                    List<ServiceIdle> serviceIdles = service.ServiceIdle
                        .Where(s => s.BeginPeriod >= DateTime.Now && s.EndPeriod < DateTime.Now
                                                                  && s.IdleType.Name.Equals("Плановый")
                                                                  && s.IdleReason.Name.Equals("Плановое обслуживание"))
                        .ToList();
                    if (serviceIdles.Count == 0 && !(service.ServiceState.Name.Equals("Отправлена заявка") || service.ServiceState.Name.Equals("Отклонен")))
                    {
                        var ctxService = _ctx.Service.Single(s => s.Id == service.Id);
                        ctxService.ServiceIdle.Add(new ServiceIdle
                        {
                            BeginPeriod = activeTask.BeginPeriod,
                            EndPeriod = activeTask.EndPeriod,
                            Date = DateTime.Now,
                            IdleReason = _ctx.IdleReason.Single(ir => ir.Name.Equals("Плановое обслуживание")),
                            IdleType = _ctx.IdleType.Single(it => it.Name.Equals("Плановый"))
                        });
                        ctxService.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Неактивен"));
                        _ctx.SaveChanges();
                    }
                }
            }

            #endregion

            #region Вывод с обслуживания

            var completeTasks = _ctx.Server_MaintenanceShedule
                .Include(sms => sms.Server)
                .Where(sms => sms.EndPeriod < DateTime.Now)
                .ToList();

            foreach (Server_MaintenanceShedule activeTask in completeTasks)
            {
                var ctxServer = _ctx.Server.Single(s => s.Id == activeTask.ServerID);
                ctxServer.Active = true;
                ctxServer.OnMaintenance = false;
                _ctx.SaveChanges();

                List<Service> services = GetServicesByServer(activeTask.Server);

                foreach (Service service in services)
                {
                    var ctxService = _ctx.Service.Single(s => s.Id == service.Id);
                    if (!(ctxService.ServiceState.Name.Equals("Отправлена заявка") || ctxService.ServiceState.Name.Equals("Отклонен")))
                    {
                        ctxService.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Активен"));
                        _ctx.SaveChanges();
                    }
                }
            }

            #endregion
        }


        /// <summary>
        /// Проверка обслуживания хранилищ данных
        /// </summary>
        public void CheckSANMaintenance()
        {
            #region Перевод на облуживание

            var activeTasks = _ctx.SAN_MaintenanceShedule
                .Include(sms => sms.SAN)
                .Where(sms => sms.BeginPeriod <= DateTime.Now && sms.EndPeriod > DateTime.Now)
                .ToList();

            foreach (SAN_MaintenanceShedule activeTask in activeTasks)
            {
                var ctxSan = _ctx.SAN.Single(s => s.Id == activeTask.SAN_Id);
                ctxSan.Active = false;
                ctxSan.OnMaintenance = true;
                _ctx.SaveChanges();

                List<Service> services = GetServicesBySAN(activeTask.SAN);

                foreach (Service service in services)
                {
                    List<ServiceIdle> serviceIdles = service.ServiceIdle
                        .Where(s => s.BeginPeriod >= DateTime.Now && s.EndPeriod < DateTime.Now
                                                                  && s.IdleType.Name.Equals("Плановый")
                                                                  && s.IdleReason.Name.Equals("Плановое обслуживание"))
                        .ToList();
                    if (serviceIdles.Count == 0 && !(service.ServiceState.Name.Equals("Отправлена заявка") || service.ServiceState.Name.Equals("Отклонен")))
                    {
                        var ctxService = _ctx.Service.Single(s => s.Id == service.Id);
                        ctxService.ServiceIdle.Add(new ServiceIdle
                        {
                            BeginPeriod = activeTask.BeginPeriod,
                            EndPeriod = activeTask.EndPeriod,
                            Date = DateTime.Now,
                            IdleReason = _ctx.IdleReason.Single(ir => ir.Name.Equals("Плановое обслуживание")),
                            IdleType = _ctx.IdleType.Single(it => it.Name.Equals("Плановый"))
                        });
                        ctxService.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Неактивен"));
                        _ctx.SaveChanges();
                    }
                }
            }

            #endregion

            #region Вывод с обслуживания

            var completeTasks = _ctx.SAN_MaintenanceShedule
                .Include(sms => sms.SAN)
                .Where(sms => sms.EndPeriod < DateTime.Now)
                .ToList();

            foreach (SAN_MaintenanceShedule activeTask in completeTasks)
            {
                var ctxSan = _ctx.SAN.Single(s => s.Id == activeTask.SAN_Id);
                ctxSan.Active = true;
                ctxSan.OnMaintenance = false;
                _ctx.SaveChanges();

                List<Service> services = GetServicesBySAN(activeTask.SAN);

                foreach (Service service in services)
                {
                    var ctxService = _ctx.Service.Single(s => s.Id == service.Id);
                    if (!(ctxService.ServiceState.Name.Equals("Отправлена заявка") || ctxService.ServiceState.Name.Equals("Отклонен")))
                    {
                        ctxService.ServiceState = _ctx.ServiceState.Single(ss => ss.Name.Equals("Активен"));
                        _ctx.SaveChanges();
                    }
                }
            }

            #endregion
        }

        #endregion

        /// <summary>
        /// Получение полного списка процессоров
        /// </summary>
        /// <returns>Список процессоров</returns>
        public BindingList<CPU> GetCPUs()
        {
            var lst = _ctx.CPU
                .Include(c => c.CpuSocket)
                .ToList();
            return new BindingList<CPU>(lst);
        }


        public bool IsDirReg()
        {
            return _ctx.User.Any(u => u.Position.Name.Equals("Генеральный директор"));
        }
    }
}
