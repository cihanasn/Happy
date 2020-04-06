using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Happy
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.GetList();
        }

        public List<Person> List { get; set; }

        private void GetList()
        {
            if (this.List == null)
                this.List = new List<Person>();

            SQLiteHelper helper = new SQLiteHelper();

            Task<List<Person>> personList = helper.GetItemsAsync();
            personList.Wait();
            this.List = personList.Result;
            if (personList.Result.Count > 0)
            {
                this.List = personList.Result;
            }
        }
    }
}
