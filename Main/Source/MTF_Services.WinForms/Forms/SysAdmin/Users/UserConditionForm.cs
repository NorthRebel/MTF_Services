using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Users
{
    /// <summary>
    /// Класс формы отбора пользователей
    /// </summary>
    public partial class UserConditionForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновки в источнике данных должностей
        /// </summary>
        private bool _afterPositionSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновки в источнике данных уровней привилегий
        /// </summary>
        private bool _afterRightsLevelSuspend;

        /// <summary>
        /// Выбранная должность
        /// </summary>
        public Position SelectedPosition { get; set; }

        /// <summary>
        /// Выбранный уровень привелегий
        /// </summary>
        public RightsLevel SelectedRightsLevel { get; set; }

        /// <summary>
        /// Список пользователей
        /// </summary>
        public BindingList<UserInfo> UsersInfoMain { get; set; }

        /// <summary>
        /// Отобранный список пользователей
        /// </summary>
        public BindingList<UserInfo> UsersInfoToShow { get; set; }

        /// <summary>
        /// Конструктор формы отбора пользователей
        /// </summary>
        public UserConditionForm(BindingList<UserInfo> usersInfoMain)
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_OK.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();
            UsersInfoMain = usersInfoMain;
            InitBindings();
            pictureBox1_Click(null,EventArgs.Empty);
            pictureBox2_Click(null,EventArgs.Empty);
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitBindings()
        {
            positionBindingSource.DataSource = _ctx.GetPositions();
            rightsLevelBindingSource.DataSource = _ctx.GetRightsLevels();
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных уровней привилегий
        /// </summary>
        private void rightsLevelBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (rightsLevelBindingSource.IsBindingSuspended)
            {
                _afterRightsLevelSuspend = true;
                rightsLevelBindingSource.ResumeBinding();
            }
            else if (_afterRightsLevelSuspend)
            {
                _afterRightsLevelSuspend = false;
                rightsLevelBindingSource.Position = comboBox1.SelectedIndex;
            }

            if (rightsLevelBindingSource.Position > -1)
            {
                var rightsLevel = rightsLevelBindingSource.Current as RightsLevel;
                if (rightsLevel != null)
                {
                    SelectedRightsLevel = rightsLevel;
                    lbl_SelectedUsersStatus.Text = $"Отобрано пользователей: {RunSelection()}";
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных должностей
        /// </summary>
        private void positionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (positionBindingSource.IsBindingSuspended)
            {
                _afterPositionSuspend = true;
                positionBindingSource.ResumeBinding();
            }
            else if (_afterPositionSuspend)
            {
                _afterPositionSuspend = false;
                positionBindingSource.Position = comboBox2.SelectedIndex;
            }

            if (positionBindingSource.Position > -1)
            {
                var position = positionBindingSource.Current as Position;
                if (position != null)
                {
                    SelectedPosition = position;
                    lbl_SelectedUsersStatus.Text = $"Отобрано пользователей: {RunSelection()}";
                }
            }
        }

        /// <summary>
        /// Выполнение отбора пользователей по условию
        /// </summary>
        private int RunSelection()
        {
            List<UserInfo> selectedUsers = null;

            try
            {
                if (SelectedRightsLevel != null && SelectedPosition != null)
                    selectedUsers = UsersInfoMain.Where(ui =>
                            ui.Position.Equals(SelectedPosition.Name) && ui.RightsLevel.Equals(SelectedRightsLevel.Name))
                        .ToList();
                else if (SelectedRightsLevel != null)
                    selectedUsers = UsersInfoMain.Where(ui => ui.RightsLevel.Equals(SelectedRightsLevel.Name)).ToList();
                else if (SelectedPosition != null)
                    selectedUsers = UsersInfoMain.Where(ui => ui.Position.Equals(SelectedPosition.Name)).ToList();
                else
                    selectedUsers = UsersInfoMain.ToList();

                UsersInfoToShow = new BindingList<UserInfo>(selectedUsers);
                return UsersInfoToShow.Count;
            }
            catch
            {
                UsersInfoToShow = null;
                return 0;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбора в списке уровней привилегий
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            SelectedRightsLevel = null;
            rightsLevelBindingSource.SuspendBinding();
            lbl_SelectedUsersStatus.Text = $"Отобрано пользователей: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбора в списке должностей
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            SelectedPosition = null;
            positionBindingSource.SuspendBinding();
            lbl_SelectedUsersStatus.Text = $"Отобрано пользователей: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену ввода условия отбора пользователей, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение совершенного отбора пользователей
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (UsersInfoToShow != null && UsersInfoToShow.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Не найдены пользователи по указанному условию!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке уровней привbлегий
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
                rightsLevelBindingSource_CurrentChanged(rightsLevelBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке должностей
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                positionBindingSource_CurrentChanged(positionBindingSource, EventArgs.Empty);
        }
    }
}
