# syntax=docker/dockerfile:1.4.3

FROM mcr.microsoft.com/dotnet/sdk:8.0 as builder
WORKDIR /build
COPY ../../. .
RUN dotnet publish -c Release -o /build/publish Backend/Backend.csproj

#########################################################################

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
EXPOSE 80
RUN apk add --no-cache curl
RUN apk add --no-cache ffmpeg
RUN apk add --no-cache icu-libs
RUN apk add --no-cache krb5-libs
RUN apk add --no-cache libgcc
RUN apk add --no-cache libintl
RUN apk add --no-cache libssl1.1
RUN apk add --no-cache libstdc++
RUN apk add --no-cache zlib
RUN apk add --update --no-cache python3 && ln -sf python3 /usr/bin/python
RUN curl -L https://github.com/yt-dlp/yt-dlp/releases/latest/download/yt-dlp -o /usr/local/bin/yt-dlp
RUN chmod a+rx /usr/local/bin/yt-dlp
COPY --from=builder /build/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]