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

INSERT INTO `Election` VALUES (1,'2021-06-02 00:00:00','2021-06-10 23:59:59',NULL,'USN election');

INSERT INTO `Roles` VALUES ('0','User'),('1','Inspector'),('2','Admin');

INSERT INTO `Users` VALUES ('03def32a-4601-4067-b0ab-bbd322aa77cb','DominicWright@aol.com',0,'Dominic ','Wright','AADQLGHf+PmWnLdKQ4E/KWfRGRGL9vaoHnl8rGkg22HIUatcWRjdHI6LAsJeQGcuGg==','f53f416c-cc7e-4ad4-a6e7-967f0f6e3fac',NULL,0,0,NULL,1,0,'DominicWright@aol.com'),('055bfcaf-c479-42b1-a727-42c2e5558c57','HelmerEklund@outlook.se',0,'Helmer','Eklund','AD1Mc6WncAzECjtpDvOjLQI2+mqB67PyCcPSmyNxrFkJfM1vEI3oEyjMQbg5z/hZ8w==','64c61ffb-f24d-44b7-b84d-c9bd003abe05',NULL,0,0,NULL,1,0,'HelmerEklund@outlook.se'),('0e0936ca-cb74-4e35-a1b2-af9fc4cb6032','Annika.Stevens@gmail.com',0,'Annika ','Stevens','ALAqNRX07pjL+xkrNVGrCv8PycA9lb1oac2fHvX9OdB2ykK3J42tLv7k76lTge1XWQ==','6f3638ed-bf2e-4052-b4d5-3d8a3c804f4d',NULL,0,0,NULL,1,0,'Annika.Stevens@gmail.com'),('17e38eac-2815-47b2-9bdc-6a0bfd7fc2c5','marius@hotmail.no',0,'Marius','Koppervik','AFX2c6DWlglvlGrw6hP2H8aAud2xnI0ZFy1aWM5rMF8MiS4RR7nNNvP4jOfrLPQE/Q==','86d3261d-479e-4379-a031-bc43521718eb',NULL,0,0,NULL,1,0,'marius@hotmail.no'),('1c850c0b-43a1-4a62-a7df-9dcdfb099336','MichaelSaether@gmail.com',0,'Michael ','Sæther','ABCDxGFz42RSbcE+2j0zmhtsOlJ+z7lbRlFpqIPlMU9133MkTFnwiyEthY6+fEC3Zw==','8f004c14-fa08-48df-99bd-6c6935efaa23',NULL,0,0,NULL,1,0,'MichaelSaether@gmail.com'),('25b86188-95f7-40e3-b84e-d9b0c43d6737','aleksander.nilsson@hotmail.no',0,'Aleksander','Nilsson','ANksiuXPBfxX/0PLhEwOMqQkpmXxj/w7saYyRKB6SU1zlt6HcFF+7PHBxsgxIuZnpA==','d05292dd-fc86-47d6-bcb4-2375e72b4ddb',NULL,0,0,NULL,1,0,'aleksander.nilsson@hotmail.no'),('26527377-448b-4c14-84c0-e8a68fabbd08','NoraGrimsmo@gmail.com',0,'Nora','Grimsmo','AB8p8PBC7GaPjQ2YzleQrMNEI/FdLjLkqV2v3PxcEm9U4jw8yX8HdutgkYe53ypTEA==','35345f96-7c85-4784-a703-ab503a78362e',NULL,0,0,NULL,1,0,'NoraGrimsmo@gmail.com'),('33de2585-0296-4850-8a8c-bd8b31553f05','Daniel@Berntsen',0,'Daniel','Berntsen','AE22DzlNn6zJ+829QkBxYZsQW184cKg3svLu5pyNfePou8nSF/XV+oQsLAKZn+mbFg==','28b0ea4e-bd35-449b-a099-581ccac8d79d',NULL,0,0,'2021-06-03 22:45:06',0,0,'Daniel@Berntsen'),('519d7eb0-0393-4821-900c-0fee91b023d1','NoahMeling@outlook.com',0,'Noah ','Meling','AMZn4BOM4QXMehaZ0jTPSFc5EmiK5NJQ2E80A2Spa+K5yEsLn8n3WK5QLCMF8bk9qA==','4f9c1993-6f8a-40c7-89d7-5167c8b1eb17',NULL,0,0,NULL,1,0,'NoahMeling@outlook.com'),('5a523241-a345-4620-9fa3-793e4496a02c','EmmaNilsen@outlook.no',0,'Emma','Nilsen','ADzDR3c68FLUMXcSA00eLFqk/cZvCO/d4P8L5hEvZizOrxM+DuQzQ9NTBK2bLj76bg==','9cce59d3-b5d2-41d4-908e-69ba634ed9f0',NULL,0,0,NULL,1,0,'EmmaNilsen@outlook.no'),('696de37f-2ec5-4964-b8b3-d621bd7db26b','Warholm@gmail.com',0,'William','Warholm','AMig6cOATp3IIRhj7+TBsB8CRNiBDDDojgpRp5axVnWPpVb+3RuovPx5M40WQi9x9Q==','2b777402-f2d3-4aab-88dc-c2595aba6d86',NULL,0,0,NULL,1,0,'Warholm@gmail.com'),('84362bee-36ca-42c5-9f99-47e5324a8380','inspector@inspector.no',0,'Inspector','Inspector','AN4ebzRpwo2e3Eo0t6QG5pPwvLatUk56w6ntuXJXy3Yl3wLykXRYJgrZnXCb6/fXYA==','b2428ce1-5140-40b4-a72f-f0b1a7bcf47c',NULL,0,0,NULL,1,0,'inspector@inspector.no'),('8a40c311-9973-4c61-8588-2f5e2c9673c3','MagnusNesset@abc.no',0,'Magnus ','Nesset','APFnz6DoawK5BlnfohI5TrDchCw5kxIL3RSnRgmcexjBwFedge8KwmxiYACuuPhy8g==','2904bbf9-eef2-440d-bc15-7015f394882c',NULL,0,0,NULL,1,0,'MagnusNesset@abc.no'),('8dd4a0b0-109d-47df-be22-42f94aa2e2ce','JulianJorgensen@dayrep.com',0,'Julian','Jørgensen','ADcHb63dJ6G/qSFjBHXKDzFG5RQ+J6Y6r+CWPRjWI2LSffCaoWaswgvLiCbNZQc5/g==','d8923ecc-53be-45c9-8803-fb787118ef02',NULL,0,0,NULL,1,0,'JulianJorgensen@dayrep.com'),('8ed90d85-44ca-47b6-b1fe-713cef9850b2','MarielleHagelund@gmail.com',0,'Marielle ','Hagelund','ALm/KsKRnLlyAu6EnRERBC0EGH4yTwm4gZpdhrN+PWniSYbOUtvlRAFdepfB5ql6cQ==','c60dbe7e-2f26-4d65-bfb4-136f30032a8f',NULL,0,0,NULL,1,0,'MarielleHagelund@gmail.com'),('aa5cf5c4-6cbf-449b-8887-a6bf83ca7dce','Ola@nordmann.no',0,'Ola','Normann','AEuoWbzxmne4DGR5C/U2fxQY+gjlMLEyMgMyRHAuuu07bNJyYsg4HFjjFzlGox9GTg==','9e6bdba3-9c5d-4e9f-a4b9-d171ae35d9b7',NULL,0,0,'2021-06-03 19:43:38',0,0,'Ola@nordmann.no'),('ae69fd9f-e223-4ab3-b609-0818b8f4ba62','admin@admin.no',0,'admin','admin','AGV1NjwbDUcxSqeoRSc3AEQIEyFkllhkA9dYoQlcIzuTAGJt20RBm+e0CxQkq8BPsw==','8c6ec17f-ff49-4568-8da3-ed1639a64739',NULL,0,0,NULL,1,0,'admin@admin.no'),('b19c6400-ccd0-4987-a4c3-e47a7b3dc628','RosieKirk@yahoo.com',0,'Rosie ','Kirk','AHKIAsgewvY8R89Tu9OhStqsn6dPKJtQPdfm0GCddBMrahK9X1Cm19VO8dFUNj5k6w==','f875ef8f-cb81-4251-9197-05e82acef756',NULL,0,0,NULL,1,0,'RosieKirk@yahoo.com'),('b6221236-4187-4c47-8f76-15280634cebc','SumayaMagnussen@gmail.com',0,'Sumaya','Magnussen','AO1BciL6zJgNTxeHgUzX2P2vTQUDIpi8ZwaB/UUNG+IPT12JvPEb+nN0JPw12cb7/Q==','28f8401f-fc56-4a0e-80e4-d7644345590a',NULL,0,0,NULL,1,0,'SumayaMagnussen@gmail.com'),('cb847488-d732-45fe-bd55-06a3d8d34763','AdamMarthinsen@online.no',0,'Adam ','Marthinsen','ABaixIkf3aqoAcNbBLzg5nAjI2xh2xkeKISYpRMBLFNL7zSYtBFltvuhn+wzxAG0HQ==','e1dca660-2247-4c27-933b-3e83107e0827',NULL,0,0,NULL,1,0,'AdamMarthinsen@online.no'),('cf726408-04fe-4eaf-8a9f-1ff2e9f996df','LotteBlindheim@minepost.no',0,'Lotte ','Blindheim','AE1TUIR7TnU9YjteVURh2DS5IiJXJRVNASF3mwBVFL0RACjXSGapjwcJtPt/2CIWjQ==','fe058910-0aed-4d4b-94bf-e6a694e3e649',NULL,0,0,NULL,1,0,'LotteBlindheim@minepost.no'),('ef5a3d0e-dc33-47e7-816c-e24f40c4fc4c','AnnaGjermundsen@gmail.com',0,'Anna ','Gjermundsen','AON5Pk3ZiViIZrlXAhQ3lYuTKNScGjbidZZ51jefNyBPNrWQxa0CK8nHnjXLdPlPbQ==','f75f6aa6-ba86-4c99-8997-a86937b0d872',NULL,0,0,NULL,1,0,'AnnaGjermundsen@gmail.com');

