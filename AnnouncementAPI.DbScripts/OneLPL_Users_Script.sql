USE OneLPL

--Create Users Table
CREATE TABLE Users(
	UserID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Name varchar(30) NULL,
	Email varchar(30) NULL,
	PhNo varchar(10) NULL,
	Address varchar(30) NULL,
);

--INSERT Data into Users Table
INSERT INTO Users (Name, Email, PhNo,Address) VALUES ('Tanmoy','t@gmail.com','9887766589','West Bengal')

--GET Data from Users Table
SELECT * FROM Users WITH (NOLOCK)


-- Create Stored Procedure for ADD,UPDATE AND DELETE Data
GO
CREATE PROCEDURE OneLPL_SaveUser ( @UserID int, @Name VARCHAR(30) = null,@Email VARCHAR(30)=null,
@PhNo VARCHAR(10) = null,@Address VARCHAR(30)=null,@Action NVARCHAR(10) = '')

AS

BEGIN

      --INSERT
      IF @Action = 'ADD'
      BEGIN
            INSERT INTO Users
            VALUES (@Name,@Email,@PhNo,@Address)
      END
 
      --UPDATE
      IF @Action = 'UPDATE'
      BEGIN
            UPDATE Users
            SET Name = @Name, Email = @Email, PhNo=@PhNo, Address = @Address
            WHERE UserID = @UserID
      END
 
      --DELETE
      IF @Action = 'DELETE'
      BEGIN
            DELETE FROM Users
            WHERE UserID = @UserID
      END
END


  --Execute Stored Procedure OneLPL_SaveUser for Adding row.
  EXEC OneLPL_SaveUser null,'Tanmoy','t@gmail.com','9887766589','West Bengal','ADD'
 EXEC OneLPL_SaveUser null,'Akhilesh','a@gmail.com','9887766589','West Bengal','ADD'
  EXEC OneLPL_SaveUser null,'Sarika','s@gmail.com','9887766589','West Bengal','ADD'
   EXEC OneLPL_SaveUser null,'Hritesh','h@gmail.com','9887766589','West Bengal','ADD'

 -- Execute Stored Procedure OneLPL_SaveUser for Updating a row.

EXEC OneLPL_SaveUser @UserID=2,@Name='Akhilesh',@Email='a@gmail.com',@PhNo='1223344556',@Address='Hyderabad',@Action='UPDATE'

--Execute Stored Procedure OneLPL_SaveUser for Deleting a row.
EXEC OneLPL_SaveUser @UserID=5,@Name='Hritesh',@Email='h@gmail.com',@PhNo='9887766589',@Address='West Bengal',@Action='DELETE'

--Create a Stored Procedure for Get the All the data available in  User table

GO
CREATE PROCEDURE OneLPL_GetUser( @UserID int = null, @Name VARCHAR(30) = null,@Email VARCHAR(30)=null,
@PhNo VARCHAR(10) = null,@Address VARCHAR(30)=null)

AS

SELECT * FROM Users; 


--Execute Stored Procedured GetAllData from Users.
EXEC OneLPL_GetUser
