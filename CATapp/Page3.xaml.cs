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
using System.Text;


namespace CATapp
{
    public partial class Page3 : PhoneApplicationPage, INotifyPropertyChanged
    {
        int lhr, lmn, lsc,ltotal;
        int ll;
        bool completed = false;

         // Because we have not specified a namespace, this
        // will be a System.Windows.Forms.Timer instance

        // Whether or not the timer is currently running
        DispatcherTimer dt = new DispatcherTimer();
        DateTime startDt = new DateTime();
        TimeSpan ts = new TimeSpan();
        
        
        private CatAppDataClasses catAppDB;
        public static string DBConnectionString = "Data Source=isostore:/catappdb.sdf";
   //     public static string DBConnectionString = "Data Source=C:/Users/shweta/Desktop/solprog1june/prog reporttry/CATapp/catappdb.sdf";
      //  public Int64 op;
        public Page3()
        {
            InitializeComponent();
            // Connect to the database and instantiate data context.
            catAppDB = new CatAppDataClasses(DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dt.Tick += new EventHandler(dt_Tick);
            startDt = DateTime.Now;
            dt.Start();
    


        }
        void dt_Tick(object sender, EventArgs e)
        {

            ts = DateTime.Now.Subtract(startDt);
            txtHour.Text = ts.Hours.ToString();
            txtMin.Text = ts.Minutes.ToString();
            txtSec.Text = ts.Seconds.ToString();

            if (txtHour.Text.Length == 1) txtHour.Text = "0" + txtHour.Text;
            if (txtMin.Text.Length == 1) txtMin.Text = "0" + txtMin.Text;
            if (txtSec.Text.Length == 1) txtSec.Text = "0" + txtSec.Text;

        }
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
        private ObservableCollection<Category> _cats;
          public ObservableCollection<Category> Categories
        {
            get
            {
                return _cats;
            }
            set
            {
                if (_cats != value)
                {
                    _cats = value;
                    NotifyPropertyChanged("Categories");
                }
            }
        }

          private ObservableCollection<Test> _tests;
          public ObservableCollection<Test> Tests
          {
              get
              {
                  return _tests;
              }
              set
              {
                  if (_tests != value)
                  {
                      _tests = value;
                      NotifyPropertyChanged("Tests");
                  }
              }
          }

          private ObservableCollection<Question_category> _quecats;
          public ObservableCollection<Question_category> Question_categories
          {
              get
              {
                  return _quecats;
              }
              set
              {
                  if (_quecats != value)
                  {
                      _quecats = value;
                      NotifyPropertyChanged("Question_categories");
                  }
              }
          }

         
          protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
          {
              IDictionary<string, string> x = this.NavigationContext.QueryString;
              String a = Convert.ToString(x["TextData"]);
              textBox1.Text = a.ToString();
             base.OnNavigatedTo(e);
             IDictionary<string, string> y = this.NavigationContext.QueryString;
             try
             {
                 String b = Convert.ToString(y["TextData1"]);
                 textBox2.Text = b.ToString();
                 String timeonoff = b.ToString();
                 long tmonoff;
                 long.TryParse(timeonoff, out tmonoff);
                 if (tmonoff == 0)
                 {
                     txtHour.Visibility = Visibility.Collapsed;
                     txtMin.Visibility = Visibility.Collapsed;
                     txtSec.Visibility = Visibility.Collapsed;
                     textBlock1.Visibility = Visibility.Collapsed;
                     textBlock3.Visibility = Visibility.Collapsed;
                 }
             }
             catch
             {
               //  MessageBox.Show("Timer is not available ");
                 txtHour.Visibility = Visibility.Collapsed;
                 txtMin.Visibility = Visibility.Collapsed;
                 txtSec.Visibility = Visibility.Collapsed;
                 textBlock1.Visibility = Visibility.Collapsed;
                 textBlock3.Visibility = Visibility.Collapsed;
             }
             base.OnNavigatedTo(e);
            /* string myparameter1Value = null;
             string myparameter2Value = null;

             NavigationContext.QueryString.TryGetValue("TextData", out myparameter1Value);
             NavigationContext.QueryString.TryGetValue("TextData1", out myparameter2Value);
              */
             
             
                 
          }
          
          
        
         //for displaying options and questions of specific category
          public void displaycatwise()
          {

              string s = textBox1.Text;
              long l;
              long.TryParse(s, out l);


              var q = (from Question qs1 in catAppDB.Questions
                       join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
                       where qs2.C_id == l
                       select qs1).Count();
              var q1 = from Question qs1 in catAppDB.Questions
                       join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
                       where qs2.C_id == l
                       select qs1;
              Questions = new ObservableCollection<Question>(q1);
 
  
             
              var v = Questions.ToList();
          
              int count = 1;
          
          
            //  long l1;

              for (int i = 0; i < q;i++ )
              {
                  PivotItem pi = new PivotItem();
                  pi.Header = string.Format("Q {0}", count);
                  Opt puc = new Opt();
                 // Question s2 = v[i].Header.Replace("<br />", "\n");
                //Question s3 = v[i].Content.Replace("<br />","\n");
                  // s2 = qq.Content.ToString();
                //Question s4 = s2 + s3;
                 // Question s5=s4.ToList();
                  puc.DataContext= v[i] as Question ;
                  pi.Content = puc;

                  pivot1.Items.Add(pi);
                  
                 
                  
                  count++;
              }
          
          }
          public void displayyearwise()
          {
              string s = textBox1.Text;
              long l;
              long.TryParse(s, out l);
              var q = (from Question qs1 in catAppDB.Questions
                       where qs1.T_id == l
                       select qs1).Count();
              var q1 = from Question qs1 in catAppDB.Questions
                       where qs1.T_id == l
                       select qs1;
              Questions = new ObservableCollection<Question>(q1);



              var v = Questions.ToList();

              int count = 1;
              //string s2,s3;
              //  long l1;

              for (int i = 0; i < q; i++)
              {
                  PivotItem pi = new PivotItem();
                  pi.Header = string.Format("Q {0}", count);
                  Opt puc = new Opt();
                  //  s2 = v[i].Header.ToString();
                  //s3 = v[i].Content.ToString();
                  // s2 = qq.Content.ToString();
                  puc.DataContext = v[i] as Question;
                  pi.Content = puc;

                  pivot1.Items.Add(pi);


                  count++;


              }

             

              //    MessageBox.Show("record Added Successfully!!!");



                //  StringBuilder strBuilder = new StringBuilder();
                  //strBuilder.AppendLine("Attempt Details");


                  //IList<Attempt> AttemptList = this.GetAttemptList();
                  //foreach (Attempt att1 in AttemptList)
                  //{
                  //    // strBuilder.AppendLine("AID" + "UID - " + att1.U_id + "TID-" + att1.T_id + "timestamp-" + att1.Timpstamp);
                  //    strBuilder.AppendLine("TID-" + att1.T_id +"UID"+ att1.U_id + "timestamp-" + att1.Timpstamp);
                  //}
                  //MessageBox.Show(strBuilder.ToString());

              
            
           
               
            
              
          }
      
         ////query for fetching options
         // public void fun1(long qid,long cid)
         // {
              
         //     var v = from Option op in catAppDB.Options
         //             join Question_category qc in catAppDB.Question_categories on op.Q_id equals qc.Q_id
         //             where op.Q_id == qid && qc.C_id == cid && op.Correct==true
         //             select op;
         //     Options = new ObservableCollection<Option>(v);
         // }
         ////query for question
         // public void fun2(long cid,long qid)
         // {
         //     var q1 = from Question qs1 in catAppDB.Questions
         //              join Question_category qs2 in catAppDB.Question_categories on qs1._id equals qs2.Q_id
         //              where qs2.C_id == cid && qs1._id == qid 
         //              select qs1;
         //     Questions = new ObservableCollection<Question>(q1);

         // }


          private void TestSubmit_Click(object sender, RoutedEventArgs e)
          {
             // Int32 hr, min, sec, total;
              completed = true;
              pivot1.Visibility = Visibility.Collapsed;

              double correct = 0.00, incorrect = 0.00, unanswered = 0.00;
              var collection = pivot1.Items;

              string shr = txtHour.Text.ToString();
              int.TryParse(shr, out lhr);
              lhr = lhr * 3600;
              string smn = txtMin.Text.ToString();
              int.TryParse(smn, out lmn);
              lmn = lmn * 60;
              string ssc = txtSec.Text.ToString();
              int.TryParse(ssc, out lsc);
              ltotal = lhr + lmn + lsc;

              for (int i = 0; i < collection.Count; i++)
              {
                  if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect == null)
                  {
                      unanswered++;
                  }
                  else if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect.Equals("true"))
                  {
                      correct++;

                      //   correct = correct - 0.25;


                  }
                  else if (((collection[i] as PivotItem).Content as Opt).IsAnswerCorrect.Equals("false"))
                  {

                      incorrect++;
                  }


              }

