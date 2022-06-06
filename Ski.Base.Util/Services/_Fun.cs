using Ski.Base.Util.Models;

namespace Ski.Base.Util.Services
{
    //global function
    #pragma warning disable CA2211 // 非常數欄位不應可見
    public static class _Fun
    {
        //directory tail seperator
        public static char DirSep = Path.DirectorySeparatorChar;

        //ap physical path, has right slash
        public static string DirRoot = _Str.GetLeft(AppDomain.CurrentDomain.BaseDirectory, "bin" + DirSep);

        //indicate error
        public const string PreError = "E:";
        public const string PreBrError = "B:";  //_BR code error

        public static string SystemError = "System Error, Please Contact Administrator.";


        #region others variables
        //from config file
        public static ConfigDto Config;

        public static SmtpDto Smtp = default;
        #endregion
    }
}
