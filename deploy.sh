﻿#!/bin/bash

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

# Cleanup
echo 'Cleanup...'
docker image prune -af
#docker system prune -af # System Prune deletes build cache as well, which is not wanted because this is a local build. If this gets pushed to a repository someday then use system instead of image.
