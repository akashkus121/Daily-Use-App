## Daily-Use App

A lightweight dashboard that unifies daily essentials:

- Weather 🌤
- Mood tracker with simple AI-like suggestions and quotes 🙂
- Daily expenses 💰
- Quick notes 📝
- Utility status (power/water) ⚡🚰

### Getting Started

Prereqs: Bash shell. If you don't have .NET 8 installed, scripts will install a local SDK.

1) Setup once:
```
bash scripts/dev-setup.sh
```

2) Run:
```
bash scripts/run.sh
```

App will auto-create a default user on first load and land on Dashboard.

### Configuration

The project currently uses EF Core with SQL Server provider in `Program.cs`.
Provide a connection string via environment or update `Program.cs` to read from `appsettings.json`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Set a connection string in `appsettings.Development.json` (optional) or export env:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DailyUse;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Alternatively, switch to SQLite quickly:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
```

And set:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=dailyuse.db"
  }
}
```

### GitHub Push

Initialize and push (replace placeholders):

```
git init
git add .
git commit -m "feat: dashboard + suggestions + starter scripts"
git branch -M main
git remote add origin https://github.com/<your-username>/<your-repo>.git
git push -u origin main
```

If you prefer SSH:

```
git remote add origin git@github.com:<your-username>/<your-repo>.git
git push -u origin main
```

### Notes

- Default route points to `Dashboard/Index`.
- Simple suggestion service provides micro-actions and a motivational quote.
- Replace `DummyWeatherService` with a real one when ready.

