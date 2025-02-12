using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace app_wpf
{
    class RadioButtonDiasble : RadioButton
    {
        protected override void OnClick()
        {
            if (IsChecked == true) IsChecked = false;
            else base.OnClick();
        }
    }
}
