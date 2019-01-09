using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MTF_Services.WinForms.Data
{
    /// <summary>
    /// Параметры для расчета простоя сервисов
    /// </summary>
    [Serializable]
    public class CostIdleParams
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private const string fileName = "CostIdleParams.dat";

        /// <summary>
        /// Годовой валовой доход компании
        /// </summary>
        public decimal YearIncome { get; set; }

        /// <summary>
        /// Число работников
        /// </summary>
        public int TotalEmployeeCount { get; set; }

        /// <summary>
        /// Средняя часовая оплата труда администратора
        /// </summary>
        public decimal AvgAdminSalary { get; set; }

        /// <summary>
        /// Средняя часовая оплата труда работника
        /// </summary>
        public decimal AvgEmployeeSalary { get; set; }

        /// <summary>
        /// Сохранение экземпляра параметров для расчета простоя сервисов в файл
        /// </summary>
        /// <param name="instance">Экземляр для сохранения</param>
        public static void Save(CostIdleParams instance)
        {
            var bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create,FileAccess.Write))
                bf.Serialize(fs, instance);
        }

        /// <summary>
        /// Выгрузка экземпляра параметров для расчета простоя сервисов из файла
        /// </summary>
        /// <returns>Выгруженный экземпляр</returns>
        public static CostIdleParams Load()
        {
            if (!File.Exists(fileName))
                return null;

            var bf = new BinaryFormatter();
            CostIdleParams deserializedInstance = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open,FileAccess.Read))
                deserializedInstance = (CostIdleParams) bf.Deserialize(fs);

            return deserializedInstance;
        }
    }
}
