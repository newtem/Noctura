# Noctura 1.4

## Overview
A custom scripting language and interpreter with an extensible runtime  
primarily developed in C# WinForms and supporting extensions via NLua

## Goals
- Design and implement a custom scripting language
- Support for multiple libraries and GUI extension with NLua

## Status
Active development

## Noctura updater
Noctura includes a lightweight automatic update system to simplify distribution
and ensure users always have access to the latest version.

### Features
- Automatic update checking and application
- Separate updater executable to safely replace the main application
- Text-based progress display during download
- Updates hosted via GitHub Releases

### Update Flow
1. The main application checks for a newer version
2. If an update is available, the updater is launched
3. The updater downloads the latest executable from GitHub
4. The running application is safely closed
5. The executable is replaced and relaunched
