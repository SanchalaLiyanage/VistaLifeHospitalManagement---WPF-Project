using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VistaLife.View;

namespace VistaLife.Resource
{
    /// <summary>
    /// Interaction logic for InPatient.xaml
    /// </summary>
    public partial class InPatient : Window
    {
        public InPatient()
        {
            InitializeComponent();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Menue menue = new Menue();
            menue.ShowDialog();
            this.Close();
        }

        private void btnAddpatient_Click(object sender, RoutedEventArgs e)
        {
            InPatient inPatient = new InPatient();
            inPatient.ShowDialog();
            this.Close();
        }

        private void btnChannel_Click(object sender, RoutedEventArgs e)
        {
            OutPatient outPatient = new OutPatient();
            outPatient.ShowDialog();
            this.Close();
        }

        private void BtnPDetails_Click(object sender, RoutedEventArgs e)
        {
            ViewPatient viewPatient = new ViewPatient();
            viewPatient.ShowDialog();
            this.Close();
        }

        private void BtnStaffDetails_Click(object sender, RoutedEventArgs e)
        {
            View_Staff viewStaff = new View_Staff();
            viewStaff.ShowDialog();
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.ShowDialog();
            this.Close();
        }

        
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtGurdianNIC.Text = "";
            txtGurdinName.Text = "";
            txtPAddress.Text = "";
            txtPadmittedDate.Text = "";
            txtPage.Text = "";
            txtPATIENTname.Text = "";
            txtPbloodtype.Text = "";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            String message = "";
            if(String.IsNullOrEmpty(txtPATIENTname.Text))
            {
                flag = 1;
                message = "Patient Name is Empty";
            }
            if (String.IsNullOrEmpty(txtPage.Text))
            {
                flag = 1;
                message = "Patient Age is Empty";
            }
            if (String.IsNullOrEmpty(txtPbloodtype.Text))
            {
                flag = 1;
                message = "Patient Blood Type is Empty";
            }
            if (String.IsNullOrEmpty(txtPAddress.Text))
            {
                flag = 1;
                message = "Patient Address is Empty";
            }
            if (String.IsNullOrEmpty(txtPadmittedDate.Text))
            {
                flag = 1;
                message = "Patient Admitted Date is Empty";
            }
            if (String.IsNullOrEmpty(txtGurdianNIC.Text))
            {
                flag = 1;
                message = "Gurdian NIC is Empty";
            }
            if (String.IsNullOrEmpty(txtGurdinName.Text))
            {
                flag = 1;
                message = "Gurdian Name is Empty";
            }
            if (flag == 1)
            {
                MessageBox.Show(message);
            }
            else
            {

                Connectioncs ConObj = new Connectioncs();
                SqlConnection Con = ConObj.GetDBCon();

                string sqlRead = $"SELECT D_ID from Ward where W_Name='{txtGurdianNIC.Text}'";

                SqlCommand cmdobj1 = new SqlCommand(sqlRead, Con);
                SqlDataReader reader = cmdobj1.ExecuteReader();
                String rr = "";
                if (reader.Read())
                {
                    rr = reader.GetString(0);
                }
                reader.Close();

                IDGenerator id = new IDGenerator();
                string opid = id.GenerateID("OP_ID");
                String pid = id.GenerateID("P_ID");
                String ipid = id.GenerateID("AP_ID");
                MessageBox.Show("Your Patient's ID = " + pid);


                string query1 = "INSERT INTO patient (P_ID,P_Name,P_Age,P_Address) VALUES('" + pid + "','" + txtPATIENTname.Text + "','" + txtPage.Text + "','" + txtPAddress.Text + "')";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();

                string query2 = "INSERT INTO InPatient (IP_ID,IP_Blood_Type,IP_Guardian_NameIP_Guardian_NIC,IP_Admitted_Date,P_ID) VALUES('" + ipid + "','" + txtPbloodtype.Text + "','" + txtGurdinName.Text + "','" + rr + "','" + txtPadmittedDate.Text + "','" + pid + "')";
                SqlCommand cmd2 = new SqlCommand(query2, Con);
                cmd2.ExecuteNonQuery();

                
                MessageBox.Show("Data Saved Successfully");
            }
             
        }
    }
}

