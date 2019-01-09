using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Forms;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Parts;

namespace MTF_Services.WinForms
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            bool existed;
            // получаем GIUD приложения
            string guid = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();

            Mutex mutexObj = new Mutex(true, guid, out existed);

            if (!existed)
            {
                MessageBox.Show("Экземпляр приложения уже запущен!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                if (!new Context().TestConnection())
                    throw new Exception("Пустая ссылка на контекст данных.");
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось подключиться к базе данных! Приложение будет закрыто.",
                    "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                Application.Exit();
            }
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        #region Необработанные исключения


        /// <summary>
        /// Обработчик события перехвата необработанного исключения в текущем домене приложений
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                MessageBox.Show("Произошла ошибка во время выполения операции! Обратитесь к системному администратору!" +
                                $"Описание ошибки: {((Exception)e.ExceptionObject).Message}", "Необработанное испключение",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Фатальная ошибка! Приложение будет закрыто!", "Необработанное испключение",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Обработчик события перехвата необработанного исключения в текущем потоке
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = MessageBox.Show("Произошла ошибка во время выполения операции! Обратитесь к системному администратору!" +
                                         $"Описание ошибки: {e.Exception.Message}", "Необработанное испключение",
                    MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Фатальная ошибка! Приложение будет закрыто!", "Необработанное испключение",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Application.Exit();
                }
            }

            if (result == DialogResult.Abort)
                Application.Exit();
        }

        #endregion
    }
}
