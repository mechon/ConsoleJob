namespace ConsoleJob.Job.Infrastructure.Extensions;

public static class TaskExtensions
{
  public static async Task<TOut> Then<TIn, TCancellationToken, TOut>(this Task<TIn> inputTask,
      Func<TIn, TCancellationToken, Task<TOut>> mapping, TCancellationToken cancellationToken)
  {
    var input = await inputTask;
    return await mapping(input, cancellationToken);
  }

  public static async Task Then<TIn, TCancellationToken>(this Task<TIn> inputTask,
      Func<TIn, TCancellationToken, Task> mapping, TCancellationToken cancellationToken)
  {
    var input = await inputTask;
    await mapping(input, cancellationToken);
  }
}
