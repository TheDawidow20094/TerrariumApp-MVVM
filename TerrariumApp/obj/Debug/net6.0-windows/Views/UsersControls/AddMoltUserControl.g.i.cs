﻿#pragma checksum "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B21C7C62DCA79CB9AC6D68E9BA87B00414A7D302"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using System.Windows.Controls.Ribbon;
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
using TerrariumApp.Views.UsersControls;


namespace TerrariumApp.Views.UsersControls {
    
    
    /// <summary>
    /// AddMoltUserControl
    /// </summary>
    public partial class AddMoltUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSpiders;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imageMolt;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpMoltDate;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tboxDescription;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddMolt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TerrariumApp;component/views/userscontrols/addmoltusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.cbSpiders = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.imageMolt = ((System.Windows.Controls.Image)(target));
            
            #line 24 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
            this.imageMolt.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.imageMolt_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dpMoltDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 27 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
            this.dpMoltDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dpMoltDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tboxDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnAddMolt = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\..\Views\UsersControls\AddMoltUserControl.xaml"
            this.btnAddMolt.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnAddMolt_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

