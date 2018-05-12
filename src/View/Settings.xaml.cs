﻿using MahApps.Metro.Controls;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phony.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        public Settings()
        {
            InitializeComponent();
        }
        public IEnumerable<Swatch> Swatches = new SwatchesProvider().Swatches;
        
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Theme == "BaseDark")
            {
                ThemeS.SelectedIndex = 1;
            }
            else if (Properties.Settings.Default.Theme == "BaseLight")
            {
                ThemeS.SelectedIndex = 0;
            }
            ThemePC.Text = Properties.Settings.Default.PrimaryColor;
            foreach (var item in Swatches)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                SolidColorBrush brush1 = new SolidColorBrush(item.ExemplarHue.Color);
                SolidColorBrush brush2 = new SolidColorBrush(item.ExemplarHue.Foreground);
                cbi.Background = brush1;
                cbi.Foreground = brush2;
                cbi.Content = item.Name;
                ThemePC.Items.Add(cbi);
                if (item.Name == Properties.Settings.Default.PrimaryColor.ToLowerInvariant())
                {
                    ThemePC.SelectedItem = cbi;
                }
            }
            ThemeAC.Text = Properties.Settings.Default.AccentColor;
            foreach (var item in Swatches)
            {
                try
                {
                    ComboBoxItem cbi = new ComboBoxItem();
                    SolidColorBrush brush1 = new SolidColorBrush(item.AccentExemplarHue.Color);
                    SolidColorBrush brush2 = new SolidColorBrush(item.AccentExemplarHue.Foreground);
                    cbi.Background = brush1;
                    cbi.Foreground = brush2;
                    cbi.Content = item.Name;
                    ThemeAC.Items.Add(cbi);
                    if (item.Name == Properties.Settings.Default.AccentColor.ToLowerInvariant())
                    {
                        ThemeAC.SelectedItem = cbi;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void SaveB_Click(object sender, RoutedEventArgs e)
        {
            
            new PaletteHelper().ReplacePrimaryColor(ThemePC.Text);
            try
            {
                new PaletteHelper().ReplaceAccentColor(ThemeAC.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            if (ThemeS.SelectedIndex == 0)
            {
                Properties.Settings.Default.Theme = "BaseLight";
                new PaletteHelper().SetLightDark(false);
            }
            else
            {
                Properties.Settings.Default.Theme = "BaseDark";
                new PaletteHelper().SetLightDark(true);
            }
            Properties.Settings.Default.PrimaryColor = ThemePC.Text;
            Properties.Settings.Default.AccentColor = ThemeAC.Text;
            Properties.Settings.Default.Save();
        }
    }
}