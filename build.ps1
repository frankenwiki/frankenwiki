$dotNetVersion = "12.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"

$msbuildExe = join-path -path (Get-ItemProperty $regKey).$regProperty -childpath "msbuild.exe"

&$msbuildExe frankenwiki.sln /verbosity:minimal /p:Configuration=Release /p:RunOctoPack=true

packages\xunit.runner.console.2.0.0\tools\xunit.console.exe src\Frankenwiki.Tests\bin\Release\Frankenwiki.Tests.dll