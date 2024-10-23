# syntax=docker/dockerfile:1.4.3

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS builder
WORKDIR /build
COPY ../../. .
RUN dotnet publish -c Release -o /build/publish Frontend/Frontend.csproj

#########################################################################

FROM nginx:alpine AS final
EXPOSE 80
EXPOSE 443
WORKDIR /usr/share/nginx/html
COPY --from=builder /build/publish/wwwroot .
COPY Frontend/nginx.conf /etc/nginx/nginx.conf