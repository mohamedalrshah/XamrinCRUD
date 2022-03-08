using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamrinFirstApp.Models;
using XamrinFirstApp.Services;

namespace XamrinFirstApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlogUpdate : ContentPage
    {
        public Blog currentBlog;
        public BlogUpdate()
        {
            InitializeComponent();
        }

        public BlogUpdate(Blog blog)
        {
            InitializeComponent();
            currentBlog = blog;
            if (blog.Id == 0)
            {
                SaveButton.Text = "Insert Blog";
                Idlabel.IsVisible = false;
            }
            BindingContext = currentBlog;
            //Idlabel.Text = blog.Id.ToString();
            //UrlBox.Text = blog.Url;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            currentBlog.Id = int.Parse(Idlabel.Text);
            currentBlog.Url = UrlBox.Text;
            using (var appDbContext = new AppDbContext())
            {
                if (currentBlog.Id == 0)
                    appDbContext.Add(currentBlog);
                else
                    appDbContext.Blogs.Update(currentBlog);
                appDbContext.SaveChanges();
            }
            await this.Navigation.PushAsync(new BlogList());
        }
    }
}