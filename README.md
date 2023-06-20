Technical test by Julian Andres Fernandez Dominguez

Sql Requirements:

--1

CREATE DATABASE STGenetics;

--2

USE STGenetics;

CREATE TABLE Animal (
    AnimalId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NULL,
    Breed VARCHAR(100) NULL,
    BirthDate DATE NULL,
    Sex VARCHAR(10) NULL,
    Price DECIMAL(10, 2) NULL,
    Status VARCHAR(20) NULL
);


--3

CREATE PROCEDURE SP_InsertAnimal
    @Name VARCHAR(100),
    @Breed VARCHAR(100),
    @BirthDate DATE,
    @Sex VARCHAR(10),
    @Price DECIMAL(10, 2),
    @Status VARCHAR(10),
	@retID int OUTPUT
AS
BEGIN
    INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status)
    VALUES (@Name, @Breed, @BirthDate, @Sex, @Price, @Status)

	SELECT @retID = SCOPE_IDENTITY()
END

CREATE PROCEDURE SP_UpdateAnimal
    @AnimalId INT,
    @Name VARCHAR(100),
    @Breed VARCHAR(100),
    @BirthDate DATE,
    @Sex VARCHAR(10),
    @Price DECIMAL(10, 2),
    @Status VARCHAR(20)
AS
BEGIN
    UPDATE Animal
    SET Name = @Name,
        Breed = @Breed,
        BirthDate = @BirthDate,
        Sex = @Sex,
        Price = @Price,
        Status = @Status
    WHERE AnimalId = @AnimalId;
END


DELETE FROM Animal
WHERE AnimalId = 1;

--4

DECLARE @i INT = 1;
DECLARE @sex VARCHAR(10);
DECLARE @birthDate DATE;
DECLARE @price DECIMAL(10, 2);

WHILE @i <= 5000
BEGIN

    SET @sex = CASE WHEN @i % 2 = 0 THEN 'female' ELSE 'male' END;
    SET @birthDate = DATEADD(DAY, -CAST(RAND() * DATEDIFF(DAY, '2020-01-01', GETDATE()) AS INT), GETDATE());    
    SET @price = CAST((RAND() * (1000 - 10) + 10) AS DECIMAL(10, 2));
    
    INSERT INTO Animal (Name, Breed, BirthDate, Sex, Price, Status)
    VALUES ('Animal' + CAST(@i AS VARCHAR(10)), 'Breed' + CAST(@i AS VARCHAR(10)), @birthDate, @sex, @price, 'active');
    
    SET @i = @i + 1;

END

--5

SELECT *
FROM Animal
WHERE BirthDate <= DATEADD(YEAR, -2, GETDATE()) AND Sex = 'female'
ORDER BY Name;

--6

SELECT Sex, COUNT(*) AS Quantity
FROM Animal
GROUP BY Sex;

--Aditional tables 

CREATE TABLE [Order]
(
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL
);


CREATE TABLE OrderDetail
(
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    AnimalId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
    CONSTRAINT FK_OrderDetail_Animal FOREIGN KEY (AnimalId) REFERENCES Animal(AnimalId)
);

