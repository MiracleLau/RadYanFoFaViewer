using RadYanFoFaViewer.Models;
using RestSharp;

namespace RadYanFoFaViewer.Utils;

public static class Update
{
    public const string Version = "V1.1.0";

    private const string UpdateUrl = "https://raw.fastgit.org/MiracleLau/RadYanFoFaDotNet/develop/update.json";
    // private const string UpdateUrl = "http://127.0.0.1:8000/update.json";
    public static UpdateInfo? AutoCheckUpdate(bool isStartup = false)
    {
        var options = new RestClientOptions(UpdateUrl) {
            ThrowOnAnyError = true,
            MaxTimeout = 2000
        };
        var client = new RestClient(options);
        var updateInfo = client.Get<UpdateInfo>(new RestRequest());
        if (updateInfo is null) return null;
        if (updateInfo.Version != Version) updateInfo.IsNewVersion = true;
        return updateInfo;
    }
}