using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (!countries.Any())
                {
                    appDbContext.Countries.Add(new Country() { Name = "Libya", ShortName = "LY" });
                    appDbContext.Countries.Add(new Country() { Name = "Tunis", ShortName = "TN" });
                    appDbContext.SaveChanges();

                    countries = new BindingList<Country>(appDbContext.Countries.ToList());
                }

                collectionView.ItemsSource = countries;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            using (var appDbContext = new AppDbContext())
            {
                appDbContext.Countries.Add(new Country() { Name = Namebox.Text, ShortName = ShortNameBox.Text });
                appDbContext.SaveChanges();
            }
            OnAppearing();
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
    }
}