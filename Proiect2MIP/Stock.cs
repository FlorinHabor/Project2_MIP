using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect2MIP
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Id, quantity;
            int.TryParse(textBox1.Text, out Id);
            int.TryParse(textBox2.Text, out quantity);

            using (ShopDbContext db = new ShopDbContext())
            {
                var res = from u in db.shops
                           where u.id == Id
                           select u;
                Shop p = db.shops.Where(o => o.id.Equals(Id)).FirstOrDefault();
                p.quantity += quantity;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Successfully added!");
                this.Close();
            }
        }
    }
}
