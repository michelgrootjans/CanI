using System.Collections.Generic;
using System.Linq;
using CanI.Core.Configuration;

namespace CanI.Core.Cleaners
{
    public class SubjectCleaner
    {
        private readonly IConfigurationLogger logger;
        private readonly IDictionary<string, string> subjectAliases;

        public SubjectCleaner(IConfigurationLogger logger)
        {
            this.logger = logger;
            subjectAliases = new Dictionary<string, string>();
        }

        public void AddSubjectAliases(string intendedSubject, string[] aliases)
        {
            foreach (var alias in aliases.Select(a => a.ToLower()))
            {
                intendedSubject = intendedSubject.ToLower();
                if (subjectAliases.ContainsKey(alias))
                {
                    logger.LogConfiguration(string.Format("overwriting subject alias '{0} = {1}' (was '{0} = {2}')", alias, intendedSubject, subjectAliases[alias]));
                    subjectAliases[alias] = intendedSubject;
                }
                else
                {
                    logger.LogConfiguration(string.Format("creating subject alias '{0} = {1}'", alias, intendedSubject));
                    subjectAliases.Add(alias, intendedSubject);
                }
            }
        }

        public string Clean(object subject)
        {
            var stringSubject = SubjectToString(subject);
            stringSubject = ReplaceSubjectAliases(stringSubject);
            return stringSubject;
        }

        private static string SubjectToString(object subject)
        {
            if (subject is string)
                return ((string)subject).ToLower();
            return subject.GetType().Name.ToLower();
        }

        private string ReplaceSubjectAliases(string subject)
        {
            if (subjectAliases.ContainsKey(subject))
                subject = subjectAliases[subject];
            foreach (var alias in subjectAliases)
                subject = subject.Replace(alias.Key, alias.Value);
            return subject;
        }

        public IEnumerable<string> AliasesFor(string allowedSubject)
        {
            yield return allowedSubject;
            foreach (var subjectAlias in subjectAliases.Where(subjectAlias => subjectAlias.Value == allowedSubject))
            {
                yield return subjectAlias.Key;
            }
        }
    }
}