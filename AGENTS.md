# Agent Instructions - YouTube Downloader

## Project Overview

- **Architecture**: Blazor WebAssembly Frontend, ASP.NET Core Backend, and a Shared library for common types/exceptions.
- **Core Functionality**: Downloads videos from YouTube using `yt-dlp` and `ffmpeg`.

## Key Entrypoints

- **Backend API**: `Backend/src/Program.cs` (ASP.NET Core Web API).
- **Frontend UI**: `Frontend/Pages/Index.razor` (Blazor WASM Page).
- **Shared Logic**: `Shared/` contains models and exceptions shared between client and server.

## Build & Execution

- **Restore Dependencies**: `dotnet restore`
- **Build Solution**: `dotnet build`
- **Run Tests**: `dotnet test -c Release` (Runs tests in `Backend.Test`)
- **Docker**: Use `docker-compose up --build` to start the full stack.

## Critical Environment & Dependencies

- **FFMPEG_PATH**: The backend requires an environment variable `FFMPEG_PATH` pointing to a valid `ffmpeg` executable.
- **yt-dlp**: The backend relies on `yt-dlp`. Ensure it is accessible in the execution path or bundled correctly
  (currently found in `Backend/bin`).

## Development Quirks

- **Shared Library**: Any changes to models in `Shared/` must be verified for breaking changes in both `Frontend` and
  `Backend`.
- **Caching**: Use `./Frontend/scripts/clean-files-for-caching.sh` when encountering stale assets or build artifacts in
  the frontend.
- **Solution Format**: The project is transitioning from `.sln` to `.slnx`.

## Verification Steps

- After backend changes: Verify API endpoints via Swagger (typically `http://localhost:<port>/swagger`).
- After frontend changes: Check browser console for WebAssembly errors or JS interop issues.
