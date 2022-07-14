using System;
using Avalonia.Logging;
using LiteDB;
using RadYanFoFaViewer.Models;

namespace RadYanFoFaViewer.Utils;

public class Config: Database
{
    private ILiteCollection<ConfigModel> _collection;
    public Config()
    {
        _collection = Db.GetCollection<ConfigModel>("configs");
    }
    /// <summary>
    /// 获取指定key的配置
    /// </summary>
    /// <param name="key">要获取的的配置的key</param>
    /// <returns>存在则返回BsonValue格式，不存在则返回null</returns>
    public BsonValue? GetConfig(string key)
    {
        var value = _collection.FindOne(x => x.Key == key);
        return value?.Value;
    }

    /// <summary>
    /// 获取指定key的配置
    /// </summary>
    /// <param name="key">要获取的的配置的key</param>
    /// <param name="defaultValue">当配置不存在时返回该默认值</param>
    /// <returns>存在则返回BsonValue格式，不存在则返回指定的默认数据</returns>
    public BsonValue GetOrDefaultConfig(string key,BsonValue defaultValue)
    {
        var value = GetConfig(key);
        return value ?? defaultValue;
    }

    /// <summary>
    /// 添加或修改配置
    /// </summary>
    /// <param name="key">配置的key</param>
    /// <param name="value">配置的值</param>
    /// <returns>是否成功</returns>
    public bool SetConfig(string key, BsonValue value)
    {
        var config = _collection.FindOne(x => x.Key == key);
        if (config is not null)
        {
            config.Value = value;
            return _collection.Update(config);
        }
        var id = _collection.Insert(new ConfigModel()
        {
            Key = key,
            Value = value
        });
        _collection.EnsureIndex(x => x.Key);
        return true;
    }
}