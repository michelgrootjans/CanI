try {
	& 'C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe' '..\build.xml' /t:CI
}
catch {
	Write-Error $error[0]
	Exit 1
}

Write-Host "Press any key to continue ..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")