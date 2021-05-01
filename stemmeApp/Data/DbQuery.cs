﻿using AspNet.Identity.MySQL;
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
            string commandText = "Select username, faculty, institute, info from candidate where username = @username";
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
                });
            }
            catch (ArgumentOutOfRangeException) { 
            }          
            return ReturnList;
        }

        public List<VoteModel> GetAllCandidates()
        {
            List<VoteModel> ReturnList = new List<VoteModel>();
            string commandText = "Select username, faculty, institute, info from candidate";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows= _database.Query(commandText, parameters);
            try
            {
                ReturnList.Add(new VoteModel()
                {
                    username = rows[0]["username"].ToString(),
                    faculty = rows[0]["faculty"].ToString(),
                    institute = rows[0]["institute"].ToString(),
                    info = rows[0]["info"].ToString(),
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

        public void UpdateCandidate(string username, string faculty, string institute, string info)
        {
            string commandText = @"Update candidate SET faculty=faculty, institute=@institute, info=@info WHERE username=@username";       
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@username", username);
            parameters.Add("@faculty", faculty);
            parameters.Add("@institute", institute);
            parameters.Add("@info", info);
            _database.Execute(commandText, parameters);
        }
        public string AdminGetUserDetails(string username)
        {
            string commandText = "Select username from candidate where username = @username";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", username } };
            String ReturnValue = _database.GetStrValue(commandText, parameters);
            return commandText;
        }
        public List <AdminUserViewModel> AdminGetUser(string username)
        {
            string commandText = @"SELECT * FROM users";
            List<AdminUserViewModel> ReturnList = new List<AdminUserViewModel>();
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", username } };
            var RV = _database.Query(commandText, parameters);
            try
            {
                ReturnList.Add(new AdminUserViewModel()
                {
                    Id = RV[0]["Id"].ToString(),
                    UserName = RV[0]["UserName"].ToString(),
                    Email = RV[0]["Email"].ToString(),
                });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }
        public void AdminUpdateUser(string UserName, string Email, string FirstName, string LastName, string PhoneNumber)
        {
            string commandText = @"UPDATE users (UserName, Email, FirstName, LastName, PhoneNumber)
                VALUES (@username, @Email, @FirstName, @LastName, @PhoneNumber)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@UserName", UserName);
            parameters.Add("@Email", Email);
            parameters.Add("@FirstName", FirstName);
            parameters.Add("@LastName", LastName);
            parameters.Add("@PhoneNumber", PhoneNumber);
            _database.Execute(commandText, parameters);
        }
    }
}