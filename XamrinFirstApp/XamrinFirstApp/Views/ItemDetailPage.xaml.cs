using System.ComponentModel;
using Xamarin.Forms;
using XamrinFirstApp.ViewModels;

namespace XamrinFirstApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}