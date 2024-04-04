using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Karinderya
{
    public class Member3
    {

        private static int[] totalAllQty = new int[6];
        private static int totalQtySum = 0;
        private static double[] totalAllPrice = new double[6];
        private static double totalAmount = 0;
        private static string inputDay = "";


        private static Form1 form;
        public Member3(Form1 form1)
        {
            form = form1;
        }

        public static void GetUserDayInput(Button button, TabControl tabControl, ListBox orderList)
        {
            inputDay = button.Text;

            switch (inputDay)
            {
                case "Lunes":
                case "Miyerkules":
                case "Biyernes":
                    tabControl.SelectedTab = tabControl.TabPages["mwfTab"];
                    (tabControl.TabPages["mwfTab"].Controls["dayLabel1"] as Label).Text = inputDay;
                    break;
                case "Martes":
                case "Huwebes":
                case "Sabado":
                    tabControl.SelectedTab = tabControl.TabPages["tthsTab"];
                    (tabControl.TabPages["tthsTab"].Controls["dayLabel2"] as Label).Text = inputDay;
                    break;
                case "Linggo":
                    tabControl.SelectedTab = tabControl.TabPages["sunTab"];
                    break;
            }

            foreach (Control control in button.Parent.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = false;
                    btn.BackColor = Color.Gray;
                }
            }
        }

        public static void SelectDayEnable(Panel panel, TabControl tabControl, ListBox orderList)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Button btn)
                {
                    btn.Enabled = true;
                    btn.BackColor = Color.White;
                }
            }

            tabControl.SelectedTab = tabControl.TabPages["homeTab"];

            foreach (TabPage tabPage in tabControl.TabPages)
            {
                if (tabPage.Name == "mwfTab" || tabPage.Name == "tthsTab")
                {
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is NumericUpDown numericUpDown)
                        {
                            numericUpDown.Value = 0;
                        }
                    }
                }
            }
        }

        public static string Order_GotonngBatangas(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[0] = qty * 70.00;
            totalAllQty[0] = qty;
            return GenerateOrder("Gotong Batangas", 70.00, qty);
        }

        public static string Order_Carbonara(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[1] = qty * 120.00;
            totalAllQty[1] = qty;
            return GenerateOrder("Carbonara", 120.00, qty);
        }
        public static string Order_InstantNoodles(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[2] = qty * 40.00;
            totalAllQty[2] = qty;
            return GenerateOrder("Instant Noodles", 40.00, qty);
        }

        public static string Order_ArrozCaldo(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[3] = qty * 143.00;
            totalAllQty[3] = qty;
            return GenerateOrder("Arroz Caldo", 143.00, qty);
        }
        public static string Order_Spaghetti(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[4] = qty * 59.00;
            totalAllQty[4] = qty;
            return GenerateOrder("Spaghetti", 59.00, qty);
        }
        public static string Order_Palabok(NumericUpDown updown)
        {
            int qty = (int)updown.Value;
            totalAllPrice[5] = qty * 133.00;
            totalAllQty[5] = qty;
            return GenerateOrder("Palabok", 133.00, qty);
        }

        private static string GenerateOrder(string item, double price, int qty)
        {
            double totalPrice = price * qty;
            SumTotalAll();
            
            if (qty > 0)
            {
                StringBuilder orderBuilder = new StringBuilder();
                orderBuilder.Append(item.PadRight(16));
                orderBuilder.Append($" {price:C2} {qty,-2}");
                orderBuilder.Append($"{totalPrice:C2}");
                return orderBuilder.ToString();
            }
            else
            {
                return null;
            }
        }
        public static int UpdateOrderQuantity(string word, string newOrder, ListBox orderList)
        {
            int index = orderList.FindString(word);
            if (index != ListBox.NoMatches && !string.IsNullOrEmpty(newOrder))
            {
                orderList.Items[index] = newOrder;
            }
            return index;
        }
        public static void UpdateOrderList(string item, string newItem,ListBox orderList)
        {
            int existingIndex = orderList.FindString(item);

            if (existingIndex != ListBox.NoMatches)
            {
                if (newItem != null)
                {
                    orderList.Items[existingIndex] = newItem;
                }
                else
                {
                    orderList.Items.RemoveAt(existingIndex);
                }
            }
            else if (newItem == null)
            {
                orderList.Items.Remove(newItem);
            }
            else
            {
                orderList.Items.Add(newItem);
            }
        }
        public static void SumTotalAll()
        {

            int initalQty = 0;
            double initailPrice = 0;

            double totalPriceSum = 0.0;

            foreach (int qty in totalAllQty)
            {
                initalQty += qty;
            }
            foreach (double price in totalAllPrice)
            {
                initailPrice += price;
            }
            totalPriceSum = initailPrice;
            totalQtySum = initalQty;

            bool isDiscounted = totalQtySum > 5 ? true : false;
            if (isDiscounted)
            {
                totalAmount = totalPriceSum * .90;
            }
            else
            {
                totalAmount = totalPriceSum;
            }

            foreach (Control control in form.Controls)
            {
                if (control.Name == "totalQty")
                {
                    (control as Label).Text = totalQtySum.ToString();
                }
                if(control.Name == "discount")
                {
                    (control as Label).Text = isDiscounted ? "10%" : "0%";
                }
                if(control.Name == "subTotal")
                {
                    (control as Label).Text = totalPriceSum.ToString("C");
                }
                if(control.Name == "totalPrice")
                {
                    (control as Label).Text = totalAmount.ToString("C");
                }
            }
        }
        public static void ProceedToPayment(Control.ControlCollection controls)
        {
            if(totalQtySum > 0)
            {
                foreach (Control control in controls)
                {
                    if (control is Button button && (button.Name == "orderButton" || button.Name == "clrButton"))
                    {
                        button.Enabled = false;
                    }
                    if (control is TabControl tabControl)
                    {
                        tabControl.SelectedTab = tabControl.TabPages["paymentTab"];
                    }
                }
            }
        }
        public static void PlaceOrder(Control.ControlCollection controls, Panel panel, TabControl tabControl, ListBox orderList)
        {
            foreach (Control control in controls)
            {
                if (control is TabControl)
                { 
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        if (tabPage.Name == "paymentTab")
                        {
                            foreach (Control tabPageControl in tabPage.Controls)
                            {
                                if (tabPageControl is TextBox textBox && textBox.Name == "cashTextBox")
                                {
                                    if (double.TryParse(textBox.Text, out double paymentAmount) && paymentAmount >= 0)
                                    {
                                        double change = paymentAmount - totalAmount;
                                        string message = $"Change: {change:C}";

                                        if (change >= 0)
                                        {
                                            MessageBox.Show(message, "Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            foreach (Control ctrl in controls)
                                            {
                                                if (ctrl is Button button && (ctrl.Name == "orderButton" || ctrl.Name == "clrButton"))
                                                {
                                                    ctrl.Enabled = true;
                                                }
                                            }
                                            SelectDayEnable(panel, tabControl, orderList);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Invalid payment amount. Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid payment amount. Please enter a valid amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
