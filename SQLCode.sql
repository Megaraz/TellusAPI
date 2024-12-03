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
	Kontaktvärde nvarchar(64) not null
		
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
	ProdukterID int not null references Produkter(ID) on delete cascade,
	[OrderID] int not null references [Order](ID) on delete cascade,
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

alter procedure AddKontaktuppgift
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

alter procedure UpdateKontaktuppgift
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

alter procedure CascadeDeleteKontaktuppgift
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



