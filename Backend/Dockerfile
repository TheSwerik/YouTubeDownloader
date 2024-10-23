# syntax=docker/dockerfile:1.4.3

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../../. .
RUN dotnet publish -c Release -o /build/publish /p:UseAppHost=false Backend/Backend.csproj

FROM base AS final
USER $APP_UID
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache curl \
 && apk add --no-cache ffmpeg \
 && apk add --no-cache icu-libs krb5-libs libgcc libintl libssl3 libstdc++ zlib \
 && apk add --update --no-cache python3 && ln -sf python3 /usr/bin/python \
 && curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp \
 && chmod a+rx /usr/local/bin/yt-dlp
COPY --from=build /build/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
