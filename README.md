# everyone-codes-npo-practice


If you want to run the CLI tool:
"cd <path to CLISearch project>"
"dotnet run --name <camera name you want to search>"
Alternatively, it's probably possible to install it as global tool, but I haven't tried that yet.

If you want to change the CLI tool:
This uses the System.CommandLine package, which is still in prerelease, the implementation might significantly change in the future.



If you want to change the db models, you need to run these commands to create a migration/change the db:
- Install dotnet cli tools
- "dotnet ef migrations add <migration name> -p <path to db project> -s <path to startup project (Api/Blazor?)>"
- "dotnet ef database update -p <path to db project> -s <path to startup project (Api/Blazor?)>"