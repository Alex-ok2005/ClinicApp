using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClinicApp
{
    public class RelayCommand
    {
        static RelayCommand()
        {
            // Инициализация команды
            OpenWinPatientCommand = new RoutedCommand("OpenWinPatientCommand", typeof(MainWindow));
            CloseWinPatientCommand = new RoutedCommand("CloseWinPatientCommand", typeof(MainWindow));
            OpenWinVisitCommand = new RoutedCommand("OpenWinVisitCommand", typeof(MainWindow));
            CloseWinVisitCommand = new RoutedCommand("CloseWinVisitCommand", typeof(MainWindow));
            OpenDeleteDialogCommand = new RoutedCommand("OpenDeleteDialogDoctorCommand", typeof(MainWindow));
            CloseDeleteDialogCommand = new RoutedCommand("CloseDeleteDialogDoctorCommand", typeof(MainWindow));
            SelectMenuItemCommand = new RoutedCommand("SelectMenuItemCommand", typeof(MainWindow));
            MenuCommand = new RoutedCommand("MenuCommand", typeof(MainWindow));
        }

        public static RoutedCommand OpenWinPatientCommand { get; private set; }
        public static RoutedCommand CloseWinPatientCommand { get; private set; }
        public static RoutedCommand OpenWinVisitCommand { get; private set; }
        public static RoutedCommand CloseWinVisitCommand { get; private set; }
        public static RoutedCommand OpenDeleteDialogCommand { get; private set; }
        public static RoutedCommand CloseDeleteDialogCommand { get; private set; }
        public static RoutedCommand SelectMenuItemCommand { get; private set; }
        public static RoutedCommand MenuCommand { get; private set; }
    }
}
