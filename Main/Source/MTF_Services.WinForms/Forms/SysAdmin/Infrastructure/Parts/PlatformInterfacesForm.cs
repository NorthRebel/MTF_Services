using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts
{
    /// <summary>
    /// Класс формы просмотра списка доступных интерфейсов хранения выбранной платформы
    /// </summary>
    public partial class PlatformInterfacesForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Конструктор формы просмотра списка доступных интерфейсов хранения выбранной платформы
        /// </summary>
        /// <param name="getAvalibleInterfacesOfPlarformBs"></param>
        public PlatformInterfacesForm(BindingList<AvalibleInterface> avalibleInterfaces)
        {
            InitializeComponent();
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));

            _ctx = new Context();
            avalibleInterfaceBindingSource.DataSource = avalibleInterfaces;
            dataGridView1.DataSource = avalibleInterfaceBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит закрытие формы
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
