CREATE DATABASE StaffInfoTrackerDb
COLLATE Cyrillic_General_CI_AS;
GO

USE StaffInfoTrackerDb;
GO

CREATE TABLE Addresses (
    AddressId INT NOT NULL IDENTITY,
    Country NVARCHAR(25) NOT NULL,
    City NVARCHAR(25) NOT NULL,
    Street NVARCHAR(25) NOT NULL,
    HouseNumber NVARCHAR(10) NOT NULL,
    ApartmentNumber SMALLINT NULL,
    PostIndex NVARCHAR(5) NULL,
    CONSTRAINT PK_Addresses PRIMARY KEY (AddressId),
    CONSTRAINT CK_Addresses_ApartmentNumber CHECK (ApartmentNumber > 0)
);
GO

CREATE TABLE Departments (
    DepartmentId INT NOT NULL IDENTITY,
    DepartmentName NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Departments PRIMARY KEY (DepartmentId)
);
GO

CREATE TABLE Positions (
    PositionId INT NOT NULL IDENTITY,
    PositionName NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Positions PRIMARY KEY (PositionId)
);
GO

CREATE TABLE CompanyDetails (
    CompanyId INT NOT NULL IDENTITY,
    CompanyName NVARCHAR(25) NOT NULL,
    AddressId INT NOT NULL,
    CONSTRAINT PK_CompanyDetails PRIMARY KEY (CompanyId),
    CONSTRAINT FK_CompanyDetails_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES Addresses (AddressId) ON DELETE CASCADE
);
GO

CREATE TABLE Employees (
    EmployeeId INT NOT NULL IDENTITY,
    PositionId INT NOT NULL,
    DepartmentId INT NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    MiddleName NVARCHAR(25) NOT NULL,
    AddressId INT NOT NULL,
    PhoneNumber NVARCHAR(10) NOT NULL,
    BirthDate DATE NOT NULL,
    HireDate DATE NOT NULL,
    Salary MONEY NOT NULL,
    CONSTRAINT PK_Employees PRIMARY KEY (EmployeeId),
    CONSTRAINT UNQ_Employees_PhoneNumber UNIQUE (PhoneNumber),
    CONSTRAINT CK_Employees_PhoneNumber CHECK (PhoneNumber LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT CK_Employees_Salary CHECK (Salary > 0),
    CONSTRAINT FK_Employees_Addresses_AddressId FOREIGN KEY (AddressId) REFERENCES Addresses (AddressId) ON DELETE CASCADE,
    CONSTRAINT FK_Employees_Departments_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES Departments (DepartmentId) ON DELETE CASCADE,
    CONSTRAINT FK_Employees_Positions_PositionId FOREIGN KEY (PositionId) REFERENCES Positions (PositionId) ON DELETE CASCADE
);
GO

INSERT INTO Addresses (Country, City, Street, HouseNumber, ApartmentNumber, PostIndex)
VALUES 
    ('Україна', 'Київ', 'Хрещатик', '10А', 5, '01001'),
    ('Україна', 'Львів', 'Ринок', '1В', 89, '79008'),
    ('Україна', 'Одеса', 'Дерибасівська', '18', 76, '65026'),
    ('Україна', 'Харків', 'Свободи', '25Г', 8, '61000'),
    ('Україна', 'Дніпро', 'Карла Маркса', '79', 123, NULL),
    ('Україна', 'Луцьк', 'Відродження', '5', 150, '04213');
GO

INSERT INTO Departments (DepartmentName)
VALUES 
    ('Дослідження та розвиток'),
    ('Логістика та постачання'),
    ('Клієнтська підтримка'),
    ('Мистецтво та дизайн'),
    ('Виробництво');
GO

INSERT INTO Positions (PositionName)
VALUES 
    ('Старший дослідник'),
    ('Менеджер з логістики'),
    ('Спеціаліст з клієнтської підтримки'),
    ('Дизайнер'),
    ('Аналітик'),
    ('Керівник проекту'),
    ('Технік');
GO

INSERT INTO Employees (DepartmentId, PositionId, FirstName, LastName, MiddleName, AddressId, PhoneNumber, BirthDate, HireDate, Salary)
VALUES 
    (2, 5, 'Олег', 'Іванов', 'Володимирович', 1, '1112223333', '1985-08-15', '2015-05-20', 60000),
    (3, 3, 'Ірина', 'Петрова', 'Сергіївна', 2, '2223334444', '1990-03-25', '2018-10-10', 55000),
    (4, 4, 'Анна', 'Сидоренко', 'Олексіївна', 3, '3334445555', '1988-12-10', '2017-08-30', 65000),
    (1, 1, 'Петро', 'Коваль', 'Миколайович', 4, '4445556666', '1975-06-02', '2005-04-15', 75000),
    (5, 6, 'Марина', 'Лисенко', 'Вікторівна', 5, '5556667777', '1993-02-14', '2019-12-01', 70000);
GO

INSERT INTO CompanyDetails (CompanyName, AddressId)
VALUES ('XYZ Corporation', 6);
GO