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
	Personnr nvarchar(13) not null,
	Förnamn nvarchar(32) not null, 
	Efternamn nvarchar(32) not null,
	unique(Personnr, Förnamn, Efternamn)
)
go

create table Adresser
(
	ID int identity primary key,
	Gatuadress nvarchar(32) not null,
	Ort nvarchar(32) not null,
	Postnr nvarchar(6) not null,
	[Lgh nummer] nvarchar(4) null
)

create table Kund2Adress
(
	ID int identity primary key,
	KunderID int not null references Kunder(ID) on delete cascade,
	AdresserID int not null references Adresser(ID) on delete cascade,
	unique(KunderID, AdresserID)
)



create table Kontaktuppgifter
(
	ID int identity primary key,
	Kontakttyp nvarchar(16) not null,
	Kontaktuppgift nvarchar(64) not null
		
)
go

create table Kund2Kontakt
(
	ID int identity primary key,
	KunderID int not null references Kunder(ID) on delete cascade,
	KontaktuppgifterID int not null references Kontaktuppgifter(ID) on delete cascade,
	unique(KunderID, KontaktuppgifterID)
)
go

create table [Order]
(
	ID int identity primary key,
	Ordernr int unique not null,
	ÄrSkickad BIT not null default 0,
	ÄrLevererad BIT not null default 0,
	ÄrBetald BIT not null default 0,
	Betalsystem nvarchar(32) null,
	TidVidBeställning DateTime not null,
	BeräknadLeverans DateTime not null,
	Kund2KontaktID int not null references Kund2Kontakt(ID) 
)
go

create table Produkter
(
	ID int identity primary key,
	ProduktTyp nvarchar(16) null,
	Produktnamn nvarchar(64) not null,
	ProduktNummer nvarchar(128) unique not null,
	Pris money not null
)
go

create table Produkter2Order
(
	ID int identity primary key,
	Antal int not null default 1,
	ProdukterID int not null references Produkter(ID),
	[OrderID] int not null references [Order](ID),
	unique(ProdukterID, [OrderID])
)
go


create procedure AddKund
	(
		@Personnr varchar(13),
		@Förnamn varchar(32),
		@Efternamn varchar(32),

		@ID int output
	)
as begin
	insert into
		Kunder
	values
		(@Personnr, @Förnamn, @Efternamn)

	set
		@ID = SCOPE_IDENTITY();
end
go

create procedure UpdateKund
	(
		@ID int,
		@Personnr varchar(13),
		@Förnamn varchar(32),
		@Efternamn varchar(32)
	)
as
begin
	update
		Kunder
	set
		Personnr = @Personnr,
		Förnamn = @Förnamn,
		Efternamn = @Efternamn
	where
		Kunder.ID = @ID
end
go

create procedure CascadeDeleteKund(@ID int)
as
	begin
		delete
			from
				Kunder
			where
				ID = @ID
	end
go




