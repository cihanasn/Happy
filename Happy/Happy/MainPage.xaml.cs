using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Happy
{

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameEntry.Text))
            {
                await DisplayAlert("Zorunlu", "Lütfen İsim Giriniz!", "TAMAM");
            }
            else
            {
                Person p1 = new Person()
                {
                    Name = NameEntry.Text,

                };
                
                SQLiteHelper helper = new SQLiteHelper();

                await helper.SaveItemAsync(p1);
                NameEntry.Text = string.Empty;
                await DisplayAlert("Başarılı", "Kişi başarılı olarak eklenmiştir.", "TAMAM");

                App.Current.MainPage = new MainPage(); //To reset UI

            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Zorunlu", "Lütfen Id Giriniz", "TAMAM");

            }
            else
            {
                Person person = new Person()
                {
                    Id = Convert.ToInt32(txtId.Text),
                    Name = NameEntry.Text
                };

                SQLiteHelper helper = new SQLiteHelper();

                await helper.SaveItemAsync(person);

                txtId.Text = string.Empty;
                NameEntry.Text = string.Empty;
                await DisplayAlert("Başarılı", "Kişi başarılı olarak güncellenmiştir.", "TAMAM");

                App.Current.MainPage = new MainPage(); //To reset UI
            }
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                await DisplayAlert("Zorunlu", "Lütfen Id Giriniz", "TAMAM");
            }
            else
            {

                SQLiteHelper helper = new SQLiteHelper();

                var person = await helper.GetItemAsync(Convert.ToInt32(txtId.Text));
                if (person != null)
                {

                    await helper.DeleteItemAsync(person);
                    txtId.Text = string.Empty;
                    await DisplayAlert("Başarılı", "Kişi başarılı olarak silindi.", "TAMAM");

                    App.Current.MainPage = new MainPage(); //To reset UI
                }
            }
        }
    }
}
