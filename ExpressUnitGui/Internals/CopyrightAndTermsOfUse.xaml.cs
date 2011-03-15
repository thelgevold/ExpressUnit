using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace ExpressUnitGui
{
    /// <summary>
    /// Interaction logic for CopyrightAndTermsOfUse.xaml
    /// </summary>
    public partial class CopyrightAndTermsOfUse : UserControl
    {
        public CopyrightAndTermsOfUse()
        {
                InitializeComponent();
        }

      
        protected void outerBorder_Loaded(object sender, RoutedEventArgs e)
        {
            ((BeginStoryboard)Resources["storyBoard"]).Storyboard.Begin();
        }

        private void outerBorder_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
