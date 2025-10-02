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

namespace FinancialCrm
{
    
    public partial class FrmBankProcess : Form
    {
        public FrmBankProcess()
        {
            InitializeComponent();
        }

        FinancialCrmDbEntities db = new FinancialCrmDbEntities();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FrmBankProcess_Load(object sender, EventArgs e)
        {
            var values = db.BankProcesses.ToList();
            dataGridView1.DataSource = values;

            var banks = db.Banks
                      .Select(b => new { b.BankId, b.BankTitle })
                      .ToList();

            cmbBanks.DataSource = banks;
            cmbBanks.DisplayMember = "BankTitle";  // ComboBox'ta bankaların adlarını göster
            cmbBanks.ValueMember = "BankId";      // BankId, seçilen değerin karşılığı olacak
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            if (cmbBanks.SelectedValue != null) // Eğer combobox'ta seçili bir banka varsa
            {
                int selectedBankId = Convert.ToInt32(cmbBanks.SelectedValue); // Seçili bankanın ID'sini al

                var bankProcesses = db.BankProcesses
                                          .Where(bp => bp.BankId == selectedBankId) // Seçili bankaya ait kayıtları getir
                                          .Select(bp => new
                                          {
                                              bp.Description,
                                              bp.ProcessDate,
                                              bp.ProcessType,
                                              bp.Amount
                                          })
                                          .ToList();

                dataGridView1.DataSource = bankProcesses; // DataGridView'e verileri ata

            }
            else
            {
                MessageBox.Show("Lütfen bir banka seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.ShowDialog();
            this.Hide();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.ShowDialog();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.ShowDialog();
            this.Hide();
        }

        private void btnSpendings_Click(object sender, EventArgs e)
        {
            FrmSpendings frmSpendings = new FrmSpendings();
            frmSpendings.ShowDialog();
            this.Hide();
        }

        private void btnBankProcess_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