              var correctscore = correct * 1;
              var incorrectscore = incorrect * 0.25;
              var score = correctscore - incorrectscore;


              txtCorrect.Text = string.Format("Correct = {0}", correct);
              txtInCorrect.Text = string.Format("Incorrect = {0}", incorrect);
              txtUnanswered.Text = string.Format("Unanswered = {0}", unanswered);
              txtScore.Text = string.Format("Score={0}", score);
              //LayoutRoot.Visibility = Visibility.Collapsed;
              MessageGrid.Visibility = Visibility.Visible;
              //     listBox1.Visibility = Visibility.Visible;
              // NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.Relative));
              submit.Visibility = Visibility.Collapsed;
              txtHour.Visibility = Visibility.Collapsed;
              txtMin.Visibility = Visibility.Collapsed;
              txtSec.Visibility = Visibility.Collapsed;
              textBlock1.Visibility = Visibility.Collapsed;
              textBlock3.Visibility = Visibility.Collapsed;

              //hr = (Convert.ToInt32(txtHour.Text)) * 3600;
              //min = (Convert.ToInt32(txtMin.Text)) * 60;
              //sec = Convert.ToInt32(txtSec.Text);
              //total = hr + min + sec;
             // long tid = 0;
         //     Boolean Tm1;
           //   Int32 tmstp = 0;

