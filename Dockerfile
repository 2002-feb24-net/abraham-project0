FROM mcd.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY *.sln ./
COPY Project0/*.csproj Project0/
COPY Project0.Library/*.csproj Project0.Library/

RUN dotnet restore

COPY . ./

RUN dotnet publish Project0 -o publish --no-restore

FROM mcr.microsoft.com/dotnet/core/runtime:3.1

WORKDIR /app

COPY --from=build /app/publish ./

CMD [ "dotnet", "Project0.App.dll"]
