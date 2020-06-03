using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Patient> Patients { get; set; }    // Набор сущностей пациентов
        public DbSet<Visit> Visits { get; set; }    // Набор сущностей посещений
    }

    // Пациент
    public class Patient : INotifyPropertyChanged 
    {
        public string name;
        public string surname;
        public string patronymic;
        public string fullName;
        public bool? sex;
        public string sexToString;
        public DateTime? birthDate;
        public string address;
        public string phone;

        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name // Имя пациента
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [MaxLength(50)]
        public string Surname // Фамилия пациента
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        [MaxLength(50)] // Отчество пациента
        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        [NotMapped]
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }
        public bool? Sex // Пол пациента
        {
            get { return sex; }
            set
            {
                sex = value;
                OnPropertyChanged("Sex");
            }
        }
        [NotMapped]
        public string SexToString
        {
            get { return sexToString; }
            set
            {
                sexToString = value;
                OnPropertyChanged("SexToString");
            }
        }
        public DateTime? BirthDate // Дата рождения пациента
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        public string Address // Адрес проживания пациента
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        [MaxLength(50)]
        public string Phone // Номер телефона пациента
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public virtual ICollection<Visit> Visits { get; set; }
        public Patient()
        {
            PropertyChanged += Patient_PropertyChanged;
            Visits = new List<Visit>();
        }
        public void Patient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "Surname") || (e.PropertyName == "Name") || (e.PropertyName == "Patronymic"))
                FullName = Surname + " " + Name + " " + Patronymic ?? "";
            else if (e.PropertyName == "Sex")
                switch (Sex)
                {
                    case true:
                        SexToString = "муж.";
                        break;
                    case false:
                        SexToString = "жен.";
                        break;
                    case null:
                        SexToString = "";
                        break;
                };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    // Посещение клиники
    public class Visit : INotifyPropertyChanged 
    {
        private bool initialVisit;
        private DateTime dateVisit;
        private string diagnosis;
        private int patientId;

        [Key]
        public int Id { get; set; }
        [Required]
        public bool InitialVisit // Тип приема
        {
            get { return initialVisit; }
            set
            {
                initialVisit = value;
                OnPropertyChanged("InitialVisit");
            }
        }
        [Required]
        public DateTime DateVisit // Дата приема
        {
            get { return dateVisit; }
            set
            {
                dateVisit = value;
                OnPropertyChanged("DateVisit");
            }
        }
        public string Diagnosis // Диагноз
        {
            get { return diagnosis; }
            set
            {
                diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }
        }
        [Required]
        public int PatientId // Id пациента
        {
            get { return patientId; }
            set
            {
                patientId = value;
                OnPropertyChanged("PatientId");
            }
        }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
