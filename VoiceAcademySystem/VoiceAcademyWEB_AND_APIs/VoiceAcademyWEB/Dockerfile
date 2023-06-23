FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /VoiceAcademyWEB
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o bin


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /VoiceAcademyWEB
COPY --from=build-env /VoiceAcademyWEB/bin .
ENTRYPOINT [ "dotnet", "VoiceAcademyWEB.dll" ]