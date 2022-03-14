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
    public partial class CountriesList : ContentPage
    {
        public BindingList<Country> countries;
        public CountriesList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var appDbContext = new AppDbContext())
            {
                countries = new BindingList<Country>(appDbContext.Countries.ToList());
                countriesList.ItemsSource = countries;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Country country = new Country();
            await this.Navigation.PushAsync(new CountryUpdate(country));
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            using (var appDbContext = new AppDbContext())
            {
                var view = sender as SwipeItem;
                var item = view.BindingContext as Country;

                appDbContext.Countries.Remove(item);
                appDbContext.SaveChanges();
                OnAppearing();
            }
        }

        private async void countriesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Country country = (Country)countriesList.SelectedItem;
            string result = await DisplayActionSheet("إختار الوظيفة", "تراجع", null, new string[] { "تعديل", "حذف" });

            if (result == "تعديل")
            {
                using (var appDbContext = new AppDbContext())
                {
                    await this.Navigation.PushAsync(new CountryUpdate(country));
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
                        appDbContext.Countries.Remove(country);
                        appDbContext.SaveChanges();
                    }
                    OnAppearing();
                }
            }
        }
    }
}