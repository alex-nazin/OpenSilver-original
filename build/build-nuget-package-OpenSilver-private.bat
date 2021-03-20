@echo off

IF NOT EXIST "nuspec/OpenSilver.nuspec" (
echo Wrong working directory. Please navigate to the folder that contains the BAT file before executing it.
PAUSE
EXIT
)



rem Define the escape character for colored text
for /F %%a in ('"prompt $E$S & echo on & for %%b in (1) do rem"') do set "ESC=%%a"

rem Define the "%PackageVersion%" variable:
set /p PackageVersion="%ESC%[92mOpenSilver version:%ESC%[0m 1.0.0-private-"

rem Get the current date and time:
for /F "tokens=2" %%i in ('date /t') do set currentdate=%%i
set currenttime=%time%

rem Create a Version.txt file with the date:
md temp
@echo OpenSilver 1.0.0-private-%PackageVersion% (%currentdate% %currenttime%)> temp/Version.txt

echo. 
echo %ESC%[95mRestoring NuGet packages%ESC%[0m
echo. 
nuget restore ../src/OpenSilver.sln
nuget restore ../src/VSExtension/VSExtension.OpenSilver.sln -MSBuildVersion 14

echo. 
echo %ESC%[95mBuilding %ESC%[0mSL %ESC%[95mconfiguration%ESC%[0m
echo. 
msbuild ../src/OpenSilver.sln -p:Configuration=SL -restore
echo. 
echo %ESC%[95mPacking %ESC%[0mOpenSilver %ESC%[95mNuGet package%ESC%[0m
echo. 
nuget.exe pack nuspec\OpenSilver.nuspec -OutputDirectory "output/OpenSilver" -Properties "PackageId=OpenSilver;PackageVersion=1.0.0-private-%PackageVersion%;Configuration=SL;AssembliesPrefix=OpenSilver"

echo. 
echo %ESC%[95mBuilding %ESC%[0mSL.WorkInProgress %ESC%[95mconfiguration%ESC%[0m
echo. 
msbuild ../src/OpenSilver.sln -p:Configuration=SL.WorkInProgress
echo. 
echo %ESC%[95mPacking %ESC%[0mOpenSilver.WorkInProgress %ESC%[95mNuGet package%ESC%[0m
echo. 
nuget.exe pack nuspec\OpenSilver.nuspec -OutputDirectory "output/OpenSilver" -Properties "PackageId=OpenSilver.WorkInProgress;PackageVersion=1.0.0-private-%PackageVersion%;Configuration=SL.WorkInProgress;AssembliesPrefix=OpenSilver"

echo. 
echo %ESC%[95mBuilding %ESC%[0mUWP %ESC%[95mconfiguration%ESC%[0m
echo. 
msbuild ../src/OpenSilver.sln -p:Configuration=UWP
echo. 
echo %ESC%[95mPacking %ESC%[0mOpenSilver.UWPCompatible %ESC%[95mNuGet package%ESC%[0m
echo. 
nuget.exe pack nuspec\OpenSilver.nuspec -OutputDirectory "output/OpenSilver" -Properties "PackageId=OpenSilver.UWPCompatible;PackageVersion=1.0.0-private-%PackageVersion%;Configuration=UWP;AssembliesPrefix=OpenSilver.UWPCompatible"

rem echo. 
rem echo %ESC%[95mBuilding %ESC%[0mVSIX %ESC%[0m
rem echo. 
rem msbuild ../src/VSExtension/VSExtension.OpenSilver.sln -p:Configuration=Release -p:VisualStudioVersion=14.0
rem echo. 
rem echo %ESC%[95mCopying %ESC%[0mOpenSilver.vsix %ESC%[95mto output folder%ESC%[0m
rem echo. 
rem xcopy ..\src\VSExtension\OpenSilver.VSIX\bin\OpenSilver\Release\OpenSilver.vsix output\OpenSilver\ /Y

explorer "output\OpenSilver"

pause
