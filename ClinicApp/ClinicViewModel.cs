using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp
{
    public class ClinicViewModel : INotifyPropertyChanged
    {
        private int countRecords;
        private Patient selectedPatient;
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

        public void SetSelectedPatient(int patientId, ApplicationContext context)
        {
            SelectedPatient = context.Patients.Where(p => p.Id == patientId).FirstOrDefault()??SelectedPatient;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

}
