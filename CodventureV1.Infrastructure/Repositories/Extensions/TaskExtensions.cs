namespace CodventureV1.Infrastructure.Repositories.Extensions;

public static class TaskExtensions
{
    public static Task<TResult> Then<TSource, TResult>(this Task<TSource> sourceTask, Func<TSource, TResult> selector, CancellationToken cancellationToken = default(CancellationToken))
    {
        TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
        sourceTask.ContinueWith(delegate (Task<TSource> task)
        {
            if (task.IsFaulted)
            {
                taskCompletionSource.TrySetException(task.Exception!.InnerExceptions);
            }
            else if (task.IsCanceled)
            {
                taskCompletionSource.TrySetCanceled();
            }
            else
            {
                try
                {
                    TResult result = selector(task.Result);
                    taskCompletionSource.TrySetResult(result);
                }
                catch (Exception exception)
                {
                    taskCompletionSource.TrySetException(exception);
                }
            }
        }, cancellationToken);
        return taskCompletionSource.Task;
    }
}