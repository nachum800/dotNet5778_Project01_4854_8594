using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    public static class ChangeTextSize
    {
        public static void RecurseChangeRatio(DependencyObject current, double ChangeRatio)
        {

            foreach (object o in LogicalTreeHelper.GetChildren(current))
            {
               
                if (o.GetType() == typeof(Button))
                {

                    ((Button)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(Label))
                {
                    ((Label)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(TextBox))
                {
                    ((TextBox)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(TextBlock))
                {
                    ((TextBlock)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(DataGridTextColumn))
                {
                    ((DataGridTextColumn)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(DataGrid))
                {
                    ((DataGrid)o).FontSize *= ChangeRatio;
                }
                if (o.GetType() == typeof(Grid))
                {
                    RecurseChangeRatio((DependencyObject)o, ChangeRatio);
                    continue;
                }
            }

        }
    }
}
