# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /VoiceAcademyAPILogin
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o bin

# Stage 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /VoiceAcademyAPILogin
COPY --from=build-env /VoiceAcademyAPILogin/bin .
ENTRYPOINT [ "dotnet", "VoiceAcademyAPILogin.dll" ]
