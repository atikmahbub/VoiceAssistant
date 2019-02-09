using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;




namespace AIproject
{
   
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        SpeechSynthesizer synth = new SpeechSynthesizer();

        string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

       public Boolean search = false;
        
        
        public Form1()
        {
            InitializeComponent();
        }

    

        private void button1_Click(object sender, EventArgs e)
        {
           this.timer1.Start();
            circularProgressBar1.Text = "Listening...";
            rec.RecognizeAsync(RecognizeMode.Multiple);
            btndisable.Enabled = true;

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            circularProgressBar1.Text = "Stop...";
            rec.RecognizeAsyncStop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            Choices command = new Choices();
            //command.Add(new string[] { "Hello","print my name","Open Chrome","Play Songs","Open Notepad","Close Notepad","Current Time", "Lock My Computer","Mark All","Copy","Paste","Looking For", "play","pause", "Close Chrome" });

            command.Add(File.ReadAllLines(@"F:/voice command/command.txt"));
           GrammarBuilder gbuilder = new GrammarBuilder();
           gbuilder.Append(command);
           Grammar grammar = new Grammar(gbuilder);
            rec.LoadGrammarAsync(grammar);
            rec.SetInputToDefaultAudioDevice();
            
            rec.SpeechRecognized += rec_speechRecognized;
         
        }

        String[] greetings= new String[4] { "Hi", "How Are You?" ,"Yes Sir","Hello" };

        public String greeting_action()
        {
            Random r = new Random();
            return greetings[r.Next(4)];
        }

        void rec_speechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            textBox1.Text = e.Result.Text;

            if (search)
            {
                Process.Start("https://www.google.com/#q="+e.Result.Text);
                search = false;
            }

            if (search == false)
            {

                if (e.Result.Text == "Looking For")
                {
                    synth.SpeakAsync("Ok Sir,Just wait");
                    search = true;
                }

            }
                switch (e.Result.Text)
                {
                
                    
        
                case "Mark":
                case "select":
                        synth.SpeakAsync("Ok Sir");
                        SendKeys.Send("^a");
                        synth.SpeakAsync("Done Sir");
                        break;

                    case "Copy":
                        synth.SpeakAsync("Ok Sir");
                        SendKeys.Send("^c");
                        synth.SpeakAsync("Done Sir");
                        break;

                    case "Paste":
                        synth.SpeakAsync("Ok Sir");
                        SendKeys.Send("^v");
                        synth.SpeakAsync("Done Sir");
                        break;


                    case "Hello":
                case "Hi":
                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                    synth.SpeakAsync(greeting_action());
                        textBox2.Clear();
                        textBox2.Text =greeting_action(); 
                        break;

                    case "print my name":
                case "my name":
                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text += "\nArtificial Inteligence";
                        synth.SpeakAsync("Artificial Inteligence");
                        break;

                    case "Open Chrome":
                case "browser":
                case "open the browser":
                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text += "Loading...";
                        synth.SpeakAsync("Opening The Browser");
                        System.Diagnostics.Process.Start("chrome.exe");
                        break;
                case "Close Chrome":
                case "close":
                case "off":
                    Process[] _proceses = null;
                    _proceses = Process.GetProcessesByName("chrome");
                    foreach (Process proces in _proceses)
                    {
                        proces.Kill();
                    }
                    break;
                  

                    case "Play Songs":
                case "Songs":

                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text += "Playing";
                        synth.SpeakAsync("Neel Paharer Gaye By Aurthohin");
                        System.Diagnostics.Process.Start("G:\\Audio\\Aurthohin\\Aurthohin - Neel paharer gaye (cancer er nishikabbo).m4a");
                        break;

                case "stop":
                case "close player":
                    synth.SpeakAsync("Sure Sir");
                    Process[] _pro = null;
                    _pro = Process.GetProcessesByName("wmplayer");
                    foreach (Process proces in _pro)
                    {
                        proces.Kill();
                    }
                    break;
                   

                    case "play":
                    case "pause":

                    synth.SpeakAsync("Ok Sir");
                    SendKeys.Send("ShiftP");

                    break;
                    case "Open Notepad":
                case "please open notepad":

                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text += "Loading...";
                        synth.SpeakAsync("Opening Notepad");
                        Process.Start("notepad.exe");
                        break;

                    case "Close Notepad":
                    case "please close notepad":


                          Process[] _procese = null;
                        _procese = Process.GetProcessesByName("notepad");
                        foreach (Process proces in _procese)
                        {
                            proces.Kill();
                        }
                        break;

                    case "Current Time":
                case "what time is it":
                case "Time":
                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text = DateTime.Now.ToLongTimeString();
                        synth.SpeakAsync("Current Time is" + DateTime.Now.ToLongTimeString());
                        break;

                    case "Lock My Computer":
                case "lock my pc":
                        textBox1.Clear();
                        textBox1.Text = e.Result.Text;
                        textBox2.Clear();
                        textBox2.Text = "Ok, Sir";
                        LockWorkStation();
                        break;

                case "My Computer":
                case "PC":

                    synth.SpeakAsync("Ok");
                    System.Diagnostics.Process.Start("explorer", myComputerPath);
                    break;

                case "Move":
                case "right":
                    synth.SpeakAsync("right");
                    SendKeys.Send("{RIGHT}");
                    break;

                case "left":
                    synth.SpeakAsync("left");
                    SendKeys.Send("{LEFT}");
                    break;


                case "Up":
                    synth.SpeakAsync("up");
                    SendKeys.Send("{UP}");
                    break;

                case "Down":
                    synth.SpeakAsync("down");
                    SendKeys.Send("{DOWN}");
                    break;

                case "Enter":
                    synth.SpeakAsync("Enter");
                    SendKeys.Send("{ENTER}");
                    break;

                case "Delete":
                case "Remove":
                    synth.SpeakAsync("Removing");
                    SendKeys.Send("{DELETE}");
                    break;

                case "hide":
                case "please hide":
                case "Go":
                case "bye":
                    textBox1.Clear();
                    textBox2.Clear();
                    synth.SpeakAsync("Ok Sir");
                    textBox2.Text += "Ok Sir";
                    this.WindowState = FormWindowState.Minimized;
                    break;

                case "where are you":
                case "show":
                    textBox1.Clear();
                    textBox2.Clear();
                    synth.SpeakAsync("I am here");
                    textBox2.Text += "I am Here";
                    this.WindowState = FormWindowState.Normal;
                    break;
                case "G":
                case "others":
                    synth.SpeakAsync("Others");
                    System.Diagnostics.Process.Start(@"G:");
                    break;
                case "color":
                case "F":
                    synth.SpeakAsync("Color");
                    System.Diagnostics.Process.Start(@"G:");
                    break;
                case "thank you":
                    synth.Speak("Take Care");
                    Application.Exit();
                    break;
                default:
                    
                    string get = textBox1.Text;

                    synth.SpeakAsync("Please Contact With My Admin Sir");
                    textBox2.Clear();
                    textBox2.Text += "Please Contact with my Admin Sir";
                    
                    StreamWriter txt = new StreamWriter("F:/voice command/alert.txt",true);
                    txt.Write(get);
                    txt.Write(Environment.NewLine);
                    txt.Close();
                    break;

            }
          
        }

        private void circularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
       
        private void timer1_Tick(object sender, EventArgs e)
        {

            circularProgressBar1.Increment(1);
            if (circularProgressBar1.Value == 80)
            {
                circularProgressBar1.Value = 10;
            }


        
        }
        Point lastpoint;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  
            {
                this.Left += e.X-lastpoint.X;
                this.Top += e.Y-lastpoint.Y;

            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

