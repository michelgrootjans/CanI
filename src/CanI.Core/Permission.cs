namespace CanI.Core
{
    public class Permission
    {
        private readonly string action;
        private readonly string subject;

        public Permission(string action, string subject)
        {
            this.action = action.ToLower();
            this.subject = subject.ToLower();
        }

        public bool Allows(string action, string subject)
        {
            var actionIsAllowed = MatchesAction(action.ToLower());
            var subjectIsAllowed = MatchesSubject(subject.ToLower());
            return actionIsAllowed && subjectIsAllowed;
        }

        private bool MatchesAction(string action)
        {
            if (this.action == "manage") 
                return true;

            return this.action == TranslateAction(action);
        }

        private bool MatchesSubject(string subject)
        {
            if (this.subject == "all") 
                return true;

            return this.subject == subject;
        }

        private static string TranslateAction(string action)
        {
            switch (action)
            {
                case "index":
                case "detail":
                case "show":
                    return "view";
                case "update":
                    return "edit";
                default:
                    return action;
            }
        }
    }
}