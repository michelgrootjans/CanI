using System.Collections.Generic;
using System.Linq;

namespace CanI.Core
{
    public class SubjectCleaner
    {
        private readonly IList<string> ignoredSubjectPostfixes;
        private readonly IDictionary<string, string> subjectAliases;

        public SubjectCleaner()
        {
            ignoredSubjectPostfixes = new List<string>();
            subjectAliases = new Dictionary<string, string>();
        }

        public void IgnorePostfix(params string[] postfixes)
        {
            foreach (var postfix in postfixes)
                ignoredSubjectPostfixes.Add(postfix.ToLower());
        }

        public void AddSubjectAliases(string intendedSubject, string[] aliases)
        {
            foreach (var alias in aliases)
            {
                subjectAliases.Add(alias.ToLower(), intendedSubject.ToLower());
            }
        }

        public string Clean(object subject)
        {
            var stringSubject = SubjectToString(subject);
            stringSubject = RemoveSubjectAffixes(stringSubject);
            stringSubject = ReplaceSubjectAliases(stringSubject);
            return stringSubject;
        }

        private static string SubjectToString(object subject)
        {
            if (subject is string)
                return ((string)subject).ToLower();
            return subject.GetType().Name.ToLower();
        }

        private string RemoveSubjectAffixes(string subject)
        {
            var matchingPostfix = ignoredSubjectPostfixes.FirstOrDefault(subject.EndsWith);
            if (matchingPostfix != null)
                return subject.Replace(matchingPostfix, "");
            return subject;
        }

        private string ReplaceSubjectAliases(string subject)
        {
            if (subjectAliases.ContainsKey(subject))
                subject = subjectAliases[subject];
            return subject;
        }
    }
}