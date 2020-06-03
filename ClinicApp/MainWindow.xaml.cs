using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClinicApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
        public static ApplicationContext db;
        public static ClinicViewModel ClinicVM;
        public MainWindow()
        {
            db = new ApplicationContext();
            db.Patients.Load();
            db.Visits.Load();
            ClinicVM = new ClinicViewModel();
            DataContext = db;
            InitializeComponent();
        }

        #region Обработчики комманд

        #region Пациенты
        private void OpenWinPatientCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                ClinicVM.SelectedPatient = new Patient();
                if (e.Parameter != null) ClinicVM.SelectedPatient = context.Patients.Where(p => p.Id == ((Patient)e.Parameter).Id).FirstOrDefault();
                DialogHost_Patient.DataContext = ClinicVM.SelectedPatient;
            }
            DialogHost_Patient.IsOpen = true;
        }
        private void OpenWinPatientCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseWinPatientCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!(e.Parameter as Button).IsCancel)
                {
                    if (db.Patients.Where(p => p.Id == ClinicVM.SelectedPatient.Id).Count() > 0)
                    {
                        var _patient = db.Patients.Where(p => p.Id == ClinicVM.SelectedPatient.Id).FirstOrDefault();
                        _patient.Name = ClinicVM.SelectedPatient.Name;
                        _patient.Surname = ClinicVM.SelectedPatient.Surname;
                        _patient.Patronymic = ClinicVM.SelectedPatient.Patronymic;
                        _patient.Sex = ClinicVM.SelectedPatient.Sex;
                        _patient.BirthDate = ClinicVM.SelectedPatient.BirthDate;
                        _patient.Address = ClinicVM.SelectedPatient.Address;
                        ClinicVM.SelectedPatient = null;
                    }
                    else db.Entry(ClinicVM.SelectedPatient).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка сохранения в базе данных. Обратитесь к администратору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogHost_Patient.IsOpen = false;
        }
        private void CloseWinPatientCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!Validation.GetHasError(textBox_Name) && !Validation.GetHasError(textBox_Surname) && !Validation.GetHasError(datePicker_BirthDate)) || (e.Parameter as Button).IsCancel;
        }
        #endregion

        #region Посещения
        private void OpenWinVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void OpenWinVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseWinVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void CloseWinVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        private void OpenDeleteDialogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogHost_DeleteRow.Tag = (e.Source as DataGrid).Name;
            switch (DialogHost_DeleteRow.Tag.ToString())
            {
                case "DGPatientList":
                    ClinicVM.SelectedPatient = (Patient)e.Parameter;
                    break;
                case "DGVisitList":
                    ClinicVM.SelectedVisit = (Visit)e.Parameter;
                    break;
            }
            DialogHost_DeleteRow.IsOpen = true;
        }
        private void OpenDeleteDialogCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseDeleteDialogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!(e.Parameter as Button).IsCancel)
                {
                    switch (DialogHost_DeleteRow.Tag.ToString())
                    {
                        case "DGPatientList":
                            db.Patients.Remove(ClinicVM.SelectedPatient);
                            break;
                        case "DGVisitList":
                            db.Visits.Remove(ClinicVM.SelectedVisit);
                            break;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка удаления записи. Обратитесь к администратору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogHost_DeleteRow.IsOpen = false;
        }
        private void CloseDeleteDialogCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void SelectMenuItemCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as RadioButton).IsChecked = true;
            MenuDriverHost.IsLeftDrawerOpen = false;
        }
        private void SelectMenuItemCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
    }
}
