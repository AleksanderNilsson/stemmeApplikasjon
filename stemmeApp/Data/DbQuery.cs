using AspNet.Identity.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace stemmeApp.Data
{
    public class DbQuery
    {
        MySQLDatabase _database = new MySQLDatabase();
        //.GetStrValue returns string from database
        //.Execute inserts into database

        /// <summary>
        /// Returns firstname by email
        /// </summary>
        public string GetFirstName(string email)
        {
            string commandText = "Select firstname from Users where email = @email";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@email", email } };
            String fornavn = _database.GetStrValue(commandText, parameters);
            return fornavn;
        }

        /// <summary>
        /// Inserts a candidate into the candidate table
        /// </summary>
        public void InsertNewNominee(string email, string info) {
            string commandText = @"Insert Into nominated (email, info)
                VALUES (@email, @info)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@email", email);
            parameters.Add("@info", info);
            _database.Execute(commandText, parameters);
        }
    }
}