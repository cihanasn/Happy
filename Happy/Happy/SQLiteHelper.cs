using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Happy
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection con;
        public SQLiteHelper()
        {
            con = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HappySQLite.db3"));
            con.CreateTableAsync<Person>().Wait();
        }

        //Insert and Update
        public Task<int> SaveItemAsync(Person person)
        {
            if (person.Id != 0)
            {
                return con.UpdateAsync(person);
            }
            else
            {
                return con.InsertAsync(person);
            }
        }

        //Delete
        public Task<int> DeleteItemAsync(Person person)
        {
            return con.DeleteAsync(person);
        }

        //Read All Items
        public Task<List<Person>> GetItemsAsync()
        {
            return con.Table<Person>().ToListAsync();
        }


        //Read Item
        public Task<Person> GetItemAsync(int personId)
        {
            return con.Table<Person>().Where(i => i.Id == personId).FirstOrDefaultAsync();
        }
    }
}

