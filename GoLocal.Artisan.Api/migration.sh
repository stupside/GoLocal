echo -e "1. migrate"
echo -e "2. update database"
echo -e "3. remove migration"
echo
read -r -p "option > " option
echo
if [ "$option" -eq 1 ]; then
  read -r -p "init (y/n) > " -n 1 first
  echo
  if [ "$first" = 'y' ]; then
    read -r -p "username > " username
    read -r -p "password > " password
    echo
    
    dotnet user-secrets init
    #dotnet user-secrets clear
    #dotnet user-secrets list
    
    dotnet user-secrets set ConnectionStrings:Context "Host=127.0.0.1; Database=golocal_core;Username=$username;Password=$password"
    
    dotnet ef migrations add Initial --context Context --project "../GoLocal.Persistence" --output-dir "../GoLocal.Persistence/EntityFramework/Migrations"
  else
    read -r -p "name > " name
    echo
    dotnet ef migrations add "$name" --context Context --project "../GoLocal.Persistence" --output-dir "../GoLocal.Persistence/EntityFramework/Migrations"
  fi
  read -r -p "apply migration (y/n) > " apply
  echo
  if [ "$apply" = 'y' ]; then
    dotnet ef database update --context Context --project "../GoLocal.Persistence"
  fi
elif [ "$option" -eq 2 ]; then
    dotnet ef database update --context Context --project "../GoLocal.Persistence"
elif [ "$option" -eq 3 ]; then
    dotnet ef database update --context Context --project "../GoLocal.Persistence"
fi

read -r -p "press a key to exit > "