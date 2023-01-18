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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proiect2MIP
{
    public partial class ShopInterface : Form
    {
        public ShopInterface()
        {
            InitializeComponent();
            using (ShopDbContext pdb = new ShopDbContext())
            {
                List<Shop> lst = pdb.shops.Where(o => o.quantity.Equals(0)).ToList();
                foreach (var d in lst)
                {
                    pdb.Entry(d).State = System.Data.Entity.EntityState.Deleted;
                }
                pdb.SaveChanges();

                var res = from s in pdb.shops
                          select new
                          {
                              s.id,
                              s.name,
                              s.description,
                              s.quantity
                          };
                dgvShop.DataSource = res.ToList();
            }
        }

        private void addNewItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shop pr = new Shop();
            pr.name = null;
            AddItems fAdd = new AddItems();
            fAdd.ShowDialog();
            this.Hide();
        }

        private void showCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UserDbContext db = new UserDbContext())
            {
                var res = from p in db.Users
                          select new
                          {
                              p.id,
                              p.LastName,
                              p.FirstName,
                              p.Username
                          };
                dgvShop.DataSource = res.ToList();
            }
        }

        private void showAviableItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                var res = from p in db.shops
                          select new
                          {
                              p.id,
                              p.name,
                              p.description,
                              p.quantity
                          };
                dgvShop.DataSource = res.ToList();
            }
        }

        private void salesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PurchaseDbContext pdb = new PurchaseDbContext())
            {
                var res = from s in pdb.purchases
                          select new
                          {
                              s.id,
                              s.user,
                              s.item,
                              s.quantity
                          };
                dgvShop.DataSource = res.ToList();
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            using (PurchaseDbContext db = new PurchaseDbContext())
            {
                int value;
                Purchase sh = new Purchase();
                sh.item = txtId.Text;
                int.TryParse(txtQuantity.Text, out value);
                sh.quantity = value;
                sh.user = LoginState.username;
                try
                {
                    db.purchases.Add(sh);
                    db.SaveChanges();
                    MessageBox.Show("Item successfully bought!", "Succes!");
                    using (ShopDbContext dt = new ShopDbContext())
                    {
                        var res = from u in dt.shops
                                  where u.name == txtId.Text
                                  select u;
                        Shop p = dt.shops.Where(o => o.name.Equals(sh.item)).FirstOrDefault();
                        p.quantity -= value;
                        dt.Entry(p).State = EntityState.Modified;
                        dt.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Faild to purchase!", "Error!");
                }
            }
        }

        private void changeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock fStock = new Stock();
            fStock.Show();
        }

        private void purchaseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PurchaseDbContext pdb = new PurchaseDbContext())
            {
                var res = from s in pdb.purchases
                          where s.user == LoginState.username
                          select new
                          {
                              s.id,
                              s.user,
                              s.item,
                              s.quantity
                          };
                dgvShop.DataSource = res.ToList();
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogIn form = new LogIn();
            form.Show();
            this.Close();
        }
    }
}
