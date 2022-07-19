using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LiteDB;
using RadYanFoFaDotNet;
using RadYanFoFaViewer.Models;

namespace RadYanFoFaViewer.Utils;

public static class Search
{
    /// <summary>
    ///     查询数据
    /// </summary>
    /// <param name="searchString">要查询的语句</param>
    /// <param name="fields">要返回的数据列</param>
    /// <param name="page">返回第几页的数据</param>
    /// <param name="isNotFullData">是否只搜索最近一年的数据</param>
    /// <returns>格式化后搜索结果，如果未获取到数据，则返回null</returns>
    public static ApiResponseResult? DoSearch(string searchString, List<string> fields, int page = 1,
        bool isNotFullData = true)
    {
        var apiEmail = "";
        var apiKey = "";
        var pageSize = 20;
        if (page <= 1) page = 1;

        // 获取api设置
        var apiSetting = (BsonDocument?) Config.GetConfig("ApiSetting");

        if (apiSetting is not null)
        {
            apiEmail = apiSetting["ApiEmail"].AsString;
            apiKey = apiSetting["ApiKey"].AsString;
        }

        // 获取搜索设置
        var searchSetting = Config.GetConfig("SearchSetting");
        if (searchSetting is not null) pageSize = searchSetting["PerPageSize"].AsInt32;

        // 实例化客户端
        var client = new FoFaClient(apiEmail, apiKey);
        client.SetGetFields(fields);
        var result = client.Search(searchString, page, pageSize, !isNotFullData);
        if (result is not {Error: false}) throw new Exception("未能获取数据，可能是Api Key信息未正确设置或者搜索语句不正确。");
        if (result.Results == null) return null;
        var totalPage = result.Size / pageSize;
        if (result.Size % pageSize != 0) totalPage += 1;
        var response = new ApiResponseResult
        {
            Error = result.Error,
            Mode = result.Mode,
            Page = result.Page,
            Query = result.Query,
            Size = result.Size,
            TotalPage = totalPage,
            Results = new List<SearchResult>()
        };
        var num = (page - 1) * pageSize;
        foreach (var r in result.Results)
        {
            num++;
            response.Results.Add(new SearchResult
            {
                Id = num,
                Host = r[0],
                IP = r[1],
                Protocol = r[2],
                Domain = r[3],
                Port = r[4],
                Title = Regex.Unescape(r[5]),
                ICP = Regex.Unescape(r[6]),
                Server = r[7],
                OS = r[8],
                CountryName = r[9]
            });
        }

        return response;
    }
}