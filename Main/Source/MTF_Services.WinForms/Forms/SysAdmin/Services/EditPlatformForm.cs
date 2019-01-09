using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Parts;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services
{
    /// <summary>
    /// Класс формы редактирования платформы
    /// </summary>
    public partial class EditPlatformForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Редактируемуя оперативная память
        /// </summary>
        public PaasType CurrentPaas { get; set; }

        /// <summary>
        /// Выбранные сервера
        /// </summary>
        public BindingList<ServerPaasInfo> SelectedServers { get; set; }

        /// <summary>
        /// Выбранные хранилища данных
        /// </summary>
        public BindingList<SANPaasInfo> SelectedSANs { get; set; }

        /// <summary>
        /// Наименование модели оперативной памяти перед редактированием
        /// </summary>
        private string _paasTypeNameBeforeEdit;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Конструктор формы создания новой платформы
        /// </summary>
        public EditPlatformForm()
        {
            InitializeComponent();
            btn_Save.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();

            CurrentPaas = new PaasType();
            paasTypeBindingSource.DataSource = CurrentPaas;

            BindSelectedServers();
            BindSelectedSANs();

            _formMode = FormMode.Add;
            Text = "Создание новой платформы";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранной платформы
        /// </summary>
        /// <param name="selectedPaas">Выбранная платформа</param>
        public EditPlatformForm(PaasType selectedPaas) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование платформы";
            InitEditPaas(selectedPaas);
        }

        /// <summary>
        /// Инициализация привязок выбранной платформы для редактирования
        /// </summary>
        /// <param name="selectedPaas">Выбранная платформа</param>
        private void InitEditPaas(PaasType selectedPaas)
        {
            CurrentPaas = selectedPaas;
            paasTypeBindingSource.DataSource = CurrentPaas;
            _paasTypeNameBeforeEdit = CurrentPaas.Name;
            paasTypeBindingSource.ResumeBinding();

            var serverList = _ctx.GetServerConfigsByPaas(CurrentPaas);
            serverList.ForEach(sl => SelectedServers.Add(_ctx.GetServerPaasInfoByServer(sl)));
            serverPaasInfoBindingSource.DataSource = SelectedServers;
            dataGridView1.DataSource = serverPaasInfoBindingSource;

            var sansList = _ctx.GetSANsListByPaas(CurrentPaas);
            sansList.ForEach(sl => SelectedSANs.Add(_ctx.GetSANPaasInfoBySAN(sl)));
            sANPaasInfoBindingSource.DataSource = SelectedSANs;
            dataGridView2.DataSource = sANPaasInfoBindingSource;
        }

        /// <summary>
        /// Привязка списка выбранных конфигураций серверов
        /// </summary>
        private void BindSelectedServers()
        {
            SelectedServers = new BindingList<ServerPaasInfo>();
            serverPaasInfoBindingSource.DataSource = SelectedServers;
            dataGridView1.DataSource = serverPaasInfoBindingSource;
        }

        /// <summary>
        /// Привязка списка выбранных хранилищ данных
        /// </summary>
        private void BindSelectedSANs()
        {
            SelectedSANs = new BindingList<SANPaasInfo>();
            sANPaasInfoBindingSource.DataSource = SelectedSANs;
            dataGridView2.DataSource = sANPaasInfoBindingSource;
        }
        
        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно добавления сервера к платформе
        /// </summary>
        private void picBtn_AddServer_Click(object sender, EventArgs e)
        {
            var selectedIDs = SelectedServers.ToList().Select(s => s.Id).ToList();
            var selectServerForm = new SelectServerForm(selectedIDs);
            if (selectServerForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selServ = selectServerForm.SelectedServerPaasInfo;
                    selServ.UsedCoreCount = 0;
                    selServ.UsedRAMVolume = 0;
                    selServ.UsedStorageVolume = 0;
                    SelectedServers.Add(selServ);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при добавлении выбранного сервера!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранного сервера
        /// </summary>
        private void picBtn_DeleteServer_Click(object sender, EventArgs e)
        {
            var selectedServer = serverPaasInfoBindingSource.Current as ServerPaasInfo;
            if (selectedServer == null)
            {
                MessageBox.Show("Конфигурация сервера не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedServers.Remove(selectedServer);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно добавления хранилища данных к платформе
        /// </summary>
        private void picBtn_AddSAN_Click(object sender, EventArgs e)
        {
            var selectedIDs = SelectedSANs.ToList().Select(s => s.Id).ToList();
            var selectSanForm = new SelectSANForm(selectedIDs);
            if (selectSanForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selSan = selectSanForm.SelectedSANPaasInfo;
                    selSan.UsedVolume = 0;
                    SelectedSANs.Add(selSan);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при добавлении выбранного хранилища данных!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранного хранилища данных
        /// </summary>
        private void picBtn_DeleteSAN_Click(object sender, EventArgs e)
        {
            var sanPaasInfo = sANPaasInfoBindingSource.Current as SANPaasInfo;
            if (sanPaasInfo == null)
            {
                MessageBox.Show("Хранилище данных сервера не выбрано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedSANs.Remove(sanPaasInfo);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (_formMode == FormMode.None)
                return;

            if (string.IsNullOrWhiteSpace(CurrentPaas.Name))
            {
                MessageBox.Show("Введите наименование платформы!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (SelectedServers.Count == 0)
            {
                MessageBox.Show("Выберите конфигурации серверов для сохранения платформы!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (SelectedSANs.Count == 0)
            {
                MessageBox.Show("Выберите хранилища данных для сохранения платформы!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool existed = false;

                switch (_formMode)
                {
                    case FormMode.Add:
                        existed = await _ctx.CheckPaasForDublicate(CurrentPaas.Name);
                        break;
                    case FormMode.Edit:
                        existed = await _ctx.CheckPaasForDublicate(CurrentPaas.Name) && !CurrentPaas.Name.Equals(_paasTypeNameBeforeEdit);
                        break;
                }

                if (existed)
                {
                    MessageBox.Show("Платформа с таким наименованием уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CurrentPaas.Server.Clear();
                foreach (var selectedServer in SelectedServers)
                    CurrentPaas.Server.Add(await _ctx.GetServerByID(selectedServer.Id));

                CurrentPaas.SAN.Clear();
                foreach (var selectedSaN in SelectedSANs)
                    CurrentPaas.SAN.Add(await _ctx.GetSANByID(selectedSaN.Id));

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewPaas(CurrentPaas);
                        MessageBox.Show("Новая платформа успешно создана!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditPaas(CurrentPaas);
                        MessageBox.Show("Изменения в платформе успешно сохранены!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                _formMode = FormMode.None;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                switch (_formMode)
                {
                    case FormMode.Add:
                        MessageBox.Show("Не удалось создать новую платформу!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case FormMode.Edit:
                        MessageBox.Show("Не удалось сохранить изменения в платформе!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену добавления/редактирования, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditPlatformForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            Owner?.Show();
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditPlatformForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}

