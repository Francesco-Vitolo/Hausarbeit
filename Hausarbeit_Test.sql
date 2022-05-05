USE master;
GO

IF DB_ID('test') IS NULL
  CREATE DATABASE test;
GO

USE test;
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

IF OBJECT_ID('Pictures') IS NOT NULL
  DROP TABLE Pictures;
GO

IF OBJECT_ID('Benutzer') IS NOT NULL
  DROP TABLE Benutzer;
GO


CREATE TABLE Produzent (
  ID_Produzent int PRIMARY KEY IDENTITY(2000,1), 
  Name nvarchar(50),
  Land nvarchar(50),
  Region nvarchar(50),
  Beschreibung nvarchar(250),
  Adresse nvarchar(100),
  Email nvarchar (100),
  Telefon nvarchar(50), -- wegen z.B. +49
  Hektar int,
);

--CREATE TABLE Pictures (
--	ID_Picture int PRIMARY KEY identity (9000,1),
--	Picture varbinary(max)
--);

CREATE TABLE Produkt (
  ID_Produkt int PRIMARY KEY IDENTITY(1000,1), 
  Name nvarchar(50),
  Art nvarchar(50),
  Qualitätssiegel nvarchar(50),
  Rebsorten nvarchar(50),
  Geschmack nvarchar(50),
  Alkoholgehalt float,
  Jahrgang int,
  Beschreibung nvarchar(250),
  Preis float,
  Aktiv bit,

  Picture varbinary(max),
  PDF_file varbinary(max),

  ID_Produzent int not null,
  CONSTRAINT fk_Produzent FOREIGN KEY (ID_Produzent)
  REFERENCES Produzent(ID_Produzent)
  ON DELETE NO ACTION,

  --ID_Picture int,
  --CONSTRAINT fk_bild FOREIGN KEY (ID_Picture)
  --REFERENCES Pictures(ID_Picture),
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
	Salt  nvarchar(50),
);

