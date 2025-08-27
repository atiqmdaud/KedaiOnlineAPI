using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using KedaiOnline.Domain.Interfaces;
using KedaiOnline.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KedaiOnline.Infrastructure.Storage;

public class BlobStorageService(IOptions<BlobStorageSettings> options) : IBlobStorageService
{
    private readonly BlobStorageSettings _settings = options.Value;
    public async Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        var blobServiceClient =  new BlobServiceClient(_settings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_settings.LogosContainerName);

        var blobClient =  containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(data);

        var blobUrl = blobClient.Uri.ToString();

        return blobUrl;
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl == null) return null;

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _settings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow,
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl)

        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_settings.ConnectionString);

        var sasToken = sasBuilder
            .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _settings.AccountKey))
            .ToString();

        return $"{blobUrl}?{sasToken}";

    // https://kedaionlinesadev.blob.core.windows.net/ logos/ kedaionline-logo.jpg

    }
        

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }
}
