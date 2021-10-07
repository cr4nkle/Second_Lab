﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsFor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.money.ToString();
            textBox2.Text= Properties.Settings.Default.per_money.ToString();
            textBox3.Text= Properties.Settings.Default.pay.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] month = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            double money = double.Parse(this.textBox1.Text);            
            double per_money = double.Parse(this.textBox2.Text);
            double pay = double.Parse(this.textBox3.Text);
            
            bool check = Logic.Check(money, pay, per_money);
            
            if (check)
            {                
                int month_number = Logic.GetMonth(money, per_money);
                
                int count = Logic.GetCount(money, pay);
                _ = MessageBox.Show("а) за какой месяц величина ежемесячного увеличения вклада превысит B руб." + "\n" +month[month_number] 
                    + "\n" + "б) через сколько месяцев размер вклада превысит C руб." + "\n" + count);

            }
            else
            {
                MessageBox.Show("Вы ввели некорректные данные.");
            }
        }
    }
    public class Logic
    {
        public static int GetMonth(double money, double per_money)
        {
            int month_number = 1;
            for (double cash = money; cash < per_money; cash *= 1.02)
            {
                month_number++;
                if (month_number == 12)
                {
                    month_number = 0;
                }
            }
            return month_number;
        }

        public static int GetCount(double money, double pay)
        {
            int count = 0;
            for (double cash = money; cash < pay; cash *= 1.02)
            {
                count++;
            }
            return count;
        }

        public static bool Check(double money, double pay, double per_money)
        {
            bool check = true;
            if (money < 1 || pay < 1 || per_money < 1 || (pay < money && per_money < money))
            {
                check = false;
            }
            return check;
        }
    }
}
