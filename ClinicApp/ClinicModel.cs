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
using System.Windows.Controls;

namespace ClinicApp
{   
    // Контекст данных
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<Patient> Patients { get; set; }    // Набор сущностей пациентов
        public DbSet<Doctor> Doctors { get; set; }    // Набор сущностей о
        public DbSet<Visit> Visits { get; set; }    // Набор сущностей посещений
    }
    // Личность
    public class Person : INotifyPropertyChanged 
    {
        private string name;
        private string surname;
        private string patronymic;
        private string fullName;
        private bool? gender;
        private string genderToString;
        private DateTime? birthDate;
        private string address;
        private string phone;
        private byte[] image;

        [Key]
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name // Имя 
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        [MaxLength(50)]
        public string Surname // Фамилия 
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        [MaxLength(50)] // Отчество 
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
        public bool? Gender // Пол 
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        [NotMapped]
        public string GenderToString
        {
            get { return genderToString; }
            set
            {
                genderToString = value;
                OnPropertyChanged("GenderToString");
            }
        }
        public DateTime? BirthDate // Дата рождения 
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        public string Address // Адрес проживания 
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        [MaxLength(50)]
        public string Phone // Номер телефона 
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public byte[] Image // Фото
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public Person()
        {
            PropertyChanged += Patient_PropertyChanged;
        }
        public void Patient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "Surname") || (e.PropertyName == "Name") || (e.PropertyName == "Patronymic"))
                FullName = Surname + " " + Name + " " + Patronymic ?? "";
            else if (e.PropertyName == "Gender")
                switch (Gender)
                {
                    case true:
                        GenderToString = "муж.";
                        break;
                    case false:
                        GenderToString = "жен.";
                        break;
                    case null:
                        GenderToString = "";
                        break;
                };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
    // Пациент
    public class Patient : Person
    {
        public ICollection<Visit> Visits { get; set; }
        public Patient()
        {
            Visits = new List<Visit>();
        }
    }
    // Доктор
    public class Doctor : Person
    {
        private string specialty;
        [NotMapped]
        public Image ImageControl { get; set; }
        [MaxLength(50)]
        public string Specialty // Специализация
        {
            get { return specialty; }
            set
            {
                specialty = value;
                OnPropertyChanged("Specialty");
            }
        }
    }
    // Посещение клиники
    public class Visit : INotifyPropertyChanged 
    {
        private bool initialVisit;
        private string initialVisitToString;
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
        [NotMapped]
        public string InitialVisitToString
        {
            get { return initialVisitToString; }
            set
            {
                initialVisitToString = value;
                OnPropertyChanged("InitialVisitToString");
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
        public Visit()
        {
            PropertyChanged += Visit_PropertyChanged;
        }
        public void Visit_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InitialVisit")
                switch (InitialVisit)
                {
                    case true:
                        InitialVisitToString = "первичный";
                        break;
                    case false:
                        InitialVisitToString = "вторичный";
                        break;
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
