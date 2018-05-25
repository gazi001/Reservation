using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RM.Common.DotNetConfig;

namespace yuding.DAL
{
    public class DataFactory
    {
        public static IDbHelper SqlDataBase()
        {
            return new SqlServerHelper(ConfigHelper.GetAppSettings("SqlServer_RM_DB"));
        }
    }
}