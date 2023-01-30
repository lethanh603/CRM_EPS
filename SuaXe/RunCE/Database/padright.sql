USE [system]
GO

/****** Object:  UserDefinedFunction [dbo].[fnPadRight]    Script Date: 04/18/2015 20:39:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Function [dbo].[fnPadRight](@i_vString Varchar(50), @i_vChar Varchar(5), @i_iLength Int)
Returns Varchar(500)
As Begin
Declare @mResult Varchar(500)
-- Remove Spaces From String
Set @mResult =RTrim(LTrim(@i_vString))
-- Check the need to run statements
If (@i_iLength > Len(@mResult) And @i_iLength <= 500)
Begin
-- Add character(s) to right side of string
Set @mResult = @mResult + Replicate(@i_vChar, (@i_iLength - Len(@mResult)))
Set @mResult = Right(@mResult, @i_iLength)
End
Return @mResult
End
GO

