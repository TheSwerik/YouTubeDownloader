FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Frontend.csproj .
COPY --from=registry.swerik.dev/youtubedownloader/shared /app/publish /app/build/wwwroot/_framework
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build/

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish/

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf



# syntax=docker/dockerfile:1.4.3

FROM alpine:3.12.0 as file-loader
WORKDIR /data
COPY scripts/clean-files-for-caching.sh /scripts/
RUN chmod -R 555 /scripts

#########################################################################

FROM file-loader as base-files
COPY --from=registry.swerik.dev/youtubedownloader/shared /build/publish /app/build/wwwroot/_framework
RUN ls -lisa /build/publish /app/build/wwwroot/_framework
COPY Frontend.csproj .
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
RUN dotnet publish -c Release -o /build/publish

#########################################################################
#########################################################################

FROM nginx:alpine AS final
EXPOSE 80
EXPOSE 443
WORKDIR /usr/share/nginx/html
COPY --from=builder /build/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf