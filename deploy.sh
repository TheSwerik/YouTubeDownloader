#!/bin/bash
# docker build -t YouTubeDownloader-frontend . --progress plain
# docker compose up

# Build Backend
cd Backend || exit
docker build -t youtubedownloader-backend . --progress plain || exit
cd ..

# Build Frontend
cd Frontend || exit
docker build -t youtubedownloader-frontend . --progress plain || exit
cd ..

# Start Compose
docker compose up -d
#docker compose -f deploy.compose.yml up -d