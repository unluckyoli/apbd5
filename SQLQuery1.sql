CREATE TABLE Animal (
	IdAnimal INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(200),
	Description NVARCHAR(200) NULL,
	Category NVARCHAR(200),
	Area NVARCHAR(200)
)
INSERT INTO Animal
VALUES('Animal', 'Desc1','Cat1', 'Area1');
INSERT INTO Animal
VALUES('Animal2', NULL,'Cat2', 'Area3');
INSERT INTO Animal
VALUES('Animal3', 'Desc3','Cat3', 'Area2');

SELECT * FROM Animal