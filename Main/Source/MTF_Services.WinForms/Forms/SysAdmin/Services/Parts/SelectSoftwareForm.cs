using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Parts
{
    /// <summary>
    /// Класс формы выбора программного обеспечения
    /// </summary>
    public partial class SelectSoftwareForm : Form
    {
        private readonly List<int> _selectedIDs;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущee программное обеспечение
        /// </summary>
        public SoftwareInfo CurrentSoftware { get; set; }

        /// <summary>
        /// Выбранное программное обеспечение
        /// </summary>
        public Software SelectedSoftware { get; set; }

        /// <summary>
        /// Флаг отмены выбора программного обеспечения
        /// </summary>
        private bool _canceled;

        /// <summary>
        /// Конструктор формы выбора программного обеспечения
        /// </summary>
        public SelectSoftwareForm(List<int> selectedIDs)
        {
            _selectedIDs = selectedIDs;
            InitializeComponent();
            SubscribeMenuItems();
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            созданиеToolStripMenuItem1.Click += созданиеToolStripMenuItem_Click;
            редактированиеToolStripMenuItem1.Click += редактированиеToolStripMenuItem_Click;
            удалениеToolStripMenuItem1.Click += удалениеToolStripMenuItem_Click;

            _ctx = new Context();
            InitBindings();
        }

        /// <summary>
        /// Инициализация привязок данных
        /// </summary>
        private void InitBindings()
        {
            softwareInfoBindingSource.DataSource = _ctx.GetSoftwaresInfo();
            dataGridView1.DataSource = softwareInfoBindingSource;
            txt_RecCount.Text = $"Общее количество: {softwareInfoBindingSource.Count}";
        }

        /// <summary>
        /// Обработчик события выхода курсора из элемента главного меню,
        /// который очищает область в строке состояния.
        /// </summary>
        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            tip_Label.Text = string.Empty;
        }

        /// <summary>
        /// Обработчик события навдения курсора на элемент главного меню,
        /// который выводит подсказку элемента в строку состояния.
        /// </summary>
        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            tip_Label.Text = ((ToolStripItem)sender).ToolTipText;
        }

        /// <summary>
        /// Подписка элементов главного меню и панели элементов на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        private void SubscribeMenuItems()
        {
            SubScribeChildMenuItems(menuBar.Items);
        }

        /// <summary>
        /// Подписка дочерних элементов главного меню на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        /// <param name="col">Коллекция элементов главного меню</param>
        private void SubScribeChildMenuItems(ToolStripItemCollection col)
        {
            for (int i = 0; i < col.Count; i++)
            {
                var item = col[i] as ToolStripMenuItem;
                if (item != null)
                {
                    item.MouseEnter += MenuItem_MouseEnter;
                    item.MouseLeave += MenuItem_MouseLeave;
                    if (item.DropDownItems.Count > 0)
                        SubScribeChildMenuItems(item.DropDownItems);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы создания нового программного обеспечения
        /// </summary>
        private void созданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editSoftwareForm = new EditSoftwareForm();
            if (editSoftwareForm.ShowDialog() == DialogResult.OK)
                InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранного программного обеспечения
        /// </summary>
        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedSoftware = _ctx.GetSoftwareByInfo(CurrentSoftware);
            if (SelectedSoftware != null)
            {
                var editRamForm = new EditSoftwareForm(SelectedSoftware);
                if (editRamForm.ShowDialog() == DialogResult.OK)
                {
                    InitBindings();
                    if (CurrentSoftware != null)
                    {
                        int pos = softwareInfoBindingSource.IndexOf(CurrentSoftware);
                        if (pos > -1)
                            softwareInfoBindingSource.Position = pos;
                    }
                }
            }
            else
                MessageBox.Show("Выберите программное обеспечение из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет удаление выбранного программного обеспечения
        /// </summary>
        private async void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedSoftware = _ctx.GetSoftwareByInfo(CurrentSoftware);
            if (SelectedSoftware != null)
            {
                var result = MessageBox.Show("Выбранное программное обеспечение будет удалено! Продолжить?",
                    "Удаление программного обеспечения", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteSoftware(SelectedSoftware);
                        InitBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении программного обеспечения!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Выберите программное обеспечение из списка для его удаления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранного программного обеспечения
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (CurrentSoftware != null)
            {
                if (_selectedIDs.Contains(CurrentSoftware.Id))
                {
                    MessageBox.Show("Выбранное программное обеспечение уже присутсвует в списке выбранных!",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите программное обеспечение из списка!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену выбора программного обеспечения, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            _canceled = true;
            Close();
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных программного обеспечения
        /// </summary>
        private void softwareInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as SoftwareInfo;
            if (selectedItem != null)
                CurrentSoftware = selectedItem;
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SelectSoftwareForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canceled)
            {
                var resut = MessageBox.Show("Программное обеспечение не будет выбрано! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (resut == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void SelectSoftwareForm_KeyDown(object sender, KeyEventArgs e)
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
