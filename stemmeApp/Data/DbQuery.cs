﻿using AspNet.Identity.MySQL;
using stemmeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;


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
            else
            {
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
        public void InsertNewCandidate(string username, string faculty, string institute, string info, int PictureId)
        {
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
        public void InsertNewImage(int id, string loc, string text)
        {
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
        public List<CandidateModel> GetCandidate(string username)
        {
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
            catch (ArgumentOutOfRangeException)
            {
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
            string commandText = "Select username, faculty, institute, info from candidate";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                for (int i = 0; i < rows.Count(); i++)
                    ReturnList.Add(new VoteModel()
                    {
                        username = rows[i]["username"].ToString(),
                        faculty = rows[i]["faculty"].ToString(),
                        institute = rows[i]["institute"].ToString(),
                        info = rows[i]["info"].ToString(),
                    });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }

        public void VoteForUser(string votedon, string voter)
        {
            try
            {
                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                var rows = _database.Query("SELECT * FROM votes WHERE voter = '" + voter + "';", parameters1);
                if (rows.Count() == 0 || rows.Count() == null)
                {


                    string commandText = @"INSERT INTO votes (Voter, Votedon) VALUES (@voter, @votedon)";
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("@voter", voter);
                    parameters.Add("@votedon", votedon);


                    _database.Execute(commandText, parameters);
                }
                else
                {
                    string commandText = @"UPDATE votes set votedon = '" + votedon + "' WHERE voter = '" + voter + "';";
                    Dictionary<string, object> parameters2 = new Dictionary<string, object>();
                    _database.Execute(commandText, parameters2);


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

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
            else
            { // if no picture is uploaded, only update the picture text
                commandText = @"Update picture SET text=@picturetext WHERE idpicture=@pictureid";
                _database.Query(commandText, parameters);
            }

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

      
        public List<AdminModel> AdminGetUsers()
        {
            string query = @"SELECT * FROM users";
            List<AdminModel> returnQuery = new List<AdminModel>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(query, parameters);
            try
            {
                for (int i = 0; i < rows.Count(); i++)
                {
                    returnQuery.Add(new AdminModel()
                    {
                        Id = rows[i]["Id"].ToString(),
                        Username = rows[i]["UserName"].ToString(),
                        Email = rows[i]["Email"].ToString(),
                        Firstname = rows[i]["Firstname"].ToString(),
                        Lastname = rows[i]["Lastname"].ToString(),
                    });
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return returnQuery;
        }
        public AdminModel AdminGetSingleUser(String Username) {
               AdminModel returnQuery = new AdminModel();

            string query = @"SELECT u.Username, u.Id, u.Email, u.Firstname, u.Lastname, u.PhoneNumber,
                            c.Faculty, c.Institute, c.Info, c.Picture
                            FROM users AS u 
                            JOIN candidate AS c ON u.UserName = c.UserName 
                            WHERE u.Username = @UserName;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@UserName", Username);
            var rows = _database.Query(query, parameters);

            if (rows != null && rows.Count == 1)
            {
                for (int i = 0; i < rows.Count(); i++)
                {
                    returnQuery = new AdminModel()
                    {

                        //From Users Table
                        Username = rows[i]["Username"].ToString(),
                        Id = rows[i]["Id"].ToString(),
                        Email = rows[i]["Email"].ToString(),
                        Firstname = rows[i]["Firstname"].ToString(),
                        Lastname = rows[i]["Lastname"].ToString(),
                        PhoneNumber = rows[i]["PhoneNumber"].ToString(),

                        //From Candidate Table
                        Faculty = rows[i]["Faculty"].ToString(),
                        Institute = rows[i]["Institute"].ToString(),
                        Info = rows[i]["Info"].ToString(),

                    };
                }
            }
            return returnQuery;
        }

        public void AdminEditUser(
            string Username, string Id, string Email,
            string Firstname, string Lastname, string PhoneNumber,
            string Faculty, string Institute, string Info)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string queryOne = @"Update users SET Email=@Email, Firstname=@Firstname, 
                Lastname=@Lastname, PhoneNumber=@PhoneNumber WHERE Username=@UserName;";
                parameters.Add("@Username", Username);
                parameters.Add("@Id", Id);
                parameters.Add("@Email", Email);
                parameters.Add("@Firstname", Firstname);
                parameters.Add("@Lastname", Lastname);
                parameters.Add("@PhoneNumber", PhoneNumber);
                _database.Execute(queryOne, parameters);

                Dictionary<string, object> alsoParameters = new Dictionary<string, object>();
                string queryTwo = @"Update candidate SET Faculty=@Faculty, Institute=@Institute, Info=@Info WHERE Username=@Username;";
                alsoParameters.Add("@Faculty", Faculty);
                alsoParameters.Add("@Institute", Institute);
                alsoParameters.Add("@Info", Info);
                alsoParameters.Add("@Username", Username);
                _database.Execute(queryTwo, alsoParameters);
            }
            
            catch (Exception e)
            {
                throw e;
            }

        }


        /// <summary>
        /// Gets all votes
        /// </summary>

            public List<Votes> getVotes()
            {
                List<Votes> ReturnList = new List<Votes>();
                string commandText = "Select * from votes";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                var rows = _database.Query(commandText, parameters);
                try
                {
                    for (int i = 0; i < rows.Count(); i++)
                        ReturnList.Add(new Votes()
                        {
                            Voter = rows[i]["Voter"].ToString(),
                            VotedOn = rows[i]["Votedon"].ToString(),
                        });
                }
                catch (ArgumentOutOfRangeException)
                {
                }
                return ReturnList;
            }

            /// <summary>
            /// Gets election information
            /// </summary>

            public List<ElectionInformation> getElectionInfo()
            {
                List<ElectionInformation> ReturnList = new List<ElectionInformation>();
                string commandText = "Select * from election";
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                var rows = _database.Query(commandText, parameters);
                try
                {
                    ReturnList.Add(new ElectionInformation()
                    {
                        IdElection = Int32.Parse(rows[0]["Idelection"]),
                        ElectionStart = DateTime.Parse(rows[0]["Startelection"]),
                        ElectionEnd = DateTime.Parse(rows[0]["Endelection"]),
                        Controlled = (rows[0]["Controlled"] == null) ? DateTime.MinValue : DateTime.Parse(rows[0]["Controlled"])

                    });
                }
                catch (Exception)
                {

            }
            return ReturnList;
        }


        /// <summary>
        /// Gets all candidates and their votes
        /// </summary>

        public List<CandidateVotes> getCandidateVotes(){
            List<CandidateVotes> ReturnList = new List<CandidateVotes>();
            string commandText = @"SELECT c.UserName, u.Firstname, u.Lastname, COUNT(v.Votedon) as Votes FROM candidate c
            LEFT JOIN Votes v ON c.UserName = v.Votedon
            LEFT JOIN Users u ON c.UserName = u.UserName
            GROUP BY c.UserName
            ORDER BY Votes DESC";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                for (int i = 0; i < rows.Count(); i++)
                    ReturnList.Add(new CandidateVotes()
                    {
                        Username = rows[i]["UserName"].ToString(),
                        Firstname = rows[i]["Firstname"].ToString(),
                        Lastname = rows[i]["Lastname"].ToString(),
                        Votes = Int32.Parse(rows[i]["Votes"]),
                    });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }

    }

    }

    