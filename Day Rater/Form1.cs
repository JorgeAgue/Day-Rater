using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Day_Rater
{
    public partial class Form1 : Form
    {
        Color[] colorArr = new Color[] { Color.DarkGray, Color.DarkRed, Color.Red, Color.OrangeRed, Color.Orange, Color.Yellow, Color.YellowGreen, Color.GreenYellow, Color.CadetBlue, Color.LightBlue, Color.Gold };
        List<string> logFile = new List<string>();
        List<string> newLogFile = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = (trackBar1.Value / 2.0) + "";
            textBox2.BackColor = colorArr[trackBar1.Value];
        }

        private void button1_Click(object sender, EventArgs e) //Add day button
        {
            double num = trackBar1.Value / 2.0;
            dayListBox.Items.Add(textBox3.Text + " " + num + "/5 " + textBox1.Text);
        }

        private void loadingFile(string fileName, string safeFileName) //Shortcut method to load files into the entry window
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
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
            fileNameTextBox.Text = safeFileName;
            file.Close();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                loadingFile(openFileDialog1.FileName, openFileDialog1.SafeFileName);
            }
        }

        private void getAvgRating(List<string> logFile) //Parse daysFile, get average of ratings
        {
            double[] avgArr = new double[logFile.Count];
            double avgRating;
            string aString;
            for (int i = 0; i < logFile.Count; i++)
            {
                aString = (logFile[i].Split(':').Last()).Split('/').First().TrimStart(' ').TrimEnd(' ');

                try
                {
                    if (!Char.IsDigit(aString, 0) || !string.IsNullOrWhiteSpace(aString))
                        avgArr[i] = double.Parse(aString);

                    avgRating = Math.Round(avgArr.Average(), 2);
                    avgTextBox.Text = avgRating.ToString();
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
            string defaultFile = Properties.Settings.Default.DefaultFile;

            Days = date;
            int i = (int)Days.DayOfWeek;

            for (int n = 1; n < 2; n++)
            {
                textBox3.Text += Days.AddDays(0).Month + "/" + Days.AddDays(0).Day + " " + dayArr[i];
            }

            if (defaultFile!= "") //If default file exists, preload it
            {
                loadingFile(defaultFile, Properties.Settings.Default.SafeDefaultFile);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileNameTextBox.Text == openFileDialog1.SafeFileName) //Not using a default file
                using (StreamWriter writer = new StreamWriter(openFileDialog1.FileName))
                {
                    foreach (var item in dayListBox.Items)
                    {
                        writer.WriteLine(item.ToString());
                        logFile= newLogFile; //Make the two logfiles equal so all changes are saved
                    }

                }
            
            else //Using a default file
            {
                using (StreamWriter writer = new StreamWriter(Properties.Settings.Default.DefaultFile))
                {
                    foreach (var item in dayListBox.Items)
                    {
                        writer.WriteLine(item.ToString());
                        logFile = newLogFile;
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Remove Day button
        {
            if (dayListBox.SelectedIndex != -1)
            {
                dayListBox.Items.RemoveAt(dayListBox.SelectedIndex);
                button4.Visible = false;
            }

        }

        private bool unSavedChanges(List<string> oldLogFile, List<string> newLogFile)
        {
            newLogFile.Clear();
            foreach (var item in dayListBox.Items)
            {
                newLogFile.Add(item.ToString());
            }


            if (newLogFile== null)
            {
                return false; //No changes were made (?)
            }
            else if (!oldLogFile.SequenceEqual(newLogFile))
            {
                return true;
            }

            else return false; // No difference between old and new
        }
        private void debugLogFiles(List<string> oldLogFile, List<string> newLogFile)
        {
            foreach (var array in logFile)
            {
                Debug.Write("\n");

                foreach (var item in array)
                {
                    Debug.Write(" ");
                    Debug.Write(item);
                }
            }
            Debug.WriteLine("\n");
            Debug.WriteLine("------------------");
            foreach (var array in newLogFile)
            {
                Debug.Write("\n");

                foreach (var item in array)
                {
                    Debug.Write(" ");
                    Debug.Write(item);
                }
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unSavedChanges(logFile, newLogFile))
            {

                DialogResult dialogResult = MessageBox.Show("You have unsaved changes. Are you sure you want to leave?", "Close Program", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
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
                textBox1.Text = input2.Substring(input2.IndexOf("/") + 2);
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
            dayListBox.Items[dayListBox.SelectedIndex] = toChange.Substring(0, toChange.IndexOf(':') + 1) + " " + num + "/5 " + textBox1.Text;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello.", "About");
        }

        private void setDefaultFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultFile = openFileDialog1.FileName; //Changes the default file
                Properties.Settings.Default.SafeDefaultFile = openFileDialog1.SafeFileName; //Changes the default file
                Properties.Settings.Default.Save();
                MessageBox.Show("Default file has been set");
            }
        }

        private void removeDefaultFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultFile = ""; //Changes the default file
            Properties.Settings.Default.SafeDefaultFile = ""; //Changes the default file
            Properties.Settings.Default.Save();
            MessageBox.Show("Default file removed");
        }
    }
}