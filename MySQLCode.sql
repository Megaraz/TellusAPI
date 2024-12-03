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


