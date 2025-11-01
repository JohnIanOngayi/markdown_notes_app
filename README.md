# markdown_notes_app

## 🚀 Project Overview

A RESTful API for markdown note-taking built with ASP.NET Core, implementing **Clean Architecture** and testing my hand at **third-party API integrations**. This project started from an **empty ASP.NET solution** to deeply understand the framework's internals.

## 🏗️ Architecture

### Key Architectural Decisions
- **Clean Separation**: Each layer has distinct responsibilities
- **Testability**: Dependency injection throughout
- **Maintainability**: Interface-based design
- **Scalability**: Ready for distributed caching & microservices

## 🛠️ Technical Stack

- **Framework**: ASP.NET Core 8
- **Database**: MySQL with Entity Framework Core
- **ORM**: Entity Framework Core + Pomelo MySQL Provider
- **Markdown Processing**: Markdig
- **Grammar Checking**: Sapling.ai API Integration
- **Caching**: In-Memory Caching (Later Redis-ready)
- **Logging**: NLog with project-specific configurations
- **Architecture**: Onion/Clean Architecture

## 📋 Features

### Core Functionality
- ✅ **CRUD Operations** for markdown notes
- ✅ **Markdown to HTML Rendering** using Markdig
- ✅ **Grammar & Spell Checking** via Sapling.ai API
- ✅ **Soft Delete Implementation** with query filters
- ✅ **RESTful API Design** with proper HTTP semantics

### Advanced Features
- 🕒 **Audit Trail**: CreatedAt, UpdatedAt auto-tracking
- ♻️ **Soft Deletes**: Interceptor-based delete handling
- 🔍 **Query Filtering**: Automatic IsDeleted filtering
- 💾 **Caching Strategy**: Content-based cache invalidation
- 📊 **Health Checks**: Dependency monitoring

## High-level Project Structure
```
src/
├── MarkdownNoteTakingApp.API/          # Presentation Layer
│   ├── Controllers/                     # Thin HTTP handlers
│   ├── Middleware/                      # Cross-cutting concerns
│   └── Extensions/                      # Service configuration
├── MarkdownNoteTakingApp.Core/          # Domain Layer
│   ├── Entities/                        # Business models
│   ├── Interfaces/                      # Contracts & abstractions
│   └── DTOs/                           # Data transfer objects
├── MarkdownNoteTakingApp.Application/   # Business Logic
│   ├── Services/                        # Use case implementations
│   └── Validators/                      # Input validation
└── MarkdownNoteTakingApp.Infrastructure/# Infrastructure
    ├── Data/                           # EF Context & Migrations
    ├── Repositories/                   # Data access
    └── ExternalServices/               # Third-party API clients

```
