﻿global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.Reflection;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Nodes;
global using ConsoleJob.Job.Application.Commands;
global using ConsoleJob.Job.Application.Queries;
global using ConsoleJob.Job.Application.Notifications;
global using ConsoleJob.Job.Domain;
global using ConsoleJob.Job.Infrastructure.Constants;
global using ConsoleJob.Job.Infrastructure.Exceptions;
global using ConsoleJob.Job.Infrastructure.Extensions;
global using ConsoleJob.Job.Infrastructure.Interfaces;
global using ConsoleJob.Job.Infrastructure.Services;
global using ConsoleJob.Job.Infrastructure.StartupSetup;
global using MediatR;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Serilog;
global using Serilog.Events;
global using Serilog.Sinks.MSSqlServer;
