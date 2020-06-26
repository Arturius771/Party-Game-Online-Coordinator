using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecretHitlerOnlineTimer {
    public partial class Form1 : Form {
        private Timer timer = new Timer();
        private int totalTime;
        private int badGuyWindowStart;
        private int badGuyWindowFinish;
        private int fontSize;
        private string badGuyTitle;
        private bool buttonPressed = false;
        private bool weOnlyNeedOneTimerTickEvent = true;
        public Form1() {
            InitializeComponent();
            comboBox1.SelectedItem = "20";
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                if (buttonPressed == false) {
                    int.TryParse(textBox1.Text, out totalTime);//gets the total time for the countdown       
                    int.TryParse(textBox2.Text, out badGuyWindowStart);//gets the total time for the countdown 
                    int.TryParse(textBox3.Text, out badGuyWindowFinish);//gets the total time for the countdown 
                    int.TryParse(comboBox1.Text, out fontSize);
                    badGuyTitle = textBox5.Text;
                    textBox4.Font = new Font("Microsoft Sans Serif", fontSize);
                    if (weOnlyNeedOneTimerTickEvent == true) {
                        timer.Interval = 1000;//1 second
                        timer.Tick += new EventHandler(timerTickMethod);
                        weOnlyNeedOneTimerTickEvent = !weOnlyNeedOneTimerTickEvent;
                    }
                    timer.Start();
                    button1.Text = "Stop";
                    buttonPressed = !buttonPressed;
                    Debug.WriteLine("Fontsize: " + fontSize);
                }//need to check that the data entered makes sense
                else {
                    timer.Stop();
                    totalTime = 0;
                    button1.Text = "Start";
                    buttonPressed = !buttonPressed;
                }
            }
            catch (Exception exception){
                textBox4.Text = exception.ToString();
            }//this doesnt work because I used tryparse            
        }//Start Button

        private void timerTickMethod(object sender, EventArgs e) {
            try {
                totalTime--;
                if (totalTime <= 1) {
                    textBox4.BackColor = Color.Red;
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + "Everyone unmute and start the game!";                    
                }
                if (totalTime <= badGuyWindowStart && totalTime >= badGuyWindowFinish) {
                    textBox4.BackColor = Color.Yellow;
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + badGuyTitle +" unmute and identify yourselves!";
                }
                if (totalTime >= badGuyWindowStart || totalTime <= badGuyWindowFinish && totalTime >= 1) {
                    textBox4.BackColor = Color.Red;
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + "Everyone mute yourselves!";
                }
                if (totalTime <= 0) {
                    textBox4.BackColor = Color.Green;
                    timer.Stop();
                }
            }
            catch (Exception exception) {
                textBox4.Text = exception.ToString();
            }
        }
    }
}
