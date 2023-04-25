FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 as build-env

WORKDIR /src

COPY . .

RUN dotnet restore src/PassportStatusesChecker.sln
RUN dotnet publish src/PassportStatusesChecker.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/nightly/runtime:6.0

COPY --from=build-env /src/out .

ENTRYPOINT ["dotnet", "PassportStatusesChecker.dll"]