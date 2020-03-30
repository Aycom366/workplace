﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Net;
using System.Json;
using System.Text.Json;
namespace advanced_ckey
{
    public partial class Formlog : Form
    {
        public Formlog()
        {
            // don't show if already logged in
        //    MessageBox.Show( Properties.Settings.Default.auth_token );
            if( Properties.Settings.Default.auth_token != String.Empty )
            {

            //    this.initTimer();
            //    this.startLogging();
            //    return;
            }
            //  What do you think you are doing?
            InitializeComponent();

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

      

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtweb.Text == "workplace.comeriver.com")
            { 
            //    MessageBox.Show("Address Missing");
            //    txtweb.Focus();
            //    return;
            }
                if (txtname.Text == "e.g example@gmail.com")
                {
                    MessageBox.Show("Email Missing");
                    txtname.Focus();
                    return;
                }
                if (txtpass.Text == "******")
                {
                    MessageBox.Show("Password Missing");
                    txtpass.Focus();
                    return;
                }

            try
            {
                string URI = "https://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
                string myParameters = "email=" + txtname.Text + "&password=" + txtpass.Text;

                using (WebClient wc = new WebClient())
                {
                    Properties.Settings.Default.username = txtname.Text;
                    Properties.Settings.Default.password = txtpass.Text;
                    Properties.Settings.Default.weburl = txtweb.Text;
                    Properties.Settings.Default.Save();
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonResponse = wc.UploadString(URI, myParameters);  
                //    MessageBox.Show( jsonResponse ); 
                    dynamic result = JsonValue.Parse( jsonResponse );

                    try
                    {
                        if( result["user_id"] != "" )
                        {
                        //    MessageBox.Show( "Login Successful..." );
                            Properties.Settings.Default.auth_token = result["auth_token"];
                            Properties.Settings.Default.user_id = result["user_id"];
                            helper.iswaiting = false;
                            panel7.Show();
                            //this.Hide();
                         // this.Close();
                         
                          //  this.startLogging();

                        }
                        else
                        {
                            
                        }
                    }                     
                    catch (Exception)
                    {
                       // helper.iswaiting = false;
                        MessageBox.Show( result["badnews"] );
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
              //  helper.iswaiting = false;
                MessageBox.Show(ex.Message);
                return;
            }


        //    this.Hide();
        //    sign_in log = new sign_in();
        //    log.ShowDialog();
        //    this.Close(); 

        }

        private void startLogging()
        {
            Form1 keylog = new Form1();
            keylog.Show();
            keylog.Hide();
            timer1.Stop();
        }

        private void Formlog_Load(object sender, EventArgs e)
        {
            helper.iswaiting = true;
            startup();
           // Properties.Settings.Default.Reset();
            if (string.IsNullOrEmpty(Properties.Settings.Default.auth_token)){
                panel7.Hide();
               

            }

            else
            {
                panel7.Show();

                if (helper.islogged ==true)
                {
                    button2.Text = "CLOCK OUT";
                }
                else
                {
                    button2.Text = "CLOCK IN";
                }
                //the code for loading the workplaces can be shared here......



            }
            // if (Properties.Settings.Default.username != string.Empty && Properties.Settings.Default.password != string.Empty)
            // {
            // this.Hide();
            //sign_in log = new sign_in();
            //log.ShowDialog();
            // this.Close();
            //  }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("You Sure About Exit?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                // this.Close();
                Application.Exit();
            }
            
        }

        private void Panel2_Click(object sender, EventArgs e)
        {
            txturl.Focus();
        }

        private void Panel3_Click(object sender, EventArgs e)
        {
            txtuname.Focus();
        }

        private void Panel4_Click(object sender, EventArgs e)
        {
            txtpass.Focus();
        }     

        private void Txtpass_Enter_1(object sender, EventArgs e)
        {
            if(txtpass.Text == "******")
            {
                txtpass.Clear();
                txtpass.ForeColor = Color.Black;
            }
            if (txtpass.Text != "******")
            {
                txtpass.ForeColor = Color.Black;
            }
            else { txtpass.ForeColor = Color.Silver; }
          
        }

        private void Txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "******";
            }
            if (txtpass.Text != "******") { txtpass.ForeColor = Color.Black; } else { txtpass.ForeColor = Color.Silver; }
            
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true && txtpass.UseSystemPasswordChar == true)
            {
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
            }
         
        }

        private void Txtweb_Enter(object sender, EventArgs e)
        {

            if (txtweb.Text == "workplace.comeriver.com")
            {
                txtweb.Clear();
            }
            if (txtweb.Text != "workplace.comeriver.com")
            {
                txtweb.ForeColor = Color.Black;
            }
            else { txtweb.ForeColor = Color.Silver; }

        }

        private void Txtweb_Leave(object sender, EventArgs e)
        {

            if (txtweb.Text == "")
            {
                txtweb.Text = "workplace.comeriver.com";
            }
            if (txtweb.Text != "workplace.comeriver.com" ){ txtweb.ForeColor = Color.Black; } else { txtweb.ForeColor = Color.Silver; }
        }

        private void Txtname_Enter(object sender, EventArgs e)
        {
            if (txtname.Text == "e.g example@gmail.com")
            {
                txtname.Clear();
            }
            if (txtname.Text != "e.g example@gmail.com")
            {
                txtname.ForeColor = Color.Black;
            }
            else { txtname.ForeColor = Color.Silver; }

        }

        private void Txtname_Leave(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                txtname.Text = "e.g example@gmail.com";
            }
            if (txtname.Text != "e.g example@gmail.com" ){ txtname.ForeColor = Color.Black; } else { txtname.ForeColor = Color.Silver; }
        }

        private void Panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Panel6_Click(object sender, EventArgs e)
        {
            txtpass.Focus();
        }

        private void Panel5_Click(object sender, EventArgs e)
        {
            txtname.Focus();
        }

        private void Panel4_Click_1(object sender, EventArgs e)
        {
            txtweb.Focus();
        }

      public  void startup() 
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey StartupPath;
            StartupPath = rk.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (StartupPath.GetValue("Workplace") == null)
            {
                StartupPath.SetValue("Workplace", Application.ExecutablePath.ToString(), RegistryValueKind.ExpandString);
            }

            this.txtpass.Text = Properties.Settings.Default.password != string.Empty ? Properties.Settings.Default.password : "******";


            this.txtname.Text = Properties.Settings.Default.username != string.Empty ? Properties.Settings.Default.username : "e.g example@gmail.com";

            //RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //reg.SetValue("Workplace", Application.ExecutablePath.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // startup();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (helper.islogged == true)
            {
                helper.islogged = false;
            }
            else helper.islogged = true;
            this.Hide();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}

