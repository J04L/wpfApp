﻿#pragma checksum "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42416BA3C4056B0C7D3F52F52050979EBE9F1B0B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Themes;
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
using app_wpf;


namespace app_wpf {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 245 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tipoHabitacionesFiltroCombobox;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox precioMinFiltro;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox precioMaxFiltro;
        
        #line default
        #line hidden
        
        
        #line 267 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider huespedesSliderFiltro;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockSlider;
        
        #line default
        #line hidden
        
        
        #line 278 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal app_wpf.RadioButtonDiasble disponibleFiltro;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pisoMinFiltro;
        
        #line default
        #line hidden
        
        
        #line 293 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pisoMaxFiltro;
        
        #line default
        #line hidden
        
        
        #line 319 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridHabitaciones;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/app_wpf;component/view/listas/listahabitaciones.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tipoHabitacionesFiltroCombobox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.precioMinFiltro = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.precioMaxFiltro = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.huespedesSliderFiltro = ((System.Windows.Controls.Slider)(target));
            
            #line 268 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
            this.huespedesSliderFiltro.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.RangeBase_OnValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TextBlockSlider = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.disponibleFiltro = ((app_wpf.RadioButtonDiasble)(target));
            return;
            case 7:
            this.pisoMinFiltro = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.pisoMaxFiltro = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 302 "..\..\..\..\..\View\Listas\ListaHabitaciones.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonFiltro_OnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            this.DataGridHabitaciones = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

