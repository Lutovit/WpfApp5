using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp5.Model;

namespace WpfApp5.ViewModel
{
    public class AddCarViewModel : INotifyPropertyChanged
    {
        public event EventHandler<CarArgs> onSaveNewCarEvent;

        private Car newCar { set; get; }
        public Car NewCar
        {
            get { return newCar; }
            set
            {
                newCar = value;
                OnPropertyChanged("NewCar");
            }
        }

        public Action CloseAction { get; set; }


        public AddCarViewModel()
        {
            newCar = new Car();
            
        }


        public string NewCarId
        {
            get { return newCar.Id; }
            set
            {
                newCar.Id = value;
                OnPropertyChanged("Id");
            }
        }


        public string NewCarName
        {
            get { return newCar.Name; }
            set
            {
                newCar.Name = value;
                OnPropertyChanged("Name");
            }
        }


        public int NewCarPrice
        {
            get { return newCar.Price; }
            set
            {
                newCar.Price = value;
                OnPropertyChanged("Price");
            }
        }


        public int NewCarYear
        {
            get { return newCar.Year; }
            set
            {
                newCar.Year = value;
                OnPropertyChanged("Year");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }                
        }


        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new RelayCommand(obj =>
                  {
                      CloseAction();
                  }));
            }
        }


        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      OnSaveCar();

                      Car car = (Car)obj;

                      MessageBox.Show(" ID: " + car?.Id + " Name: " + car?.Name + " Price: " + car?.Price + " Year: " + car?.Year);
                  }));
            }
        }


        private void OnSaveCar() 
        {
            if (!String.IsNullOrWhiteSpace(NewCar.Name) && !String.IsNullOrWhiteSpace(NewCar.Price.ToString()) && !String.IsNullOrWhiteSpace(NewCar.Year.ToString())) 
            {
                CarArgs args = new CarArgs { car = NewCar };
                onSaveNewCarEvent?.Invoke(this, args);
                NewCar = new Car();
            }            
        }
    }
}
