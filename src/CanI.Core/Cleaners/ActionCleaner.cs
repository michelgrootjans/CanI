using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CanI.Core.Cleaners
{
    public class ActionCleaner
    {
        private readonly IDictionary<string, string> actionAliases;

        public ActionCleaner()
        {
            actionAliases = new Dictionary<string, string>
            {
                {"view", "view"},
                {"index", "view"},
                {"show", "view"},
                {"detail", "view"},
                {"read", "view"},

                {"create", "create"},
                {"insert", "create"},
                
                {"edit", "edit"},
                {"update", "edit"},
                {"change", "edit"},

                {"delete", "delete"},
                {"remove", "delete"},
                {"destroy", "delete"}
            };
        }

        public string Clean(string action)
        {
            var lowerCaseAction = action.ToLower();
            if (actionAliases.ContainsKey(lowerCaseAction))
                return actionAliases[lowerCaseAction];
            return lowerCaseAction;
        }

        public void ConfigureAliases(string intendedAction, params string[] aliases)
        {
            foreach (var alias in aliases)
                actionAliases.Add(alias, intendedAction);
        }

        public IEnumerable<string> AliasesFor(string action)
        {
            var referenceAction = actionAliases[action];
            return actionAliases
                .Where(entry => entry.Value == referenceAction)
                .Select(entry => entry.Key);
        }
    }
}