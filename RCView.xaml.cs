using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Controls.Button;
using System.Windows.Shapes;

using UsbUirt;
using System.Threading;
using System.IO.Ports;
using WPF_RemoteControl_with_USBUIRT;




namespace WPF_RemoteControl_with_USBUIRT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string numero;
        private string ligar;
        public string desligar;
        public string aumvol;
        public string dimivol;
        public string aumca;
        public string dimica;
        public string mute;
        Controller mc = new Controller();
        private static string irCode = "0000 0071 0000 0032 0080 0040 0010 0010 0010 0030 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0030 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0030 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0030 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0030 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0010 0030 0010 0aad";
        /// CodeFormat = Define o formato dos códigos quando usados para transmitir ou aprender códigos IR
        //private static CodeFormat transmitFormat = CodeFormat.Pronto;
        //private static LearnCompletedEventArgs learnCompletedEventArgs = null; ///é anulado o Evento LearnCompleted

        public MainWindow()
        {

            InitializeComponent();
        }
        private static CodeFormat transmitFormat = CodeFormat.Pronto;
        private static LearnCompletedEventArgs learnCompletedEventArgs = null; ///é anulado o Evento LearnCompleted
        private static void mc_TransmitCompleted(object sender, TransmitCompletedEventArgs e)
        {
            ManualResetEvent waitEvent = e.UserState as ManualResetEvent;
            waitEvent.Set();
        }

        void LearnCompletedEvent(object sender, LearnCompletedEventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

        private static void TestLearn(Controller mc, CodeFormat learnFormat, LearnCodeModifier learnCodeModifier)
        {
            learnCompletedEventArgs = null;
            Console.WriteLine("<Press x to abort Learn>");
            mc.Learning += new UsbUirt.Controller.LearningEventHandler(mc_Learning);
      //      mc.LearnCompleted += new UsbUirt.Controller.LearnCompletedEventHandler(mc_LearnCompleted);
            
            try
            {
                try
                {
                   mc.Learn(learnFormat, learnCodeModifier, 0, new TimeSpan(0, 0, 0));
                    //mc.LearnAsync(learnFormat, learnCodeModifier, learnCompletedEventArgs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling LearnAsync! ***");
                    throw;
                }


                       while (learnCompletedEventArgs == null)
                   {

                //string numero= ;
                // string irCode = ;

              //  int ret = numero.CompareTo(irCode);

                string s = Console.ReadLine();
                if (s.Length != 0 && s[0] == 'x')
                {

                    if (learnCompletedEventArgs == null)
                    {
                        Console.WriteLine("Calling LearnAsyncCancel...");
                        mc.LearnAsyncCancel(learnCompletedEventArgs);
                        Thread.Sleep(1000);
                        break;
                    }

                    else
                    {
                        Console.WriteLine("<Press x to abort Learn>");
                    }
                 }

                 }

                //if (string.IsNullOrEmpty(irCode))
                //{
                //    Console.WriteLine("String nula ou vazia");
                //}

                if (learnCompletedEventArgs != null && learnCompletedEventArgs.Cancelled == false && learnCompletedEventArgs.Error == null)
                {
                   
                    
                        irCode = learnCompletedEventArgs.Code;
                        Console.WriteLine("...Done...IRCode = {0}", irCode);
                        transmitFormat = learnFormat;
                  
                }

            }
            finally
            {
                mc.Learning -= new UsbUirt.Controller.LearningEventHandler(mc_Learning);
                mc.LearnCompleted -= new UsbUirt.Controller.LearnCompletedEventHandler(mc_LearnCompleted);
            }
           
        

        }

        public static void mc_Learning(object sender, LearningEventArgs e)
        {
            Console.WriteLine("Learning: {0}% freq={1} quality={2}", e.Progress, e.CarrierFrequency, e.SignalQuality);
        }

        private static void mc_LearnCompleted(object sender, LearnCompletedEventArgs e)
        {
            learnCompletedEventArgs = e;
            Console.WriteLine("Learn complete. Press return to continue.");
        }

        private static void mc_Received(object sender, ReceivedEventArgs e)
        {
            Console.WriteLine("Received: {0}", e.IRCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {

          //public  string numero;
          //  numero = irCode;
             if(radioButton1.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
               
                          ligar = irCode;
                       
                                 //  Console.WriteLine("asdkasgdaisgdi  " + ligar);
             }
            
           
            if(radioButton2.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                desligar=irCode;
            }
            if(radioButton3.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                aumvol=irCode;
            }
            if(radioButton4.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                dimivol=irCode;
            }
            if(radioButton5.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                aumca=irCode;
            }
            if(radioButton6.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                dimivol=irCode;
            }
            if(radioButton7.IsChecked==true)
            {
                TestLearn(mc, CodeFormat.Uuirt, LearnCodeModifier.None);
                mute=irCode;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                //Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(ligar, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
            

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(desligar, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(aumvol, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(dimivol, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(aumca, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(dimica, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            using (ManualResetEvent waitEvent = new ManualResetEvent(false))
            {
                mc.TransmitCompleted += new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                Console.WriteLine("\nTransmitting IR Code (non-blocking)...");
                try
                {
                    mc.TransmitAsync(mute, transmitFormat, 10, TimeSpan.Zero, waitEvent);
                    Console.WriteLine("...Returned from call...");
                    if (waitEvent.WaitOne(5000, false))
                    {
                        Console.WriteLine("...IR Transmission Complete!");
                    }
                    else
                    {
                        Console.WriteLine("*** ERROR: Timeout error waiting for IR to finish!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("*** ERROR calling TransmitAsync! ***");
                    throw;
                }
                finally
                {
                    mc.TransmitCompleted -= new UsbUirt.Controller.TransmitCompletedEventHandler(mc_TransmitCompleted);
                }
            }
        }

        }

    }
