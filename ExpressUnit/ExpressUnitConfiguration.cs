using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ExpressUnit
{
    public class ExpressUnitConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("runTestsOnStartup", DefaultValue="false", IsRequired=true)]
        public bool RunTestsOnStartup
        {
            get
            {
                return (Boolean)this["runTestsOnStartup"];
            }
            set
            {
                this["runTestsOnStartup"] = value;
            }
        }

        [ConfigurationProperty("degreeOfParallelism", DefaultValue="1", IsRequired=true)]
        public int DegreeOfParallelism
        {
            get
            {
                return (int)this["degreeOfParallelism"];
            }
            set
            {
                this["degreeOfParallelism"] = value;
            }
        }

        [ConfigurationProperty("webTestSettings",IsRequired=false)]
        public WebTestSettings WebTestSettings
        {
            get
            {
                return (WebTestSettings)this["webTestSettings"];
            }
            set
            { this["webTestSettings"] = value; }
        }
    }

    public class WebTestSettings : ConfigurationElement
    {
        [ConfigurationProperty("startUrl", IsRequired=false)]
        public string StartUrl
        {
            get
            {
                return (string)this["startUrl"];
            }
            set
            {
                this["startUrl"] = value;
            }
        }

        [ConfigurationProperty("seleniumHost", IsRequired=false)]
        public string SeleniumHost
        {
            get
            {
                return (string)this["seleniumHost"];
            }
            set
            {
                this["seleniumHost"] = value;
            }
        }

        [ConfigurationProperty("seleniumPort", IsRequired=false)]
        public int SeleniumPort
        {
            get
            {
                return (int)this["seleniumPort"];
            }
            set
            {
                this["seleniumPort"] = value;
            }
        }

        [ConfigurationProperty("browser", IsRequired=false)]
        public string Browser
        {
            get
            {
                return (string)this["browser"];
            }
            set
            {
                this["browser"] = value;
            }
        }
    }
}
