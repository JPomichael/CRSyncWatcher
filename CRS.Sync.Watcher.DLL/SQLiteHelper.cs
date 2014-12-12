using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.DLL
{
    public static class SQLiteHelper
    {
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=e:\\CRSInterFaceResult.db");

        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public static int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int result = 0;
            using (conn)
            {
                conn.Open();
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(conn))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        result = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return result;
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public static SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            conn.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public static DataTable GetSchema()
        {
            using (conn)
            {
                conn.Open();
                DataTable data = conn.GetSchema("TABLES");
                conn.Close();
                //foreach (DataColumn column in data.Columns) 
                //{ 
                //        Console.WriteLine(column.ColumnName); 
                //} 
                return data;
            }
        }

    }
}
