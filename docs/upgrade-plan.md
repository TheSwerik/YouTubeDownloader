# Upgrade and Refactor Plan - YouTube Downloader

## Overview

This plan outlines the steps to upgrade the YouTube Downloader application to .NET 10, transition to the new `.slnx`
solution format, address security vulnerabilities, and ensure full functional parity.

## Goals

- [ ] Update all projects to Target Framework `.net10.0`.
- [ ] Convert `YouTubeDownloader.sln` to `YouTubeDownloader.slnx`.
- [ ] Identify and remediate security vulnerabilities in dependencies and code.
- [ ] Verify all existing features work correctly after the upgrade.

## Milestones

### Phase 1: Environment & Infrastructure Setup

- [ ] Update `global.json` to target .NET 10 SDK.
- [ ] Update project files (`Backend`, `Frontend`, `Shared`, `Backend.Test`) to target `.net10.0`.
- [ ] Convert solution file to `.slnx`.

### Phase 2: Dependency & Security Audit

- [ ] Perform a security audit on all NuGet packages.
- [ ] Identify and fix known vulnerabilities (CVEs).
- [ ] Review code for common security issues (e.g., injection, insecure handling of URLs/tokens).

### Phase 3: Refactoring & Updates

- [ ] Update any deprecated APIs to their .NET 10 equivalents.
- [ ] Refactor code for performance improvements or cleaner architecture as identified during the audit.
- [ ] Clean up `bin` and `obj` directories across all projects.

### Phase 4: Verification & QA

- [ ] Run all existing unit tests in `Backend.Test`.
- [ ] Perform manual testing of core features (Download, UI interactions).
- [ ] Verify Docker deployment if applicable.

## Schedule

| Task                  | Priority | Status  |
|:----------------------|:---------|:--------|
| SDK & Project Updates | High     | Pending |
| Solution Conversion   | Medium   | Pending |
| Security Audit        | High     | Pending |
| Verification          | High     | Pending |
