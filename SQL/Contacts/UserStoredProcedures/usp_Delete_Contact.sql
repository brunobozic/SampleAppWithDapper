create procedure [dbo].[DeleteGroupMeeting]    
(    
    @Id int     
)    
As    
BEGIN    
    DELETE FROM GroupMeeting WHERE Id=@Id    
END