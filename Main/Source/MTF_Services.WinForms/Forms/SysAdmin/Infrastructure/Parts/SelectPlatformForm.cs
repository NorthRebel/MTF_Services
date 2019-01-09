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
    /// Класс формы выбора платформы
    /// </summary>
    public partial class SelectPlatformForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Выбранная платформа
        /// </summary>
        public PlatformInfo SelectedPlatformInfo { get; set; }

        /// <summary>
        /// Флаг редактирования платформ
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы выбора платформы
        /// </summary>
        public SelectPlatformForm()
        {
            InitializeComponent();
            SubscribeMenuItems();

            btn_ShowSupportedInterfacesList.Image = new Bitmap(Resources.hdd, new Size(20, 20));
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            созданиеToolStripMenuItem1.Click += созданиеToolStripMenuItem_Click;
            редактированиеToolStripMenuItem1.Click += редактированиеToolStripMenuItem_Click;
            удалениеToolStripMenuItem1.Click += удалениеToolStripMenuItem_Click;

            toolStripButton4.Click += btn_ShowSupportedInterfacesList_Click;

            toolStripButton5.Click += созданиеToolStripMenuItem_Click;
            toolStripButton6.Click += редактированиеToolStripMenuItem_Click;
            toolStripButton7.Click += удалениеToolStripMenuItem_Click;
            
            _ctx = new Context();
            InitCollection();
        }

        /// <summary>
        /// Инициализация привязки коллекции
        /// </summary>
        private void InitCollection()
        {
            platformInfoBindingSource.DataSource = _ctx.GetPlatformsInfo();
            dataGridView1.DataSource = platformInfoBindingSource;
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

            foreach (ToolStripItem toolBarControl in toolBar.Items)
            {
                toolBarControl.MouseEnter += MenuItem_MouseEnter;
                toolBarControl.MouseLeave += MenuItem_MouseLeave;
            }
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
        /// Обработчик события изменения текущего источника данных платформ
        /// </summary>
        private void platformInfoBindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            var bs = sender as BindingSource;
            if (bs != null && bs.DataSource is BindingList<PlatformInfo>)
                lbl_RecordsCount.Text = $"Кол-во: {((BindingList<PlatformInfo>)bs.DataSource).Count}";
            else
                lbl_RecordsCount.Text = "Кол-во: 0";
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных платформ
        /// </summary>
        private void platformInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as PlatformInfo;
            if (selectedItem != null)
                SelectedPlatformInfo = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену выбора платформы, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение выбранной платформы
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (SelectedPlatformInfo != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите платформу из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы создания новой платформы
        /// </summary>
        private void созданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editPlatformForm = new EditPlatformForm();
            if (editPlatformForm.ShowDialog() == DialogResult.OK)
            {
                var selectedElement = platformInfoBindingSource.Current as PlatformInfo;
                Edited = true;
                InitCollection();
                if (selectedElement != null)
                    platformInfoBindingSource.Position = platformInfoBindingSource.IndexOf(selectedElement);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранной платформы
        /// </summary>
        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedElement = platformInfoBindingSource.Current as PlatformInfo;
            if (selectedElement == null)
            {
                MessageBox.Show("Выберите платформу из списка или добавьте новую!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                var editPlatformForm = new EditPlatformForm(selectedElement);
                if (editPlatformForm.ShowDialog() == DialogResult.OK)
                {
                    InitCollection();
                    Edited = true;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранной платформы
        /// </summary>
        private async void удалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedElement = platformInfoBindingSource.Current as PlatformInfo;
            if (selectedElement == null)
            {
                MessageBox.Show("Выберите платформу из списка или добавьте новую!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var platform = _ctx.GetPlatformByPlatformInfo(selectedElement);
                var result = MessageBox.Show("Выбранная платформа будет удалена! Продолжить?",
                    "Удаление платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeletePlatform(platform);
                        InitCollection();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении оперативной памяти!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }            
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию о выбранной платформе!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который открывает диалоговое окно формы просмотра списка доступных интерфейсов хранения выбранной платформы
        /// </summary>
        private void btn_ShowSupportedInterfacesList_Click(object sender, EventArgs e)
        {
            if (SelectedPlatformInfo != null)
            {
                try
                {
                    var selPlatform = _ctx.GetPlatformByPlatformInfo(SelectedPlatformInfo);
                    var platformInterfacesForm = new PlatformInterfacesForm(_ctx.GetAvalibleInterfacesOfPlarformBS(selPlatform.Platform_StorageInt));
                    platformInterfacesForm.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Ошибка при получении данных об выбранной платформе!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Выберите платформу из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }
    }
}
