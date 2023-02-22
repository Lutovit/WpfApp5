using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp5.Model;
using WpfApp5.View;

namespace WpfApp5.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Car selectedCar;     
        public ObservableCollection<Car> Cars { set; get; }
        public Action CloseAction { get; set; }

        public MainWindowViewModel() 
        {
            Cars = new ObservableCollection<Car> 
            {
                new Car{  Id="1", Name = "Opel", Price = 12500, Year = 2010},
                new Car{  Id="2", Name = "Ford", Price = 17300, Year = 2011},
                new Car{  Id="3", Name = "Gaz", Price = 8400, Year = 2012},
                new Car{  Id="4", Name = "BMW", Price = 14200, Year = 2013},
                new Car{  Id="5", Name = "FIAT", Price = 8300, Year = 2014},
                new Car{  Id="6", Name = "SCANIA", Price = 20000, Year = 2015}
            };             
        }


        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                selectedCar = value;
                OnPropertyChanged("selectedCar");
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


        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      AddCarViewModel viewModel = new AddCarViewModel();
                      viewModel.onSaveNewCarEvent += (sender, args) =>
                      {
                          if (args?.car != null) 
                          {
                              Car newCar = args.car;
                              newCar.Id = (Cars.Count + 1).ToString();
                              Cars.Add(newCar);
                          }
                      };
                      AddCar view = new AddCar(viewModel);
                      view.Show();
                  }));
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


        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Car car = (Car)obj;

                        if (car != null)
                        {
                           Cars.Remove(car);
                        }
                    },
                    (obj) => Cars.Count > 0));
                    }
        }



    }
}
