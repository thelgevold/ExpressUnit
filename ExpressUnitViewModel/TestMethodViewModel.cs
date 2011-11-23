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
using System.Diagnostics;
using System.Linq;
using System.Text;
using ExpressUnit;
using System.Windows.Media;
using System.Collections;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Controls;
using System.ComponentModel;
using System.Xml.Linq;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Windows;

namespace ExpressUnitViewModel
{
    public delegate void AddResultControl(TestResult res);
    public delegate void ClearResultControl();
    public delegate void CloseApp(int exitCode);

    public class TestMethodViewModel : BaseViewModel
    {
        private BackgroundWorker backgroundWorker;
        private ICommand loadTestsCommand;
        private ICommand runTestsCommand;
        private ICommand runTestsFromTreeViewCommand;
        private ComboBoxItem selectedItem;
        private IList<TestFixture> tests;
        private IList<TestResult> testResults = new List<TestResult>();
        private ITestManager testManager = new TestManager();
        private int testsPassed;
        private int testsFailed;
        private AddResultControl addResultControl;
        private ClearResultControl clearResultControl;
        private string lastTestRun;
        private string totalRunTime;
        private Visibility rotatorVisibility = Visibility.Hidden;
        private int totalTestCount;
        private ITest testsToRun;
        private bool xmlReportSaved;

        public string CurrentTestCategory { get; set; }
        
        private object testResultLock = new object();

        public TestMethodViewModel(AddResultControl addResultControl,ClearResultControl clearResultControl)
        {
            this.addResultControl = addResultControl;
            this.clearResultControl = clearResultControl;
            LoadTests(TestType.All);
        }

        public bool ConsoleMode
        {
            get;
            set;
        }

        public CloseApp CloseApp
        {
            get;
            set;
        }
        
        public ITestManager TestManager
        {
            get
            {
                return testManager;
            }
            set
            {
                testManager = value;
            }
        }

        public string TotalRunTime
        {
            get
            {
                return totalRunTime;
            }
            set
            {
                totalRunTime = value;
                OnPropertyChanged("TotalRunTime");
            }
        }

        public string LastTestRun
        {
            get
            {
                return lastTestRun;
            }
            set
            {
                lastTestRun = value;
                OnPropertyChanged("LastTestRun");
            }
        }

        public int TestsPassed
        {
            get
            {
                return testsPassed;
            }
            set
            {
                testsPassed = value;
                OnPropertyChanged("TestsPassed");
            }
        }

        public int TotalTestCount
        {
            get
            {
                return totalTestCount;
            }
            set
            {
                totalTestCount = value;
                OnPropertyChanged("TotalTestCount");
            }

        }

        public int TestsFailed
        {
            get
            {
                return testsFailed;
            }
            set
            {
                testsFailed = value;
                OnPropertyChanged("TestsFailed");
            }
        }

        public Visibility RotatorVisibility
        {
            get
            {
                return rotatorVisibility;
            }
            set
            {
                rotatorVisibility = value;
                OnPropertyChanged("RotatorVisibility");
            }
        }

        void backgroundWorker_RunAllTests(object sender, DoWorkEventArgs e)
        {
            RunAllTests();
        }

        #region Commands

        public ICommand RunTestsCommand
        {
            get
            {
                if (runTestsCommand == null)
                {
                    runTestsCommand = new RelayCommand(RunTests, CanTestsRun);
                }
                SelectedTestsToRun = null;
                return runTestsCommand;
            }
        }

        private bool CanTestsRun()
        {
            return backgroundWorker == null || backgroundWorker.IsBusy == false;
        }

        public ICommand RunTestsFromTreeViewCommand
        {
            get
            {
                if (runTestsFromTreeViewCommand == null)
                {
                    runTestsFromTreeViewCommand = new RelayCommand(RunSelectedTests, CanTestsRun);
                }
                return runTestsFromTreeViewCommand;
            }
        }

        public ICommand LoadTestsCommand
        {
            get
            {
                if (loadTestsCommand == null)
                {
                    loadTestsCommand = new RelayCommand(LoadTests);
                }
                return loadTestsCommand;
            }
        }

        #endregion

        public IList<TestResult> TestResults
        {
            get
            {
                return testResults;
            }
            set
            {
                testResults = value;
                OnPropertyChanged("TestResults");
            }
        }

