/*
Copyright (C) 2009  Torgeir Helgevold

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
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
using ExpressUnit;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Threading;
using ExpressUnitViewModel;

namespace ExpressUnitGui
{
    /// <summary>
    /// Interaction logic for Tests.xaml
    /// </summary>
    public partial class TestMethodView : UserControl
    {
        public TestMethodView()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            App.TestMethodViewModel = GuiHelper.CreateTestMethodViewModel(CloseApp, AddResultControl, ClearTestResult);
            App.TestMethodViewModel.SelectedItem = (ComboBoxItem)cmbTestType.FindName(TestType.All);

            if (App.RunAllTests == true)
            {
                App.TestMethodViewModel.RunTestsCommand.Execute(null);
            }
            
            this.DataContext = App.TestMethodViewModel;
        }
  
        private void ClearTestResult()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            resultPanel.Children.Clear();
                        }
                        ));
        }
       
        public void AddResultControl(TestResult res)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            TestResultControl resultControl = new TestResultControl();
                            resultControl.Initialize(res);
                            resultPanel.Children.Add(resultControl);
                        }
                        ));
        }

        private void CloseApp(int exitCode)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal,
                   new Action(
                       delegate()
                       {
                           Application.Current.Shutdown(exitCode);
                       }
                       )); 

        }
    }
  
}
