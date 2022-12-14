#### Start: Template Description

## Using the dotnet CLI template

1. Install template from [NuGet (https://www.nuget.org/packages/MEchon.ConsoleJob.Template)](https://www.nuget.org/packages/MEchon.ConsoleJob.Template):

```powershell
dotnet new -i MEchon.ConsoleJob.Template
```

2. You should see "MEchon Console Job Template" with shortname "console-job" in the list of templates from `dotnet new list` after this installation successfully.

3. Go to your development directory

4. Run this command to create the solution structure in a subfolder name `My.ProjectName`:

```
dotnet new console-job -o My.ProjectName
```

The `My.ProjectName` directory and solution file will be created, and inside that will be all of your new solution contents, properly namespaced and ready to run/test!

#### End: Template Description
---


| Code Analyzer | Security Scan | Build | Pipeline
| ----- | ----- | ----- | ----- |
| <insert badge link> | <insert badge link> | <insert badge link> | <insert badge link> |

# Introduction
Describe the purpose of the job

### Data mapping
Show mapping in table form.

*Example:*

| Data | Mapped Field | Source |
| ----------- | ----------- | ----------- |
| Employee Name   | EmployeeName | SAP / Dump File / Etc |
| Position   | Position | SAP / Dump File / Etc |

### Integrations
List all services and external resources used.

*Example:*

| Service | Staging | Production |
| ----------- | ----------- | ----------- |
| Database | `staging_db` | `production_db` |
| Microservice | `https://staging/service` | `https://production/service` |
| Dump File | `ftp:server\directory\file.csv` | `ftp:server\directory\file.csv` |

# Development Setup

#### Install the following:

1. [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. [VS Code](https://code.visualstudio.com/download) or [Visual Studio](https://visualstudio.microsoft.com/downloads/)
3. Other requirements in order to build and run this project

#### Setup configurations

Other configurations needed such as user-secrets, environment variables, certificates, file sources, etc.

# QA Testing
Describe what is need in order for QA to prepare for the test.

# Deployment
List in ordinal steps needed to deploy the app.

*Example:*
1. Run CI/CD
2. Schedule job on `Windows Task Scheduler`
* Ensure task runs under a service account, see table below.
* `Run whether user is logged on or not` is selected.
* `Do not store password` is **NOT** checked.

| Stage | Server | Service Account| Directory |
| ----------- | ----------- | ----------- | ----------- |
| Development | - | - | - |
| Staging | `staging_server` | `service_account` | `deployment directory` |
| Production | `production_server` | `service_account` | `deployment directory` |

# Support
Detail how to troubleshoot the application. Show where the file logs are located and the queries for database logs.

*Example:*

Executions generate report and are sent to email addresses configured on `appsettings.json`.

If no email received, you can check db logs:

| Stage | Server | Database |
| ----------- | ----------- | ----------- |
| Development | - | - |
| Staging | `staging_db_server` | `db_name`|
| Production | `production_db_server` | `db_name` |

Run SQL snippet:
``` sql
SELECT * FROM [db_name].[dbo].[Events] WHERE Application = '<Application Name>' ORDER BY TimeStamp DESC
```

You can also check file logs:

| Stage | Server | Directory |
| ----------- | ----------- | ----------- |
| Development | - | - |
| Staging | `staging_server` | `deployment directory\Logs\` |
| Production | `production_server` | `deployment directory\Logs\` |

Serilog's `SelfLog` is also enabled. You can view it on the same directory as above with file name `self.log`.
