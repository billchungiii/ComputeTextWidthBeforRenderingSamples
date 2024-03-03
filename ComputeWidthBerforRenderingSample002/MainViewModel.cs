using DataBindingLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ComputeWidthBerforRenderingSample002
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

            #region compute width before binding
            ComputeWidthOfText(people);
            #endregion
            People = people;
            People.CollectionChanged += People_CollectionChanged;
        }

        public ICommand AddCommand
        {
            get => new RelayCommand(x =>
            {
                People.Add(new Person { Name = "Shrek family", City = "Far Far Away Kingdom" });
            });
        }

        #region new method for collection changed
        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            var typeface = GetTypeface();
            foreach (var item in e.NewItems.Cast<Person>())
            {
                ComputeEachTextWidth(typeface, item);
            }
        }
        #endregion

        #region add new properties

        private double _widthOfName;
        public double WidthOfName
        {
            get { return _widthOfName; }
            set { SetProperty(ref _widthOfName, value); }
        }

        private double _widthOfCity;
        public double WidthOfCity
        {
            get { return _widthOfCity; }
            set { SetProperty(ref _widthOfCity, value); }
        }
        #endregion

        #region compute width before rendering
        private void ComputeWidthOfText(ObservableCollection<Person> people)
        {
            var typeface = GetTypeface();
            foreach (var item in people)
            {
                ComputeEachTextWidth(typeface, item);
            }
        }

        private void ComputeEachTextWidth(Typeface typeface, Person item)
        {
            var formattedName = GetFormattedText(item.Name, typeface);
            if (formattedName.Width > WidthOfName)
            {
                WidthOfName = formattedName.Width;
            }
            var formattedCity = GetFormattedText(item.City, typeface);
            if (formattedCity.Width > WidthOfCity)
            {
                WidthOfCity = formattedCity.Width;
            }
        }


        private Typeface GetTypeface()
        {
            return new Typeface(new FontFamily("新細明體"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
        }

        private FormattedText GetFormattedText(string text, Typeface typeface)
        {
            FormattedText formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, 12.0, Brushes.Black, 1.0);
            return formattedText;
        }
        #endregion
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
