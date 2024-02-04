# Migrations

## Generate
```
$ dotnet ef migrations add
```

## Apply pending
```
$ dotnet ef database update
```

## Revert
```
$ dotnet ef database update [migration] # 0 - revert all
```

# Scaffolding
```
$ dotnet aspnet-codegenerator razorpage -m User -dc ActiveCity.Data.ActiveCityContext -udl -outDir Pages/Users --referenceScriptLibraries --databaseProvider sqlite
```