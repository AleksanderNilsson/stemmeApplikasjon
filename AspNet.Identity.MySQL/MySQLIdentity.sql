CREATE TABLE `Roles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL,
  PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `Firstname` varchar(100) NOT NULL,
  `Lastname` varchar(100) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL UNIQUE,
  PRIMARY KEY (`Id`)
);

CREATE TABLE `UserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id` (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE `UserLogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  KEY `ApplicationUser_Logins` (`UserId`),
  CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
);

CREATE TABLE `UserRoles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IdentityRole_Users` (`RoleId`),
  CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ;

CREATE TABLE Candidate(
UserName VARCHAR(256) NOT NULL,
Faculty VARCHAR(100),
Institute VARCHAR(100),
Info LONGTEXT,
Picture INT UNIQUE,
Primary key (UserName),
foreign key (UserName) references Users(UserName)
);


CREATE TABLE Votes(
Voter VARCHAR(256),
Votedon VARCHAR(256),
primary key (Voter),
foreign key (Voter) references Users(UserName),
foreign key (Votedon) references Candidate(UserName)
);

CREATE TABLE Election(
Idelection INT,
Startelection DATETIME,
Endelection DATETIME,
Controlled DATETIME,
Title VARCHAR(100),
primary key (Idelection)
);

CREATE TABLE Picture(
Idpicture INT,
Loc VARCHAR(100),
Text VARCHAR(256),
Alt VARCHAR(100),
primary key(Idpicture),
foreign key (Idpicture) references Candidate(Picture)
);

INSERT INTO `Roles`(`Id`, `Name`) VALUES ('0','user');
INSERT INTO `Roles`(`Id`, `Name`) VALUES ('1','Inspector');
INSERT INTO `Roles`(`Id`, `Name`) VALUES ('2','Admin');
