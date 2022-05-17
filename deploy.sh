﻿#!/bin/bash
# docker build -t YouTubeDownloader-frontend . --progress plain
# docker compose up

# Build Shared
echo 'Building Shared Lib...'
cd Shared || exit
docker build -t youtubedownloader-shared . --progress plain || exit
cd ..

# Build Backend
echo 'Building Backend...'
cd Backend || exit
docker build -t youtubedownloader-backend . --progress plain || exit
cd ..

# Build Frontend
echo 'Building Frontend...'
cd Frontend || exit
docker build -t youtubedownloader-frontend . --progress plain || exit
cd ..

# Start Compose
echo 'Starting...'
docker compose up -d
#docker compose -f deploy.compose.yml up -d

echo 'Cleanup...'
docker image prune -af