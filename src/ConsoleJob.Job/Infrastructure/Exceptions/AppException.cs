﻿namespace ConsoleJob.Job.Infrastructure.Exceptions;

public class AppException : ApplicationException
{
  public AppException(string? message) : base(message) { }
}
