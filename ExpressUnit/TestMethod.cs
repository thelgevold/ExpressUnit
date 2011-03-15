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
using System.Reflection;
using System.ComponentModel;

namespace ExpressUnit
{
    public class TestMethod :INotifyPropertyChanged, ITest
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string color;

        public string UseCase
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Color"));
            }
        }

        
        public string Name
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public MemberInfo TestTearDown
        {
            get;
            set;
        }

        public MemberInfo TestSetup
        {
            get;
            set;
        }

        public MemberInfo MemberInfo
        {
            get;
            set;
        }


        public TestConstruct TestConstruct
        {
            get { return TestConstruct.TestMethod; }
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
    }
}
