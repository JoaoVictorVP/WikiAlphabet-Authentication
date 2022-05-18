namespace Authentication.Server
{
    public static class Emails
    {
        public static string PasswordChanged(string requestHelpLink = "#") 
        {
            return GetEmail("REQUEST_HELP_LINK", requestHelpLink);
        }

        public static string EmailChanged()
        {
            return GetEmail("", "");
        }
        static string GetEmail(string replaceWhat, string replaceFor)
        {
            string html = File.ReadAllText("EmailTemplates/PasswordChanged.html");
            if(replaceWhat is not (null or ""))
                html = html.Replace("{" + replaceWhat + "}", replaceFor);
            return html;
        }
    }
}