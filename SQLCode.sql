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

insert into 
    Kunder (Personnr, Förnamn, Efternamn)
values
    ('19900121-1235', 'Anna', 'Svensson'),
    ('19850512-5678', 'Erik', 'Johansson'),
    ('19751230-9876', 'Karin', 'Andersson'),
    ('20000115-1122', 'Johan', 'Nilsson'),
    ('19950620-3344', 'Lena', 'Karlsson'),
    ('19930325-5566', 'Oscar', 'Pettersson'),
    ('19881118-7788', 'Emma', 'Lindström'),
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
    ('Parkvägen 45', 'Göteborg', '41258', 'A12'),
    ('Lundvägen 3', 'Malmö', '21764', null),
    ('Skogsvägen 8', 'Uppsala', '75236', 'B7'),
    ('Fiskarvägen 5', 'Luleå', '97234', null),
    ('Havsvägen 19', 'Visby', '62145', 'C9'),
    ('Västerlånggatan 13', 'Stockholm', '11355', null),
    ('Strandvägen 22', 'Helsingborg', '25223', 'D4'),
    ('Kyrkogatan 9', 'Örebro', '70212', null),
    ('Hamnvägen 6', 'Kalmar', '39122', 'E1')
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
	Kontaktvärde nvarchar(64) not null
		
)
go

insert into
    Kontaktuppgifter(Kontakttyp, Kontaktvärde)
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
	ÄrSkickad BIT not null default 0,
	ÄrLevererad BIT not null default 0,
	ÄrBetald BIT not null default 0,
	Betalsystem nvarchar(32) null,
	TidVidBeställning DateTime not null,
	BeräknadLeverans DateTime not null,
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
    ('Kök', 'Kaffemaskin', 'P005', 2999.99),
    ('Kök', 'Brödrost', 'P006', 499.99),
    ('Fritid', 'Cykel', 'P007', 7999.99),
    ('Fritid', 'Tält', 'P008', 1499.99),
    ('Kläder', 'Jacka', 'P009', 1999.99),
    ('Kläder', 'Skor', 'P010', 999.99)
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

-- Då jag satt "on delete cascade" i själva tabellskapandet så behövs det inte här
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

-- Då jag satt "on delete cascade" i själva tabellskapandet så behövs det inte här
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
    @Kontaktvärde nvarchar(64),
    @ID int output
)
as
begin
    insert into 
        Kontaktuppgifter (Kontakttyp, Kontaktvärde)
    values 
        (@Kontakttyp, @Kontaktvärde);

    set 
        @ID = SCOPE_IDENTITY();
end
go

create procedure UpdateKontaktuppgift
(
    @ID int,
    @Kontakttyp nvarchar(16),
    @Kontaktvärde nvarchar(64)
)
as
begin
    update 
        Kontaktuppgifter
    set 
        Kontakttyp = @Kontakttyp,
        Kontaktvärde = @Kontaktvärde
    where 
        ID = @ID;
end
go

-- Då jag satt "on delete cascade" i själva tabellskapandet så behövs det inte här
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
    @ÄrSkickad bit,
    @ÄrLevererad bit,
    @ÄrBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBeställning datetime,
    @BeräknadLeverans datetime,
    @Kund2KontaktID int,
    @ID int output
)
as
begin
    insert into 
        [Order] 
        (
            Ordernr, 
            ÄrSkickad, 
            ÄrLevererad, 
            ÄrBetald, 
            Betalsystem, 
            TidVidBeställning, 
            BeräknadLeverans, 
            Kund2KontaktID
        )
    values 
        (
            @Ordernr, 
            @ÄrSkickad, 
            @ÄrLevererad, 
            @ÄrBetald, 
            @Betalsystem, 
            @TidVidBeställning, 
            @BeräknadLeverans, 
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
    @ÄrSkickad bit,
    @ÄrLevererad bit,
    @ÄrBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBeställning datetime,
    @BeräknadLeverans datetime,
    @Kund2KontaktID int
)
as
begin
    update 
        [Order]
    set 
        Ordernr = @Ordernr,
        ÄrSkickad = @ÄrSkickad,
        ÄrLevererad = @ÄrLevererad,
        ÄrBetald = @ÄrBetald,
        Betalsystem = @Betalsystem,
        TidVidBeställning = @TidVidBeställning,
        BeräknadLeverans = @BeräknadLeverans,
        Kund2KontaktID = @Kund2KontaktID
    where 
        ID = @ID;
end
go

-- Då jag satt "on delete cascade" i själva tabellskapandet så behövs det inte här
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



