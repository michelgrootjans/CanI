using System.Collections.Generic;

namespace CanI.Core
{
    public class ActionCleaner
    {
        private readonly IDictionary<string, string> actionAliases;

        public ActionCleaner()
        {
            actionAliases = new Dictionary<string, string>
            {
                {"index", "view"},
                {"show", "view"},
                {"detail", "view"},
                {"read", "view"},
                {"insert", "create"},
                {"update", "edit"},
                {"change", "edit"},
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

        public void ConfigureAliases(string intendedAction, string[] aliases)
        {
            foreach (var alias in aliases)
                actionAliases.Add(alias, intendedAction);
        }
    }
}