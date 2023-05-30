USE OneLPL

--Create Announcements Table
CREATE TABLE Announcements(
	Announcement_ID int PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
	Title VARCHAR(30),
	Summary VARCHAR(30),
	Descriptions VARCHAR(30),
	CreatedDate DateTime DEFAULT GETDATE()
);

--INSERT Data into Announcements Table
INSERT INTO Announcements (UserID,Title,Summary,Descriptions) VALUES ((SELECT UserID FROM Users WHERE Users.UserID=1),'Party','Sunday 10PM','Come Join us')

--GET Data from Announcements Table
SELECT * FROM Announcements WITH (NOLOCK)


-- Create Stored Procedure for ADD,UPDATE AND DELETE Data
GO
CREATE PROCEDURE OneLPL_SaveAnnouncement
   @Announcement_ID INT,@UserID INT,
   @Title VARCHAR(30)=null, @Summary VARCHAR(30)=null,
   @Descriptions VARCHAR(50)=null, @CreatedDate DateTime , @Action VARCHAR(20)=''

AS
SET @CreatedDate = GetDate()
 BEGIN
   --ADD
   IF @Action = 'ADD'
      BEGIN
            INSERT INTO Announcements(UserID,Title,Summary,Descriptions,CreatedDate)
            VALUES ((SELECT UserID FROM Users WHERE Users.UserID=@UserID),@Title,@Summary,@Descriptions,@CreatedDate)
      END
	   --UPDATE
      IF @Action = 'UPDATE'
      BEGIN
           UPDATE Announcements SET  Title=@Title,Summary=@Summary,
           Descriptions=@Descriptions WHERE Announcement_ID = @Announcement_ID
      END
	  --DELETE
      IF @Action = 'DELETE'
      BEGIN
            DELETE FROM Announcements
            WHERE Announcement_ID = @Announcement_ID
      END
      
 END

  --Execute Stored Procedure spSAVE for Adding row.
  EXEC OneLPL_SaveAnnouncement null,1,'Party','Sunday 10PM','Come Join us','','ADD'
 EXEC OneLPL_SaveAnnouncement null,2,'Party','Sunday 10PM','Come Join us','','ADD'
  EXEC OneLPL_SaveAnnouncement null,3,'Party','Sunday 10PM','Come Join us','','ADD'
   EXEC OneLPL_SaveAnnouncement null,4,'Party','Sunday 10PM','Come Join us','','ADD'

-- Execute Stored Procedure spSAVE for Updating a row.

EXEC OneLPL_SaveAnnouncement @Announcement_ID = 3,@UserID=null,@Title='Client Visit',@Summary='Friday 10PM',@Descriptions='Come Join Us', @CreatedDate='',@Action='UPDATE'

--Execute Stored Procedure spSAVE for Deleting a row.
EXEC OneLPL_SaveAnnouncement @Announcement_ID = 5,@UserID=null,@CreatedDate='',@Action='DELETE'

--Create a Stored Procedure for Get the All the data available in announcement and Users table
GO
CREATE PROCEDURE OneLPL_GetAnnouncement  @Announcement_ID INT =null,@UserID INT = null,
   @Title VARCHAR(30)=null, @Summery VARCHAR(30)=null,
   @Descriptions VARCHAR(50)=null, @CreatedDate DateTime = null

AS

SELECT Announcement_ID, Users.UserID, Name, Email, Title, Summary,Descriptions,CreatedDate FROM Announcements
INNER JOIN Users
ON Announcements.UserID=Users.UserID;


--Execute Stored Procedured GetAllData.
EXEC OneLPL_GetAnnouncement;