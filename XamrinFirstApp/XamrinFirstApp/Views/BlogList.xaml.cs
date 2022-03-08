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
    public partial class BlogList : ContentPage
    {
        public BindingList<Blog> blogs;
        public BlogList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (var appDbContext = new AppDbContext())
            {
                blogs = new BindingList<Blog>(appDbContext.Blogs.ToList());
                if (!blogs.Any())
                {
                    appDbContext.Blogs.Add(new Blog() { Id = 1, Url = "FIRST URL" });
                    appDbContext.Blogs.Add(new Blog() { Id = 2, Url = "SECOND URL" });
                    appDbContext.SaveChanges();
                    blogs = new BindingList<Blog>(appDbContext.Blogs.ToList());
                }

                blogsList.ItemsSource = blogs;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Blog blog = new Blog();
            await this.Navigation.PushAsync(new BlogUpdate(blog));
        }


        private async void blogsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Blog blog = (Blog)blogsList.SelectedItem;
            //Blog b = (Blog)e.CurrentSelection.First();
            string result = await DisplayActionSheet("إختار الوظيفة", "تراجع", null, new string[] { "Update", "Delete" });

            if (result == "Update")
            {
                using (var appDbContext = new AppDbContext())
                {
                    await this.Navigation.PushAsync(new BlogUpdate(blog));
                }
            }
            else if (result == "Delete")
            {
                string deleteResult = await DisplayActionSheet("هل أنت متأكد من الحذف؟", null, null,
                    new string[] { "Yes", "No" });

                if (deleteResult == "Yes")
                {
                    using (var appDbContext = new AppDbContext())
                    {
                        appDbContext.Blogs.Remove(blog);
                        appDbContext.SaveChanges();
                    }
                    OnAppearing();
                }
            }
        }
    }
}