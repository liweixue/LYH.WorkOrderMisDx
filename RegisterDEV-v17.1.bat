@echo off
color 1e
title ×¢²áDEV¿Ø¼þ

set pathdev="%~dp0"

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.BonusSkins.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.BonusSkins.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.BonusSkins.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.BonusSkins.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.Data.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Data.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.Data.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Data.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.Utils.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Utils.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.Utils.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Utils.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.XtraBars.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraBars.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.XtraBars.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraBars.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.XtraEditors.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.XtraEditors.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.XtraGrid.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraGrid.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.XtraGrid.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraGrid.v17.1\17.1.4.0__b88d1754d700e49a

%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe -i "DevExpress.XtraLayout.v17.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraLayout.v17.1\17.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.XtraLayout.v17.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraLayout.v17.1\17.1.4.0__b88d1754d700e49a


echo 'OK'