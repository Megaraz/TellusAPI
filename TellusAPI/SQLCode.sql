use Master
go

drop database Tellus
go
create database Tellus
go

create table Kunder
(
	ID int identity(1001, 1) primary key,
	Personnr nvarchar(13) null,
	Förnamn nvarchar(32) not null, 
	Efternamn nvarchar(32) not null,
	Födelseår int(4) not null

)

create table K2K
(
	ID int identity primary key,
	KunderID int references Kunder(ID) not null,
	KontaktuppgifterID int references Kontaktuppgifter(ID) not null,
	unique(KunderID, KontaktuppgifterID)
)

create table Kontaktuppgifter
(
	ID int identity primary key,
	Adress nvarchar(
	
)

