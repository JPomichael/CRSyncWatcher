using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRS.Sync.Watcher.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.Linq;

namespace CRS.Sync.Watcher.DLL
{
    public static class ConnHelper
    {
        //!+ DB连接字符串配置
        //    2014-11-19   JPomichael
        public static string dbConnectionConfig = ConfigurationManager.ConnectionStrings["estay_ecsdbConnectionString"].ToString();

        /// <summary>
        /// 获取数据操作接口（linq）
        /// </summary>
        /// <returns></returns>
        public static estay_ecsdbDataContext estay_ecsdb()
        {
            string _ConnectionString = dbConnectionConfig;
            return new estay_ecsdbDataContext(_ConnectionString);
        }
        /// <summary>
        /// 获取数据操作接口（linq）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static estay_ecsdbDataContext estay_ecs(string connectionString)
        {
            return new estay_ecsdbDataContext(connectionString);
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection Conn()
        {
            string cldb_ConnectionString = dbConnectionConfig;
            return new SqlConnection(cldb_ConnectionString);
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public static SqlConnection Conn(string connectionString)
        {
            return new SqlConnection(connectionString);
        }


    }
}

