$nugetApiKey = Read-Host 'Enter your NuGet API key: '
$versionNumber = Read-Host 'Enter the version number of the NuGet Package you want to publish: '

try {
	& 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe' '..\build.xml' /t:Publish /p:NuGetApiKey=$nugetApiKey /p:VersionNumber=$versionNumber
}
catch {
	Write-Error $error[0]
	Exit 1
}

Write-Host "Press any key to continue ..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")