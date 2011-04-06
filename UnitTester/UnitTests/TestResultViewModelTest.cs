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
using ExpressUnitModel;
using ExpressUnit;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using ExpressUnitViewModel;

namespace UnitTester.UnitTests
{
    [TestClass]
    public class TestResultViewModelTest
    {
        [UnitTest]
        public void IsDetailedSectionVisibile_Is_Colapsed_By_Default_Test()
        {
            TestResult result = new TestResult();
            result.Passed = true;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            Confirm.Equal(Visibility.Collapsed, viewModel.IsDetailedSectionVisibile);
        }

        [UnitTest]
        public void SectionVisibilityCommand_Will_Fire_And_Toggle_Detail_Section_To_Visible_Test()
        {
            TestResult result = new TestResult();
            result.Passed = true;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            viewModel.SectionVisibilityCommand.Execute(null);
            Confirm.Equal(Visibility.Visible, viewModel.IsDetailedSectionVisibile);
        }

        [UnitTest]
        public void SectionVisibilityCommand_Will_Fire_Twice_And_Toggle_Detail_Section_To_Colapsed_Test()
        {
            TestResult result = new TestResult();
            result.Passed = true;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            viewModel.SectionVisibilityCommand.Execute(null);
            viewModel.SectionVisibilityCommand.Execute(null);

            Confirm.Equal(Visibility.Collapsed, viewModel.IsDetailedSectionVisibile);
        }

        [UnitTest]
        public void ErrorVisibility_Will_Return_Visibility_Colapsed_If_Test_Passed_Test()
        {
            TestResult result = new TestResult();
            result.Passed = true;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            Confirm.Equal(Visibility.Collapsed, viewModel.ErrorVisibility);
        }

        [UnitTest]
        public void ErrorVisibility_Will_Return_Visibility_Visible_If_Test_Failed_Test()
        {
            TestResult result = new TestResult();
            result.Passed = false;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            Confirm.Equal(Visibility.Visible, viewModel.ErrorVisibility);
        }

        [UnitTest]
        public void DurationVisibility_Will_Return_Visibility_Visible_If_Test_Passed_Test()
        {
            TestResult result = new TestResult();
            result.Passed = true;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            Confirm.Equal(Visibility.Visible, viewModel.DurationVisibility);
       
        }

        [UnitTest]
        public void DurationVisibility_Will_Return_Visibility_Colapsed_If_Test_Failed_Test()
        {
            TestResult result = new TestResult();
            result.Passed = false;
            TestResultViewModel viewModel = new TestResultViewModel(result);

            Confirm.Equal(Visibility.Collapsed, viewModel.DurationVisibility);

        }
    }
}
