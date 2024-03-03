using DataBindingLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComputeWidthBerforRenderingSample001
{
    public class MainViewModel : NotifyPropertyBase
    {
        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { SetProperty(ref _people, value); }
        }
        public MainViewModel()
        {
            var people = new ObservableCollection<Person>()
            {
                new Person {Name = "Amuro", City = "New York"},
                new Person {Name = "Char Aznable",  City = "New York"},
                new Person {Name = "Bright Noa",  City = "Tokyo"},
                new Person {Name = "Kamille Bidan", City = "Las Vegas"},
                new Person {Name = "Emma", City = "Singapore"},
                new Person {Name = "Fa Yuiry", City = "Singapore"},
                new Person {Name = "Banagher", City = "Taipei"},
                new Person {Name = "Marida Cruz", City = "Las Vegas"},
                new Person {Name = "Suletta Mercury", City = "Sydney"},
                new Person {Name = "Mikazuki Augus", City = "Kaohsiung"},
            };
            People = people;
        }

        public ICommand AddCommand
        {
            get => new RelayCommand(x =>
            {
                People.Add(new Person { Name = "Shrek family", City = "Far Far Away Kingdom" });
            });
        }
    }

    public class Person : NotifyPropertyBase
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }
    }
}
