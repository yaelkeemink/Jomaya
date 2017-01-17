FROM microsoft/dotnet:1.0.1-core
ARG source=./bin/Debug/netcoreapp1.0/publish
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
COPY $source .

ENTRYPOINT ["dotnet", "Jomaya.Klantenservice.Facade.dll"]