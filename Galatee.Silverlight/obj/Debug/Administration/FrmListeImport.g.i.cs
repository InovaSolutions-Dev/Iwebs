﻿#pragma checksum "D:\TFS_SOURCE_EDM\iWEBS_EDMSA\Galatee.Silverlight\Administration\FrmListeImport.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3C99D7D46EB899EF515372BC885E2EC3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SilverlightContrib.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Galatee.Silverlight.Administration {
    
    
    public partial class FrmListeImport : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal SilverlightContrib.Controls.GroupBox GroupBox;
        
        internal System.Windows.Controls.DataGrid dgImport;
        
        internal System.Windows.Controls.ContextMenu MenuContextuel;
        
        internal System.Windows.Controls.MenuItem MenuContextuelCreer;
        
        internal System.Windows.Controls.MenuItem MenuContextuelConsulter;
        
        internal System.Windows.Controls.MenuItem MenuContextuelExecuter;
        
        internal System.Windows.Controls.MenuItem MenuContextuelModifier;
        
        internal System.Windows.Controls.MenuItem MenuContextuelSupprimer;
        
        internal System.Windows.Controls.Button btn_Valider;
        
        internal System.Windows.Controls.Button btn_Annule;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Galatee.Silverlight;component/Administration/FrmListeImport.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.GroupBox = ((SilverlightContrib.Controls.GroupBox)(this.FindName("GroupBox")));
            this.dgImport = ((System.Windows.Controls.DataGrid)(this.FindName("dgImport")));
            this.MenuContextuel = ((System.Windows.Controls.ContextMenu)(this.FindName("MenuContextuel")));
            this.MenuContextuelCreer = ((System.Windows.Controls.MenuItem)(this.FindName("MenuContextuelCreer")));
            this.MenuContextuelConsulter = ((System.Windows.Controls.MenuItem)(this.FindName("MenuContextuelConsulter")));
            this.MenuContextuelExecuter = ((System.Windows.Controls.MenuItem)(this.FindName("MenuContextuelExecuter")));
            this.MenuContextuelModifier = ((System.Windows.Controls.MenuItem)(this.FindName("MenuContextuelModifier")));
            this.MenuContextuelSupprimer = ((System.Windows.Controls.MenuItem)(this.FindName("MenuContextuelSupprimer")));
            this.btn_Valider = ((System.Windows.Controls.Button)(this.FindName("btn_Valider")));
            this.btn_Annule = ((System.Windows.Controls.Button)(this.FindName("btn_Annule")));
        }
    }
}

