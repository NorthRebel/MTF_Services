using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Parts
{
    /// <summary>
    /// Класс формы выбора конфигурации сервера
    /// </summary>
    public partial class SelectServerForm : Form
    {
        private readonly List<int> _selectedIDs;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Выбранный сервер
        /// </summary>
        public ServerPaasInfo SelectedServerPaasInfo { get; set; }

        /// <summary>
        /// Коллекция конфигураций серверов
        /// </summary>
        public BindingList<ServerPaasInfo> ServersPaasInfo { get; set; }

        /// <summary>
        /// Конструктор формы выбора конфигурации сервера
        /// </summary>
        public SelectServerForm(List<int> selectedIDs)
        {
            _selectedIDs = selectedIDs;
            InitializeComponent();
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();
            InitCollection();
        }

        /// <summary>
        /// Инициализация привязки коллекции конфигураций серверов.
        /// </summary>
        private async void InitCollection()
        {
            ServersPaasInfo = new BindingList<ServerPaasInfo>();
            var serverList = await _ctx.GetServerConfigs();
            serverList.ForEach(sl => ServersPaasInfo.Add(_ctx.GetServerPaasInfoByServer(sl)));
            serverPaasInfoBindingSource.DataSource = ServersPaasInfo;
            dataGridView1.DataSource = serverPaasInfoBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену выбора конфигурации сервера, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранной конфигурации сервера
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (SelectedServerPaasInfo != null)
            {
                if (_selectedIDs.Contains(SelectedServerPaasInfo.Id))
                {
                    MessageBox.Show("Выбранная конфигурация сервера уже присутсвует в списке выбранных!",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите сервер из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных конфигураций серверов
        /// </summary>
        private void serverPaasInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServerPaasInfo;
            if (selectedItem != null)
                SelectedServerPaasInfo = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void SelectServerForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btn_OK_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}
