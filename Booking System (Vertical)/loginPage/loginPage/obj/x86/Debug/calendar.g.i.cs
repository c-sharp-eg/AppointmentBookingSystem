﻿#pragma checksum "..\..\..\calendar.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1717937D847B3B1413F407E57C130863"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1008
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace loginPage {
    
    
    /// <summary>
    /// calendar
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class calendar : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menu;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem fileMenu;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem fileLogOut;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem fileExit;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem patientMenu;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem patientNew;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem patientEdit;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem patientPast;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem apptMenu;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem apptCheckIn;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem apptCancel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem billMenu;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\calendar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem billOpen;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/loginPage;component/calendar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\calendar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menu = ((System.Windows.Controls.Menu)(target));
            return;
            case 2:
            this.fileMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 3:
            this.fileLogOut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 11 "..\..\..\calendar.xaml"
            this.fileLogOut.Click += new System.Windows.RoutedEventHandler(this.fileLogOut_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.fileExit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 12 "..\..\..\calendar.xaml"
            this.fileExit.Click += new System.Windows.RoutedEventHandler(this.fileExit_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.patientMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 6:
            this.patientNew = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 7:
            this.patientEdit = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 8:
            this.patientPast = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 9:
            this.apptMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 10:
            this.apptCheckIn = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 11:
            this.apptCancel = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 12:
            this.billMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 13:
            this.billOpen = ((System.Windows.Controls.MenuItem)(target));
            
            #line 25 "..\..\..\calendar.xaml"
            this.billOpen.Click += new System.Windows.RoutedEventHandler(this.billOpen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

