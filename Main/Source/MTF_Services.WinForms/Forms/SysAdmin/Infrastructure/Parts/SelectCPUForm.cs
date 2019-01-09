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
    /// Класс формы выбора процессора
    /// </summary>
    public partial class SelectCPUForm : Form
    {
        /// <summary>
        /// Текущий разъем процессора
        /// </summary>
        private readonly CpuSocket _currentCpuSocket;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущий процессор
        /// </summary>
        public CPUInfo CurrentCPU { get; set; }

        /// <summary>
        /// Выбранный процессор
        /// </summary>
        public CPU SelectedCPU { get; set; }

        /// <summary>
        /// Флаг отмены выбора процессора
        /// </summary>
        private bool _canceled;

        /// <summary>
        /// Конструктор формы выбора процессора
        /// </summary>
        /// <param name="currentCpuSocket">Текущий разъем процессора</param>
        public SelectCPUForm(CpuSocket currentCpuSocket)
        {
            _currentCpuSocket = currentCpuSocket;
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
            cPUInfoBindingSource.DataSource = _ctx.GetCPUsInfo();
            dataGridView1.DataSource = cPUInfoBindingSource;
            txt_RecCount.Text = $"Общее количество: {cPUInfoBindingSource.Count}";
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
        /// который открывает диалоговое окно формы создания нового процессора
        /// </summary>
        private void созданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editCpuForm = new EditCPUForm();
            if (editCpuForm.ShowDialog() == DialogResult.OK)
                InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранного процессора
        /// </summary>
        private async void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedCPU = await _ctx.GetCPUByCPUInfo(CurrentCPU);
            if (SelectedCPU != null)
            {
                var editCpuForm = new EditCPUForm(SelectedCPU);
                if (editCpuForm.ShowDialog() == DialogResult.OK)
                {
                    InitBindings();
                    if (CurrentCPU != null)
                    {
                        int pos = cPUInfoBindingSource.IndexOf(CurrentCPU);
                        if (pos > -1)
                            cPUInfoBindingSource.Position = pos;
                    }
                }
            }
            else
                MessageBox.Show("Выберите процессор из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранного процессора
        /// </summary>
        private async void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedCPU = await _ctx.GetCPUByCPUInfo(CurrentCPU);
            if (SelectedCPU != null)
            {
                var result = MessageBox.Show("Выбранный процессор будет удален! Продолжить?",
                    "Удаление процессора", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteCPU(SelectedCPU);
                        InitBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении процессора!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Выберите процессор из списка для его удаления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных процессоров
        /// </summary>
        private void cPUInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as CPUInfo;
            if (selectedItem != null)
                CurrentCPU = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбора процессора, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            _canceled = true;
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SelectCPUForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canceled)
            {
                var resut = MessageBox.Show("Процессор не будет выбран! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (resut == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранного процессора
        /// </summary>
        private async void btn_OK_Click(object sender, EventArgs e)
        {
            SelectedCPU = await _ctx.GetCPUByCPUInfo(CurrentCPU);
            if (SelectedCPU != null)
            {
                if (SelectedCPU.CpuSocket != _currentCpuSocket)
                {
                    MessageBox.Show("Выбранный процессор не подходит к разъему платформы", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите процессор из списка!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
