FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache ffmpeg
RUN apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib

FROM youtubedownloader-shared AS shared

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY --from=shared /app/publish ./packages
COPY Backend.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
