USE [system_2020]

INSERT [dbo].[DMSTATUS] ([idstatustype], [status], [idstatus], [note], [sign], [statusname], [idcolor], [iscall]) VALUES (N'TS000001', 1, N'ST000010', N'', N'KKNM', N'Khách không nghe máy', N'', 1)
GO
INSERT [dbo].[DMSTATUS] ([idstatustype], [status], [idstatus], [note], [sign], [statusname], [idcolor], [iscall]) VALUES (N'TS000001', 1, N'ST000011', NULL, N'KNHL', N'Khách nghe máy, hẹn liêu lạc lại sau', N'', 1)
GO
INSERT [dbo].[DMSTATUS] ([idstatustype], [status], [idstatus], [note], [sign], [statusname], [idcolor], [iscall]) VALUES (N'TS000001', 1, N'ST000012', NULL, N'CSTT', N'Đang chắm sóc, chỉ trao đổi thông thường, chưa xuất hiện nhu cầu', N'', 1)
GO
INSERT [dbo].[DMSTATUS] ([idstatustype], [status], [idstatus], [note], [sign], [statusname], [idcolor], [iscall]) VALUES (N'TS000001', 1, N'ST000013', NULL, N'NPNC', N'Cần nhập phiếu nhu cầu', N'', 1)

INSERT [dbo].[sysControls] ([form_name], [control_name], [type], [text_EN], [text_VI], [status], [field_name], [sql_lue], [valuemember_lue], [displaymember_lue], [caption_col_lue_VI], [fieldname_col_lue], [caption_col_lue_EN], [ma_sub_menu], [stt], [ShortcutKeys], [image]) VALUES (N'frm_DMSTATUS_S', N'chk_iscall_I6', N'CheckEdit', N'Call', N'Call', 1, N'iscall', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
