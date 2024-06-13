# Entities

Contain all the models and Database context

## First step :

Grab the correct connection string and put it into the `appsetting.json` file. Remember to put it at `ConnectionStrings.Phase2Database`. Example :

```json
{
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft.AspNetCore": "Warning"
		}
	},
	"AllowedHosts": "*",
	"ConnectionStrings": {
		"Phase2Database": "CONNECTION STRING HERE"
	}
}
```

## Generate database :

Once the connection string is valid, simply open `Package Manager Console` and run `Update-Database`.

## Scaffolding Database :

To re-scaffold the `DbContext.cs` as well as those models Entities inside the `Models` folder. Simply delete them and reinvoke the following command inside the `Package Manager Console` :

```powershell
Scaffold-DbContext "Name=ConnectionStrings:Phase2Database" Microsoft.EntityFrameWorkCore.SqlServer -outputdir Models -context FamsContext -contextdir Context -DataAnnotations -Force
```

Please remember to
