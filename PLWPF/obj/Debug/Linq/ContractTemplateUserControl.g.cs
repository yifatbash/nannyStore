﻿#pragma checksum "..\..\..\Linq\ContractTemplateUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "91C958E86BAF410239AC3F40E7CD082F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PLWPF;
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


namespace PLWPF {
    
    
    /// <summary>
    /// ContractTemplateUserControl
    /// </summary>
    public partial class ContractTemplateUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NannyLabel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ChildLabel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MotherLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DateLabel;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SumLabel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label id;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Linq\ContractTemplateUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button moreDetails;
        
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
            System.Uri resourceLocater = new System.Uri("/PLWPF;component/linq/contracttemplateusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Linq\ContractTemplateUserControl.xaml"
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
            
            #line 8 "..\..\..\Linq\ContractTemplateUserControl.xaml"
            ((PLWPF.ContractTemplateUserControl)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NannyLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ChildLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.MotherLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.DateLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.SumLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.id = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.moreDetails = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Linq\ContractTemplateUserControl.xaml"
            this.moreDetails.Click += new System.Windows.RoutedEventHandler(this.moreDetails_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

