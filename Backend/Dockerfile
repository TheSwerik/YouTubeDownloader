FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
RUN apk add ffmpeg
#RUN apk add --no-cache ffmpeg
RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Backend.csproj .
RUN dotnet restore Backend.csproj
COPY . .
RUN #dotnet build Backend.csproj -c Release -o /app/build
RUN dotnet build Backend.csproj -c Debug -o /app/build

FROM build AS publish
RUN #dotnet publish Backend.csproj -c Release -o /app/publish
RUN dotnet publish Backend.csproj -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