INSERT INTO `UserRoles` VALUES ('03def32a-4601-4067-b0ab-bbd322aa77cb','0'),('055bfcaf-c479-42b1-a727-42c2e5558c57','0'),('0e0936ca-cb74-4e35-a1b2-af9fc4cb6032','0'),('17e38eac-2815-47b2-9bdc-6a0bfd7fc2c5','0'),('1c850c0b-43a1-4a62-a7df-9dcdfb099336','0'),('25b86188-95f7-40e3-b84e-d9b0c43d6737','0'),('26527377-448b-4c14-84c0-e8a68fabbd08','0'),('33de2585-0296-4850-8a8c-bd8b31553f05','0'),('519d7eb0-0393-4821-900c-0fee91b023d1','0'),('5a523241-a345-4620-9fa3-793e4496a02c','0'),('696de37f-2ec5-4964-b8b3-d621bd7db26b','0'),('84362bee-36ca-42c5-9f99-47e5324a8380','0'),('8a40c311-9973-4c61-8588-2f5e2c9673c3','0'),('8dd4a0b0-109d-47df-be22-42f94aa2e2ce','0'),('8ed90d85-44ca-47b6-b1fe-713cef9850b2','0'),('b19c6400-ccd0-4987-a4c3-e47a7b3dc628','0'),('b6221236-4187-4c47-8f76-15280634cebc','0'),('cb847488-d732-45fe-bd55-06a3d8d34763','0'),('cf726408-04fe-4eaf-8a9f-1ff2e9f996df','0'),('ef5a3d0e-dc33-47e7-816c-e24f40c4fc4c','0'),('84362bee-36ca-42c5-9f99-47e5324a8380','1'),('ae69fd9f-e223-4ab3-b609-0818b8f4ba62','2');

