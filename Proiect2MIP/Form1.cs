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
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string password, username;

            password = txtPassword.Text;
            username = txtUsername.Text;

            using (UserDbContext db = new UserDbContext())
            {
                bool a = true;
                var user = from u in db.Users
                           where u.Username == username && u.Password == password
                           select u;
                foreach (var item in user)
                {
                    MessageBox.Show("Logat", "Succes");
                    LoginState.username = txtUsername.Text;
                    ShopInterface fShop = new ShopInterface();
                    this.Hide();
                    fShop.Show();
                    a = false;

                }
                if (a)
                    MessageBox.Show("Username and Password do not match!");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp fSignUp = new SignUp();
            this.Hide();
            fSignUp.Show();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string password, username;
            password = txtPassword.Text;
            username = txtUsername.Text;
            using (UserDbContext db = new UserDbContext())
            {
                bool a = false;
                var user = from u in db.Users
                           where u.Username == username && u.Password == password
                           select u;
                foreach (var item in user)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    a = true;

                }
                if (a)
                {
                    db.SaveChanges();
                    MessageBox.Show("User deleted succesfully.", "Succes");
                }
                else
                {
                    MessageBox.Show("Data provided does not match an user!");
                }
            }
        }
    }
}
