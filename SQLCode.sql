use Master
go
-- ta bort Tellus om man redan har en
drop database Tellus
go
-- Skapar databasen som jag kallar Tellus
create database Tellus
go
-- Anv�nd Tellus f�r allt nedan
use Tellus
go
-- Skapar table f�r Kunder
create table Kunder
(
	ID int identity(1001, 1) primary key,
	Personnr nvarchar(13) not null,
	F�rnamn nvarchar(32) not null, 
	Efternamn nvarchar(32) not null,
	unique(Personnr, F�rnamn, Efternamn) -- Alla dessa attribut ihop m�ste vara unika.
)
go
-- L�gger till poster med data i Kunder
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
-- Skapar tabellen Adresser
create table Adresser
(
	ID int identity primary key,
	Gatuadress nvarchar(32) not null,
	Ort nvarchar(32) not null,
	Postnr nvarchar(6) not null,
	[Lgh nummer] nvarchar(4) null
)
go
-- L�gger till poster med data i Adresser
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
-- Skapar junctiontable Kund2Adress f�r m�nga-till-m�nga mellan Kunder och Adresser
create table Kund2Adress
(
	ID int identity primary key,
	KundID int not null references Kunder(ID) on delete cascade, -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	AdressID int not null references Adresser(ID) on delete cascade, -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	unique(KundID, AdressID) -- dessa tv� ihop m�ste vara unika
)
go
-- Skapar (poster)relationer mellan Kunder och Adresser i Kund2Adresser
insert into
    Kund2Adress(KundID, AdressID)
values
    (1001, 1), (1001, 2), (1002, 3), (1003, 4), (1004, 4),
    (1006, 6), (1006, 7), (1008, 8), (1009, 8), (1010, 10)
go


-- Skapar Kontaktuppgifter table
create table Kontaktuppgifter
(
	ID int identity primary key,
	Kontakttyp nvarchar(16) not null,
	Kontaktv�rde nvarchar(64) not null
		
)
go
-- Skapar poster med data i Kontaktuppgifter
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

-- Skapar junctiontable Kund2Kontakt f�r m�nga-till-m�nga relation mellan Kunder och Kontaktuppgifter
create table Kund2Kontakt
(
	ID int identity primary key,
	KundID int not null references Kunder(ID) on delete cascade, -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	KontaktuppgiftID int not null references Kontaktuppgifter(ID) on delete cascade, -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	unique(KundID, KontaktuppgiftID) -- dessa tv� ihop m�ste vara unika
)
go
-- Skapar (poster)relationer mellan Kunder och Kontaktuppgifter
insert into
    Kund2Kontakt(KundID, KontaktuppgiftID)
values
    (1001, 1), (1001, 2), (1002, 2), (1002, 3), (1003, 5),
    (1003, 6), (1004, 7), (1004, 8), (1005, 8), (1005, 10)
go
-- Skapar en sekvens som genererar ett nummer som startar p� 1000 och �kar med 1 per g�ng.
create sequence OrdernrSequence
start with 10000
increment by 1
go
-- Skapar table f�r Order, med KundID referens f�r en-till-m�nga relation(En kund kan ha flera ordrar)
create table [Order]
(
	ID int identity primary key,
	Ordernr int unique not null,
	�rSkickad BIT not null default 0, -- s�tter defaultv�rden p� BIT till 0 explicit �ven om 0 ofta �r default.
	�rLevererad BIT not null default 0,
	�rBetald BIT not null default 0,
	Betalsystem nvarchar(32) null,
	TidVidBest�llning DateTime not null,
	Ber�knadLeverans DateTime not null,
	KundID int not null references Kunder(ID) on delete cascade -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
)
go
-- S�tter in poster med data i Order, anv�nder min tidigare skapade Sekvens(OrdernrSequence) f�r att generera Ordernr
insert into
    [Order]
values
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, 'Swish', GETDATE(), DATEADD(day, 5, GETDATE()), 1001), -- Kan s�tta antal dagar f�r ber�knad leverans
    (NEXT VALUE FOR OrdernrSequence, 1, 0, 0, 'Klarna', GETDATE(), DATEADD(day, 3, GETDATE()), 1001),
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, null, GETDATE(), DATEADD(day, 7, GETDATE()), 1001),
    (NEXT VALUE FOR OrdernrSequence, 1, 1, 1, 'Kort', GETDATE(), DATEADD(day, 2, GETDATE()), 1004),
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 1, 'Faktura', GETDATE(), DATEADD(day, 10, GETDATE()), 1005),
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, null, GETDATE(), DATEADD(day, 8, GETDATE()), 1006),
    (NEXT VALUE FOR OrdernrSequence, 1, 1, 1, 'Swish', GETDATE(), DATEADD(day, 1, GETDATE()), 1007),
    (NEXT VALUE FOR OrdernrSequence, 1, 0, 1, 'Klarna', GETDATE(), DATEADD(day, 4, GETDATE()), 1008),
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, 'Kort', GETDATE(), DATEADD(day, 6, GETDATE()), 1009),
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, 'Faktura', GETDATE(), DATEADD(day, 12, GETDATE()), 1010)
go


