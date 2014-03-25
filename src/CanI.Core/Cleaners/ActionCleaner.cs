using System;
using System.Collections.Generic;
using System.Linq;
using CanI.Core.Configuration;

namespace CanI.Core.Cleaners
{
    public class ActionCleaner
    {
        private readonly IConfigurationLogger logger;
        private readonly IDictionary<string, string> actionAliases;

        public ActionCleaner(IConfigurationLogger logger)
        {
            this.logger = logger;
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

            foreach (var alias in actionAliases)
            {
                lowerCaseAction = lowerCaseAction.Replace(alias.Key, alias.Value);
            }

            return lowerCaseAction;
        }

        public void ConfigureAliases(string intendedAction, params string[] aliases)
        {
            foreach (var alias in aliases)
            {
                if (actionAliases.ContainsKey(alias.ToLower()))
                {
                    if (actionAliases[alias.ToLower()] == intendedAction) continue;

                    logger.LogConfiguration(string.Format("overwriting action alias '{0} = {1}' (was '{0} = {2}')", alias.ToLower(), intendedAction, actionAliases[alias.ToLower()]));
                    actionAliases[alias.ToLower()] = intendedAction;
                }
                else
                {
                    logger.LogConfiguration(string.Format("creating action alias '{0} = {1}'", alias.ToLower(), intendedAction));
                    actionAliases.Add(alias.ToLower(), intendedAction);
                }
            }
        }

        public IEnumerable<string> AliasesFor(string action)
        {
            if (actionAliases.ContainsKey(action))
            {
                var referenceAction = actionAliases[action];
                return actionAliases
                    .Where(entry => entry.Value == referenceAction)
                    .Select(entry => entry.Key);
            }

            return new List<string> { action };
        }
    }
}