using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Karinderya
{
    public partial class Form1 : Form
    {
        public Member3 M3 { get; private set; } 
        public Form1()
        {
            InitializeComponent();
            M3 = new Member3(this);

            foreach (Button btn in Button_Panel.Controls)
            {
                btn.Click += (sender, e) =>
                {
                    Member3.GetUserDayInput(btn, tabControl, orderList);
                };
            }
            Member3.SumTotalAll();
        }

        private void back1_Click(object sender, EventArgs e)
        {
            Member3.SelectDayEnable(Button_Panel, tabControl, orderList);
        }

        private void back2_Click(object sender, EventArgs e)
        {
            Member3.SelectDayEnable(Button_Panel, tabControl, orderList);
        }

        private void back3_Click(object sender, EventArgs e)
        {
            Member3.SelectDayEnable(Button_Panel, tabControl, orderList);
        }


        private void food101qty_ValueChanged(object sender, EventArgs e)
        {
            string gotongBatangas = Member3.Order_GotonngBatangas(food101qty);
            Member3.UpdateOrderList("Gotong Batangas", gotongBatangas, orderList);
        }

        private void food102qty_ValueChanged(object sender, EventArgs e)
        {
            string carbonara = Member3.Order_Carbonara(food102qty);
            Member3.UpdateOrderList("Carbonara", carbonara, orderList);
        }

        private void food103qty_ValueChanged(object sender, EventArgs e)
        {
            string instantNoodles = Member3.Order_InstantNoodles(food103qty);
            Member3.UpdateOrderList("Instant Noodles", instantNoodles, orderList);
        }

        private void food201qty_ValueChanged(object sender, EventArgs e)
        {
            string arrozCaldo = Member3.Order_ArrozCaldo(food201qty);
            Member3.UpdateOrderList("Arroz Caldo", arrozCaldo, orderList);
        }

        private void food202qty_ValueChanged(object sender, EventArgs e)
        {
            string spaghetti = Member3.Order_Spaghetti(food202qty);
            Member3.UpdateOrderList("Spaghetti", spaghetti, orderList);
        }

        private void food203qty_ValueChanged(object sender, EventArgs e)
        {
            string palabok = Member3.Order_Palabok(food203qty);
            Member3.UpdateOrderList("Palabok", palabok, orderList);
        }

        private void orderButton_Click(object sender, EventArgs e)
        {
            Member3.ProceedToPayment(this.Controls);
        }

        private void clrButton_Click(object sender, EventArgs e)
        {
            Member3.SelectDayEnable(Button_Panel, tabControl, orderList);
        }

        private void placeOrder_Click(object sender, EventArgs e)
        {
            Member3.PlaceOrder(this.Controls, Button_Panel, tabControl, orderList);
            cashTextBox.Text = null;
        }
    }
}
