using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Sitecore.Analytics;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace JonathanRobbins.ProfileTagRules
{
    [UsedImplicitly]
    public class WhenVisitorTag<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string TagName { get; set; }
        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.IsNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current");
            if (Tracker.Current.Contact == null 
                || string.IsNullOrEmpty(Value)
                || string.IsNullOrEmpty(TagName)
                || string.IsNullOrEmpty(Tracker.Current.Contact.Tags[TagName]))
                return false;
            string userValue = Value ?? string.Empty;
            string tagValue = Tracker.Current.Contact.Tags[TagName];
            return Compare(tagValue, userValue);
        }
    }
}
