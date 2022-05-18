namespace Authentication.Server
{
    public static class Emails
    {
        public static string PasswordChanged(string requestHelpLink = "#") 
        {
            string html = File.ReadAllText("EmailTemplates/PasswordChanged.html");
            html = html.Replace("{REQUEST_HELP_LINK}", requestHelpLink);
            return html;
        }
    }
}