# syntax=docker/dockerfile:1.4.3

FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder
WORKDIR /build
COPY ../../. .
RUN dotnet publish -c Release -o /build/publish Backend/Backend.csproj

#########################################################################

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache curl \
 && apk add --no-cache ffmpeg \
 && apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib \
 && apk add --update --no-cache python3 && ln -sf python3 /usr/bin/python \
 && curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp \
 && chmod a+rx /usr/local/bin/yt-dlp
COPY --from=builder /build/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]