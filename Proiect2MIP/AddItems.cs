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
    public partial class AddItems : Form
    {
        public AddItems()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                Shop sh = new Shop();
                sh.name = txtName.Text;
                sh.description = txtDescription.Text;
                sh.ReciveTime = dateTimePicker1.Value;
                sh.ExpiryDate = dateTimePicker2.Value;
                sh.quantity = (int)numericUpDown1.Value;
                try
                {
                    db.shops.Add(sh);
                    db.SaveChanges();
                    MessageBox.Show("Item added successfully!", "Succes!");
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
    }
}
