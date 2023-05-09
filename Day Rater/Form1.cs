using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Day_Rater
{
    public partial class Form1 : Form
    {
        Color[] colorArr = new Color[] { Color.DarkGray, Color.DarkRed, Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.YellowGreen, Color.GreenYellow, Color.CadetBlue, Color.LightBlue, Color.Gold };
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = (trackBar1.Value / 2.0) + "";
            textBox2.BackColor = colorArr[trackBar1.Value];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double num = trackBar1.Value / 2.0;
            dayListBox.Items.Add(textBox3.Text + " " + num + "/5 " + textBox1.Text);
        }


        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string line;
            List<string> logFile = new List<string>();
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(openFileDialog1.FileName);
                // File.ReadAllText(openFileDialog1.FileName);
                dayListBox.Items.Clear();
                while ((line = file.ReadLine()) != null)
                {

                    dayListBox.Items.Add(line);
                    logFile.Add(line);

                }
                dayListBox.SelectedIndex = dayListBox.Items.Count - 1;
                dayListBox.SelectedIndex = -1;
                getAvgRating(logFile);
                fileNameTextBox.Text = openFileDialog1.SafeFileName;
                file.Close();
            }
        }
        private void getAvgRating(List<string> logFile) //Parse daysFile, get average of ratings
        {
            double[] avgArr = new double[logFile.Count];
            string aString;
            for (int i = 0; i < logFile.Count; i++)
            {
                aString = (logFile[i].Split(':').Last()).Split('/').First().TrimStart(' ').TrimEnd(' ');
               
                try
                {

                    if (!Char.IsDigit(aString, 0) || !string.IsNullOrWhiteSpace(aString))
                        avgArr[i] = double.Parse(aString);
                    avgTextBox.Text = avgArr.Average().ToString();
                }

                catch (FormatException)
                {
                    Debug.WriteLine("Format expection in log file");
                    avgTextBox.Text = "N/A";
                    break;
                }
                
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime date, Days;
            String[] dayArr = { "Sun:", "Mon:", "Tue:", "Wes:", "Thu:", "Fri:", "Sat:" };
            date = DateTime.Now;

            Days = date;
            int i = (int)Days.DayOfWeek;

            for (int n = 1; n < 2; n++)
            {
                textBox3.Text += Days.AddDays(0).Month + "/" + Days.AddDays(0).Day + " " + dayArr[i];
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(openFileDialog1.FileName))
            {
                foreach (var item in dayListBox.Items)
                {
                    writer.WriteLine(item.ToString());
                }

            }
        }

        private void button2_Click(object sender, EventArgs e) //Remove Day button
        {
            if (dayListBox.SelectedIndex != -1)
            { 
            dayListBox.Items.RemoveAt(dayListBox.SelectedIndex);
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Close Program", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.frm1 = this;
            frm.Show();
        }

        private void dayListBox_Click(object sender, EventArgs e) //Selecting an item
        {
            if (dayListBox.SelectedItem != null)
            {
                string input = dayListBox.SelectedItem.ToString();
                string input2 = input.Substring(input.IndexOf(':') + 1);
                textBox1.Text = input2.Substring(input2.IndexOf("/") + 3);
                button4.Visible = true;
                button4.Enabled = true;
                button2.Enabled = true;
            }

        }

        private void button3_Click(object sender, EventArgs e) //Unselect button
        {
            if (dayListBox.SelectedItem != null)
            {
                dayListBox.SelectedIndex = -1;
                button4.Visible = false;
                button4.Enabled = false; //Change day
                button2.Enabled = false; //Remove day
            }
        }

        private void button4_Click(object sender, EventArgs e) //Change day button
        {
            string toChange = dayListBox.SelectedItem.ToString();
            double num = trackBar1.Value / 2.0;
            dayListBox.Items[dayListBox.SelectedIndex] = toChange.Substring(0,toChange.IndexOf(':') +1) + " " + num + "/5 " + textBox1.Text;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sup", "About");
        }
    }
}