-- Skapar table Produkter
create table Produkter
(
	ID int identity primary key,
	ProduktTyp nvarchar(16) null,
	Produktnamn nvarchar(64) not null,
	ProduktNummer nvarchar(128) unique not null, -- Produktnummer m�ste vara unikt
	Pris money not null
)
go
-- L�gger till poster med data i Produkter
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
-- Skapar junctiontable Produkt2Order f�r m�nga-till-m�nga relation mellan Produkter och Order.
-- Kom p� nu i efterhand att det hade r�ckt med en-till-m�nga(En order kan ha m�nga produkter)
create table Produkt2Order
(
	ID int identity primary key,
	Antal int not null default 1,
	ProduktID int not null references Produkter(ID) on delete cascade, -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	[OrderID] int not null references [Order](ID) on delete cascade -- on delete cascade tar "automatiskt" bort relaterade poster i alla tabeller.
	
)
go
-- Skapar poster(relation) mellan Produkt och Order
insert into
    Produkt2Order(ProduktID, OrderID, Antal)
values
    (1, 1, 1), (1, 2, 2), (3, 3, 1), (4, 4, 1), (5, 4, 2),
    (6, 6, 3), (7, 7, 1), (8, 8, 1), (9, 9, 1), (10, 10, 1)
go

-- Skapar procedur f�r att l�gga till en Kund i tabellen Kunder
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
		@ID = SCOPE_IDENTITY(); -- S�tter output parametern @ID till SCOPE_IDENTITY
end
go
-- Skapar procedur f�r Uppdatera en Kundpost i Kunder
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
		Kunder.ID = @ID -- Condition f�r vilken post som skall uppdateras
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
				ID = @ID -- Condition f�r vilken post som skall deletas i Kunder och cascadea alla relaterade
	end
go
-- Skapar procedur f�r att l�gga till en Adress post i Adresser
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
        @ID = SCOPE_IDENTITY(); -- S�tter output parametern @ID till SCOPE_IDENTITY
end
go

-- Skapar procedur f�r att uppdatera en Adresspost i Adresser
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
        ID = @ID; -- Condition f�r vilken post som skall uppdateras
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
        ID = @ID; -- Condition f�r vilken post som skall deletas
end
go

-- Skapar procedur f�r att l�gga till en Kontaktuppgift i Kontaktuppgifter
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
        @ID = SCOPE_IDENTITY(); -- S�tter outparametern @ID till SCOPE_IDENTITY()
end
go

-- Skapar procedur f�r att uppdatera en post i Kontaktuppgifter
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
        ID = @ID; -- Condition f�r vilken post som skall uppdateras
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
        ID = @ID; -- Condition f�r vilken post som skall deletas
end
go


-- Skapar procedur f�r att l�gga till en Order i Order
create procedure AddOrder
(
    @�rSkickad bit,
    @�rLevererad bit,
    @�rBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBest�llning datetime,
    @Ber�knadLeverans datetime,
    @KundID int,

    @ID int output,
    @Ordernr int output
)
as
begin
    declare @GeneratedOrdernr int = next value for OrdernrSequence; -- deklarerar en variabel med tilldelat v�rde via min sekvens OrdernrSequence
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
            KundID
        )
    values 
        (
            @GeneratedOrdernr, -- S�tter Ordernr till @GeneratedOrdernr som d� fick ett v�rde i b�rjan via sekvensen.
            @�rSkickad, 
            @�rLevererad, 
            @�rBetald, 
            @Betalsystem, 
            @TidVidBest�llning, 
            @Ber�knadLeverans, 
            @KundID
        );

    set 
        @ID = SCOPE_IDENTITY(); -- S�tter out-Parametern @ID till SCOPE_IDENTITY()
    set
        @Ordernr = @GeneratedOrdernr; -- S�tter out-parametern @Ordernr till @GeneratedOrdernr
end
go

