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
            ClinicVM = new ClinicViewModel();
            DataContext = db;
            InitializeComponent();
        }

        #region Обработчики комманд

        #region Пациенты
        private void OpenWinPatientCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClinicVM.SelectedPatient = (Patient)(e.Parameter ?? new Patient());
            DialogHost_Patient.DataContext = ClinicVM.SelectedPatient;
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
                    if (db.Entry(ClinicVM.SelectedPatient).State == EntityState.Detached)
                        db.Entry(ClinicVM.SelectedPatient).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка сохранения в базе данных. Вызовите системного администратора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DialogHost_Patient.IsOpen = false;
        }
        private void CloseWinPatientCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
        #region Посещения
        private void AddVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void AddVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void EditVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void EditVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void DeleteVisitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void DeleteVisitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
        #region Врачи
        private void AddDoctorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void AddDoctorCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void EditDoctorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void EditDoctorCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void DeleteDoctorCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void DeleteDoctorCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
        private void OpenDeleteDialogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ClinicVM.SelectedPatient = (Patient)e.Parameter;
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
                    db.Entry(ClinicVM.SelectedPatient).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"В методе: {ex.TargetSite} возникло исключение: { ex.Message}");
                MessageBox.Show($"Ошибка удаления записи. Вызовите системного администратора.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
