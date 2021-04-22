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
        

        /// <summary>
        /// Checks if a username exists in the database
        /// </summary>
        public Boolean CheckIfUserExists(string Username)
        {
            Boolean UserExists;
            string commandText = "Select username from Users where username = @username";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            String fornavn = _database.GetStrValue(commandText, parameters);
            if (fornavn == null)
            {
                UserExists = false;
            }
            else {
                UserExists = true;
            }
            return UserExists;
        }


        /// <summary>
        /// Inserts a new candidate into the candidate table
        /// </summary>
        public void InsertNewCandidate(string username, string faculty, string institute, string info, int PictureId) {
            string commandText = @"Insert Into candidate (username, faculty, institute, info, picture)
                VALUES (@username, @faculty, @institute, @info, @pictureid)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);
            parameters.Add("@faculty", faculty);
            parameters.Add("@institute", institute);
            parameters.Add("@info", info);
            parameters.Add("@pictureid", PictureId);
            _database.Execute(commandText, parameters);
        }

        /// <summary>
        /// Inserts a new entry into the picture table
        /// </summary>
        public void InsertNewImage(int id, string loc, string text) {
            string commandText = @"Insert Into picture (idpicture, loc, text)
             VALUES (@id, @loc, @text)";          
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", id);
            parameters.Add("@loc", loc);
            parameters.Add("@text", text);
            _database.Execute(commandText, parameters);
        }


        /// <summary>
        /// Returns an unique int to be used as pictureid in the database
        /// </summary>
        public int CheckForAvailableImageId()
        {
            Boolean AvailableImageId = false;
            Random r = new Random();
            int random = r.Next(1, 99999);
            while (AvailableImageId == false)
            {
                
                string commandText = @"SELECT picture FROM candidate WHERE picture = @r";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@r", random);
                String result = _database.GetStrValue(commandText, parameters);
                if (result == null)
                {
                    AvailableImageId = true;
                }
                random = r.Next(1, 99999);
            }
            return random;
        }


    }
}