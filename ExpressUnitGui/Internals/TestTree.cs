using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace ExpressUnitGui
{
    public class TestTreeView : TreeView
    {
        public TestTreeView()
            : base()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(Change);
        }

        void Change(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SelectedItem != null)
            {
                SetValue(SelectedItem_Property, SelectedItem);
            }
        }

        public object CurrentlySelectedItem
        {
            get { return (object)GetValue(SelectedItem_Property); }
            set { SetValue(SelectedItem_Property, value); }
        }
        public static readonly DependencyProperty SelectedItem_Property = DependencyProperty.Register("CurrentlySelectedItem", typeof(object), typeof(TestTreeView), new UIPropertyMetadata(null));
    } 

}
