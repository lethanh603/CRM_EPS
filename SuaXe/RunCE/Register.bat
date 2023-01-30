@echo off

gacutil -u DevExpress.Data.v11.1
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Data.v11.1\11.1.6.0__b88d1754d700e49a
copy DevExpress.Data.v11.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Data.v11.1\11.1.6.0__b88d1754d700e49a

gacutil -u DevExpress.Utils.v11.1
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Utils.v11.1\11.1.6.0__b88d1754d700e49a
copy DevExpress.Utils.v11.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Utils.v11.1\11.1.6.0__b88d1754d700e49a

gacutil -u DevExpress.CodeRush.Common
mkdir %windir%\assembly\GAC_MSIL\DevExpress.CodeRush.Common\11.1.6.0__35c9f04b7764aa3d
copy DevExpress.CodeRush.Common.dll %windir%\assembly\GAC_MSIL\DevExpress.CodeRush.Common\11.1.6.0__35c9f04b7764aa3d
REM copy DevExpress.CodeRush.Common.dll "C:\Program Files\DevExpress 2011.1\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll"
if "[%ProgramFiles(x86)%]" == "[]" (copy DevExpress.CodeRush.Common.dll "%ProgramFiles%\DevExpress 2011.1\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll") else (copy DevExpress.CodeRush.Common.dll "%ProgramFiles(x86)%\DevExpress 2011.1\IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll")


echo 'OK'
pause