docker rm $(docker ps -a -q) -f

docker network create jomaya-eventbus
docker network create jomaya-frontend
docker network create jomaya-autoservice
docker network create jomaya-klantenservice
docker network create jomaya-integratieservice
docker network create jomaya-foutlogging
docker network create jomaya-auditlog

cd ..\..
cd "Jomaya.RabbitMQ"
docker-compose up --build -d
cd ..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.Frontend\Jomaya.Frontend.Facade"
docker-compose up --build -d
cd ..\..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.Auditlog"
docker-compose up --build -d
cd ..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.AutoService\Jomaya.AutoService\src\003 - Facade\Jomaya.AutoService.Facade"
docker-compose up --build -d
cd ..\..\..\..\..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.Klantenservice\Jomaya.Klantenservice\src\003 - Facade\Jomaya.Klantenservice.Facade"
docker-compose up --build -d
cd ..\..\..\..\..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.IntegratieService\Jomaya.IntegratieService\src\003 - Facade\Jomaya.IntegratieService.Facade"
docker-compose up --build -d
cd ..\..\..\..\..
cd "99-shared\deliverables"

TIMEOUT 10

cd ..\..
cd "Jomaya.FoutLogging\Jomaya.FoutLogging\src\003 - Facade\Jomaya.FoutLogging.Facade"
docker-compose up --build -d
cd ..\..\..\..\..
cd "99-shared\deliverables"

docker ps -a

pause