using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect2MIP
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPassword.Text)
            {
                txtPassword2.Text = "";
                txtPassword.Text = "";
                MessageBox.Show("Passwords don't match!", "Error!");
            }
            else
            {
                if(UserExists(txtUsername.Text) != true)
                {
                    using (UserDbContext db = new UserDbContext())
                    {
                        User u = new User();
                        u.LastName = txtLastName.Text;
                        u.FirstName = txtFirstName.Text;
                        u.Username = txtUsername.Text;
                        u.Password = txtPassword.Text;
                        try
                        {
                            db.Users.Add(u);
                            db.SaveChanges();
                            MessageBox.Show("User added successfully!", "Succes!");
                            LoginState.username = txtUsername.Text;
                            ShopInterface fShop = new ShopInterface();
                            this.Close();
                            fShop.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("One or more fields are incorrect!", "Error!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("User already exists!");
                }
            }
        }
        private bool UserExists(string username)
        {
            using (UserDbContext ut = new UserDbContext())
            {
                var rez = from u in ut.Users
                          where u.Username == txtUsername.Text
                          select new { u.Username };
                try
                {
                    if(rez.First()!= null)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;

        }
    }
}
