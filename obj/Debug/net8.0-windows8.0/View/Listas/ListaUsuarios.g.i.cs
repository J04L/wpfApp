// Updated by XamlIntelliSenseFileGenerator 12/02/2025 22:44:32
#pragma checksum "..\..\..\..\..\View\Listas\ListaUsuarios.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FE6A38DF12EE6043E6D0E185AAE2B5A0F24F0CB4"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
using app_wpf;


namespace app_wpf
{


    /// <summary>
    /// ListaUsuarios
    /// </summary>
    public partial class ListaUsuarios : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 59 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbxRol;

#line default
#line hidden


#line 65 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DtpNacimiento;

#line default
#line hidden


#line 69 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CbxCiudad;

#line default
#line hidden


#line 78 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBM;

#line default
#line hidden


#line 79 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBF;

#line default
#line hidden


#line 80 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RBUndetermined;

#line default
#line hidden


#line 85 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton TgglVip;

#line default
#line hidden


#line 90 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnBuscar;

#line default
#line hidden


#line 113 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridUsuarios;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/app_wpf;component/view/listas/listausuarios.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:

#line 33 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ListaHabitaciones_click);

#line default
#line hidden
                    return;
                case 2:
                    this.CbxRol = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 3:
                    this.DtpNacimiento = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 4:
                    this.CbxCiudad = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 5:
                    this.RBM = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 6:
                    this.RBF = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 7:
                    this.RBUndetermined = ((System.Windows.Controls.RadioButton)(target));
                    return;
                case 8:
                    this.TgglVip = ((System.Windows.Controls.Primitives.ToggleButton)(target));
                    return;
                case 9:
                    this.BtnBuscar = ((System.Windows.Controls.Button)(target));
                    return;
                case 10:

#line 104 "..\..\..\..\..\View\Listas\ListaUsuarios.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NuevoUser_click);

#line default
#line hidden
                    return;
                case 11:
                    this.DataGridUsuarios = ((System.Windows.Controls.DataGrid)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox TbxNombre;
        internal System.Windows.Controls.TextBox TbxApellido;
        internal System.Windows.Controls.TextBox TbxEmail;
    }
}

