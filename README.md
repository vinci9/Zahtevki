API za upravljanje zahtevkov z uporabo Clean Architecture, Entity Framework InMemory bazo in unit testi.

Uporaba:
1. V mapi projekta odpremo terminal in zaženemo:
dotnet restore

2. Za zagon testov se premaknemo v mapo Zahtevki.Tests

3. Teste poženemo z:
dotnet test

Če hočemo sami testirati določeno funkcionalnost:
1. Premaknemo se v mapo Zahtevek.Api

2. V terminalu zaženemo:
dotnet run

3. V brskalniku odpremo povezavo:
http://localhost:5036/swagger/index.html

ALI

1. v drugem terminalu uporabimo curl comande:

curl -X POST http://localhost:5036/api/tasks -H "Content-Type: application/json" -d "{\"title\": \"Test Task\", \"description\": \"Test Description\", \"dueDate\": \"2024-01-01T00:00:00Z\", \"priority\": 1}"

curl -X GET http://localhost:5036/api/tasks
   





   



