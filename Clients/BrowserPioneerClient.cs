using BrowserPioneer.Client.Configurations;
using HappreeTool.Utils.HttpUtils;
using Microsoft.Extensions.Options;

namespace BrowserPioneer.Client.Clients;

public class BrowserPioneerClient(IOptions<BrowserPioneerSettings> options,
                                  HttpWrapper httpWrapper)
{
    private readonly string _remoteDownloadServer = $"{options.Value.BaseUrl}/image/download";
    private readonly string _webpageContentServer = $"{options.Value.BaseUrl}/webpage/content";

    public async Task<byte[]> DownloadImageFromWebsitePageAsync(string imageXPath,
                                                                string? pageUrl,
                                                                byte downCount = 8)
    {
        RemoteImageDownloadRequest command = new RemoteImageDownloadRequest
        {
            PageUrl = pageUrl,
            ImageXPath = imageXPath,
            DownCount = downCount,
        };

        HttpResponseMessage response = await httpWrapper.PostJsonAsync(_remoteDownloadServer, command);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }

    internal class RemoteImageDownloadRequest
    {
        public string ImageXPath { get; set; } = default!;

        public string? PageUrl { get; set; }

        public byte DownCount { get; set; }
    }

    public async Task<string> GetWebpageContentAsync(string pageUrl)
    {
        RemoteWebpageContentRequest request = new RemoteWebpageContentRequest
        {
            PageUrl = pageUrl
        };

        HttpResponseMessage response = await httpWrapper.PostJsonAsync(_webpageContentServer, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    internal class RemoteWebpageContentRequest
    {
        public string PageUrl { get; set; } = default!;
    }
}