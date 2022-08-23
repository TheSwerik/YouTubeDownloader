# syntax=docker/dockerfile:1.4.3

FROM alpine:3.12.0 as file-loader
WORKDIR /data
COPY scripts/clean-files-for-caching.sh /scripts/
RUN chmod -R 555 /scripts

#########################################################################

FROM file-loader as base-files
COPY --from=registry.swerik.dev/youtubedownloader/shared /app/publish ./packages
COPY Backend.csproj .
RUN /scripts/clean-files-for-caching.sh .

#########################################################################

FROM file-loader as source-files
COPY . .
RUN /scripts/clean-files-for-caching.sh .

#########################################################################
#########################################################################

FROM mcr.microsoft.com/dotnet/sdk:6.0 as dependencyfetcher
WORKDIR /build
COPY --from=base-files /data .
RUN dotnet restore

#########################################################################

FROM dependencyfetcher as tester
WORKDIR /build
COPY --from=source-files /data .
RUN dotnet test

#########################################################################

FROM dependencyfetcher as builder
COPY --from=source-files /data .
RUN dotnet publish -c Release -o ./publish

#########################################################################
#########################################################################

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache curl \
 && apk add --no-cache ffmpeg \
 && apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib \
 && apk add --update --no-cache python3 && ln -sf python3 /usr/bin/python \
 && curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp \
 && chmod a+rx /usr/local/bin/yt-dlp

#########################################################################

FROM base as final
WORKDIR /app
COPY --from=builder /build/publish .
USER 1001:1001
ENTRYPOINT ["dotnet", "Backend.dll"]