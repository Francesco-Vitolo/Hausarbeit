IF DB_ID('DB_Vinothek') IS NULL
  CREATE DATABASE DB_Vinothek;
GO

USE DB_Vinothek;
GO 

IF OBJECT_ID('Produkt') IS NOT NULL
  DROP TABLE Produkt;
GO

IF OBJECT_ID('Produzent') IS NOT NULL
  DROP TABLE Produzent;
GO

IF OBJECT_ID('Event') IS NOT NULL
  DROP TABLE Event;
GO

IF OBJECT_ID('EventPos') IS NOT NULL
  DROP TABLE EventPos;
GO

IF OBJECT_ID('Logins') IS NOT NULL
  DROP TABLE Logins;
GO

IF OBJECT_ID('Benutzer') IS NOT NULL
  DROP TABLE Benutzer;
GO

CREATE TABLE Produzent (
  ID_Produzent int PRIMARY KEY IDENTITY(2000,1), 
  Name nvarchar(50),
  Land nvarchar(50),
  Region nvarchar(50),
  Adresse nvarchar(100),
  Email nvarchar (100),
  Telefon nvarchar(50), -- wegen z.B. +49
  Hektar int,
);

CREATE TABLE Produkt (
  ID_Produkt int PRIMARY KEY IDENTITY(9000,1), 
  Name nvarchar(50),
  Art nvarchar(50),
  Qualit‰tssiegel nvarchar(50),
  Rebsorten nvarchar(50),
  Geschmack nvarchar(50),
  Alkoholgehalt float,
  Jahrgang int,
  Beschreibung nvarchar(500),
  Preis float,
  Aktiv bit,

  Picture varbinary(max),
  PDF_file varbinary(max),

  ID_Produzent int not null,
  CONSTRAINT fk_Produzent FOREIGN KEY (ID_Produzent)
  REFERENCES Produzent(ID_Produzent)
  ON DELETE NO ACTION,
);

CREATE TABLE Event (
	ID_Veranstaltung int PRIMARY KEY IDENTITY(3000,1),
	Name nvarchar(50),
	AnzahlPersonen int,
	Datum Date,
	Zeit nvarchar(20),
);

CREATE TABLE EventPos (
	ID_Veranstaltung int,
	ID_Produkt int,
	Constraint PK_Person PRIMARY KEY (ID_Veranstaltung,ID_produkt)
);

CREATE TABLE Benutzer (
	ID_Benutzer int PRIMARY KEY IDENTITY(1,1),
	username nvarchar(50),
	Passwort nvarchar(64),
);

CREATE TABLE Logins(
	ID_Login int PRIMARY KEY IDENTITY(0,1),
	ID_Benutzer int,
	Datum Datetime	

  CONSTRAINT fk_Benutzer FOREIGN KEY (ID_Benutzer)
  REFERENCES Benutzer(ID_Benutzer)
  ON DELETE NO ACTION,
);

Insert into Benutzer values
('admin',  '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('Francesco' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user2' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user3' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user4' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user6' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user5' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user7' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff'),
('user5' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff');

-- PW:admin


Insert into Logins values
('1',GETDATE()),
('2',GETDATE()),
('6',GETDATE()),
('3',GETDATE()),
('1',GETDATE()),
('3',GETDATE()),
('1',GETDATE()),
('1',GETDATE()),
('1',GETDATE());

Insert into Produzent values 
('Weingut01'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut02'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut03'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut04'        ,'Italien','Veneto',  'Adresse',			'8max@mustermann.de', 12375678910, 15),
('Weingut05'        ,'Italien','Veneto',  'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut06'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut07'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut08'        ,'Italien','Veneto',  'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut09'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut10'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut40'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut41'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut42'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut43'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut44'        ,'Italien','Veneto',  'Adresse',			'4max@mustermann.de', 12375678910, 15),
('Weingut45'        ,'Italien','Veneto',  'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut46'        ,'Italien','Veneto',  'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut47'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut48'        ,'Italien','Veneto',  'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut11'        ,'Italien','Veneto',  'Adresse01',			'max@mustermann.de',  2375678910, 15),
('Weingut12'        ,'Italien','Veneto',  'Adresse02',			'max@mustermann.de',  2375678910, 15),
('Weingut13'        ,'Italien','Veneto',  'Adresse03',			'max@mustermann.de',  2375678910, 15),
('Weingut14'        ,'Italien','Veneto',  'Adresse04',			'max@mustermann.de',  12375678910, 15),
('Weingut15'        ,'Italien','Veneto',  'Adresse05',			'max@mustermann.de',  412375678910, 15),
('Weingut16'        ,'Italien','Veneto',  'Adresse06',			'max@mustermann.de',  2375678910, 15),
('Weingut17'        ,'Italien','Veneto',  'Adresse07',			'max@mustermann.de',  2375678910, 15),
('Weingut18'        ,'Italien','Veneto',  'Adresse08',			'max@mustermann.de',  412375678910, 15),
('Weingut19'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut20'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut21'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut22'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut23'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut24'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  212375678910, 15),
('Weingut25'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  412375678910, 15),
('Weingut26'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  12375678910, 15),
('Weingut27'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut28'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut29'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  12375678910, 15),
('Weingut30'        ,'Italien','Veneto',  'Adresse',			'max@mustermann.de',  2375678910, 15),																																																							
('Weingut49'        ,'Italien','Veneto',  'Adresse',			'3max@mustermann.de', 12375678910, 15);


Insert into Produkt
(
Name,
Art,
Qualit‰tssiegel,
Rebsorten,
Geschmack,
Alkoholgehalt,
Jahrgang,
Beschreibung,
Preis,
Aktiv,
ID_Produzent)
values 

('011',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('012',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('013',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('014',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('015',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('016',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('017',				'Weiﬂwein','Vino Bianco','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('018',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('019',				'Weiﬂwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('020',				'RosÈwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2008  ),
('021',				'Cremant','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2002  ),
('022',				'Metodo classico','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2007 ),
('023',				'Spuamnte','DOC','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2004  ),
('024',				'Weiﬂwein','IGT','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2001),
('025',				'RosÈwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2008  ),
('026',				'Cremant','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2002  ),
('027',				'Metodo classico','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2007 ),
('028',				'Spuamnte','DOC','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2004  ),
('029',				'Weiﬂwein','IGT','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2001),
('030',				'RosÈwein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2008  ),
('031',				'Cremant','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2002  ),
('032',				'Metodo classico','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2007 ),
('033',				'Spuamnte','DOC','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2004  ),
('034',				'Weiﬂwein','IGT','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2001);

INSERT INTO Event
Values
('Weinkarte-April',20,'2022-02-02','00:00'),
('Weinprobe',8,'2022-04-13','18:00'),
('Weinprobe',150,'2022-06-05','19:00'),
('Weinprobe',8,'2022-09-06','19:00'),
('Weinprobe',16,'2022-10-24','20:15');

INSERT INTO EventPos Values 
(3000,9000),
(3000,9001),
(3000,9002),
(3000,9003),
(3000,9004),
(3000,9005),
(3000,9006),
(3000,9007),
(3000,9008),
(3001,9001),
(3002,9002),
(3001,9003),
(3002,9004),
(3001,9005),
(3002,9006),
(3003,9007);