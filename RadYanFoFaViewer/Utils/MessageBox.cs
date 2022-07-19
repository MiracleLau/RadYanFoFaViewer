using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Models;

namespace RadYanFoFaViewer.Utils;

public static class MessageBox
{
    /// <summary>
    ///     显示一个普通的提示框
    /// </summary>
    /// <param name="title">提示框标题</param>
    /// <param name="msg">提示框内容</param>
    /// <returns></returns>
    public static IMsBoxWindow<string> NormalMsgBox(string title = "提示", string msg = "")
    {
        var msgBox = MessageBoxManager.GetMessageBoxCustomWindow(new MessageBoxCustomParams
        {
            ContentTitle = title,
            ContentMessage = msg,
            FontFamily = "Microsoft YaHei,Simsun,苹方-简,宋体-简",
            ButtonDefinitions = new[]
            {
                new ButtonDefinition
                    {Name = "确认", IsDefault = true}
            },
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        });
        return msgBox;
    }
}