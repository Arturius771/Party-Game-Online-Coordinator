﻿using System;
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
        //logical declarations
        private Timer timer = new Timer();
        private bool buttonPressed = false;
        private bool weOnlyNeedOneTimerTickEvent = true;
        //user choice declarations
        private string badGuyTitle;
        private int totalTime;
        private int badGuyWindowStart;
        private int badGuyWindowFinish;
        private int fontSize;
        private bool colourEnabled;
        public Form1() {
            InitializeComponent();
            comboBox1.SelectedItem = "25";
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                if (buttonPressed == false) {
                    int.TryParse(textBox1.Text, out totalTime);//gets the total time for the countdown       
                    int.TryParse(textBox2.Text, out badGuyWindowStart);//gets the total time for the countdown 
                    int.TryParse(textBox3.Text, out badGuyWindowFinish);//gets the total time for the countdown 
                    int.TryParse(comboBox1.Text, out fontSize);
                    badGuyTitle = textBox5.Text;
                    colourEnabled = checkBox1.Checked;
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
                    buttonReset();
                }
            }
            catch (Exception exception){
                textBox4.Text = exception.ToString();
            }//this doesnt work because I used tryparse            
        }//Start Button

        private void buttonReset() {
            timer.Stop();
            totalTime = 0;
            button1.Text = "Start";
            buttonPressed = !buttonPressed;
        }

        private void timerTickMethod(object sender, EventArgs e) {
            try {
                totalTime--;
                if (totalTime <= 1) {
                    if(colourEnabled == true) {
                        textBox4.BackColor = Color.Salmon;
                    }
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + "Everyone unmute and start the game!";                    
                }
                if (totalTime <= badGuyWindowStart && totalTime >= badGuyWindowFinish) {
                    if(colourEnabled == true) {
                        textBox4.BackColor = Color.PeachPuff;
                    }
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + badGuyTitle +" unmute and identify yourselves!";
                }
                if (totalTime >= badGuyWindowStart || totalTime <= badGuyWindowFinish && totalTime >= 1) {
                    if(colourEnabled == true) {
                        textBox4.BackColor = Color.Salmon;
                    }
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + "Everyone mute yourselves!";
                }
                if (totalTime <= 0) {
                    if(colourEnabled == true) {
                        textBox4.BackColor = Color.GreenYellow;
                    }
                    textBox4.Text = totalTime.ToString() + Environment.NewLine + "Everyone unmute and start the game!";
                    buttonReset();
                }
            }
            catch (Exception exception) {
                textBox4.Text = exception.ToString();
            }
        }
    }
}
