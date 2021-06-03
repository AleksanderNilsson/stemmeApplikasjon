using AspNet.Identity.MySQL;
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
            string commandText = "Select username from Candidate where username = @username";
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
        /// Gets the role of a user
        /// </summary>
        public String getUserRole(string UserId)
        {
            string role = "";
            string commandText = @"SELECT name from Roles r, UserRoles u WHERE u.UserId = @id AND r.Id = u.RoleId;";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            DbQuery db = new DbQuery();
            parameters.Add("@id", UserId);
            var result = _database.Query(commandText, parameters);
            if (result.Count != 0)
            {
                role = result[0]["name"];
            }
            return role;
        }


        //CANDIDATE TABLE


        /// <summary>
        /// Inserts a new candidate into the candidate table
        /// </summary>
        public void InsertNewCandidate(string username, string faculty, string institute, string info, int PictureId)
        {
            string commandText = @"Insert Into Candidate (username, faculty, institute, info, picture)
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
        /// Returns an entry from the candidate table with picture
        /// </summary>
        public List<CandidateModel> GetCandidate(string username)
        {
            List<CandidateModel> ReturnList = new List<CandidateModel>();
            string commandText = @"Select username, faculty, institute, info, Picture.loc, Picture.text
            from Candidate, Picture where username = @username AND Candidate.Picture = Picture.Idpicture;";
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


        public List<Candidates> GetAllCandidates()
        {
            List<Candidates> ReturnList = new List<Candidates>();
            string commandText = @"SELECT c.username, c.faculty, c.institute, c.info, p.loc, p.text, u.firstname, u.lastname FROM Candidate as c LEFT JOIN Picture as p ON c.Picture = p.Idpicture LEFT JOIN Users as u ON c.username = u.email";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                for (int i = 0; i < rows.Count(); i++)
                    ReturnList.Add(new Candidates()
                    {
                        username = rows[i]["username"].ToString(),
                        faculty = rows[i]["faculty"].ToString(),
                        institute = rows[i]["institute"].ToString(),
                        info = rows[i]["info"].ToString(),
                        picture = rows[i]["loc"].ToString(),
                        firstname = rows[i]["firstname"].ToString(),
                        lastname = rows[i]["lastname"].ToString(),

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
            string commandText = @"Update Candidate SET faculty=@faculty, institute=@institute, info=@info WHERE username=@username";
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
                commandText = @"Update Picture SET loc=@dbpath, text=@picturetext WHERE idpicture=@pictureid";
                _database.Query(commandText, parameters);
            }
            else
            { // if no picture is uploaded, only update the picture text
                commandText = @"Update Picture SET text=@picturetext WHERE idpicture=@pictureid";
                _database.Query(commandText, parameters);
            }

        }


        /// <summary>
        /// Removes a candidate in the candidate and picture table, and deletes all votes
        /// </summary>
        public void removeCandidate(string Username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            int PictureId = GetPictureId(Username);
            String commandText = "DELETE FROM Picture WHERE idpicture = @pictureid";
            parameters.Add("@pictureid", PictureId);
            _database.Execute(commandText, parameters);
            commandText = "DELETE FROM Votes WHERE votedon = @username";
            _database.Execute(commandText, parameters);
            commandText = "DELETE FROM Candidate WHERE username = @username";
            _database.Execute(commandText, parameters);

        }


        /// <summary>
        /// Returns true if user is candidate, false if not
        /// </summary>
        public Boolean CheckIfUserIsCandidate(string Username)
        {
            Boolean UserIsCandidate;
            string query = "SELECT UserName FROM Candidate WHERE username = @username";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            String ReturnValue = _database.GetStrValue(query, parameters);
            if (ReturnValue == null)
            {
                UserIsCandidate = false;
            }
            else
            {
                UserIsCandidate = true;
            }
            return UserIsCandidate;
        }


        //PICTURE TABLE


        /// <summary>
        /// Inserts a new entry into the picture table
        /// </summary>
        public void InsertNewImage(int id, string loc, string text)
        {
            string commandText = @"Insert Into Picture (idpicture, loc, text)
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

                string commandText = @"SELECT picture FROM Candidate WHERE picture = @r";
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
        /// Returns pictureid for a user
        /// </summary>
        /// 
        public dynamic GetPictureId(string username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", username } };
            string commandText = "Select picture from Candidate WHERE username = @username";
            var rows = _database.Query(commandText, parameters);
            String PictureIdString = rows[0]["picture"];
            int PictureId = Convert.ToInt32(PictureIdString); 
            return PictureId;
        }

        //
        //VOTES TABLE
        //
        public Boolean CheckIfVotedOn(string Voter)
        {
            Boolean VotedOn;
            string query = "SELECT Voter from `Votes` WHERE Voter=@Voter";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@Voter", Voter } };
            String ReturnValue = _database.GetStrValue(query, parameters);
            if (ReturnValue == null)
            {
                VotedOn = false;
            }
            else
            {
                VotedOn = true;
            }
            return VotedOn;
        }

        public String GetCurrentVote(string Voter)
        {
            String CurrentVotedOn = "";
            string commandText = "SELECT `Votedon` FROM Votes WHERE Voter=@Voter;";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@Voter", Voter } };
            var rows = _database.Query(commandText, parameters);
            try
            {
                CurrentVotedOn = rows[0]["Votedon"].ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return CurrentVotedOn;
        }
        public void VoteForUser(string votedon, string voter)
        {
            try
            {
                Dictionary<string, object> parameters1 = new Dictionary<string, object>();
                var rows = _database.Query("SELECT * FROM Votes WHERE voter = '" + voter + "';", parameters1);
                if (rows.Count() == 0 || rows.Count() == null)
                {


                    string commandText = @"INSERT INTO Votes (Voter, Votedon) VALUES (@voter, @votedon)";
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("@voter", voter);
                    parameters.Add("@votedon", votedon);


                    _database.Execute(commandText, parameters);
                }
                else
                {
                    string commandText = @"UPDATE Votes set votedon = '" + votedon + "' WHERE voter = '" + voter + "';";
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
        /// Gets all votes
        /// </summary>
        public List<Votes> getVotes()
        {
            List<Votes> ReturnList = new List<Votes>();
            string commandText = "Select * from Votes";
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

        public void RemoveVote(string Username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@username", Username } };
            String commandText = "DELETE FROM Votes WHERE voter = @username";
            _database.Execute(commandText, parameters);
        }
        


        /// <summary>
        /// Gets all candidates and their votes
        /// </summary>
        public List<CandidateVotes> getCandidateVotes()
        {
            List<CandidateVotes> ReturnList = new List<CandidateVotes>();
            string commandText = @"SELECT c.UserName, u.Firstname, u.Lastname, COUNT(v.Votedon) as Votes, p.Loc, p.Text  FROM Candidate c
                                LEFT JOIN Votes v ON c.UserName = v.Votedon
                                LEFT JOIN Users u ON c.UserName = u.UserName
                                LEFT JOIN Picture p ON c.Picture = p.Idpicture
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
                        Picture = rows[i]["Loc"].ToString(),
                        PictureText = rows[i]["Text"].ToString(),
                    });
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return ReturnList;
        }

        /// <summary>
        //ADMIN PANEL
        /// </summary>


        public List<AdminModel> AdminGetUsers()
        {
            string query = @"SELECT * FROM Users";
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

            string query = @"SELECT
                            u.UserName,
                            u.Id,
                            u.Email,
                            u.Firstname,
                            u.Lastname,
                            c.Faculty,
                            c.Institute,
                            c.Info,
                            ur.RoleId,
                            r.Name,
                            p.Loc
                        FROM
                            Users AS u
                        LEFT JOIN Candidate AS c
                        ON
                            u.UserName = c.UserName
                        LEFT JOIN UserRoles AS ur
                        ON
                            u.Id = ur.UserId
                        LEFT JOIN Roles AS r
                        ON
                            r.Id = ur.RoleId
                        LEFT JOIN Picture AS p
                        ON
                            c.Picture = p.Idpicture
                        WHERE u.UserName = @UserName;
                        ";                      
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
                        Username = rows[i]["UserName"].ToString(),
                        Id = rows[i]["Id"].ToString(),
                        Email = rows[i]["Email"].ToString(),
                        Firstname = rows[i]["Firstname"].ToString(),
                        Lastname = rows[i]["Lastname"].ToString(),

                        //From Candidate Table
                        Faculty = (rows[i]["Faculty"] == null) ? "null" : rows[i]["Faculty"].ToString(),
                        Institute = (rows[i]["Institute"] == null) ? "null" : rows[i]["Institute"].ToString(),
                        Info = (rows[i]["Info"] == null) ? "null" : rows[i]["Faculty"].ToString(),

                        //From Picture Table
                        Picture = (rows[i]["Loc"] == null) ? "null" : rows[i]["Loc"].ToString(),

                        //From Role Table(s)
                        RoleId = (rows[i]["RoleId"] == null) ? "0" : rows[i]["RoleId"].ToString(),
                        RoleName = (rows[i]["Name"] == null) ? "user" : rows[i]["Name"].ToString(),

                    }; 
                }
            }
            return returnQuery;
        }


        public void AdminEditUser(
            string Id, string Username, string Email, string Firstname, string Lastname,
            string Faculty, string Institute, string Info, string RoleId, string Picture)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string query = @"
                BEGIN;
                UPDATE `Users` 
                SET Id=@Id,Username=@Username,Email=@Email,Firstname=@Firstname,Lastname=@Lastname 
                WHERE Username=@Username;
                
                UPDATE `Candidate` 
                SET Faculty=@Faculty,Institute=@Institute,Info=@Info 
                WHERE Username=@Username;
                
                UPDATE `UserRoles` SET RoleId=@RoleId 
                WHERE UserID=@Id; 
                
                COMMIT;";
                parameters.Add("@Username", Username);
                parameters.Add("@Id", Id);
                parameters.Add("@Email", Email);
                parameters.Add("@Firstname", Firstname);
                parameters.Add("@Lastname", Lastname);

                parameters.Add("@Faculty", Faculty);
                parameters.Add("@Institute", Institute);
                parameters.Add("@Info", Info);

                parameters.Add("@RoleId", RoleId);

                parameters.Add("@Picture", Picture);

                _database.Execute(query, parameters);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public void AdminUpdateElection(string Title, int IdElection, DateTime Startelection, DateTime Endelection)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string query = @"
                UPDATE `Election` 
                SET Title=@Title,Startelection=@Startelection,Endelection=@Endelection
                WHERE IdElection=1";
                parameters.Add("@Title", Title);
                parameters.Add("@IdElection", 1);
                parameters.Add("@Startelection", Startelection);
                parameters.Add("@Endelection", Endelection);

                _database.Execute(query, parameters);
            }

            catch (Exception e)
            {
                throw e;
            }

        }

        public void EndElection(string Title, int IdElection, DateTime Startelection, DateTime Endelection)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                string query = @"
                UPDATE `Election` 
                SET Title=@Title,Startelection=@Startelection,Endelection=@Endelection
                WHERE IdElection=1";
                parameters.Add("@Title", "Ended Election");
                parameters.Add("@IdElection", 1);
                parameters.Add("@Startelection", DateTime.Now);
                parameters.Add("@Endelection", DateTime.Now);

                _database.Execute(query, parameters);
            }

            catch (Exception e)
            {
                throw e;
            }

        }
        public void AdminDeleteUser(string Username)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "@UserName", Username } };
            string query = "DELETE FROM Users WHERE Username = @UserName";
            _database.Execute(query, parameters);
        }

        
        //ELECTION TABLE


        /// <summary>
        /// Gets election information
        /// </summary>
        public List<ElectionInformation> getElectionInfo()
        {
            List<ElectionInformation> ReturnList = new List<ElectionInformation>();
            string commandText = "Select * from Election";
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
        /// Sets the date the election was coontrolled
        /// </summary>
        public void SetControlDate(int id) {
            string commandText = @"Update Election SET controlled=@date WHERE idelection=@id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();        
            parameters.Add("@date", DateTime.Now);
            parameters.Add("@id", id);
              
           _database.Query(commandText, parameters);
            
        }


        public ElectionDateInformation ElectionPanel()
        {
            ElectionDateInformation returnQuery = new ElectionDateInformation();
            string commandText = "Select * from Election";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            var rows = _database.Query(commandText, parameters);
            try
            {
                returnQuery = new ElectionDateInformation()
                {
                    Title = rows[0]["Title"],
                    Startelection = DateTime.Parse(rows[0]["Startelection"]),
                    Endelection = DateTime.Parse(rows[0]["Endelection"]),
                };
            }
            catch (Exception)
            {

            }
            return returnQuery;
        }
           
    }

}

    