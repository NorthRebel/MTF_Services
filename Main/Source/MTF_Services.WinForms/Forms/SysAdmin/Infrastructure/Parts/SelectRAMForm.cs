using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts
{
    /// <summary>
    /// Класс формы выбора оперативной памяти
    /// </summary>
    public partial class SelectRAMForm : Form
    {
        private readonly RamType _currentRamType;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущий оперативная память
        /// </summary>
        public RAMInfo CurrentRAM { get; set; }

        /// <summary>
        /// Выбранная оперативная память
        /// </summary>
        public RAM SelectedRAM { get; set; }

        /// <summary>
        /// Флаг отмены выбора оперативной памяти
        /// </summary>
        private bool _canceled;

        /// <summary>
        /// Конструктор формы выбора оперативной памяти
        /// </summary>
        /// <param name="currentRamType">Текущий тип памяти</param>
        public SelectRAMForm(RamType currentRamType)
        {
            _currentRamType = currentRamType;
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
            rAMInfoBindingSource.DataSource = _ctx.GetRAMsInfo();
            dataGridView1.DataSource = rAMInfoBindingSource;
            txt_RecCount.Text = $"Общее количество: {rAMInfoBindingSource.Count}";
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
        /// который открывает диалоговое окно формы создания новой оперативной памяти
        /// </summary>
        private void созданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editRamForm = new EditRAMForm();
            if (editRamForm.ShowDialog() == DialogResult.OK)
                InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранной оперативной памяти
        /// </summary>
        private async void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedRAM = await _ctx.GetRAMByRAMInfo(CurrentRAM);
            if (SelectedRAM != null)
            {
                var editRamForm = new EditRAMForm(SelectedRAM);
                if (editRamForm.ShowDialog() == DialogResult.OK)
                {
                    InitBindings();
                    if (CurrentRAM != null)
                    {
                        int pos = rAMInfoBindingSource.IndexOf(CurrentRAM);
                        if (pos > -1)
                            rAMInfoBindingSource.Position = pos;
                    }
                }
            }
            else
                MessageBox.Show("Выберите оперативную память из списка для ее редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранной оперативной памяти
        /// </summary>
        private async void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedRAM = await _ctx.GetRAMByRAMInfo(CurrentRAM);
            if (SelectedRAM != null)
            {
                var result = MessageBox.Show("Выбранная оперативная память будет удалена! Продолжить?",
                    "Удаление оперативной памяти", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteRAM(SelectedRAM);
                        InitBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении оперативной памяти!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Выберите оперативную память из списка для ее удаления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных оперативной памяти
        /// </summary>
        private void rAMInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as RAMInfo;
            if (selectedItem != null)
                CurrentRAM = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбора оперативной памяти, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            _canceled = true;
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SelectRAMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canceled)
            {
                var resut = MessageBox.Show("Оперативная память не будет выбрана! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (resut == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранной оперативной памяти
        /// </summary>
        private async void btn_OK_Click(object sender, EventArgs e)
        {
            SelectedRAM = await _ctx.GetRAMByRAMInfo(CurrentRAM);
            if (SelectedRAM != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите оперативную память из списка!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
