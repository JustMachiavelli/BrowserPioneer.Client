using BrowserPioneer.Client.Configurations;
using HappreeTool.Utils.HttpUtils;
using Microsoft.Extensions.Options;

namespace BrowserPioneer.Client.Clients;

public class BrowserPioneerJavClient(IOptions<BrowserPioneerSettings> options,
                                     HttpWrapper httpWrapper)
{
    private readonly string _remoteDbCollectServer = $"{options.Value.BaseUrl}/website/javdb/collect";
    private readonly string _remoteAddDbListServer = $"{options.Value.BaseUrl}/website/javdb/list";

    public async Task AddToDbCollection(string url)
    {
        CollectJavDbQuery command = new CollectJavDbQuery
        {
            PageUrl = url,
        };

        HttpResponseMessage response = await httpWrapper.PostJsonAsync(_remoteDbCollectServer, command);
        response.EnsureSuccessStatusCode();
    }

    private class CollectJavDbQuery
    {
        public string PageUrl { get; set; } = default!;
    }

    public async Task AddList(string? url, string listId)
    {
        AddJavDbListQuery command = new AddJavDbListQuery
        {
            Url = url,
            ListId = listId
        };

        HttpResponseMessage response = await httpWrapper.PostJsonAsync(_remoteAddDbListServer, command);
        response.EnsureSuccessStatusCode();
    }

    public class AddJavDbListQuery
    {
        public string? Url { get; set; }
        public string ListId { get; set; } = default!;
    }
}