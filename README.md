# FastMeiliSync

FastMeiliSync is a powerful synchronization server built using ASP.NET Core, designed to sync multiple databases with multiple instances of the search engine [MeiliSearch](https://www.meilisearch.com/). It currently supports **PostgreSQL**, with future support planned for **MySQL** and **MongoDB**.

## Features
- **Multiple Database Sync:** Automatically syncs multiple databases with multiple MeiliSearch instances.
- **Real-time updates:** Ensures that all changes in your databases are reflected in the corresponding MeiliSearch instances in real-time.
- **Future Support:** Upcoming support for MySQL and MongoDB.
- **Easy Setup:** Simple configuration to get started.

## Supported Databases
- **PostgreSQL** (Current)
- **MySQL** (Upcoming)
- **MongoDB** (Upcoming)

## Getting Started

### Prerequisites
- [.NET 6+ SDK](https://dotnet.microsoft.com/download/dotnet)
- [MeiliSearch](https://www.meilisearch.com/) instances
- Supported databases (e.g., PostgreSQL)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/ahmed1672003/FastMeiliSync.git
   cd FastMeiliSync
