namespace DentistAssistant.Extensions
{
    public static class LoginInfo
    {
        public static string UserNo { get; private set; }
        public static string UserName { get; private set; }

        public static void SetLoginUser(string userNo, string userName)
        {
            UserNo = userNo;
            UserName = userName;
        }
    }
}
