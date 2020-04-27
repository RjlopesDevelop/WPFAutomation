﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.IO.Ports;

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
            atualizaListaCOMs();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = ComboBox1.Items[ComboBox1.SelectedIndex].ToString();
                    serialPort1.Open();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                     MessageBox.Show("Porta conectada com sucesso!", "Aviso");
                    btConectar.Content = "Desconectar";
                    // ComboBox1.Style.Setters[0].IsSealed = false;

                }
            }
            else
            {

                try
                {
                    serialPort1.Close();
                    // comboBox1.Enabled = true;
                     MessageBox.Show("Porta desconetada com sucesso!", "Aviso");
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
            if (ComboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (ComboBox1.Items[i++].Equals(s) == false)
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
                return;                     //retorna
            }

            //limpa comboBox
            ComboBox1.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                ComboBox1.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            ComboBox1.SelectedIndex = 0;
        }
        private void timerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }
        private void btEnviar_Click(object sender, EventArgs e)
        {
             //porta está aberta
            if (serialPort1.IsOpen == true){
                serialPort1.Write("A");  //envia o texto presente no textbox Enviar

                if (btEnviar.Content.Equals("DESLIGADO") )
                {
                      btEnviar.Content = "LIGADO";
                }
                else
                {
                    btEnviar.Content = "DESLIGADO";
                }
              
            }         
              
        }
    }
}
