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
	F�rnamn nvarchar(32) not null, 
	Efternamn nvarchar(32) not null,
	unique(Personnr, F�rnamn, Efternamn)
)
go

insert into 
    Kunder (Personnr, F�rnamn, Efternamn)
values
    ('19900121-1235', 'Anna', 'Svensson'),
    ('19850512-5678', 'Erik', 'Johansson'),
    ('19751230-9876', 'Karin', 'Andersson'),
    ('20000115-1122', 'Johan', 'Nilsson'),
    ('19950620-3344', 'Lena', 'Karlsson'),
    ('19930325-5566', 'Oscar', 'Pettersson'),
    ('19881118-7788', 'Emma', 'Lindstr�m'),
    ('20040214-9900', 'Sara', 'Berg'),
    ('19970903-0011', 'David', 'Holm'),
    ('19830530-2233', 'Sofia', 'Fransson')
go

create table Adresser
(
	ID int identity primary key,
	Gatuadress nvarchar(32) not null,
	Ort nvarchar(32) not null,
	Postnr nvarchar(6) not null,
	[Lgh nummer] nvarchar(4) null
)
go

insert into 
    Adresser (Gatuadress, Ort, Postnr, [Lgh nummer])
values
    ('Storgatan 12', 'Stockholm', '11122', null),
    ('Parkv�gen 45', 'G�teborg', '41258', 'A12'),
    ('Lundv�gen 3', 'Malm�', '21764', null),
    ('Skogsv�gen 8', 'Uppsala', '75236', 'B7'),
    ('Fiskarv�gen 5', 'Lule�', '97234', null),
    ('Havsv�gen 19', 'Visby', '62145', 'C9'),
    ('V�sterl�nggatan 13', 'Stockholm', '11355', null),
    ('Strandv�gen 22', 'Helsingborg', '25223', 'D4'),
    ('Kyrkogatan 9', '�rebro', '70212', null),
    ('Hamnv�gen 6', 'Kalmar', '39122', 'E1')
go

create table Kund2Adress
(
	ID int identity primary key,
	KunderID int not null references Kunder(ID) on delete cascade,
	AdresserID int not null references Adresser(ID) on delete cascade,
	unique(KunderID, AdresserID)
)
go

insert into
    Kund2Adress(KunderID, AdresserID)
values
    (1001, 1), (1001, 2), (1002, 3), (1003, 4), (1004, 5),
    (1006, 6), (1006, 7), (1008, 8), (1009, 9), (1010, 10)
go



create table Kontaktuppgifter
(
	ID int identity primary key,
	Kontakttyp nvarchar(16) not null,
	Kontaktv�rde nvarchar(64) not null
		
)
go

insert into
    Kontaktuppgifter(Kontakttyp, Kontaktv�rde)
values
   ('Telefon', '0701234567'),
    ('E-post', 'anna.svensson@example.com'),
    ('Telefon', '0707654321'),
    ('E-post', 'erik.johansson@example.com'),
    ('Telefon', '0709876543'),
    ('E-post', 'karin.andersson@example.com'),
    ('Telefon', '0701122334'),
    ('E-post', 'johan.nilsson@example.com'),
    ('Telefon', '0703344556'),
    ('E-post', 'lena.karlsson@example.com')
go


create table Kund2Kontakt
(
	ID int identity primary key,
	KunderID int not null references Kunder(ID) on delete cascade,
	KontaktuppgifterID int not null references Kontaktuppgifter(ID) on delete cascade,
	unique(KunderID, KontaktuppgifterID)
)
go

insert into
    Kund2Kontakt(KunderID, KontaktuppgifterID)
values
    (1001, 1), (1001, 2), (1002, 3), (1002, 4), (1003, 5),
    (1003, 6), (1004, 7), (1004, 8), (1005, 9), (1005, 10)
go

create table [Order]
(
	ID int identity primary key,
	Ordernr int identity(10000, 1) unique not null,
	�rSkickad BIT not null default 0,
	�rLevererad BIT not null default 0,
	�rBetald BIT not null default 0,
	Betalsystem nvarchar(32) null,
	TidVidBest�llning DateTime not null,
	Ber�knadLeverans DateTime not null,
	Kund2KontaktID int not null references Kund2Kontakt(ID) 
)
go

insert into
    [Order]
values
    (10001, 0, 0, 0, 'Swish', GETDATE(), DATEADD(day, 5, GETDATE()), 1),
    (10002, 1, 0, 0, 'Klarna', GETDATE(), DATEADD(day, 3, GETDATE()), 2),
    (10003, 0, 0, 0, null, GETDATE(), DATEADD(day, 7, GETDATE()), 3),
    (10004, 1, 1, 1, 'Kort', GETDATE(), DATEADD(day, 2, GETDATE()), 4),
    (10005, 0, 0, 1, 'Faktura', GETDATE(), DATEADD(day, 10, GETDATE()), 5),
    (10006, 0, 0, 0, null, GETDATE(), DATEADD(day, 8, GETDATE()), 6),
    (10007, 1, 1, 1, 'Swish', GETDATE(), DATEADD(day, 1, GETDATE()), 7),
    (10008, 1, 0, 1, 'Klarna', GETDATE(), DATEADD(day, 4, GETDATE()), 8),
    (10009, 0, 0, 0, 'Kort', GETDATE(), DATEADD(day, 6, GETDATE()), 9),
    (10010, 0, 0, 0, 'Faktura', GETDATE(), DATEADD(day, 12, GETDATE()), 10)
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

insert into
    Produkter
