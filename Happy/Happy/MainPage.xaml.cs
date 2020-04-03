using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            SQLiteHelper helper = new SQLiteHelper();

            

            //Get All Persons
            var personList = await helper.GetItemsAsync();
            if (personList.Count > 0)
            {
                lstPersons.ItemsSource = personList;
            }
            else
            {
                //Add two man to DB
                Person p1 = new Person();
                p1.Name = "Cihan";
                await helper.SaveItemAsync(p1);

                Person p2 = new Person();
                p2.Name = "Beste";
                await helper.SaveItemAsync(p2);

                personList.Add(p1);
                personList.Add(p2);
                lstPersons.ItemsSource = personList;
            }

           
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                await DisplayAlert("Zorunlu", "Lütfen İsim Giriniz!", "TAMAM");            
            }
            else
            {
                Person p1 = new Person()
                {
                    Name = txtName.Text
                };

                SQLiteHelper helper = new SQLiteHelper();
               
                await helper.SaveItemAsync(p1);
                txtName.Text = string.Empty;
                await DisplayAlert("Başarılı", "Kişi başarılı olarak eklenmiştir.", "TAMAM");
                
                var personList = await helper.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }
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
                    Name = txtName.Text
                };

                SQLiteHelper helper = new SQLiteHelper();

                await helper.SaveItemAsync(person);

                txtId.Text = string.Empty;
                txtName.Text = string.Empty;
                await DisplayAlert("Başarılı", "Kişi başarılı olarak güncellenmiştir.", "TAMAM");

                var personList = await helper.GetItemsAsync();
                if (personList != null)
                {
                    lstPersons.ItemsSource = personList;
                }
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

                    var personList = await helper.GetItemsAsync();
                    if (personList != null)
                    {
                        lstPersons.ItemsSource = personList;
                    }
                }
            }
        }
    }
}
