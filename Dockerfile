FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5005

ENTRYPOINT ["dotnet", "docker-dotnetcore-api.dll"]
