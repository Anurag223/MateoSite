#region Header

// Schlumberger Private
// Copyright 2020 Schlumberger.  All rights reserved in Schlumberger
// authored and generated code (including the selection and arrangement of
// the source code base regardless of the authorship of individual files),
// but not including any copyright interest(s) owned by a third party
// related to source code or object code authored or generated by
// non-Schlumberger personnel.
// This source code includes Schlumberger confidential and/or proprietary
// information and may include Schlumberger trade secrets. Any use,
// disclosure and/or reproduction is prohibited unless authorized in
// writing.

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tlm.Fed.Adapters.Qtrac.Common.Helper
{
    public static class SqlDbContextHelper
    {
        public static async Task<IList<int>> FromStoreProcedureGetIntegerList(this DbContext dbContext, string storeProcedure, Action<DbCommand> addParam)
        {
            var conn = dbContext.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = storeProcedure;
                    addParam(command);
                    var reader = await command.ExecuteReaderAsync();

                    return await reader.TranslateIntValueAsync();
                }
            }
            finally
            {
                conn?.Close();
            }
        }

        public static void WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException(
                    "Call LoadStoredProc before using this method");
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
        }

        public static void WithSqlParam<T>(this DbCommand cmd, string paramName, T? paramValue)
            where T : struct

        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException(
                    "Call LoadStoredProc before using this method");
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            if (paramValue.HasValue)
                param.Value = paramValue;
            else
                param.Value = DBNull.Value;

            cmd.Parameters.Add(param);
        }

        public static async Task<IList<int>> TranslateIntValueAsync(this DbDataReader reader)
        {
            var results = new List<int>();

            while (await reader.ReadAsync())
            {
                var isDbNull = await reader.IsDBNullAsync(0);
                if (!isDbNull)
                {
                    var obj = reader.GetFieldValue<int>(0);
                    results.Add(obj);
                }
            }

            return results;
        }
    }
}