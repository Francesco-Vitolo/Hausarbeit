using System;
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
        private double scale = 1;
        private Benutzer user { get; }
        public MainWindow(Benutzer user) //user wird weitergegeben
        {
            InitializeComponent();
            Style = FindResource("Window_Default") as Style;
            this.user = user;
            this.user.Passwort = null;
            tb_username.DataContext = this.user;
            Frame_Main.Content = new Page_MainMenu();
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
            if (e.Delta < 0 && scale > 0.7) //Mousewheel runter
                scale -= 0.1;
            else if (e.Delta > 0 && scale < 1.5) //Mousewheel hoch
                scale += 0.1;
            Frame_Main.LayoutTransform = new ScaleTransform(scale, scale);
            label_zoom.Content = Math.Round(scale * 100 ,0) + " %"; //wird angezeigt
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

        public int getUserID() //user - ID für Nutzerverwaltung (nur bei Admin)
        {
            return user.ID_Benutzer;
        }
    }
}
