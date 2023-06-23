# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /VoiceAcademyAPIGateWay
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o bin

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /VoiceAcademyAPIGateWay
COPY --from=build-env /VoiceAcademyAPIGateWay/bin .
ENTRYPOINT [ "dotnet", "VoiceAcademyAPIGateWay.dll" ]
