﻿using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MoonSec
{
    public partial class MainWindow : Window
    {
        MoonSecNet moonSec;
        string fileP;
        public MainWindow()
        {
            InitializeComponent();

            moonSec = new MoonSecNet("YOUR_API_KEY");

            Platforms.ItemsSource = PlatformCollection.GetPlatforms().Keys;
            Platforms.SelectedIndex = 0;

            Bytecodes.ItemsSource = ByteCodeCollection.GetList().Values;
            Bytecodes.SelectedIndex = 2;

            strEncCheck.IsChecked = true;
            constEncCheck.IsChecked = true;
            antiDumpCheck.IsChecked = true;
            smallOutputCheck.IsChecked = false;
        }

        private void Platforms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            moonSec.UpdatePlatform((Platform)Platforms.SelectedIndex);
        }

        private void Bytecodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            moonSec.UpdateBytecode((Bytecode)Bytecodes.SelectedIndex);
        }

        private void obfuscate_Click(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(fileP, Encoding.UTF8);
            moonSec.Obfuscate(sr.ReadToEnd());
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Lua files (*.lua)|*.lua|Text Files|*.txt|All Files|*.*";
            ofd.FileName = "";
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;

            if (ofd.ShowDialog() == true)
            {
                if (File.Exists(ofd.FileName))
                {
                    fileP = ofd.FileName;
                    file.Text = ofd.FileName;
                    moonSec.UpdateTarget(ofd.FileName);
                }
            }
        }

        private void strEncCheck_Checked(object sender, RoutedEventArgs e)
        {
            moonSec.UpdateOptions(new bool[] { strEncCheck.IsChecked == true, constEncCheck.IsChecked == true, antiDumpCheck.IsChecked == true, smallOutputCheck.IsChecked == true });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