-- Skapar procedur f�r att uppdatera en Order post
create procedure UpdateOrder
(
    @ID int,
    @�rSkickad bit,
    @�rLevererad bit,
    @�rBetald bit,
    @Betalsystem nvarchar(32),
    @TidVidBest�llning datetime,
    @Ber�knadLeverans datetime,
    @KundID int,

    @Ordernr int output
)
as
begin
    update 
        [Order]
    set 
        �rSkickad = @�rSkickad,
        �rLevererad = @�rLevererad,
        �rBetald = @�rBetald,
        Betalsystem = @Betalsystem,
        TidVidBest�llning = @TidVidBest�llning,
        Ber�knadLeverans = @Ber�knadLeverans,
        KundID = @KundID
    where 
        ID = @ID;
    set
        @Ordernr = (select Ordernr from [Order] where ID = @ID); -- SubQuery f�r att f� Ordernr f�r posten man updaterar.
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
-- Skapar procedur f�r att deletea en specifik post i Order, och cascade-deletar d� "automatiskt"
create procedure CascadeDeleteOrder
(
    @ID int
)
as
begin
    delete from 
        [Order]
    where 
        ID = @ID; -- Condition f�r vilken post som skall deletas
end
go

-- Skapar procedur f�r att l�gga till en Produkt i Produkter
create procedure AddProdukt
(
    @ProduktTyp nvarchar(16),
    @Produktnamn nvarchar(64),
    @ProduktNummer nvarchar(128),
    @Pris money,

    @ID int output
)
as
begin
    insert into
        Produkter
        (
            ProduktTyp,
            Produktnamn,
            ProduktNummer,
            Pris
        )
    values
    (
        @ProduktTyp,
        @Produktnamn,
        @ProduktNummer,
        @Pris
    );

    set 
        @ID = SCOPE_IDENTITY(); -- S�tter outparametern @ID till SCOPE_IDENTITY()
end
go

-- Skapar procedur f�r att uppdatera en specifik Produkt i Produkter
create procedure UpdateProdukt
(
    @ID int,
    @ProduktTyp nvarchar(16),
    @Produktnamn nvarchar(64),
    @Pris money
)
as
begin
    update
        Produkter
    set
        ProduktTyp = @ProduktTyp,
        Produktnamn = @Produktnamn,
        Pris = @Pris
    where
        ID = @ID; -- Condition f�r vilken post som skall uppdateras
end
go

-- D� jag satt "on delete cascade" i sj�lva tabellskapandet s� beh�vs det inte h�r
-- Skapar procedur f�r att deletea en Produkt fr�n Produkter
create procedure CascadeDeleteProdukt
(
    @ID int
)
as
begin
    delete from
        Produkter
    where
        ID = @ID; -- Condition f�r vilken post som skall deletas
end
go

-- Skapar procedur f�r att l�gga till en post(relation) mellan en Kund och en Adress
create procedure AddKund2Adress
(
    @KundID int,
    @AdressID int,
    
    @ID int output
)
as
begin
    insert into 
        Kund2Adress (KundID, AdressID)
    values
        (@KundID, @AdressID)
    set 
        @ID = SCOPE_IDENTITY(); -- S�tter out-parametern @ID till SCOPE_IDENTITY()
        
end
go

-- Skapar procedur f�r att f� samtliga Adresser som h�r till en Kund(ID)
create procedure GetAdresserByKundID
(
    @KundID int
)
as
    begin
        select
            k2a.ID,
            k2a.KundID,
            k.Personnr, k.F�rnamn, k.Efternamn,
            k2a.AdressID,
            a.Gatuadress, a.Ort, a.Postnr, a.[Lgh nummer]
        from
            Adresser as a
        join 
            Kund2Adress as k2a on 
            A.ID = k2a.AdressID
        join
            Kunder as k on
            k2a.KundID = k.ID
        where
            k.ID = @KundID -- Condition f�r vilken kund som allt ska visas
    end
go

-- Skapar procedur f�r att f� samtliga Kunder som h�r till en Adress(ID)
create procedure GetKunderByAdressID
(
    @AdressID int
)
as
begin
    select
            k2a.ID,
            k2a.KundID,
            k.Personnr, k.F�rnamn, k.Efternamn,
            k2a.AdressID, 
            a.Gatuadress, a.Ort, a.Postnr, a.[Lgh nummer]
        from
            Adresser as a
        join 
            Kund2Adress as k2a on 
            A.ID = k2a.AdressID
        join
            Kunder as k on
            k2a.KundID = k.ID
        where
            a.ID = @AdressID; -- Condition f�r vilken adress som allt ska visas
end
go

-- Skapar procedur f�r att uppdatera en post(relation) i Kund2Adress
create procedure UpdateKund2Adress
(
    @ID int,
    @KundID int,
    @AdressID int
)
as
    begin
        update
            Kund2Adress
        set
            KundID = @KundID,
            AdressID = @AdressID
        where
            ID = @ID -- Condition f�r vilken post som skall uppdateras
    end
go

-- Skapar procedur f�r att deletea en post fr�n Kund2Adress
create procedure CascadeDeleteKund2Adress -- D� jag satt "on delete cascade" vid table creation s� beh�vs det inte h�r.
(
    @ID int
)
as
    begin
        delete from
            Kund2Adress
        where
            ID = @ID -- Condition f�r vilken post som skall deletas
    end
go

