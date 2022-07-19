using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using LiteDB;
using RadYanFoFaViewer.Models;
using RadYanFoFaViewer.Views;
using RestSharp;

namespace RadYanFoFaViewer.Utils;

public static class Update
{
    public const string Version = "V1.1.0";

    private const string UpdateUrl = "https://raw.fastgit.org/MiracleLau/RadYanFoFaViewer/master/update.json";
    // private const string UpdateUrl = "http://127.0.0.1:8000/update.json";

    public static void AutoCheckUpdate(Action successAction, Action errorAction, bool isStartup = false)
    {
        new Task(() =>
        {
            try
            {
                if (isStartup)
                {
                    var updateSetting = Config.GetOrDefaultConfig("UpdateSetting", new BsonDocument
                    {
                        ["AutoCheckUpdate"] = true
                    });
                    if (!updateSetting["AutoCheckUpdate"].AsBoolean) return;
                }

                var options = new RestClientOptions(UpdateUrl)
                {
                    ThrowOnAnyError = true,
                    MaxTimeout = 2000
                };
                var client = new RestClient(options);
                var updateInfo = client.Get<UpdateInfo>(new RestRequest());
                if (updateInfo is null)
                {
                    Dispatcher.UIThread.Post(() =>
                    {
                        MessageBox.NormalMsgBox(msg: "无法获取更新").Show();
                        errorAction();
                    });
                }
                else
                {
                    if (updateInfo.Version != Version)
                    {
                        Dispatcher.UIThread.Post(() =>
                        {
                            var updateWindow = new UpdateWindow();
                            updateWindow.SetUpdateInfo(updateInfo);
                            updateWindow.Topmost = true;
                            updateWindow.Show();
                            successAction();
                        });
                    }
                    else
                    {
                        if (!isStartup)
                        {
                            Dispatcher.UIThread.Post(() =>
                            {
                                MessageBox.NormalMsgBox(msg: "当新版本已是最新版").Show();
                                successAction();
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    MessageBox.NormalMsgBox(msg: $"无法获取更新：{ex.Message}").Show();
                    errorAction();
                });
            }
        }).Start();
    }
}