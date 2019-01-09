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
    /// Класс формы выбора хранилища данных
    /// </summary>
    public partial class SelectSANForm : Form
    {
        private readonly List<int> _selectedIDs;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Выбранное хранилище данных
        /// </summary>
        public SANPaasInfo SelectedSANPaasInfo { get; set; }

        /// <summary>
        /// Коллекция хранилищ данных
        /// </summary>
        public BindingList<SANPaasInfo> SANPaasInfo { get; set; }

        /// <summary>
        /// Конструктор формы выбора хранилища данных
        /// </summary>
        public SelectSANForm(List<int> selectedIDs)
        {
            _selectedIDs = selectedIDs;
            InitializeComponent();
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();
            InitCollection();
        }

        /// <summary>
        /// Инициализация привязки коллекции хранилищ данных.
        /// </summary>
        private async void InitCollection()
        {
            SANPaasInfo = new BindingList<SANPaasInfo>();
            var sansList = await _ctx.GetSANsList();
            sansList.ForEach(sl => SANPaasInfo.Add(_ctx.GetSANPaasInfoBySAN(sl)));
            sANPaasInfoBindingSource.DataSource = SANPaasInfo;
            dataGridView1.DataSource = sANPaasInfoBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену выбора хранилища данных, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранного хранилища данных
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (SelectedSANPaasInfo != null)
            {
                if (_selectedIDs.Contains(SelectedSANPaasInfo.Id))
                {
                    MessageBox.Show("Выбранное файловое хранилище уже присутсвует в списке выбранных!",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите хранилище данных из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных конфигураций хранилищ данных
        /// </summary>
        private void sANPaasInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as SANPaasInfo;
            if (selectedItem != null)
                SelectedSANPaasInfo = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void SelectSANForm_KeyDown(object sender, KeyEventArgs e)
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
