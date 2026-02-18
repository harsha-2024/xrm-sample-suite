# XRM Sample Suite (C# / .NET)

A robust, end‑to‑end sample demonstrating **Microsoft Dataverse (Dynamics 365 / XRM) development with C# and .NET**:

- **Plugins** (IPlugin) for server-side business logic (net462)
- **Custom Workflow Activity** (CodeActivity) example (net462)
- **Reusable Common library** (ServiceClient factory, helpers)
- **Console client** using **Microsoft.PowerPlatform.Dataverse.Client** for CRUD, FetchXML, and batch operations
- **Unit tests** using **FakeXrmEasy** for plugin logic

> **Targets & Tooling**
> - Plugin and Workflow projects target **.NET Framework 4.6.2** (Dataverse requirement) and SDK-style projects. 
> - Client / test can use newer .NET.
> - Use **Power Platform CLI** (PAC) or **Plugin Registration Tool** to register the plugin.
>
> **Docs:** Microsoft recommends plug‑ins/custom workflow projects target **.NET Framework 4.6.2** and be built as SDK‑style projects.  Also note guidance around batch requests inside plug‑ins (avoid `ExecuteMultipleRequest`).

## Projects

- `XrmSample.Common` – shared helpers (ServiceClient factory, tracing helpers)
- `XrmSample.Plugins` – two sample plugins + one code activity
  - `AccountCreateValidationPlugin` (Pre‑Operation) 
  - `FollowUpTaskPostCreatePlugin` (Post‑Operation)
  - `SetAccountCategoryWorkflow` (CodeActivity)
- `XrmSample.ConsoleClient` – console app showing Dataverse connection and operations
- `XrmSample.Tests` – unit tests using FakeXrmEasy

## Quick Start

1. **Prerequisites**
   - Visual Studio 2022 (for net462 & SDK-style) or VS Code + PAC CLI
   - .NET Framework 4.6.2 Developer Pack
   - Power Platform CLI (PAC) or Plugin Registration Tool
   - NuGet restore
2. **Restore & Build**
   ```bash
   # From the repository root
   dotnet restore
   dotnet build
   ```
3. **Run Console Client**
   - Set environment variable `DATAVERSE_CONNECTION` with your connection string
   - Or edit `src/XrmSample.ConsoleClient/appsettings.json`
   ```bash
   dotnet run --project src/XrmSample.ConsoleClient
   ```
4. **Register Plugins**
   - Use PAC: `pac plugin init` for new projects, or use **Plugin Registration Tool** to register `XrmSample.Plugins.dll`

## Security & Guidance
- Prefer **Service Principal** / **Managed Identity** over user creds.
- Avoid `ExecuteMultipleRequest` **inside plug‑ins**; it’s intended for client‑to‑server latency reduction and can cause timeouts/rollbacks in plug‑ins. Use single requests or offload to async processing.

## License
MIT – for educational/reference use.
