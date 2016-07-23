using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WV.WebApplication.Utils
{
    public class DataAccess
    {
        private AWContext _context ;

        public DataAccess()
        {
            _context = new AWContext();
        }

        public DataSet GetDataSet(string sql, CommandType commandType, Dictionary<string, Object> parameters)
        {
            // creates resulting dataset
            var result = new DataSet();

            // creates a data access context (DbContext descendant)
            using (var context = _context)
            {
                // creates a Command 
                var cmd = context.Database.Connection.CreateCommand();
                cmd.CommandType = commandType;
                cmd.CommandText = sql;

                // adds all parameters
                foreach (var pr in parameters)
                {
                    var p = cmd.CreateParameter();
                    p.ParameterName = pr.Key;
                    p.Value = pr.Value;
                    cmd.Parameters.Add(p);
                }

                try
                {
                    // executes
                    context.Database.Connection.Open();
                    var reader = cmd.ExecuteReader();

                    // loop through all resultsets (considering that it's possible to have more than one)
                    do
                    {
                        // loads the DataTable (schema will be fetch automatically)
                        var tb = new DataTable();
                        tb.Load(reader);
                        result.Tables.Add(tb);

                    } while (!reader.IsClosed);
                }
                finally
                {
                    // closes the connection
                    context.Database.Connection.Close();
                }
            }

            // returns the DataSet
            return result;
        }
    }
}