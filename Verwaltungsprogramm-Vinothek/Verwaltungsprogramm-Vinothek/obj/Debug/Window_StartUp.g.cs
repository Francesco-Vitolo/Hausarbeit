﻿#pragma checksum "..\..\Window_StartUp.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "81FED0E8520B7F70E3D7F6EBDD3522FD53D55710B8316AC6E1157597CA6E49B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Verwaltungsprogramm_Vinothek;


namespace Verwaltungsprogramm_Vinothek {
    
    
    /// <summary>
    /// Window_StartUp
    /// </summary>
    public partial class Window_StartUp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_Menu;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ListeProdukte;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ListeProduzenten;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ListeEvents;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_Liste;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSearch;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\Window_StartUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid data;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Verwaltungsprogramm-Vinothek;component/window_startup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window_StartUp.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Grid_Menu = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.ListeProdukte = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Window_StartUp.xaml"
            this.ListeProdukte.Click += new System.Windows.RoutedEventHandler(this.Button_MainMenu);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ListeProduzenten = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\Window_StartUp.xaml"
            this.ListeProduzenten.Click += new System.Windows.RoutedEventHandler(this.Button_MainMenu);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ListeEvents = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\Window_StartUp.xaml"
            this.ListeEvents.Click += new System.Windows.RoutedEventHandler(this.Button_MainMenu);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Grid_Liste = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.tbSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 56 "..\..\Window_StartUp.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_Search_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Add = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\Window_StartUp.xaml"
            this.Add.Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.data = ((System.Windows.Controls.DataGrid)(target));
            
            #line 70 "..\..\Window_StartUp.xaml"
            this.data.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.SelectItem_DoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

