using Models;

namespace Persistence
{
    public static class DatasetOperations
    {
        public static IResult UpsertData<T>(T[] data, Action<T[]> upsertFunc, string successMessage)
        {
            try
            {
                upsertFunc(data);
                return TypedResults.Ok(new DatabaseInteractionResult
                {
                    Success = true,
                    Message = successMessage,
                    RowsAffected = data.Length
                });
            }
            catch (Exception ex)
            {
                return TypedResults.Json(new DatabaseInteractionResult
                {
                    Success = false,
                    Message = ex.Message,
                    RowsAffected = 0
                }, statusCode: 500);
            }
        }
    }
}