FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Frontend.csproj .
COPY --from=youtubedownloader-shared /app/publish /app/build/wwwroot/_framework
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build/

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish/

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf