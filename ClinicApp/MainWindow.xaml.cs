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
            try
            { 
                db = new ApplicationContext();
                db.Patients.Load();
                db.Visits.Load();
                ClinicVM = new ClinicViewModel(db);
                DataContext = db;
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка доступа к базе данных. Обратитесь к администратору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
            textBlock_CountRecords.DataContext = ClinicVM;
            dummyElement.DataContext = ClinicVM;
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
            e.CanExecute = (ClinicVM != null);
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
                    else
                    {
                        db.Entry(ClinicVM.SelectedPatient).State = EntityState.Added;
                        ClinicVM.PatientCount = db.Patients.Count();
                        DGPatientList.SelectedIndex = DGPatientList.Items.Count - 1;
                        DGPatientList.ScrollIntoView(DGPatientList.Items[DGPatientList.Items.Count - 1]);
                    }
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
            e.CanExecute = (!Validation.GetHasError(textBox_Name) 
                && !Validation.GetHasError(textBox_Surname) 
                && !Validation.GetHasError(datePicker_BirthDate)) 
                || (e.Parameter as Button).IsCancel;
        }
        #endregion

        #region Посещения
        private void OpenWinVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                if (e.Parameter != null) ClinicVM.SelectedVisit = context.Visits.Where(p => p.Id == ((Visit)e.Parameter).Id).FirstOrDefault();
                else
                {
                    ClinicVM.SelectedVisit = new Visit();
                    ClinicVM.SelectedVisit.DateVisit = DateTime.Now;
                }
                DialogHost_Visit.DataContext = ClinicVM.SelectedVisit;
                comboBox_Patient.ItemsSource = db.Patients.Local;
            }
            DialogHost_Visit.IsOpen = true;
        }
        private void OpenWinVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ClinicVM != null);
        }
        private void CloseWinVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (!(e.Parameter as Button).IsCancel)
                {
                    if (db.Visits.Where(p => p.Id == ClinicVM.SelectedVisit.Id).Count() > 0)
                    {
                        var _visit = db.Visits.Where(p => p.Id == ClinicVM.SelectedVisit.Id).FirstOrDefault();
                        _visit.InitialVisit = ClinicVM.SelectedVisit.InitialVisit;
                        _visit.DateVisit = ClinicVM.SelectedVisit.DateVisit;
                        _visit.Diagnosis = ClinicVM.SelectedVisit.Diagnosis;
                        _visit.PatientId = ClinicVM.SelectedVisit.PatientId;
                        ClinicVM.SelectedVisit = null;
                    }
                    else
                    {
                        db.Entry(ClinicVM.SelectedVisit).State = EntityState.Added;
                        ClinicVM.VisitCount = db.Visits.Count();
                        DGVisitList.SelectedIndex = DGVisitList.Items.Count - 1;
                        DGVisitList.ScrollIntoView(DGVisitList.Items[DGVisitList.Items.Count - 1]);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка сохранения в базе данных. Обратитесь к администратору.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogHost_Visit.IsOpen = false;
        }
        private void CloseWinVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!Validation.GetHasError(comboBox_Patient) 
                && !Validation.GetHasError(datePicker_DateVisit) 
                && !Validation.GetHasError(comboBox_InitialVisit)) 
                || (e.Parameter as Button).IsCancel;
        }
        #endregion

        #region Удаление записей из таблиц
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
                    ClinicVM.VisitCount = db.Visits.Count();
                    ClinicVM.PatientCount = db.Patients.Count();
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
        #endregion

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