              //we calculate values like time elapsed etc for inserting in attempts table
              //using (catAppDB = new CatAppDataContext(DBConnectionString))
              //{
              //    IList<User> UserList = this.GetUserList();
                  /*   foreach (User user in UserList)
                     {
                         u1 = 10;
                         MessageBox.Show("ID"+ u1);
                          strBuilder.AppendLine("AID - " + att._id + " UID - " + att.U_id + "TID-" + att.T_id + "timestamp-" + att.Timpstamp + "complete" + att.Complete + "elapsed time" + att.Elapsed_time + "timed" + att.Timed );
                     }*/
               /*   if (tmval == 1)
                      Tm1 = true;
                  else
                      Tm1 = false;
                  //    */       
                  //IList<Test> TestList = this.GetTestList();
                  //foreach (Test ts1 in TestList)
                  //{
                  //    tid = ts1._id;
                  //    //      MessageBox.Show("TID" + tid);
                  //    tmstp = ts1.Allowed_time;//
                  //    //    MessageBox.Show("timestamp" + tmstp);


                  //    //storing above test detail in attempts


                  //    }
                 
                  
              //}
          }
          private void OK_Click(object sender, RoutedEventArgs e)
          {

              IDictionary<string, string> y = this.NavigationContext.QueryString;
                String b = Convert.ToString(y["TextData1"]);
                 textBox2.Text = b.ToString();
                 MessageBox.Show("b" + textBox2.Text);
                 String timeonoff = b.ToString();
                 long tmonoff;
                 long.TryParse(timeonoff, out tmonoff);
              bool tmd;
              if (tmonoff == 0)
              {
                  tmd = false;

              }
              else
              {
                  tmd = true;
              }
              string s = textBox1.Text;
              long l;
              long.TryParse(s, out l);
              var timest = from Test t in catAppDB.Tests
                           where t._id == l
                           select t;
              Tests = new ObservableCollection<Test>(timest);
              var time = Tests.ToList();
              var t2 = time[0] as Test;

              var tt = t2.Allowed_time;
              txtAllowed.Text = tt.ToString();

              // txtAllowed.Text = Tests.ToString();




              // MessageBox.Show("Allowed Time" + txtAllowed.Text);
              //var tt = Tests.ToList();
              // int ts = tt[0].Allowed_time;
              //  txtAllowed.Text = Tests.ToString();
              string tmpstp = txtAllowed.Text;
              int l1;
              long uid = 0;
              int.TryParse(tmpstp, out l1);
              ll = l1;
            

              IList<User> UserList = this.GetUserList();
              foreach (User usr1 in UserList)
              {
                  uid = usr1._id;
              }

              using (catAppDB = new CatAppDataClasses(DBConnectionString))
              {
                  Attempt newAttempt = new Attempt
                 {
                     T_id = l,
                     Timpstamp = l1,
                     Elapsed_time=l1-ltotal,
                     U_id = uid,

                     //T_id = 13,
                     //Timpstamp = 2100,

                     Complete = completed,
                     //Elapsed_time = 200,
                     Timed = tmd

                 };


                  catAppDB.Attempts.InsertOnSubmit(newAttempt);

                  catAppDB.SubmitChanges();

                  //DialogClosed("OK");
              }
          }

