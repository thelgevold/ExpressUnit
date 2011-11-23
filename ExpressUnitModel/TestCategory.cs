using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressUnitModel
{
    public class TestCategory : Attribute
    {
        public TestCategory(string categories)
        {
            if(string.IsNullOrWhiteSpace(categories) == false)
            {
                Categories = categories.Split(',').ToList();
            }
        }

        public List<string> Categories
        {
            get; set;
        }
    }
}
