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
using System.Windows.Threading;

namespace CATapp
{
    
    public partial class Opt : PhoneApplicationPage,INotifyPropertyChanged
    {
        
        public string IsAnswerCorrect = null;
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
        public Opt()
        {
            InitializeComponent();
            // Connect to the database and instantiate data context.
            catAppDB = new CatAppDataClasses(DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

           /* dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dt.Tick += new EventHandler(dt_Tick);
            startDt = DateTime.Now;
            dt.Start();*/

        }
        /*void dt_Tick(object sender, EventArgs e)
        {

            ts = DateTime.Now.Subtract(startDt);
            txtHour.Text = ts.Hours.ToString();
            txtMin.Text = ts.Minutes.ToString();
            txtSec.Text = ts.Seconds.ToString();

            if (txtHour.Text.Length == 1) txtHour.Text = "0" + txtHour.Text;
            if (txtMin.Text.Length == 1) txtMin.Text = "0" + txtMin.Text;
            if (txtSec.Text.Length == 1) txtSec.Text = "0" + txtSec.Text;

        }
     */
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private ObservableCollection<Question> _ques;
        public ObservableCollection<Question> Questions
        {
            get
            {
                return _ques;
            }
            set
            {
                if (_ques != value)
                {
                    _ques = value;
                    NotifyPropertyChanged("Questions");
                }
            }
        }
        private ObservableCollection<Option> _opts;
        public ObservableCollection<Option> Options
        {
            get
            {
                return _opts;
            }
            set
            {
                if (_opts != value)
                {
                    _opts = value;
                    NotifyPropertyChanged("Options");
                }
            }
        }



        private void radioButton1_Click_1(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            
            //Boolean b = true;

            //if (radio.Tag.ToString().Equals(b.ToString()))
            //{
            //    IsAnswerCorrect = "true";
            //   // radio.IsChecked = true;
            //}
            //else
            //{
            //    IsAnswerCorrect = "false";
            //    //radio.IsChecked = false;
            //}
            string id = txtquesID.Text.ToString();
            long qid;
            long.TryParse(id, out qid);
            var opp = (from Option opt in catAppDB.Options where opt.Q_id==qid
                      select opt).Count();


            var opp1 = from Option opt in catAppDB.Options
                       where opt.Q_id == qid
                       select opt;

           var opt1list = opp1.ToList();

           for (int i = 0; i < opp; i++)
           {
              
               if (radio.IsChecked == true)
               {
                   txtoptid.Text = "";
                   var opt2 = opt1list[i] as Option;
                   var opt3 = opt2._id;
                   txtoptid.Text = opt3.ToString();
               }
           }


            //MessageBox.Show("Optid" + txtoptid.Text);
          //  txtAllowed.Text = tt.ToString();

         /*   using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                Attempt_detail newAttempt_detail = new Attempt_detail
                {
                   

                };


                catAppDB.Attempt_details.InsertOnSubmit(newAttempt_detail);

                catAppDB.SubmitChanges();

                //DialogClosed("OK");
            }*/

        }
        public IList<Attempt_detail> GetAttempt_detailList()
        {
            // Fetching data from local database
            IList<Attempt_detail> Attempt_detailList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<Attempt_detail> Attempt_detailQuery = from attempt1 in catAppDB.Attempt_details select attempt1;
                Attempt_detailList = Attempt_detailQuery.ToList();
            }
            return Attempt_detailList;
        }

        public IList<Option> GetOptionList()
        {
            // Fetching data from local database
            IList<Option> OptionList = null;
            using (catAppDB = new CatAppDataClasses(DBConnectionString))
            {
                IQueryable<Option> OptionQuery = from option1 in catAppDB.Options select option1;
                OptionList = OptionQuery.ToList();
            }
            return OptionList;
        }
      
      

        private void optListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }

        //private void scrollViewer1_TextInput(object sender, TextCompositionEventArgs e)
        //{

        //}

        //private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}


     
       
       

      
       
    }
}