          public IList<User> GetUserList()
          {
              // Fetching data from local database
              IList<User> UserList = null;
              using (catAppDB = new CatAppDataClasses(DBConnectionString))
              {
                  IQueryable<User> UserQuery = from Us in catAppDB.Users select Us;
                  UserList = UserQuery.ToList();
              }
              return UserList;
          }

          public IList<Test> GetTestList()
          {
              // Fetching data from local database
              IList<Test> TestList = null;
              using (catAppDB = new CatAppDataClasses(DBConnectionString))
              {
                  IQueryable<Test> TestQuery = from Ts in catAppDB.Tests select Ts;
                  TestList = TestQuery.ToList();
              }
              return TestList;
          }

          public IList<Attempt> GetAttemptList()
          {
              // Fetching data from local database
              IList<Attempt> AttemptList = null;
              using (catAppDB = new CatAppDataClasses(DBConnectionString))
              {
                  IQueryable<Attempt> AttemptQuery = from attempt1 in catAppDB.Attempts select attempt1;
                  AttemptList = AttemptQuery.ToList();
              }
              return AttemptList;
          }
    

          private void Cancel_Click(object sender, RoutedEventArgs e)
          {
              
           //   DialogClosed("Cancel");
          }

          private void DialogClosed(string ClosedBy)
          {
              txtCorrect.Text = string.Empty;
              txtInCorrect.Text = string.Empty;
              txtUnanswered.Text = string.Empty;

              if (ClosedBy.Equals("OK"))
              {
                  //Submit
                  MessageBox.Show("Submitted successfully");
                  NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
                  //  Messenger.Default.Send<string>("Category");
              }
              else
              {
                  //Cancel
              }
          }

         
          private void catwise_Loaded(object sender, RoutedEventArgs e)
          {
              displaycatwise();
          }
        
             
          
      

          private void yearwise_Loaded(object sender, RoutedEventArgs e)
          {
              noque nq = new noque();
              Opt op = new Opt();
              string st = nq.button2.Content.ToString();
              string st2 = "ON";
              if (st.Equals(st2))
              {

                    displayyearwise();
                    
              }
              
          
              else if (!(st.Equals(st2)))
              {
                  displayyearwise();
                    
               }
              
              //displayyearwise();
          }

        
      
      
    }
}