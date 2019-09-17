using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Using
{
    class Person : IDisposable
    {
        public string head;
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //var i = 0;
            //Console.WriteLine("i before using " + i);
            using (Person person = new Person())
            {
                person.head = "------ this head ------";
                //Console.WriteLine("this person head " + person.head);
                //i = 10;
                //Console.WriteLine("Inside using");
            }
            //Console.WriteLine("i after using " + i);
            //Console.WriteLine(i);
            /////////////////////////////////////////////////////////////////////////////////
            string connectionString = @"Data Source=.;Initial Catalog=ExpensesDB;Integrated Security=True;Pooling=False;uid=delivery; Password=.;";
            string sql = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);
                // Отображаем данные
                var myData = ds.Tables[0];
                foreach (DataRow dataRow in myData.Rows)
                {
                    string name = dataRow["Id"].ToString();
                    Console.WriteLine(name);
                }
                
            }
            /////////////////////////////////////////////////////////////////////////////////
            Console.Read();
        }
    }
}