-- Skapar procedur f�r att l�gga till ny (post)relation mellan Produkt och Order, samt antal
create procedure AddProdukt2Order
(
    @ProduktID int,
    @OrderID int,
    @Antal int,

    @ID int output
)
as
begin
    insert into 
        Produkt2Order (ProduktID, OrderID, Antal)
    values
        (@ProduktID, @OrderID, @Antal);

    set 
        @ID = SCOPE_IDENTITY(); -- S�tter out-parametern @ID till SCOPE_IDENTITY()
end
go

-- Skapar procedur f�r att h�mta alla Produkter som h�r till en Order
create procedure GetProdukterByOrderID
(
    @OrderID int
)
as
begin
    select
        p2o.ID,
        p2o.ProduktID,
        p2o.OrderID,
        p2o.Antal,
        p.Produktnamn,
        p.Pris,
        o.Ordernr
    from
        Produkt2Order as p2o
    join 
        Produkter as p on 
        p2o.ProduktID = p.ID�
    join
        [Order] as o on
        p2o.OrderID = o.ID
    where
        p2o.OrderID = @OrderID -- Condition f�r vilken Order som allt ska visas
end
go

-- Skapar procedur f�r att uppdatera en post(relation) i Produkt2Order
create procedure UpdateProdukt2Order
(
    @ID int,
    @ProduktID int,
    @OrderID int,
    @Antal int
)
as
begin
    update 
        Produkt2Order
    set 
        ProduktID = @ProduktID,
        OrderID = @OrderID,
        Antal = @Antal
    where 
        ID = @ID -- Condition f�r vilken post som skall uppdateras
end
go

-- Skapar procedur f�r att deletea en post(relation) fr�n Produkt2Order
create procedure DeleteProdukt2Order
(
    @ID int
)
as
begin
    delete from 
        Produkt2Order
    where 
        ID = @ID -- Condition f�r vilken post som skall deletas
end
go

-- Skapar procedur f�r att l�gga till en (post)relation mellan Kund och Kontaktuppgift
create procedure AddKund2Kontakt
(
    @KundID int,
    @KontaktuppgiftID int,

    @ID int output
)
as
begin
    insert into 
        Kund2Kontakt (KundID, KontaktuppgiftID)
    values
        (@KundID, @KontaktuppgiftID);
    set 
        @ID = SCOPE_IDENTITY(); -- S�tter out-parametern @ID till SCOPE_IDENTITY()
end
go

-- Skapar procedur f�r att f� alla Kontaktuppgifter som h�r till en Kund(ID)
create procedure GetKontaktuppgifterByKundID
(
    @KundID int
)
as
begin
    select
        k2k.ID,
        k2k.KundID,
        k.Personnr, k.F�rnamn, k.Efternamn,
        k2k.KontaktuppgiftID,
        ku.Kontakttyp, ku.Kontaktv�rde
    from
        Kontaktuppgifter as ku
    join 
        Kund2Kontakt as k2k on 
        ku.ID = k2k.KontaktuppgiftID
    join
        Kunder as k on
        k2k.KundID = k.ID
    where
        k.ID = @KundID; -- Condition f�r vilken Kund allt ska visas
end
go

-- Skapar procedur f�r att f� alla Kunder som h�r till en Kontaktuppgift(ID)
create procedure GetKunderByKontaktuppgiftID
(
    @KontaktuppgiftID int
)
as
begin
    select
        k2k.ID,
        k2k.KundID,
        k.Personnr, k.F�rnamn, k.Efternamn,
        k2k.KontaktuppgiftID, 
        ku.Kontakttyp, ku.Kontaktv�rde
    from
        Kontaktuppgifter as ku
    join 
        Kund2Kontakt as k2k on 
        ku.ID = k2k.KontaktuppgiftID
    join
        Kunder as k on
        k2k.KundID = k.ID
    where
        ku.ID = @KontaktuppgiftID; -- Condition f�r vilken Kontaktuppgift allt ska visas
end
go

-- Skapar procedur f�r att uppdatera en post(relation) i Kund2Kontakt
create procedure UpdateKund2Kontakt
(
    @ID int,
    @KundID int,
    @KontaktuppgiftID int
)
as
begin
    update
        Kund2Kontakt
    set
        KundID = @KundID,
        KontaktuppgiftID = @KontaktuppgiftID
    where
        ID = @ID; -- Condition f�r vilken post som skall uppdateras
end
go

-- Skapar en procedur f�r att deletea en post(relation) i Kund2Kontakt
create procedure CascadeDeleteKund2Kontakt -- D� jag satt "on delete cascade" i table creation beh�vs det inte h�r
(
    @ID int
)
as
begin
    delete from
        Kund2Kontakt
    where
        ID = @ID; -- Condition f�r vilken post som skall deletas
end
go


    




