using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ClinicApp
{
    public class SexToBooleanConverter : IValueConverter
    {
        private const string TrueText = "Мужской";
        private const string FalseText = "Женский";
        public static readonly SexToBooleanConverter Instance = new SexToBooleanConverter();

        private SexToBooleanConverter()
        {
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(true, value)
                ? TrueText
                : FalseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value, TrueText);
        }
    }
    public class ClinicViewModel : INotifyPropertyChanged
    {
        private int countRecords;
        private Patient selectedPatient;
        private Visit selectedVisit;
        public int CountRecords
        {
            get { return countRecords; }
            set
            {
                countRecords = value;
                OnPropertyChanged("CountRecords");
            }
        }
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }
        public Visit SelectedVisit
        {
            get { return selectedVisit; }
            set
            {
                selectedVisit = value;
                OnPropertyChanged("SelectedVisit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
