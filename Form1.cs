using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Task1
{
    public partial class Form1 : Form
    {
        DB_Tools db = new DB_Tools();
        bool add;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TableUpdate();
        }

        private void TableUpdate()
        {
            db.Fill("SELECT * FROM Customers", dataSet.Customers);
            db.Fill("SELECT * FROM Orders", dataSet.Orders);
            db.Fill("SELECT * FROM Providers", dataSet.Providers);
        }

        private void Cust_AddB_Click(object sender, EventArgs e)
        {
            CustP.Visible = true;
            ToolP.Visible = false;
            add = true;
        }

        private void Order_AddB_Click(object sender, EventArgs e)
        {

        }

        private void CustOkB_Click(object sender, EventArgs e)
        {
            if (add)
            {
                db.Query($"INSERT INTO [dbo].[Customers] ([Family], [Address], [Balance], [TelephoneCs]) " +
                    $"VALUES('{CustNameTB.Text}', '{CustAddressTB.Text}', {CustBalanceTB.Text}, '{CustPhoneTB.Text}');");
            }
            else
            {
                db.Query($"UPDATE [dbo].[Customers] SET [Family] = '{CustNameTB.Text}', [Address] = '{CustAddressTB.Text}', " +
                    $"[Balance] = {CustBalanceTB.Text}, [TelephoneCs] = '{CustPhoneTB.Text}' WHERE [ID] = {CustomersT.CurrentRow.Cells[0].Value}");
            }
            TableUpdate();
            CustNameTB.Text = "";
            CustAddressTB.Text = "";
            CustBalanceTB.Text = "";
            CustPhoneTB.Text = "";
            CustP.Visible = false;
            ToolP.Visible = true;
        }

        private void CustCancelB_Click(object sender, EventArgs e)
        {
            CustP.Visible = false;
            ToolP.Visible = true;
        }

        private void Cust_UpdateB_Click(object sender, EventArgs e)
        {
            add = false;
            CustP.Visible = true;
            ToolP.Visible = false;
            CustNameTB.Text = CustomersT.CurrentRow.Cells[1].Value.ToString();
            CustAddressTB.Text = CustomersT.CurrentRow.Cells[2].Value.ToString();
            CustBalanceTB.Text = CustomersT.CurrentRow.Cells[3].Value.ToString();
            CustPhoneTB.Text = CustomersT.CurrentRow.Cells[4].Value.ToString();
        }

        private void Cust_RemoveB_Click(object sender, EventArgs e)
        {
            db.Query($"DELETE FROM [dbo].[Customers] WHERE [ID] = {CustomersT.CurrentRow.Cells[0].Value}");
            TableUpdate();
        }

        private void OrderAddB_Click(object sender, EventArgs e)
        {
            add = true;
            ToolP.Visible = false;
            OrderP.Visible = true;
        }

        private void OrderOkB_Click(object sender, EventArgs e)
        {
            if (add)
            {
                db.Query($"INSERT INTO [dbo].[Orders] ([ID], [Commodity], [Number], [Limit]) VALUES ({OrderCustTB.SelectedValue}, '{OrderComTB.Text}', {OrderNumTB.Text}, {OrderLimitTB.Text})");
            }
            else
            {
                db.Query($"UPDATE [dbo].[Orders] SET Commodity ='{OrderComTB.Text}', Number ={OrderNumTB.Text}, Limit = {OrderLimitTB.Text}" +
                    $" WHERE [ID] = {OrdersT.CurrentRow.Cells[0].Value}");
            }
            TableUpdate();
            OrderComTB.Text = "";
            OrderNumTB.Text = "";
            OrderLimitTB.Text = "";
            ToolP.Visible = true;
            OrderP.Visible = false;
        }

        private void OrderRemoveB_Click(object sender, EventArgs e)
        {
            db.Query($"DELETE FROM [dbo].[Orders] WHERE [ID] = {OrdersT.CurrentRow.Cells[0].Value}");
            TableUpdate();
        }

        private void OrderUpdateB_Click(object sender, EventArgs e)
        {
            add = false;
            ToolP.Visible = false;
            OrderP.Visible = true;
            OrderComTB.Text = OrdersT.CurrentRow.Cells[1].Value.ToString();
            OrderNumTB.Text = OrdersT.CurrentRow.Cells[2].Value.ToString();
            OrderLimitTB.Text = OrdersT.CurrentRow.Cells[3].Value.ToString();
        }

        private void OrderCancelB_Click(object sender, EventArgs e)
        {
            ToolP.Visible = true;
            OrderP.Visible = false;
        }

        private void ProvCancelB_Click(object sender, EventArgs e)
        {
            ToolP.Visible = true;
            ProvP.Visible = false;
        }

        private void ProvAddB_Click(object sender, EventArgs e)
        {
            add = true;
            ToolP.Visible = false;
            ProvP.Visible = true;
        }

        private void ProvUpdateB_Click(object sender, EventArgs e)
        {
            add = false;
            ToolP.Visible = false;
            ProvP.Visible = true;
            ProvComTB.Text = ProvidersT.CurrentRow.Cells[1].Value.ToString();
            ProvNameTB.Text = ProvidersT.CurrentRow.Cells[2].Value.ToString();
            ProvAddressTB.Text = ProvidersT.CurrentRow.Cells[3].Value.ToString();
            ProvPriceTB.Text = ProvidersT.CurrentRow.Cells[4].Value.ToString();
            ProvPhoneTB.Text = ProvidersT.CurrentRow.Cells[5].Value.ToString();

        }

        private void ProvOkB_Click(object sender, EventArgs e)
        {
            if (add)
            {
                db.Query($"INSERT INTO [dbo].[Providers] ([Commodity], [Name], [Address], [Price], [TelephonePr]) " +
                    $"VALUES ('{ProvComTB.Text}*','{ProvNameTB.Text}','{ProvAddressTB.Text}',{Convert.ToInt16(ProvPriceTB.Text) * 2},'{ProvPhoneTB.Text}');");
                //MessageBox.Show($"INSERT INTO [dbo].[Providers] ([Commodity], [Name], [Address], [Price], [TelephonePr]) " +
                //    $"VALUES ('{ProvComTB.Text}*','{ProvNameTB.Text}','{ProvAddressTB.Text}',{ProvPriceTB.Text}*2,'{ProvPhoneTB.Text}');");
            }
            else
            {
                db.Query($"UPDATE [dbo].[Providers] SET [Commodity] = '{ProvComTB.Text}', [Name] = '{ProvNameTB.Text}', [Address] = '{ProvAddressTB.Text}', " +
                    $"[Price] = {ProvPriceTB.Text}, [TelephonePr] = '{ProvPhoneTB.Text}' WHERE [ID] ={ProvidersT.CurrentRow.Cells[0].Value}");
            }
            TableUpdate();
            ProvComTB.Text = "";
            ProvNameTB.Text = "";
            ProvAddressTB.Text = "";
            ProvPriceTB.Text = "";
            ProvPhoneTB.Text = "";
            ToolP.Visible = true;
            ProvP.Visible = false;
        }

        private void ProvRemoveB_Click(object sender, EventArgs e)
        {
            db.Query($"DELETE FROM [dbo].[Providers] WHERE [ID] = {ProvidersT.CurrentRow.Cells[0].Value}");
            TableUpdate();
        }
    }
}
