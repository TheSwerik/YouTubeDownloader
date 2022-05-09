docker build -t frontend . --progress plain
docker run -d -p 8080:80 frontend
