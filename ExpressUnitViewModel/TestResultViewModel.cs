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
using ExpressUnit;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace ExpressUnitViewModel
{
    public class TestResultViewModel : BaseViewModel
    {
        private TestResult testResult;
        private ICommand sectionVisibilityCommand;
        private Visibility isDetailedSectionVisibile;
        public TestResultViewModel(TestResult result)
        {
            testResult = result;
            Initialize();
        }

        public TestResult TestResult
        {
            get
            {
                return this.testResult;
            }
        }

        public string UseCase
        {
            get
            {
                return TestResult.UseCase;
            }
        }

        private void Initialize()
        {
            IsDetailedSectionVisibile = Visibility.Collapsed;
            SolidColorBrush brush = new SolidColorBrush();
            if (testResult.Passed == true)
            {
                brush.Color = Colors.LightGreen;
            }
            else
            {
                brush.Color = Colors.Red;
            }
            BackgroundBrush = brush;
        }

        public ICommand SectionVisibilityCommand
        {
            get
            {
                if (sectionVisibilityCommand == null)
                {
                    sectionVisibilityCommand = new RelayCommand(ToggleSectionVisibility);
                }

                return sectionVisibilityCommand;
            }
        }

        private void ToggleSectionVisibility()
        {
            if (IsDetailedSectionVisibile == Visibility.Collapsed)
            {
                IsDetailedSectionVisibile = Visibility.Visible;
            }
            else
            {
                IsDetailedSectionVisibile = Visibility.Collapsed;
            }
        }

        public Brush BackgroundBrush
        {
            get;
            set;
        }

        public Visibility IsDetailedSectionVisibile
        {
            get
            {
                return isDetailedSectionVisibile;
            }
            set
            {
                isDetailedSectionVisibile = value;
                OnPropertyChanged("IsDetailedSectionVisibile");
            }

        }

        public Visibility ErrorVisibility
        {
            get
            {
                if (Passed == true)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public Visibility DurationVisibility
        {
            get
            {
                if (Passed == false)
                {
                    return Visibility.Collapsed;
                }

                return Visibility.Visible;
            }
        }

        public string TestName
        {
            get
            {
                return testResult.TestName;
            }
            set
            {
                testResult.TestName = value;
                OnPropertyChanged("TestName");
            }
        }

        public bool Passed
        {
            get
            {
                return testResult.Passed;
            }
            set
            {
                testResult.Passed = value;
                OnPropertyChanged("Passed");
            }
        }

        public string ResultText
        {
            get
            {
                return testResult.ResultText;
            }
        }

        public string StackTrace
        {
            get
            {
                return testResult.StackTrace;
            }
        }

        public string Duration
        {
            get
            {
                return testResult.Duration.ToString();
            }
        }
    }
}
