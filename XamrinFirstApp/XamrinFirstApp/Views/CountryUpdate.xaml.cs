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
                SaveButton.Text = "Insert Country";
                Idlabel.IsVisible = false;
                DTlabel.IsVisible = false;
            }
            BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            currentCountry.Id = int.Parse(Idlabel.Text);
            currentCountry.Name = NameBox.Text;
            currentCountry.Name = NameBox.Text;
            currentCountry.CreationDateTime = DateTime.Now;
            using (var appDbContext = new AppDbContext())
            {
                if (currentCountry.Id == 0)
                    appDbContext.Add(currentCountry);
                else
                    appDbContext.Countries.Update(currentCountry);
                appDbContext.SaveChanges();
            }
            await this.Navigation.PushAsync(new CountriesList());
        }
    }
}