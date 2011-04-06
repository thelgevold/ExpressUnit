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
using ExpressUnitViewModel;
using System.Windows.Controls;
using ExpressUnit;
using System.Windows.Threading;
using System.Threading;
using UnitTester.Mocks;

namespace UnitTester.UnitTests
{
    
    [TestClass]
    public class TestMethodViewModelTest
    {
        [UnitTest]
        public void TestResultViewModels_Will_Return_TestResultViewModels_Based_On_Test_Results_Test()
        {
            TestMethodViewModel vm = new TestMethodViewModel(AddResultControl, ClearResultControl);
            vm.TestResults = new List<TestResult>();

            TestResult r1 = new TestResult();
            r1.Passed = true;
            r1.TestName = "Test1";
            vm.TestResults.Add(r1);

            r1 = new TestResult();
            r1.Passed = true;
            r1.TestName = "Test2";
            vm.TestResults.Add(r1);

            Confirm.Equal(2, vm.TestResultViewModels.Count);
            Confirm.Equal("Test1", vm.TestResultViewModels[0].TestResult.TestName);
            Confirm.Equal("Test2", vm.TestResultViewModels[1].TestResult.TestName);
        }

        [UnitTest]
        public void Setting_SelectedItem_Will_Load_Tests()
        {
            Thread t;
            t = new Thread(TestBody);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void TestBody()
        {
            TestMethodViewModel viewModel = new TestMethodViewModel(AddResultControl,ClearResultControl);

            viewModel.Tests = new List<TestFixture>();
            Confirm.Equal(0, viewModel.Tests.Count);

            ComboBoxItem item = new ComboBoxItem();
            item.Name = "UnitTests";
            viewModel.SelectedItem = item;

            Confirm.Different(0, viewModel.Tests.Count);

        }

        [UnitTest]
        public void RunTestCommand_Will_Execute_Passing_Test()
        {
            TestMethodViewModel viewModel = new TestMethodViewModel(AddResultControl, ClearResultControl);

            MockTestManager testManager = new MockTestManager();
            testManager.PassTests = true;

            viewModel.TestManager = testManager;

            TestFixture fixture = new TestFixture();

            TestMethod testMethod = new TestMethod();
            testMethod.Name = "test1";
            fixture.Tests = new List<TestMethod>();
            fixture.Tests.Add(testMethod);

            viewModel.Tests = new List<TestFixture>();
            viewModel.Tests.Add(fixture);

            viewModel.RunTestsCommand.Execute(null);

            Thread.Sleep(5000);

            Confirm.Equal(1, viewModel.TestsPassed);
            Confirm.Equal(0,viewModel.TestsFailed);
       
        }

        public void AddResultControl(TestResult res) { }

        public void ClearResultControl(){}

    }
}
