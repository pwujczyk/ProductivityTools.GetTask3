﻿CREATE TABLE [gt].[Tomato]
(
	[TomatoId] INT NOT NULL IDENTITY(1,1),
    [Status] INT NOT NULL, 
    [Created] DATETIME2 NOT NULL DEFAULT(GETDATE()),
    [Finished] DATETIME2 NULL, 
	CONSTRAINT PK_Tomato PRIMARY KEY(TomatoId)
)