        public ComboBoxItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                LoadTests();
                OnPropertyChanged("SelectedItem");
            }
        }

        public IList<TestFixture> Tests
        {
            get
            {
                return tests;
            }
            set
            {
                tests = value;
                OnPropertyChanged("Tests");
            }
        }

        public ITest SelectedTestsToRun
        {
            get
            {
                return testsToRun;
            }
            set
            {
                testsToRun = value;
                OnPropertyChanged("TestsToRun");
            }
        }

        private void ResetTreeNodeColor()
        {
            foreach (TestFixture f in Tests)
            {
                foreach (TestMethod m in f.Tests)
                {
                    m.Color = "Yellow";
                }
            }
        }

        public void LoadTests(string testType)
        {
            TestManager manager = new TestManager();
            Tests = manager.GetTests(testType,CurrentTestCategory);
        }

        private void RunTests()
        {
            SelectedTestsToRun = null;
            SetupTestRunWorker();
        }

        private void SetupTestRunWorker()
        {
            ResetTreeNodeColor();
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_RunAllTests);
            backgroundWorker.RunWorkerAsync();
        }

        private void StopTestExecution()
        {
            backgroundWorker.CancelAsync();
        }

        private void RunSelectedTests()
        {
            SetupTestRunWorker();
        }

        private void RunAllTests()
        {
            lock(testResultLock)
            {
                this.xmlReportSaved = false;    
            }
            
            
            System.Threading.Timer saveTimer = null;
            if (ConsoleMode == true)
            {
                saveTimer = new System.Threading.Timer(state => EnsureXmlReportIsSaved(), null, 30000, 30000);
            }

            TotalRunTime = string.Empty;
            RotatorVisibility = Visibility.Visible;
            TestResults = new List<TestResult>();
            LastTestRun = DateTime.Now.ToString("hh:mm:ss");
            this.clearResultControl();
            TestsPassed = 0;
            TestsFailed = 0;

            Stopwatch testsExecutionTime = Stopwatch.StartNew();
            List<TestMethod> allTests = GetTestsToRun();
            TotalTestCount = allTests.Count;

            TaskFactory factory = CreateTaskFactory();
            
            // group by order and order by the order
            var allTestsGroupedByOrder = allTests.GroupBy(t => t.Order).OrderBy(t => t.Key);

            //runs all tests in order using n threads
            foreach (var group in allTestsGroupedByOrder)
            {
                List<Task> tasks = new List<Task>();
                foreach (TestMethod method in group)
                {
                    tasks.Add(factory.StartNew((obj) => Run(obj as TestMethod), method));
                }
                Task.WaitAll(tasks.ToArray());
            }

            testsExecutionTime.Stop();

            TotalRunTime = string.Format("{0:D2} hrs, {1:D2} mins, {2:D2} secs", testsExecutionTime.Elapsed.Hours, testsExecutionTime.Elapsed.Minutes, testsExecutionTime.Elapsed.Seconds);

            if (saveTimer != null)
            {
                saveTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            }

            EnsureXmlReportIsSaved();

            if (ConsoleMode == true)
            {
                CloseApp(testsFailed);
            }

            RotatorVisibility = Visibility.Hidden;
        }
        
        public void EnsureXmlReportIsSaved()
        {
            lock (testResultLock)
            {
                if (xmlReportSaved == false && testResults.Any() == true)
                {
                    XDocument doc = XmlManager.CreateTestReport(this.TestResults);
                    doc.Save("report.xml", SaveOptions.None);
                    xmlReportSaved = true;
                }
            }
        }

        private List<TestMethod> GetTestsToRun()
        {
            List<TestMethod> allTests = new List<TestMethod>();
            if (SelectedTestsToRun != null)
            {
                if (SelectedTestsToRun.TestConstruct == TestConstruct.TestMethod)
                {
                    allTests.Add((TestMethod)SelectedTestsToRun);
                }
                else
                {
                    allTests.AddRange(((TestFixture)SelectedTestsToRun).Tests);
                }
            }
            else
            {
                allTests = Tests.SelectMany(t => t.Tests).ToList();
            }

            return allTests;
        }

        private TaskFactory CreateTaskFactory()
        {
            ExpressUnitConfigurationSection config = (ExpressUnitConfigurationSection)System.Configuration.ConfigurationManager.GetSection("ExpressUnitConfiguration");
            LimitedConcurrencyLevelTaskScheduler lcts = new LimitedConcurrencyLevelTaskScheduler(config.DegreeOfParallelism);
            TaskFactory factory = new TaskFactory(lcts);

            return factory;
        }

        private void Run(TestMethod method)
        {
            method.Color = "Orange";

            TestResult res = TestManager.RunTest(method);
          
            AddTestResult(res,method);
        }

        private void AddTestResult(TestResult res, TestMethod method)
        {
            lock (testResultLock)
            {
                if (res.Passed == true)
                {
                    method.Color = "Green";
                    TestsPassed++;
                }
                else
                {
                    method.Color = "Red";
                    TestsFailed++;
                }

                this.addResultControl(res);
                this.TestResults.Add(res);
                
                xmlReportSaved = false;

                string summaryLine = String.Format("{0} / {1} Tests Run - {2} Succeeded - {3} Failed", TestsPassed + TestsFailed, TotalTestCount, TestsPassed, TestsFailed);
                string resultString = res.ToString();

                int lineSize = Math.Max(summaryLine.Length, resultString.Length);

                StringBuilder resultOutput = new StringBuilder();
                resultOutput.AppendLine(String.Empty.PadRight(lineSize, '*'));
                resultOutput.AppendLine(summaryLine);
                resultOutput.AppendLine(resultString);
                resultOutput.AppendLine(String.Empty.PadRight(lineSize, '*'));

                if (testsFailed > 0)
                {
                    resultOutput.AppendLine("Failed tests:");
                    var failedTests = testResults.Where(t => t.Passed == false);
                    foreach(var test in failedTests)
                    {
                        resultOutput.AppendLine(test.ToString());
                    }
                    resultOutput.AppendLine(String.Empty.PadRight(lineSize, '*'));
                }
                
                Console.WriteLine(resultOutput.ToString());
            }
        }

        public List<TestResultViewModel> TestResultViewModels
        {
            get
            {
                List<TestResultViewModel> vmList = new List<TestResultViewModel>();

                foreach (TestResult res in this.TestResults)
                {
                    TestResultViewModel vm = new TestResultViewModel(res);
                    vmList.Add(vm);
                }

                return vmList;
            }
        }
        private void LoadTests()
        {
            TestManager manager = new TestManager();
            Tests = manager.GetTests(SelectedItem.Name,CurrentTestCategory);
        }
       
    }
}