Insert into Benutzer values
('admin',	'22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff', 'salt in Programm festgelegt'),
('admin2' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff', 'salt in Programm festgelegt'),
('admin3' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff', 'salt in Programm festgelegt'),
('admin4' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff', 'salt in Programm festgelegt'),
('admin5' , '22b9b596cae2c3d3f45b96fc2f126263f9c49dfc395bc00daa76514b1743beff', 'salt in Programm festgelegt');

-- PW:admin


Insert into Produzent values 
('Alberto Ravazzi'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  12375678910, 15),
('Tenuta Asinara'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  612375678910, 15),
('Beltrame'					,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  712375678910, 15),
('Bernadeschi'				,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  412375678910, 15),
('Bibi Graetz'				,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  12375678910, 15),
('Bulgarini'				,'Italien','Gardasee','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'max@mustermann.de',  12375678910, 15),
('Ca dei Frati'				,'Italien','Gardasee','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'max@mustermann.de',  12375678910, 15),
('Cantina Albea'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  412375678910, 15),
('Cantina Tomaso Gianolio'        ,'Italien','Toskana','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam uptua.', 'Adresse',				'max@mustermann.de',  12375678910, 15),
('Bricco Lagotto'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'max@mustermann.de',  12375678910, 15),
('Weingut11'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut12'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut13'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut14'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  12375678910, 15),
('Weingut15'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  412375678910, 15),
('Weingut16'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut17'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut18'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  412375678910, 15),
('Weingut19'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut20'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut21'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut22'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut23'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut24'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  212375678910, 15),
('Weingut25'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  412375678910, 15),
('Weingut26'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  12375678910, 15),
('Weingut27'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut28'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
('Weingut29'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  12375678910, 15),
('Weingut30'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'max@mustermann.de',  2375678910, 15),
																																																							
('Alberto Ravazzi'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'4max@mustermann.de', 12375678910, 15),
('Tenuta Asinara'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'5max@mustermann.de', 12375678910, 15),
('Beltrame'					,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'5max@mustermann.de', 12375678910, 15),
('Bernadeschi'				,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'4max@mustermann.de', 12375678910, 15),
('Bibi Graetz'				,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Bulgarini'				,'Italien','Gardasee','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'3max@mustermann.de', 12375678910, 15),
('Ca dei Frati'				,'Italien','Gardasee','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'8max@mustermann.de', 12375678910, 15),
('Cantina Albea'			,'Italien','','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Cantina Tomaso Gianolio'        ,'Italien','Toskana','Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, s',	'Adresse',			'9max@mustermann.de', 12375678910, 15),
('Bricco Lagotto'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',	'5max@mustermann.de', 12375678910, 15),
('Weingut11'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut12'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut13'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut14'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'8max@mustermann.de', 12375678910, 15),
('Weingut15'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut16'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut17'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut18'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut19'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut20'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut21'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut22'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut23'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut24'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'4max@mustermann.de', 12375678910, 15),
('Weingut25'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut26'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'2max@mustermann.de', 12375678910, 15),
('Weingut27'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut28'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15),
('Weingut29'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'3max@mustermann.de', 12375678910, 15),
('Weingut30'        ,'Italien','Toskana', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.', 'Adresse',			'1max@mustermann.de', 12375678910, 15);

Insert into Produkt
(
Name,
Art,
Qualitätssiegel,
Rebsorten,
Geschmack,
Alkoholgehalt,
Jahrgang,
Beschreibung,
Preis,
Aktiv,
ID_Produzent,
Picture)
values 
('010',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	14.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\Lugana_Bulgarini_010.jpg', SINGLE_BLOB) as T1)),
('Indolente',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			10.90,1, 2001,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\indolente.png', SINGLE_BLOB) as T1)),
('i Frati',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	22.90,1, 2006,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\Lugana_Ca_Dei_Frati.jpg', SINGLE_BLOB) as T1)),
('Wein_1',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\Lugana_Bulgarini.jpg', SINGLE_BLOB) as T1)),
('Wein_2',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	16.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\Cavalchina_Bardolino.jpg', SINGLE_BLOB) as T1)),
('Wein_3',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	17.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\cereja.png', SINGLE_BLOB) as T1)),
('Wein_4',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	18.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\cerviolo.jpg', SINGLE_BLOB) as T1)),
('Wein_5',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	19.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\Diffidente.png', SINGLE_BLOB) as T1)),
('Wein_6',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	20.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\dueterre.jpg', SINGLE_BLOB) as T1)),
('Wein_7',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	21.90,1, 2005,(SELECT * FROM OPENROWSET(BULK 'C:\Users\Francesco\Desktop\Bilder\gavi.jpg', SINGLE_BLOB) as T1));
Insert into Produkt
(
Name,
Art,
Qualitätssiegel,
Rebsorten,
Geschmack,
Alkoholgehalt,
Jahrgang,
Beschreibung,
Preis,
Aktiv,
ID_Produzent)
values 
('Prezioso',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			100.90,1, 2000),
('Tazzelenghe',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			11.90,1, 2002),
('Governo',			'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			12.90,1, 2003),
('Casamatta',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			13.90,1, 2004),

('A te',			'Weisswein','DOCG','Roero Arneis','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptu',23.90,1, 2000),
																																																							 		
('Prezioso',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2000  ),
('Tazzelenghe',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2002  ),
('Governo',			'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2003  ),
('Casamatta',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2004  ),
('010',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('011',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('012',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('013',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('014',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('015',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('016',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('017',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('i Frati',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2006  ),
('A te',			'Weisswein','DOCG','Roero Arneis','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam volupt', 15.90,1, 2007  ),
																																																										 
('Prezioso',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2000  ),
('Tazzelenghe',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2002  ),
('Governo',			'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2003  ),
('Casamatta',		'Rotwein','DOCG','','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',			15.90,1, 2004  ),
('010',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('011',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('012',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('013',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('014',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('015',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('016',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('017',				'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2005  ),
('i Frati',			'Weisswein','DOCG','Lugana','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',	15.90,1, 2006  ),
('A te',			'Weisswein','DOCG','Roero Arneis','trocken',14,2020,'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam volup.', 15.90,1, 2007  );

INSERT INTO Event 
Values('Weinprobe_Gunther',8,'29-04-2022','19:00');
--Freitag, 29 April 2022
INSERT INTO EventPos Values 
(3000,1000),
(3000,1002),
(3000,1005),
(3000,1006);
SELECT * FROM EventPos;

SELECT P1.Name, P2.Name, Rebsorten FROM Produkt P1
JOIN Produzent P2 
on P1.ID_Produzent = P2.ID_Produzent;



--Declare @id int;

--select @id = ID_Produzent from Produkt
--where ID_Produkt = 1000;

--Select * from Produzent 
--where ID_Produzent = @id;

--select * from Produzent