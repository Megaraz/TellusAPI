drop database Tellus
go

select * from Kunder
order by
	ID 
go

drop table Kunder
exec dbo.CascadeDeleteKund @ID = 1007;

select
    K.ID,
	K.Personnr, K.Förnamn, K.Efternamn,
    KU.Kontakttyp, KU.Kontaktvärde,    
    O.*
from
	Kunder as K
full join
	Kund2Kontakt as K2K on
	K2K.KunderID = K.ID
full join
	Kontaktuppgifter as KU on
	K2K.KontaktuppgifterID = KU.ID
full join
    [Order] as O on
    K.ID = O.KundID
where
    K.ID = 1001
group by
    COUNT(Kontaktvärde) 
go

select * from Kund2Kontakt

insert into [order]
values
    (NEXT VALUE FOR OrdernrSequence, 0, 0, 0, null, GETDATE(), DATEADD(day, 7, GETDATE()), 1)
    

select
    K.Personnr, K.Förnamn, K.Efternamn,
    '---' as [ ],
    A.Gatuadress, A.Ort, A.Postnr, A.[Lgh nummer]
from
    Kunder as K
full join
    Kund2Adress as K2A on
    K.ID = K2A.KundID
full join
    Adresser as A on
    K2a.AdressID = A.ID
    



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
    '---',
    k.*
from
    Adresser as a
join 
    Kund2Adress as k2a on 
    A.ID = k2a.AdresserID
join
    Kunder as k on
    k2a.KunderID = k.ID
go


select
    *
from
    Kunder
order by
    ID

drop table
    Adresser
drop table
    Kund2Adress


truncate table Kunder
go

drop database Tellus


drop table Produkter2Order


select * from Produkter

select * from [Order]


drop database Tellus

select * from Kontaktuppgifter

select * from Produkter
go

create procedure GetAdresserByKundID
(
    @KundID int
)
as
    begin
        select
            k2a.KundID,
            a.Gatuadress, a.Ort, a.Postnr, a.[Lgh nummer],
            k.Personnr, k.Förnamn, k.Efternamn
        from
            Adresser as a
        join 
            Kund2Adress as k2a on 
            A.ID = k2a.AdressID
        join
            Kunder as k on
            k2a.KundID = k.ID
        where
            k.ID = @KundID
    end
go

exec GetAdresserByKundID @KundID = 1001
go

create procedure GetAdresserByKundID
(
    @KundID int
)
as
begin
    select
        a.Gatuadress,
        a.Ort,
        a.Postnr,
        a.[Lgh nummer],
        k2a.KundID,
        k.Personnr,
        k.Förnamn,
        k.Efternamn
    from
        Adresser as a
    join 
        Kund2Adress as k2a on a.ID = k2a.AdresserID
    join
        Kunder as k on k2a.KundID = k.ID
    where
        k.ID = @KundID;
end;
go



select
    a.Gatuadress, a.Ort, a.Postnr, a.[Lgh nummer],
    k2a.KundID,
    k.Personnr, k.Förnamn, k.Efternamn
from
    Adresser as a
join 
    Kund2Adress as k2a on 
    A.ID = k2a.AdressID
join
    Kunder as k on
    k2a.KundID = k.ID
where
    k.ID = 1001
go


create procedure GetKunderByAdressID
(
    @AdressID int
)
as
begin
    select
            k2a.KundID,
            k.Personnr, k.Förnamn, k.Efternamn,
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
            a.ID = @AdressID;
end
go

exec GetKunderByAdressID @AdressID = 1
