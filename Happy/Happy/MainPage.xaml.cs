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
    }
}
