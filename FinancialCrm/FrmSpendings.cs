using FinancialCrm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinancialCrm
{
    public partial class FrmSpendings : Form
    {
        public FrmSpendings()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();
        private void FrmSpendings_Load(object sender, EventArgs e)
        {
            var totalSpending = db.Spendings.Sum(x => x.SpendingAmount);
            lblTotalSpending.Text = totalSpending.ToString() + " ₺";

            var mostSpentCategory = (from s in db.Spendings
                                     group s by s.CategoryId into g
                                     orderby g.Sum(s => s.SpendingAmount) descending
                                     select new
                                     {
                                         CategoryId = g.Key,
                                         TotalAmount = g.Sum(s => s.SpendingAmount)
                                     }).FirstOrDefault();
            if (mostSpentCategory != null)
            {
                // Kategori adını almak için Categories tablosunu sorgula
                var categoryName = db.Categories
                                     .Where(c => c.CategoryId == mostSpentCategory.CategoryId)
                                     .Select(c => c.CategoryName)
                                     .FirstOrDefault();

                lblMaxCategory.Text = $"{categoryName}";
            }



            // En az harcama yapılan kategoriyi bul
            var leastSpentCategory = (from s in db.Spendings
                                      group s by s.CategoryId into g
                                      orderby g.Sum(s => s.SpendingAmount) ascending // Küçükten büyüğe sırala
                                      select new
                                      {
                                          CategoryId = g.Key,
                                          TotalAmount = g.Sum(s => s.SpendingAmount)
                                      }).FirstOrDefault();

            if (leastSpentCategory != null)
            {
                // Kategori adını almak için Categories tablosunu sorgula
                var categoryName = db.Categories
                                     .Where(c => c.CategoryId == leastSpentCategory.CategoryId)
                                     .Select(c => c.CategoryName)
                                     .FirstOrDefault();

                // Sonucu label'a yaz
                lblMinCategory.Text = $"{categoryName}";
            }
            else
            {
                lblMinCategory.Text = "Harcama verisi bulunamadı.";
            }

            //chart kodları
            // Spending tablosundaki harcamaları kategori bazında gruplayıp toplamları hesaplıyoruz
            var categorySpendingData = (from s in db.Spendings
                                        group s by s.CategoryId into g
                                        select new
                                        {
                                            CategoryId = g.Key,
                                            TotalAmount = g.Sum(s => s.SpendingAmount)
                                        }).ToList();

            // Grafiği temizle
            chart1.Series.Clear();

            // Yeni bir seri oluştur ve chart'a ekle
            Series series = new Series("Kategori Bazlı Harcamalar");
            series.ChartType = SeriesChartType.Pie; // Pasta Grafiği (İstersen Column veya Bar yapabilirsin)

            // Kategorileri al ve chart'a ekle
            foreach (var data in categorySpendingData)
            {
                string categoryName = db.Categories
                                        .Where(c => c.CategoryId == data.CategoryId)
                                        .Select(c => c.CategoryName)
                                        .FirstOrDefault() ?? "Bilinmeyen Kategori";

                series.Points.AddXY(categoryName, data.TotalAmount);
            }

            // Seriyi chart'a ekle
            chart1.Series.Add(series);

            // Grafiğin legend (açıklamalar) ayarları
            chart1.Legends[0].Enabled = true;
            chart1.Series[0].IsValueShownAsLabel = true; // Değerleri göster
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.ShowDialog();
            this.Hide();
        }

        private void btnBankProcess_Click(object sender, EventArgs e)
        {
            FrmBankProcess frmBankProcess = new FrmBankProcess();
            frmBankProcess.ShowDialog();
            this.Hide();
        }

        private void btnSpendings_Click(object sender, EventArgs e)
        {

        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.ShowDialog();
            this.Hide();
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.ShowDialog();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.ShowDialog();
            this.Hide();
        }
    }
}
