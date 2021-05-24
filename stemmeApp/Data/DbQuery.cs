using AspNet.Identity.MySQL;
using MySql.Data.MySqlClient;
using stemmeApp.Models;
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
        /// Checks if a username exists in the users table
        /// </summary>
        public Boolean CheckIfUserExists(string Username)
        {
            Boolean UserExists;
            string commandText = "Select username from Users where username = @username";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            String ReturnValue = _database.GetStrValue(commandText, parameters);
            if (ReturnValue == null)
            {
                UserExists = false;
            }
            else {
                UserExists = true;
            }
            return UserExists;
        }

        /// <summary>
        /// Checks if a username exists in the candidate table
        /// </summary>
        public Boolean CheckIfCandidateExists(string Username)
        {
            Boolean UserExists;
            string commandText = "Select username from candidate where username = @username";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            String ReturnValue = _database.GetStrValue(commandText, parameters);
            if (ReturnValue == null)
            {
                UserExists = false;
            }
            else
            {
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

        /// <summary>
        /// Returns an entry from the candidate table
        /// </summary>
         public List<CandidateModel> GetCandidate(string username) {
            List<CandidateModel> ReturnList = new List<CandidateModel>();
            string commandText = @"Select username, faculty, institute, info, picture.loc, picture.text
            from candidate, picture where username = @username AND candidate.Picture = picture.Idpicture;";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", username } };
            var rows = _database.Query(commandText, parameters);
            try
            {
                ReturnList.Add(new CandidateModel()
                {
                    Email = rows[0]["username"].ToString(),
                    Faculty = rows[0]["faculty"].ToString(),
                    Institute = rows[0]["institute"].ToString(),
                    Info = rows[0]["info"].ToString(),
                    Picture = rows[0]["loc"].ToString(),
                    PictureText = rows[0]["text"].ToString(),
                });
            }
            catch (ArgumentOutOfRangeException) { 
            }          
            return ReturnList;
        }

        /// <summary>
        /// Returns pictureid for a user
        /// </summary>
        /// 
        public dynamic GetPictureId(string username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", username } }; 
            string commandText = "Select picture from candidate WHERE username = @username";
            var rows = _database.Query(commandText, parameters);
            String PictureIdString = rows[0]["picture"];
            int PictureId = Convert.ToInt32(PictureIdString);
            return PictureId;
        }

        public List<VoteModel> GetAllCandidates()
        {
            List<VoteModel> ReturnList = new List<VoteModel>();
            string commandText = @"Select username, faculty, institute, info, picture.loc, picture.text
            from candidate, picture where candidate.Picture = picture.Idpicture;"; ;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                for(int i=0;i<rows.Count();i++)
                ReturnList.Add(new VoteModel()
                {
                    username = rows[i]["username"].ToString(),
                    faculty = rows[i]["faculty"].ToString(),
                    institute = rows[i]["institute"].ToString(),
                    info = rows[i]["info"].ToString(),
                    picture = rows[i]["loc"].ToString(),


                });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }
        public List<VoteModel> GetPicture()
        {
            List<VoteModel> ReturnList = new List<VoteModel>();
            string commandText = "Select loc from picture";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                for (int i = 0; i < rows.Count(); i++)
                    ReturnList.Add(new VoteModel()
                    {
                        loc = rows[i]["loc"].ToString(),

                    });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }

        /// <summary>
        /// Updates an entry in the candidate table
        /// </summary>

        public void UpdateCandidate(string username, string faculty, string institute, string info, string DbPath, string picturetext)
        {
            string commandText = @"Update candidate SET faculty=@faculty, institute=@institute, info=@info WHERE username=@username";       
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            int PictureId = GetPictureId(username);
            parameters.Add("@pictureid", PictureId);
            parameters.Add("@username", username);
            parameters.Add("@faculty", faculty);
            parameters.Add("@institute", institute);
            parameters.Add("@info", info);
            parameters.Add("@dbpath", DbPath);
            parameters.Add("@picturetext", picturetext);
            _database.Execute(commandText, parameters);
            if (DbPath != null) //Only updates picture location if a new picture is uploaded
            {
                commandText = @"Update picture SET loc=@dbpath, text=@picturetext WHERE idpicture=@pictureid";
                _database.Query(commandText, parameters);
            }
            else { // if no picture is uploaded, only update the picture text
                commandText = @"Update picture SET text=@picturetext WHERE idpicture=@pictureid";
                _database.Query(commandText, parameters);
            }
            
        }

        public List <AdminGetUsers> AdminGetUsers()
        {
            string query = @"SELECT * FROM users";
            List<AdminGetUsers> returnQuery = new List<AdminGetUsers>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rs = _database.Query(query, parameters);
            try
            {
                for (int i = 0; i < rs.Count(); i++) {
                    returnQuery.Add(new AdminGetUsers()
                {
                    Id = rs[i]["Id"].ToString(),
                    Email = rs[i]["Email"].ToString(),
                    UserName = rs[i]["UserName"].ToString(),

                });
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return returnQuery;
        }
        public List<AdminGetUserDetails> AdminGetUserDetails(string userDetails)
        {
            string query = @"SELECT * FROM users";
            List<AdminGetUserDetails> returnQuery = new List<AdminGetUserDetails>();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", userDetails } };
            var rs = _database.Query(query, parameters);
            try
            {
                returnQuery.Add(new AdminGetUserDetails()
                    {
                        Email = rs[0]["Email"].ToString(),
                        FirstName = rs[0]["Firstname"].ToString(),
                        LastName = rs[0]["Lastname"].ToString(),
                    });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return returnQuery;
        }


        /// <summary>
        /// Removes a candidate in the candidate and picture table
        /// </summary>
        public void removeCandidate(string Username)
        {            
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            int PictureId = GetPictureId(Username);
            String commandText = "DELETE FROM picture WHERE idpicture = @pictureid";
            parameters.Add("@pictureid", PictureId);
            _database.Execute(commandText, parameters);
            commandText = "DELETE FROM candidate WHERE username = @username";
            _database.Execute(commandText, parameters);

        }
    }
}