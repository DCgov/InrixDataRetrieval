using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InRixDataManager
{
    public partial class frmDebugMode : Form
    {
        List<String> RoadList;
        DataIncome inData;
        public frmDebugMode()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String message = this.richTextBox1.Text;
            DateTime tempdt = new DateTime() ;
            richTextBox2.Text = Parser.ParseKeyToken(message, ref tempdt);
            textBox1.Text = tempdt.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String message = this.richTextBox1.Text;
            inData = Parser.ParseData(message);
            textBox1.Text = inData.TimeStamp.ToString();
            richTextBox2.Text = "";

            int x = 0;
            foreach(InrixData tInrix in inData.List){
                x++;
                if (tInrix.TMCCode != null) richTextBox2.Text += ("TMC=" + tInrix.TMCCode + " ");
                if (tInrix.Score >0) richTextBox2.Text += ("Scr=" + tInrix.Score + " ");
                if (tInrix.Speed > 0) richTextBox2.Text += ("Spd=" + tInrix.Speed + " ");
                if (tInrix.TravelTimeMinutes > 0) richTextBox2.Text += ("TTM=" + tInrix.TravelTimeMinutes + " ");
                if (tInrix.Average > 0) richTextBox2.Text += ("Avg=" + tInrix.Average + " ");
                if (tInrix.Reference > 0) richTextBox2.Text += ("Ref=" + tInrix.Reference + " ");
                richTextBox2.Text += "\n";
                if (x > 100)
                {
                    break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = DataRetriever.getCredentialKey();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = DataRetriever.getData(textBox2.Text, textBox3.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DBOperator.InsertData(inData);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DBOperator.UpdateData(inData);
        }

        private void button7_Click(object sender, EventArgs e)
        {
           RoadList = new List<string>();
           DBOperator.getRoadList(ref RoadList);
           foreach (String a in RoadList)
           {
               listBox1.Items.Add(a);
           }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataIncome tmp = new DataIncome();
            tmp = RouteFilter.DCFilter(inData, RoadList);
            foreach(InrixData a in tmp.List){
                listBox2.Items.Add(a.TMCCode + " speed=" + a.Speed + " score=" + a.Score);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
            richTextBox1.Text = richTextBox3.Text;
            button1_Click(sender, e);
            textBox2.Text = richTextBox2.Text;
            button4_Click(sender, e);
            richTextBox1.Text = richTextBox3.Text;
            button2_Click(sender,e);
            button7_Click(sender,e);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("110+4133".CompareTo("110+4100").ToString());
            MessageBox.Show("110+4133".CompareTo("110+4133").ToString());
            MessageBox.Show("110+4133".CompareTo("110+4255").ToString());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OperationManager.Execute("speed,score,ttm");
        }
    }
}