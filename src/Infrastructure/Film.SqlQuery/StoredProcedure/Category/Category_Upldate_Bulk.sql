SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Type [dbo].[Category_Update_Bulk_Value] AS TABLE
(Code				int,
 [Name]				nvarchar(100),
 IsEnabled			bit,
 [RowVersion]		timestamp,
 [Description]		nvarchar(MAX),
 [Priority]			int,
 [Hash]				VARBINARY(256))

GO
CREATE OR ALTER   PROC [Category_Upldate_Bulk] 
(@CategoryBulk dbo.Category_Update_Bulk_Value READONLY)


AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN 
	
		UPDATE [dbo].[Categories] 
		SET    
			
			 [dbo].[Categories].[Name]=CategoryUPD.[Name]
			 ,[dbo].[Categories].IsEnabled=CategoryUPD.IsEnabled
			 ,[dbo].[Categories].[Description]=CategoryUPD.[Description]
			 ,[dbo].[Categories].[Priority]=CategoryUPD.[Priority]
			 ,[dbo].[Categories].[LastUpdate]=GetDate()
			,[dbo].[Categories].[Hash]=CategoryUPD.[Hash]
			
		FROM [dbo].[Categories]
		INNER JOIN @CategoryBulk as CategoryUPD 
		 ON [dbo].[Categories].[Code]=CategoryUPD.Code
		
	END
GO
