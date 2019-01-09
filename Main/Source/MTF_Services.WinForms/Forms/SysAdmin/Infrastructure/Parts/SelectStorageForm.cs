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
    /// Класс формы выбора накопителя
    /// </summary>
    public partial class SelectStorageForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущий накопитель
        /// </summary>
        public StorageInfo CurrentStorage { get; set; }

        /// <summary>
        /// Выбранный накопитель
        /// </summary>
        public Strorage SelectedStorage { get; set; }

        /// <summary>
        /// Флаг отмены выбора накопителя
        /// </summary>
        private bool _canceled;

        /// <summary>
        /// Конструктор формы выбора накопителя
        /// </summary>
        public SelectStorageForm()
        {
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
            storageInfoBindingSource.DataSource = _ctx.GetStoragesInfo();
            dataGridView1.DataSource = storageInfoBindingSource;
            txt_RecCount.Text = $"Общее количество: {storageInfoBindingSource.Count}";
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
        /// который открывает диалоговое окно формы создания нового накопителя
        /// </summary>
        private void созданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editStorageForm = new EditStorageForm();
            if (editStorageForm.ShowDialog() == DialogResult.OK)
                InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранного накопителя
        /// </summary>
        private async void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedStorage = await _ctx.GetStorageByStorageInfo(CurrentStorage);
            if (SelectedStorage != null)
            {
                var editStorageForm = new EditStorageForm(SelectedStorage);
                if (editStorageForm.ShowDialog() == DialogResult.OK)
                {
                    InitBindings();
                    if (CurrentStorage != null)
                    {
                        int pos = storageInfoBindingSource.IndexOf(CurrentStorage);
                        if (pos > -1)
                            storageInfoBindingSource.Position = pos;
                    }
                }
            }
            else
                MessageBox.Show("Выберите накопитель из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранного накопителя
        /// </summary>
        private async void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedStorage = await _ctx.GetStorageByStorageInfo(CurrentStorage);
            if (SelectedStorage != null)
            {
                var result = MessageBox.Show("Выбранный накопитель будет удален! Продолжить?",
                    "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteStrorage(SelectedStorage);
                        InitBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении накопителя!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Выберите накопитель из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных накопителей
        /// </summary>
        private void storageInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as StorageInfo;
            if (selectedItem != null)
                CurrentStorage = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбора накопителя, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            _canceled = true;
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SelectStorageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canceled)
            {
                var resut = MessageBox.Show("Накопитель не будет выбран! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
                if (resut == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранного накопителя
        /// </summary>
        private async void btn_OK_Click(object sender, EventArgs e)
        {
            SelectedStorage = await _ctx.GetStorageByStorageInfo(CurrentStorage);
            if (SelectedStorage != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите процессор из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
