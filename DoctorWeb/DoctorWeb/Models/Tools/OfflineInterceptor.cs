using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DoctorWeb.Models.Tools
{
    public class OfflineInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            if (command.CommandText.StartsWith("DELETE") || command.CommandText.StartsWith("UPDATE"))
            {
                DbType[] quotedParameterTypes = new DbType[] {
                DbType.AnsiString, DbType.Date,
                DbType.DateTime, DbType.Guid, DbType.String,
                DbType.AnsiStringFixedLength, DbType.StringFixedLength
                };
                string query = command.CommandText;

                var arrParams = new SqlParameter[command.Parameters.Count];
                command.Parameters.CopyTo(arrParams, 0);

                foreach (SqlParameter p in arrParams.OrderByDescending(p => p.ParameterName.Length))
                {
                    string value = p.Value.ToString();
                    if (quotedParameterTypes.Contains(p.DbType))
                        value = "'" + value + "'";
                    query = query.Replace(p.ParameterName, value);
                }

                using (var db = new ApplicationDbContext())
                {
                    db.OfflineRecords.Add(new Models.Offline() { Query = query, ExecutedAt = DateTime.Now, IsExecuted = false });
                    db.SaveChanges();
                }
            }
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (command.CommandText.StartsWith("INSERT") && !command.CommandText.Contains("[Offlines]"))
            {
                DbType[] quotedParameterTypes = new DbType[] {
                DbType.AnsiString, DbType.Date,
                DbType.DateTime, DbType.Guid, DbType.String,
                DbType.AnsiStringFixedLength, DbType.StringFixedLength
                };
                string query = command.CommandText;

                var arrParams = new SqlParameter[command.Parameters.Count];
                command.Parameters.CopyTo(arrParams, 0);

                foreach (SqlParameter p in arrParams.OrderByDescending(p => p.ParameterName.Length))
                {
                    string value = p.Value.ToString();
                    if (quotedParameterTypes.Contains(p.DbType))
                        value = "'" + value + "'";
                    query = query.Replace(p.ParameterName, value);
                }

                using (var db = new ApplicationDbContext())
                {
                    db.OfflineRecords.Add(new Models.Offline() { Query = query, ExecutedAt = DateTime.Now, IsExecuted = false });
                    db.SaveChanges();
                }
            }
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            string text = command.CommandText;
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }
    }
}