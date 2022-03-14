using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinFirstApp.Models;
using XamrinFirstApp.Services;

namespace XamrinFirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CityUpdate : ContentPage
    {
        public City currentCity { get; set; }
        public List<Country> countriesList { get; set; }
        public CityUpdate()
        {
            InitializeComponent();
        }

        public CityUpdate(City city)
        {
            InitializeComponent();
            currentCity = city;

            using (var appDbContext = new AppDbContext())
            {
                countriesList = appDbContext.Countries.ToList();
            }

            if (city.Id == 0)
            {
                SaveButton.Text = "Insert City";
                Idlabel.IsVisible = false;
            }
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            currentCity.Id = int.Parse(Idlabel.Text);
            currentCity.Name = NameBox.Text;
            currentCity.CountryId = ((Country)CountryPicker.SelectedItem).Id;
            using (var appDbContext = new AppDbContext())
            {
                if (currentCity.Id == 0)
                    appDbContext.Add(currentCity);
                else
                    appDbContext.Cities.Update(currentCity);
                appDbContext.SaveChanges();
            }
            await this.Navigation.PushAsync(new CitiesList());
        }
    }
}