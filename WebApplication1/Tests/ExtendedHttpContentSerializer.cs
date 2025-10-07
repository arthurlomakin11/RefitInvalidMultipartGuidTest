using System.Globalization;
using System.Reflection;
using System.Text;
using Refit;

namespace WebApplication1.Tests;

/// <summary>
/// Temporary fix for Refit Multipart serialization issue:
/// Refit does not natively support Guid and DateOnly types in multipart/form-data.
/// This serializer converts them to plain text before sending.
/// See: https://github.com/reactiveui/refit/issues/2016
/// </summary>
public class ExtendedHttpContentSerializer(
    IHttpContentSerializer inner)
    : IHttpContentSerializer
{
    public HttpContent ToHttpContent<T>(T item)
    {
        if (item is Guid guid)
        {
            var guidContent = guid.ToString();

            var newContent = new StringContent(
                guidContent,
                Encoding.UTF8,
                "text/plain");

            return newContent;
        }

        if (item is DateOnly dateOnly)
        {
            var dateContent = dateOnly.ToString("O", CultureInfo.InvariantCulture);

            var newContent = new StringContent(
                dateContent,
                Encoding.UTF8,
                "text/plain");

            return newContent;
        }

        return inner.ToHttpContent(item);
    }

    public async Task<T?> FromHttpContentAsync<T>(
        HttpContent content,
        CancellationToken cancellationToken = default)
    {
        return await inner
            .FromHttpContentAsync<T>(content, cancellationToken);
    }

    public string? GetFieldNameForProperty(PropertyInfo propertyInfo)
    {
        return inner.GetFieldNameForProperty(propertyInfo);
    }
}