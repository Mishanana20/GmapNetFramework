DROP DATABASE [TestTask_Gmap];  
GO  

CREATE DATABASE [TestTask_Gmap]
GO

USE [TestTask_Gmap];
CREATE TABLE [dbo].[markers](
  id int IDENTITY(1,1) NOT NULL,
  name VARCHAR(255) NOT NULL,
  longitude FLOAT,
  latitude FLOAT
  CONSTRAINT [PK_markers] PRIMARY KEY CLUSTERED 
  (
    id ASC
  )
)
GO