#!/bin/bash
# docker build -t YouTubeDownloader-frontend . --progress plain
# docker compose up

# Build Backend
cd Backend || exit
docker build -t youtubedownloader-backend . --progress plain
cd ..

# Build Frontend
cd Frontend || exit
docker build -t youtubedownloader-frontend . --progress plain
cd ..

# Start Compose
docker compose -f deploy.compose.yml up -d