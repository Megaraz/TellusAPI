use Master
go

drop database Tellus
go
create database Tellus
go

use Tellus
go

create table Kunder
(
	ID int identity(1001, 1) primary key,
	Personnr nvarchar(13) null,
	Förnamn nvarchar(32) not null, 
	Efternamn nvarchar(32) not null,
	Födelseår int not null,

)
go

create table Kontaktuppgifter
(
	ID int identity primary key,
	Kontakttyp nvarchar(16) not null,
	Kontaktuppgift nvarchar(64) not null,
		
)
go

create table Kund2Kontakt
(
	ID int identity primary key,
	KunderID int references Kunder(ID) not null,
	KontaktuppgifterID int references Kontaktuppgifter(ID) not null,
	unique(KunderID, KontaktuppgifterID)
)
go
