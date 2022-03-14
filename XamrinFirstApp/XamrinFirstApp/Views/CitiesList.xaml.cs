using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinFirstApp.Models;
using XamrinFirstApp.Services;

namespace XamrinFirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CitiesList : ContentPage
    {
        public BindingList<City> cities;
        public CitiesList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var appDbContext = new AppDbContext())
            {
                cities = new BindingList<City>(appDbContext.Cities.Include(c => c.Country).ToList());
                citiesList.ItemsSource = cities;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            City city = new City();
            await this.Navigation.PushAsync(new CityUpdate(city));
        }


        private async void citiesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            City city = (City)citiesList.SelectedItem;
            string result = await DisplayActionSheet("إختار الوظيفة", "تراجع", null, new string[] { "تعديل", "حذف" });

            if (result == "تعديل")
            {
                using (var appDbContext = new AppDbContext())
                {
                    await this.Navigation.PushAsync(new CityUpdate(city));
                }
            }
            else if (result == "حذف")
            {
                string deleteResult = await DisplayActionSheet("هل أنت متأكد من الحذف؟", null, null,
                    new string[] { "نعم", "لا" });

                if (deleteResult == "نعم")
                {
                    using (var appDbContext = new AppDbContext())
                    {
                        appDbContext.Cities.Remove(city);
                        appDbContext.SaveChanges();
                    }
                    OnAppearing();
                }
            }
        }
    }
}