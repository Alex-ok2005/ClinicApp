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
using System.Windows.Controls;
using System.Windows.Data;

namespace ClinicApp
{
    public class GenderToBooleanConverter : IValueConverter
    {
        private const string TrueText = "Мужской";
        private const string FalseText = "Женский";
        public static readonly GenderToBooleanConverter Instance = new GenderToBooleanConverter();

        private GenderToBooleanConverter()
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
    public class InitialVisitToBooleanConverter : IValueConverter
    {
        private const string TrueText = "Первичный";
        private const string FalseText = "Вторичный";
        public static readonly InitialVisitToBooleanConverter Instance = new InitialVisitToBooleanConverter();

        private InitialVisitToBooleanConverter()
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
        private int activeTabItem;
        private int countRecords;
        private int patientCount;
        private int visitCount;
        private int doctorCount;
        private Patient selectedPatient; 
        private Visit selectedVisit;
        private Doctor selectedDoctor;
        public int ActiveTabItem
        {
            get { return activeTabItem; }
            set
            {
                activeTabItem = value;
                switch (activeTabItem)
                {
                    case 0:
                        CountRecords = PatientCount;
                        break;
                    case 1:
                        CountRecords = VisitCount;
                        break;
                    case 2:
                        CountRecords = DoctorCount;
                        break;
                }
                OnPropertyChanged("ActiveTabItem");
            }
        } // Активная вкладка
        public int CountRecords
        {
            get { return countRecords; }
            set
            {
                countRecords = value;
                OnPropertyChanged("CountRecords");
            }
        } // Количество запией в активой таблице
        public int PatientCount
        {
            get { return patientCount; }
            set
            {
                patientCount = value;
                switch (ActiveTabItem)
                {
                    case 0:
                        CountRecords = PatientCount;
                        break;
                    case 1:
                        CountRecords = VisitCount;
                        break;
                    case 2:
                        CountRecords = DoctorCount;
                        break;
                }
                OnPropertyChanged("PatientCount");
            }
        }  // Количество запией в таблице пациентов
        public int VisitCount
        {
            get { return visitCount; }
            set
            {
                visitCount = value;
                switch (ActiveTabItem)
                {
                    case 0:
                        CountRecords = PatientCount;
                        break;
                    case 1:
                        CountRecords = VisitCount;
                        break;
                    case 2:
                        CountRecords = DoctorCount;
                        break;
                }
                OnPropertyChanged("VisitCount");
            }
        } // Количество запией в таблице посещений
        public int DoctorCount
        {
            get { return doctorCount; }
            set
            {
                doctorCount = value;
                switch (ActiveTabItem)
                {
                    case 0:
                        CountRecords = PatientCount;
                        break;
                    case 1:
                        CountRecords = VisitCount;
                        break;
                    case 2:
                        CountRecords = DoctorCount;
                        break;
                }
                OnPropertyChanged("DoctorCount");
            }
        } // Количество запией в таблице докторов
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        } // Выбранный для изменения пациент
        public Visit SelectedVisit
        {
            get { return selectedVisit; }
            set
            {
                selectedVisit = value;
                OnPropertyChanged("SelectedVisit");
            }
        } // Выбранный для изменения визит
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        } // Выбранный для изменения визит
        public string ImageFile { get; set; }
        public ClinicViewModel(ApplicationContext context)
        {
            PatientCount = context.Patients.Count();
            VisitCount = context.Visits.Count();
            DoctorCount = context.Doctors.Count();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
