using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Threading;

namespace CATapp
{
    public partial class datainsertion : PhoneApplicationPage
    {

        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";

        public datainsertion()
        {
            InitializeComponent();
            catAppDB = new CatAppDataClasses(DBConnectionString);

            this.DataContext = this;

        }
        private ObservableCollection<Category> _attempt_details;
        public ObservableCollection<Category> attempt_details
        {
            get
            {
                return _attempt_details;
            }
            set
            {
                if (_attempt_details != value)
                {
                    _attempt_details = value;
                    //NotifyPropertyChanged("attempt_details");
                }
            }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {

         //   string data = textBox1.Text;

          /*  cmd.CommandType = CommandType.Text;
            cmd.CommandText = _InsertRow;
            cmd.Parameters.Add(new SqlParameter("@FirstName", MySqlDbType.VarChar, 50)).Value = txtFName.Text;
            cmd.Parameters.Add(new SqlParameter("@LastName", MySqlDbType.VarChar, 50)).Value = txtLName.Text;
            */
            /*
            
            datainsertion db = new datainsertion
            {
                _q_id.convert.ToInt32(textBox1.Text.ToString()),
                 int o_id=Convert.ToInt32(textBox1.Text.ToString()),
                int y=1;
            };
            catAppDB.Attempt_details.InsertOnSubmit(db);
            catAppDB.SubmitChanges();
            MessageBox.Show("Record inserted successfully");
            */
           /* 
            catappdb.Employees.InsertOnSubmit(newEmployee);
            Empdb.SubmitChanges();
            MessageBox.Show("Employee Added Successfully!!!");
            */
            
           /* ' using (EmployeeDataContext Empdb = new EmployeeDataContext(strConnectionString))
            {
                Employee newEmployee = new Employee
                {
                    EmployeeID = Convert.ToInt32(txtEmpid.Text.ToString()),
                    EmployeeAge = txtAge.Text.ToString(),
                    EmployeeName = txtName.Text.ToString()
                };

                Empdb.Employees.InsertOnSubmit(newEmployee);
                Empdb.SubmitChanges();
                MessageBox.Show("Employee Added Successfully!!!");
            }*/
        
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}