INSERT INTO `Candidate` VALUES ('Daniel@Berntsen','Helse- og sosialvitenskap','Sykepleie- og helsevitenskap','We all know that there are too many cliques and factions that divide us as students here at West Branch High. This Friday, please consider voting for me, Ben Davis, for student council. I’ll make it my number one job to bring all West Branch Eagles together so we can ‘Fly High as One',8382),('HelmerEklund@outlook.se','Fakultet for humaniore, idretts- og utdanningsvitenskap','Institutt for språk og litteratur','Helmer Eklund is from Sweden but has lived in Norway the past few years. Helmer always likes to take a chat, so if you see him don\'t miss the opportunity! I think that with Helmer as president every student would get a better experience, both in and outside of class.',11135),('JulianJorgensen@dayrep.com','USN Handelshøyskolen','Institutt for økonomi og IT','Hello I\'m Julian. I am the representative for my class in the student democracy, but now I want to take it a step further and i feel i can represent every student in a good way. I want everyone\'s stay at USN to be as good as possible. I am outward-facing and like to meet new poeple, but also stubborn and doesn\'t bend if I fight for something I think is unfair. So vote for me!',97446),('NoraGrimsmo@gmail.com','Fakultet for helse- og sosialvitenskap','Institutt for sykepleie- og helsevitenskap','My name is Nora Grimsmo. I am 20 year old and I\'m studying to be a nurse. I Have been part of the student council in primary school and high school. I want to be a voice for all students and make everyones life just a little better :)',43022),('Ola@nordmann.no','USN Handelshøyskolen','Institutt for økonomi og IT','My name is Ola Nordmann. I am 23 years old and Im studying to be a Engineer. I Have been part of the student council in primary school and high school. I will strive for a greener school day.',80583);

