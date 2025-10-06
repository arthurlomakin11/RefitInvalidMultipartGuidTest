using Refit;

namespace WebApplication1.Interfaces;

public interface IUploadApi
{
    [Multipart]
    [Post("/api/upload/upload")]
    Task<ApiResponse<object>> UploadAsync([AliasAs("id")] Guid id, [AliasAs("file")] StreamPart file);
}