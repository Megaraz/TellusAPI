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

INSERT INTO Kunder (Personnr, Förnamn, Efternamn)
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
    k.Förnamn
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
    



