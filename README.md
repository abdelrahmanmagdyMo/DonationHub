DonationHub Web APi Project in .Net 10

## Overview :

DonationHub is a donation platform API built with ASP.NET Core (.NET 10). It lets
administrators organize charitable work into **categories** (such as Health, Orphans,
Education, Environment, and Relief) and **campaigns** under each category, so donors
can find and support the causes they care about.

Key features:

- **Bilingual content:** names and descriptions are stored in Arabic and English.
- **Secure access:** authentication with ASP.NET Core Identity and JWT (with refresh
  tokens), and role-based authorization so only Admins can manage categories.
- **Safe deletion:** records are soft-deleted, so history is preserved and related data
  is never broken.
- **Clean architecture:** separate Domain, Application, Infrastructure, and API layers.

## Tech stack:

.NET 10, ASP.NET Core Web API, Entity Framework Core, SQL Server, ASP.NET
Core Identity, Scalar

dotnet user-secrets init --project .\DonationHub.Api\DonationHub.Api.csproj

dotnet user-secrets set "Jwt:SecretKey" "<long-random-key>" --project .\DonationHub.Api\DonationHub.Api.csproj

dotnet user-secrets set "SeedAdmin:Password" "<admin-password>" --project .\DonationHub.Api\DonationHub.Api.csproj

dotnet user-secrets set "SeedUsers:CampaignManager:Email" "manager@example.com" --project .\DonationHub.Api\DonationHub.Api.csproj
dotnet user-secrets set "SeedUsers:CampaignManager:FullName" "Test Manager" --project .\DonationHub.Api\DonationHub.Api.csproj

dotnet user-secrets set "SeedUsers:Donor:Email" "donor@example.com" --project .\DonationHub.Api\DonationHub.Api.csproj
dotnet user-secrets set "SeedUsers:Donor:FullName" "Test Donor" --project .\DonationHub.Api\DonationHub.Api.csproj

## Prerequisites

.NET 10 SDK
SQL Server (LocalDB, Express, Developer, or Docker)
EF Core CLI tool:
bash
dotnet tool install --global dotnet-ef

Create the database
bash
dotnet ef database update --project DonationHub.Infrastructure --startup-project DonationHub.API

The categories, the Admin, the Campaign Manager and the Donor are seeded when the application starts in the Development environment.
Run
bash
dotnet run --project DonationHub.API

The API listens on the URLs printed in the console (see DonationHub.API/Properties/launchSettings.json). Scalar and the OpenAPI document are only enabled in the Development environment.

Try it (Scalar)
Start the API and open https://localhost:<port>/scalar.
Call POST /Auth/login (TODO: confirm the full route, it may include api/v1) with the Admin email and the password you set in user-secrets.
Copy the access token from the response.
Click Authorize (Bearer) and paste the token.
Call a protected endpoint, for example DELETE /Category/{id}.

Expected results:

Situation Result
No token on a protected endpoint 401 Unauthorized
Valid token of a non-Admin user (Donor or Campaign Manager) on an Admin endpoint 403 Forbidden
Admin token, category exists 200 OK
Admin token, unknown id 404 Not Found
Endpoints
Method Route Access
POST /Auth/Register Public
POST /Auth/login Public
POST /Auth/refresh Public (needs a refresh token)
POST /Auth/logout Authenticated
POST /Auth/me Authenticated
POST /Auth/admin/staff Admin (creates Admin or Campaign Manager)
GET /Category/categories Public
GET /Category/{id} Public
POST /Category Admin
PUT /Category/{id} Admin
DELETE /Category/{id} Admin (soft delete)

the base route defined in BaseApiController ([Route("api/v{version:apiVersion}/[controller]")])

Roles
Role Purpose
Admin Manages categories and creates staff accounts
CampaignManager Staff account for managing campaigns
Donor Regular user who donates
Project structure
DonationHub/
├── DonationHub.Domain Entities, value objects (LocalizedText), domain rules. No dependency on other layers.
├── DonationHub.Application Contracts (IUnitOfWork, repository interfaces), DTOs, auth service interfaces.
├── DonationHub.Infrastructure EF Core DbContext, configurations, migrations, repositories, interceptors, Identity, seeding.
└── DonationHub.API Controllers, authentication/authorization setup, Program.cs, OpenAPI/Scalar.

Dependencies point inward: API → Infrastructure → Application → Domain.

Design decisions

Each decision lists what was chosen, why, and the alternative that was rejected.

1. Writes and reads use Generic Repository + Unit of Work;
2. Soft delete through a SaveChanges interceptor
   Decision: Entities implementing ISoftDeletable are never physically deleted. SoftDeleteInterceptor turns a delete into an update of IsDeleted and DeletedAtUtc.
   Why: Donation history must stay traceable, and deleting a category must not break campaigns that reference it. The interceptor keeps this rule in one place, so repositories and controllers stay simple.
   Alternative considered: Hard delete (loses history, breaks foreign keys), or soft-delete logic in every repository (duplicated and easy to forget).
3. Global query filter for deleted rows
   Decision: HasQueryFilter(x => !x.IsDeleted) on soft-deletable entities.
   Why: Every query hides deleted rows automatically, so nobody has to remember the condition.
   Alternative considered: Filtering manually in each query. It is error-prone, and one missed filter would expose deleted data.
4. Localized text as a value object
   Decision: Name and Description use a LocalizedText value object (Arabic and English), stored in the same table as Name_Arabic, Name_English, and so on.
   Why: It groups two related values, validates them together, keeps the entity readable, and avoids joins.
   Alternative considered: Separate string properties on every entity (repetition, easy to forget a language), or a separate translations table (more flexible for many languages, but more joins and complexity than the project needs).
5. ASP.NET Identity with JWT, refresh tokens and role-based authorization
   Decision: Identity manages users and roles, JWT access tokens protect endpoints, refresh tokens are stored hashed, and endpoints use [Authorize(Roles = "...")].
   Why: Identity provides tested password hashing and user management. Short-lived JWTs avoid server-side sessions, and storing only the token hash means a database leak does not expose usable refresh tokens. Three roles are enough for the current needs.
   Alternative considered: A custom user table with hand-written hashing (security risk), cookie sessions (harder for separate clients), or policy-based authorization (more flexible, but unnecessary for three roles).
6. Secrets in user-secrets, not in appsettings.json
   Decision: The JWT key, connection string and seeded credentials live in dotnet user-secrets for development.
   Why: Anything in appsettings.json ends up in git history. User-secrets keeps real values on the developer's machine only.
   Alternative considered: Committing values to appsettings.json (leaks credentials), or environment variables only (fine for production, less convenient locally).
   What is included in Sprint 1
   Layered solution: Domain, Application, Infrastructure, API
   SQL Server database with EF Core migrations
   Category entity with bilingual name and description, and full CRUD endpoints
   Soft delete (interceptor and global query filter)
   Generic Repository and Unit of Work for writes
   ASP.NET Identity with JWT, refresh tokens, login, logout, refresh, register, and me
   Role-based authorization (Admin, CampaignManager, Donor), and an Admin-only endpoint to create staff
   Seeded categories, Admin, Campaign Manager and Donor
   API versioning and Scalar API documentation with Bearer authentication
