SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF Object_ID(N'Film_Upldate_Bulk') IS NOT NULL
Drop PROC [dbo].[Film_Upldate_Bulk]
GO

IF TYPE_ID(N'Film_Update_Bulk_Value') IS NOT NULL
Drop Type [dbo].[Film_Update_Bulk_Value]
GO

CREATE Type [dbo].[Film_Update_Bulk_Value] AS TABLE
(Code				int,
 [Name]				nvarchar(100),
 IsEnabled			bit,
 [RowVersion]		timestamp,
 [Description]		nvarchar(MAX),
 CategoryId			int,
 [Hash]				VARBINARY(256))

GO
CREATE OR ALTER   PROC [Film_Upldate_Bulk] 
(@FilmBulk dbo.Film_Update_Bulk_Value READONLY)


AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN 
	
		UPDATE [dbo].[Films] 
		SET    
			
			 [dbo].[Films].[Name]=FilmUPD.[Name]
			 ,[dbo].[Films].IsEnabled=FilmUPD.IsEnabled
			 ,[dbo].[Films].[Description]=FilmUPD.[Description]
			 ,[dbo].[Films].CategoryId=FilmUPD.CategoryId
			 ,[dbo].[Films].[LastUpdate]=GetDate()
			,[dbo].[Films].[Hash]=FilmUPD.[Hash]
			
		FROM [dbo].[Films]
		INNER JOIN @FilmBulk as FilmUPD 
		 ON [dbo].[Films].[Code]=FilmUPD.Code
		
	END
GO
