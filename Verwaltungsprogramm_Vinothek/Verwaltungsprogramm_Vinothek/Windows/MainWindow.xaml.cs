using System;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Verwaltungsprogramm_Vinothek.Pages;
using Verwaltungsprogramm_Vinothek.Windows;

namespace Verwaltungsprogramm_Vinothek
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double Scale { get; set; } = 1;
        private Benutzer User { get; }

        private string tmpDirectory = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\tmp";
        public MainWindow(Benutzer user) //user wird weitergegeben
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            User = user;
            Frame_Main.Content = new Page_MainMenu();

            if (!Directory.Exists(tmpDirectory))
            {
                Directory.CreateDirectory(tmpDirectory);
            }
        }

        private void Button_Click_Expander(object sender, RoutedEventArgs e) //Expander = Menü an der Seite
        {
            Button b = (Button)sender;
            string name = b.Name;
            switch (name)
            {
                case "Option_0":
                    Frame_Main.Content = new Page_MainMenu();
                    break;
                case "Option_1":
                    Frame_Main.Content = new Page_Grid_List("ListeProdukte");
                    break;
                case "Option_2":
                    Frame_Main.Content = new Page_Grid_List("ListeProduzenten");
                    break;
                case "Option_3":
                    Frame_Main.Content = new Page_Grid_List("ListeEvents");
                    break;
                case "Option_4":
                    Frame_Main.Content = new Page_Add_Produkt();
                    break;
                case "Option_5":
                    Frame_Main.Content = new Page_Add_Produzent();
                    break;
                case "Option_6":
                    Frame_Main.Content = new Page_Add_Veranstaltung();
                    break;
            }
            expander.IsExpanded = false;
        }

        private async void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e) //Zoom - Funktion
        {
            if (Keyboard.Modifiers != ModifierKeys.Control) //Key STRG
                return;
            if (e.Delta < 0 && Scale > 0.7) //Mausrad runter
                Scale -= 0.1;
            else if (e.Delta > 0 && Scale < 1.5) //Mausrad hoch
                Scale += 0.1;
            Frame_Main.LayoutTransform = new ScaleTransform(Scale, Scale);
            label_zoom.Content = Math.Round(Scale * 100 ,0) + " %"; //wird angezeigt
            await Timer(4000);
        }

        private Task Timer(int i)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(i);
                Dispatcher.Invoke(() => { label_zoom.Content = null; });
            });
        }

        private void Button_Previous_Click(object sender, RoutedEventArgs e)
        {
            NavigationService n = NavigationService.GetNavigationService((DependencyObject)Frame_Main.Content); //Bei Hauptmenü geht der GoBack - Button nicht
            if(Frame_Main.Content.GetType() != typeof(Page_MainMenu))
            n.GoBack();
        }

        public int GetUserID() //user - ID für Nutzerverwaltung (nur bei Admin)
        {
            return User.ID_Benutzer;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (Directory.Exists(tmpDirectory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(tmpDirectory);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(tmpDirectory);
            }
        }
    }
}
