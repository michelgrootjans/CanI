Run volgend script met powershell: build\scripts\buildCI.ps1 
Dit script doet het volgende:
	* Versienummer updaten (buildnumber + 1)
	* Applicatie compilen in release
	* Tests uitvoeren (test resultaten komen onder build\artifacts\nunit)
	* NuGet package aanmaken (packages wordt geplaatst onder build\artifacts\nuget)


Om de NuGet package te uploaden naar nuget.org, run je volgend script met powershell: build\scripts\buildPublish.ps1 
Wanneer je dit script runt, wordt er om volgende input gevraagd:
	* Je NuGet API Key (deze vind je terug in je nuget.org account)
	* De versienummer van de te deployen package in volgend formaat x.x.x.x
Vervolgens worden volgende packages gepushed naar nuget.org:
	* CanI.Core.[versienummer].nupkg
	* CanI.Mvc.[versienummer].nupkg