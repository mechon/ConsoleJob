namespace ConsoleJob.Job.Infrastructure;

internal static class Extensions
{
  public static async Task<TOut> Then<TIn, TCancellationToken, TOut>(this Task<TIn> inputTask,
      TCancellationToken cancellationToken, Func<TIn, TCancellationToken, Task<TOut>> mapping)
  {
    var input = await inputTask;
    return await mapping(input, cancellationToken);
  }

  public static async Task Then<TIn, TCancellationToken>(this Task<TIn> inputTask,
      TCancellationToken cancellationToken, Func<TIn, TCancellationToken, Task> mapping)
  {
    var input = await inputTask;
    await mapping(input, cancellationToken);
  }
}
