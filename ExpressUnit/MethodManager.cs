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
using ExpressUnitModel;

namespace ExpressUnit
{
    public class MethodManager
    {
        public IList<TestMethod> GetTestsInTestClass(Type t, string testType)
        {
            MemberInfo[] memberInfo = GetMemberInfo(t, testType);

            IList<TestMethod> testMethods = new List<TestMethod>();

            MemberInfo testSetup = GetPostTestMethod<TestSetup>(t);
            MemberInfo testTearDown = GetPostTestMethod<TestTearDown>(t);

            foreach (MemberInfo info in memberInfo)
            {
                TestMethod m = new TestMethod();

                m.TestSetup = testSetup;
                m.TestTearDown = testTearDown;

                TestAttribute attribute = ((TestAttribute)info.GetCustomAttributes(typeof(TestAttribute), false)[0]);

                m.UseCase = attribute.UseCase ?? "Undefined";
                m.Order = attribute.Order;

                m.Name = info.Name;
                m.Type = t;
                m.Color = "Yellow";
                m.MemberInfo = info;
                testMethods.Add(m);
            }

            return testMethods;
        }

        private MemberInfo GetPostTestMethod<T>(Type type)
        {
            return type.GetMembers().Where(t => Attribute.IsDefined(t, typeof(T))).FirstOrDefault();
        }

        private MemberInfo[] GetMemberInfo(Type t, string testType)
        {
            if (testType == TestType.UnitTest)
            {
                var q = from m in t.GetMembers()
                        where Attribute.IsDefined(m, typeof(UnitTest)) == true && Attribute.IsDefined(m, typeof(Ignore)) == false
                        orderby m.Name ascending
                        select m;
                return q.ToArray<MemberInfo>();
            }
            else if (testType == TestType.IntegrationTest)
            {
                var q = from m in t.GetMembers()
                        where Attribute.IsDefined(m, typeof(IntegrationTest)) == true && Attribute.IsDefined(m, typeof(Ignore)) == false
                        orderby m.Name ascending
                        select m;
                return q.ToArray<MemberInfo>();
            }
            else if (testType == TestType.WebTest)
            {
                var q = from m in t.GetMembers()
                        where Attribute.IsDefined(m, typeof(WebTest)) == true && Attribute.IsDefined(m, typeof(Ignore)) == false
                        orderby m.Name ascending
                        select m;
                return q.ToArray<MemberInfo>();
            }
            else if (testType == TestType.All)
            {
                var q = from m in t.GetMembers()
                        where (Attribute.IsDefined(m, typeof(WebTest)) == true || Attribute.IsDefined(m, typeof(IntegrationTest)) == true || Attribute.IsDefined(m, typeof(UnitTest)) == true) && Attribute.IsDefined(m, typeof(Ignore)) == false
                        orderby m.Name ascending
                        select m;
                return q.ToArray<MemberInfo>();
            }

            throw new ArgumentException("testType");
        }
    }
}
