USE [system]
GO

/****** Object:  UserDefinedFunction [dbo].[fnPadLeft]    Script Date: 04/18/2015 20:39:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Function [dbo].[fnPadLeft](@i_vString Varchar(50), @i_vChar Varchar(5), @i_iLength Int)
Returns Varchar(500)
As Begin
Declare @mResult Varchar(500)
-- Remove Spaces From String
Set @mResult =RTrim(LTrim(@i_vString))
-- Check the need to run statements
If (@i_iLength > Len(@mResult) And @i_iLength <= 500)
Begin
-- Add character(s) to left side of string
Set @mResult = Replicate(@i_vChar, (@i_iLength - Len(@mResult))) + @mResult
Set @mResult = Left(@mResult, @i_iLength)
End
Return @mResult
End
GO

