using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinFirstApp.Models;
using XamrinFirstApp.Services;

namespace XamrinFirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryUpdate : ContentPage
    {
        public Country currentCountry { get; set; }
        public CountryUpdate()
        {
            InitializeComponent();
        }

        public CountryUpdate(Country country)
        {
            InitializeComponent();
            currentCountry = country;
            if (country.Id == 0)
            {
                this.Title = "Insert Country";
                SaveButton.Text = "Insert Country";
                Idlabel.IsVisible = false;
                DTlabel.IsVisible = false;
            }
            else
            {
                this.Title = "Update Country";
            }
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //currentCountry.Name = NameBox.Text;
            //currentCountry.ShortName = ShortNameBox.Text;

            using (var appDbContext = new AppDbContext())
            {
                if (currentCountry.Id == 0)
                {
                    currentCountry.CreationDateTime = DateTime.Now;
                    appDbContext.Add(currentCountry);
                }
                else
                    appDbContext.Countries.Update(currentCountry);

                appDbContext.SaveChanges();
            }
            await this.Navigation.PushAsync(new CountriesList());
        }
    }
}