INSERT INTO `Picture` VALUES (8382,'content/Pictures/Daniel@Berntsen.jpg','Portrait of Daniel',NULL),(11135,'content/Pictures/HelmerEklund@outlook.se.jpg','Portrait of Helmer',NULL),(43022,'content/Pictures/NoraGrimsmo@gmail.com.jpg','Portrait picture of me',NULL),(80583,'content/Pictures/Ola@nordmann.no.jpg','picture of Ola',NULL),(97446,'content/Pictures/JulianJorgensen@dayrep.com.jpg','Picture of me',NULL);

INSERT INTO `Votes` VALUES ('Annika.Stevens@gmail.com','Daniel@Berntsen'),('LotteBlindheim@minepost.no','Daniel@Berntsen'),('MagnusNesset@abc.no','Daniel@Berntsen'),('AdamMarthinsen@online.no','HelmerEklund@outlook.se'),('Daniel@Berntsen','HelmerEklund@outlook.se'),('MichaelSaether@gmail.com','HelmerEklund@outlook.se'),('NoahMeling@outlook.com','HelmerEklund@outlook.se'),('Warholm@gmail.com','HelmerEklund@outlook.se'),('DominicWright@aol.com','JulianJorgensen@dayrep.com'),('EmmaNilsen@outlook.no','JulianJorgensen@dayrep.com'),('SumayaMagnussen@gmail.com','JulianJorgensen@dayrep.com'),('AnnaGjermundsen@gmail.com','NoraGrimsmo@gmail.com'),('MarielleHagelund@gmail.com','NoraGrimsmo@gmail.com'),('JulianJorgensen@dayrep.com','Ola@nordmann.no'),('marius@hotmail.no','Ola@nordmann.no'),('Ola@nordmann.no','Ola@nordmann.no'),('RosieKirk@yahoo.com','Ola@nordmann.no');

