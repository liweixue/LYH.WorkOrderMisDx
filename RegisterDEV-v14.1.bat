@echo off
color 1e
title ×¢²áDEV¿Ø¼þ

set pathdev="%~dp0"

REM gacutil -u "DevExpress.BonusSkins.v14.1"
REM mkdir %windir%\assembly\GAC_MSIL\DevExpress.BonusSkins.v14.1\14.1.4.0__b88d1754d700e49a
REM copy %pathdev%DevExpress.BonusSkins.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.BonusSkins.v14.1\14.1.4.0__b88d1754d700e49a

gacutil -u "DevExpress.Data.v14.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Data.v14.1\14.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.Data.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Data.v14.1\14.1.4.0__b88d1754d700e49a

gacutil -u "DevExpress.Utils.v14.1"
mkdir %windir%\assembly\GAC_MSIL\DevExpress.Utils.v14.1\14.1.4.0__b88d1754d700e49a
copy %pathdev%DevExpress.Utils.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.Utils.v14.1\14.1.4.0__b88d1754d700e49a

REM gacutil -u "DevExpress.XtraBars.v14.1"
REM mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraBars.v14.1\14.1.4.0__b88d1754d700e49a
REM copy %pathdev%DevExpress.XtraBars.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraBars.v14.1\14.1.4.0__b88d1754d700e49a

REM gacutil -u "DevExpress.XtraEditors.v14.1"
REM mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v14.1\14.1.4.0__b88d1754d700e49a
REM copy %pathdev%DevExpress.XtraEditors.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraEditors.v14.1\14.1.4.0__b88d1754d700e49a

REM gacutil -u "DevExpress.XtraGrid.v14.1"
REM mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraGrid.v14.1\14.1.4.0__b88d1754d700e49a
REM copy %pathdev%DevExpress.XtraGrid.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraGrid.v14.1\14.1.4.0__b88d1754d700e49a

REM gacutil -u "DevExpress.XtraLayout.v14.1"
REM mkdir %windir%\assembly\GAC_MSIL\DevExpress.XtraLayout.v14.1\14.1.4.0__b88d1754d700e49a
REM copy %pathdev%DevExpress.XtraLayout.v14.1.dll %windir%\assembly\GAC_MSIL\DevExpress.XtraLayout.v14.1\14.1.4.0__b88d1754d700e49a


echo 'OK'