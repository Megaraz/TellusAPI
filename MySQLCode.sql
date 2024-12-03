select * from Kunder
order by
	ID 
go

drop table Kunder
exec dbo.CascadeDeleteKund @ID = 1007;

select
	*
from
	Kunder as K
left join
	Kund2Kontakt as K2K on
	K2K.KunderID = K.ID
right join
	Kontaktuppgifter as KU on
	K2K.KontaktuppgifterID = KU.ID
go

INSERT INTO Kunder (Personnr, F�rnamn, Efternamn)
VALUES 
('19900101-1234', 'Anna', 'Svensson'),
('19850505-5678', 'Erik', 'Johansson'),
('20000101-9876', 'Maria', 'Andersson'),
('19781212-4321', 'Lars', 'Karlsson'),
('19950615-1111', 'Emma', 'Nilsson');
GO


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

alter procedure UpdateKontaktuppgift
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

use Tellus

select 
    * 
from 
    Adresser
go

select
    *
from
    Kunder

insert into
    Kund2Adress(KunderID, AdresserID)
values
    (1025, 2)
go

select
    a.*,
    k.F�rnamn
from
    Adresser as a
join 
    Kund2Adress as k2a on 
    A.ID = k2a.AdresserID
join
    Kunder as k on
    k2a.KunderID = k.ID
where
    k.id = 1025;
    



