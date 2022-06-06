using Ski.Base.Util.Enums;
using Ski.Base.Util.Models;

namespace Ski.Base.Util.Services
{
    //global function
    #pragma warning disable CA2211 // 非常數欄位不應可見
    public static class _Fun
    {
        #region input parameters
        //is devironment or not
        public static bool IsDev;

        //private static ServiceContainer _DI;
        public static IServiceProvider DiBox;

        //database type
        public static DbTypeEnum DbType;

        //program auth type
        public static AuthTypeEnum AuthType;
        #endregion

        #region Db variables
        //datetime format for read/write db
        public static string DbDtFmt;
        public static string DbDateFmt;

        //for read page rows
        public static string ReadPageSql;

        //for delete rows
        public static string DeleteRowsSql;
        #endregion

        //c# datetime format, when js send to c#, will match to _fun.MmDtFmt
        public const string CsDtFmt = "yyyy/MM/dd HH:mm:ss";
        public const string CsDtFmt2 = "yyyy/MM/dd HH:mm";

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

        /// <summary>
        /// initial db environment for Ap with db function !!
        /// </summary>
        /// <param name="isDev">is devironment or not</param>
        /// <param name="diBox"></param>
        /// <param name="dbType"></param>
        /// <param name="authType"></param>
        /// <returns>error msg if any</returns>
        public static string Init(bool isDev, IServiceProvider diBox, DbTypeEnum dbType = DbTypeEnum.MSSql,
            AuthTypeEnum authType = AuthTypeEnum.None)
        {
            //set instance variables
            IsDev = isDev;
            DiBox = diBox;
            DbType = dbType;
            AuthType = authType;

            Config.HtmlImageUrl = _Str.AddSlash(Config.HtmlImageUrl);

            #region set smtp
            var smtp = Config.Smtp;
            if (smtp != "")
            {
                try
                {
                    var cols = smtp.Split(',');
                    Smtp = new SmtpDto()
                    {
                        Host = cols[0],
                        Port = int.Parse(cols[1]),
                        Ssl = bool.Parse(cols[2]),
                        Id = cols[3],
                        Pwd = cols[4],
                        FromEmail = cols[5],
                        FromName = cols[6]
                    };
                }
                catch
                {
                    return "config Smtp is wrong(7 cols): Host,Port,Ssl,Id,Pwd,FromEmail,FromName";
                }
            }
            #endregion

            #region set DB variables
            //0:select, 1:order by, 2:start row(base 0), 3:rows count
            switch (dbType)
            {
                case DbTypeEnum.MSSql:
                    DbDtFmt = "yyyy-MM-dd HH:mm:ss";
                    DbDateFmt = "yyyy-MM-dd";

                    //for sql 2012, more easy
                    //note: offset/fetch not sql argument
                    ReadPageSql = @"
select {0} {1}
offset {2} rows fetch next {3} rows only
";
                    DeleteRowsSql = "delete {0} where {1} in ({2})";
                    break;

                case DbTypeEnum.MySql:
                    DbDtFmt = "YYYY-MM-DD HH:mm:SS";
                    DbDateFmt = "YYYY-MM-DD";

                    ReadPageSql = @"
select {0} {1}
limit {2},{3}
";
                    //TODO: set delete sql for MySql
                    //DeleteRowsSql = 
                    break;

                case DbTypeEnum.Oracle:
                    DbDtFmt = "YYYY/MM/DD HH24:MI:SS";
                    DbDateFmt = "YYYY/MM/DD";

                    //for Oracle 12c after(same as mssql)
                    ReadPageSql = @"
                            Select {0} {1}
                            Offset {2} Rows Fetch Next {3} Rows Only
                            ";
                    //TODO: set delete sql for Oracle
                    //DeleteRowsSql = 
                    break;
            }
            #endregion

            //case of ok
            return "";
        }
    }
}