values
    ('Elektronik', 'Mobiltelefon', 'P001', 6999.99),
    ('Elektronik', 'Laptop', 'P002', 14999.99),
    ('Hem', 'Soffa', 'P003', 8999.99),
    ('Hem', 'Matbord', 'P004', 4999.99),
    ('K�k', 'Kaffemaskin', 'P005', 2999.99),
    ('K�k', 'Br�drost', 'P006', 499.99),
    ('Fritid', 'Cykel', 'P007', 7999.99),
    ('Fritid', 'T�lt', 'P008', 1499.99),
    ('Kl�der', 'Jacka', 'P009', 1999.99),
    ('Kl�der', 'Skor', 'P010', 999.99)
go

create table Produkter2Order
(
	ID int identity primary key,
	Antal int not null default 1,
	ProdukterID int not null references Produkter(ID) on delete cascade,
	[OrderID] int not null references [Order](ID) on delete cascade,
	unique(ProdukterID, [OrderID])
)
go

insert into
    Produkter2Order(ProdukterID, OrderID, Antal)
values
    (1, 1, 1), (2, 2, 2), (3, 3, 1), (4, 4, 1), (5, 4, 2),
    (6, 6, 3), (7, 7, 1), (8, 8, 1), (9, 9, 1), (10, 10, 1)
go


create procedure AddKund
	(
		@Personnr varchar(13),
		@F�rnamn varchar(32),
		@Efternamn varchar(32),

		@ID int output
	)
as begin
	insert into
		Kunder
	values
		(@Personnr, @F�rnamn, @Efternamn)

	set
		@ID = SCOPE_IDENTITY();
end
go

create procedure UpdateKund
	(
		@ID int,
		@Personnr varchar(13),
		@F�rnamn varchar(32),
		@Efternamn varchar(32)
	)
as
begin
	update
		Kunder
	set
		Personnr = @Personnr,
		F�rnamn = @F�rnamn,
		Efternamn = @Efternamn
	where
		Kunder.ID = @ID
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
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

create procedure AddAdress
(
    @Gatuadress nvarchar(32),
    @Ort nvarchar(32),
    @Postnr nvarchar(6),
    @LghNummer nvarchar(4),
    @ID int output
)
as
begin
    insert into 
        Adresser (Gatuadress, Ort, Postnr, [Lgh nummer])
    values 
        (@Gatuadress, @Ort, @Postnr, @LghNummer);

    set 
        @ID = SCOPE_IDENTITY();
end
go


create procedure UpdateAdress
(
    @ID int,
    @Gatuadress nvarchar(32),
    @Ort nvarchar(32),
    @Postnr nvarchar(6),
    @LghNummer nvarchar(4)
)
as
begin
    update 
        Adresser
    set 
        Gatuadress = @Gatuadress,
        Ort = @Ort,
        Postnr = @Postnr,
        [Lgh nummer] = @LghNummer
    where 
        ID = @ID;
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
create procedure CascadeDeleteAdress
(
    @ID int
)
as
begin
    delete from 
        Adresser
    where 
        ID = @ID;
end
go

create procedure AddKontaktuppgift
(
    @Kontakttyp nvarchar(16),
    @Kontaktv�rde nvarchar(64),
    @ID int output
)
as
begin
    insert into 
        Kontaktuppgifter (Kontakttyp, Kontaktv�rde)
    values 
        (@Kontakttyp, @Kontaktv�rde);

    set 
        @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateKontaktuppgift
(
    @ID int,
    @Kontakttyp nvarchar(16),
    @Kontaktv�rde nvarchar(64)
)
as
begin
    update 
        Kontaktuppgifter
    set 
        Kontakttyp = @Kontakttyp,
        Kontaktv�rde = @Kontaktv�rde
    where 
        ID = @ID;
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
create procedure CascadeDeleteKontaktuppgift
(
    @ID int
)
as
begin
    delete from 
        Kontaktuppgifter
    where 
        ID = @ID;
end
go

create procedure AddOrder
(
    @Ordernr int,
    @�rSkickad bit,
    @�rLevererad bit,
    @�rBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBest�llning datetime,
    @Ber�knadLeverans datetime,
    @Kund2KontaktID int,
    @ID int output
)
as
begin
    insert into 
        [Order] 
        (
            Ordernr, 
            �rSkickad, 
            �rLevererad, 
            �rBetald, 
            Betalsystem, 
            TidVidBest�llning, 
            Ber�knadLeverans, 
            Kund2KontaktID
        )
    values 
        (
            @Ordernr, 
            @�rSkickad, 
            @�rLevererad, 
            @�rBetald, 
            @Betalsystem, 
            @TidVidBest�llning, 
            @Ber�knadLeverans, 
            @Kund2KontaktID
        );

    set 
        @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateOrder
(
    @ID int,
    @Ordernr int,
    @�rSkickad bit,
    @�rLevererad bit,
    @�rBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBest�llning datetime,
    @Ber�knadLeverans datetime,
    @Kund2KontaktID int
)
as
begin
    update 
        [Order]
    set 
        Ordernr = @Ordernr,
        �rSkickad = @�rSkickad,
        �rLevererad = @�rLevererad,
        �rBetald = @�rBetald,
        Betalsystem = @Betalsystem,
        TidVidBest�llning = @TidVidBest�llning,
        Ber�knadLeverans = @Ber�knadLeverans,
        Kund2KontaktID = @Kund2KontaktID
    where 
        ID = @ID;
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
create procedure CascadeDeleteOrder
(
    @ID int
)
as
begin
    delete from 
        [Order]
    where 
        ID = @ID;
end
go



