#!/bin/bash
# docker build -t YouTubeDownloader-frontend . --progress plain
# docker compose up

# Build Backend
echo 'Building Backend'
cd Backend || exit
docker build -t youtubedownloader-backend . --progress plain || exit
cd ..

# Build Frontend
echo 'Building Frontend'
cd Frontend || exit
docker build -t youtubedownloader-frontend . --progress plain || exit
cd ..

# Start Compose
echo 'Starting...'
docker compose up -d
#docker compose -f deploy.compose.yml up -d