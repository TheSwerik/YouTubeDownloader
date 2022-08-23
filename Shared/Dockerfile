# syntax=docker/dockerfile:1.4.3

FROM alpine:3.12.0 as file-loader
WORKDIR /data
COPY --link scripts/clean-files-for-caching.sh /scripts/
RUN chmod -R 555 /scripts

#########################################################################

FROM file-loader as base-files
COPY --link --from=registry.swerik.dev/youtubedownloader/shared /app/publish ./packages
COPY --link Shared.csproj .
RUN /scripts/clean-files-for-caching.sh .

#########################################################################

FROM file-loader as source-files
COPY --link . .
RUN /scripts/clean-files-for-caching.sh .

#########################################################################
#########################################################################

FROM mcr.microsoft.com/dotnet/sdk:6.0 as dependencyfetcher
WORKDIR /build
COPY --link --from=base-files /data .
RUN dotnet restore

#########################################################################

FROM dependencyfetcher as tester
WORKDIR /build
COPY --link --from=source-files /data .
RUN dotnet test

#########################################################################

FROM dependencyfetcher as final
COPY --link --from=source-files /data .
RUN dotnet publish -c Release -o /build/publish