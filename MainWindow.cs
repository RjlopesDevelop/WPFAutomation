using System.Threading;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.IO.Ports;
using System.Windows.Threading;

namespace WPFSerialApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SerialPort serialPort1 = new SerialPort();
        
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;

			timer.Start();
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = ComboBoxPort.Items[ComboBoxPort.SelectedIndex].ToString();
                    serialPort1.Open();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    msgPortSerial.Content =  "PORTA CONECTADA";
                    btConectar.Content = "Desconectar";
                    // ComboBox1.Style.Setters[0].IsSealed = false;

                }
            }
            else
            {

                try
                {
                    serialPort1.Close();
                    // ComboBoxPort.Enabled = true;
                    //  MessageBox.Show("Porta desconetada com sucesso!", "Aviso");
                    msgPortSerial.Content = "PORTA DESCONECTADA";
                    btConectar.Content = "Conectar";
                }
                catch
                {

                    return;
                }

            }
            // MessageBox.Show(" Conectado!!", "Aviso");
        }
        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (ComboBoxPort.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (ComboBoxPort.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                if (ComboBoxPort.Items.Count == 0)
                   msgPortSerial.Content = "NENHUMA PORTA ENCONTRADA";
                  // msgNotSerial.Visibility = Visibility.Visible;
                   
                   return;                     //retorna
            }

            //limpa comboBox
            ComboBoxPort.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                ComboBoxPort.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            ComboBoxPort.SelectedIndex = 0;
              msgPortSerial.Content = string.Empty;

        }
        private void timerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }
        private void btEnviar_Click(object sender, EventArgs e)
        {
            //porta está aberta
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write("A");  //envia o texto presente no textbox Enviar

                if (btEnviar.Content.Equals("DESLIGADO"))
                {
                    btEnviar.Content = "LIGADO";
                }
                else
                {
                    btEnviar.Content = "DESLIGADO";
                }

            }

        }
        private void timer_Tick(object sender, EventArgs e)
		{
			 atualizaListaCOMs();
		}
        
    }
}
