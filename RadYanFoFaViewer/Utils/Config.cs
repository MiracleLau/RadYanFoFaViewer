using LiteDB;
using RadYanFoFaViewer.Models;

namespace RadYanFoFaViewer.Utils;

public class Config : Database
{
    /// <summary>
    ///     获取指定key的配置
    /// </summary>
    /// <param name="key">要获取的的配置的key</param>
    /// <returns>存在则返回BsonValue格式，不存在则返回null</returns>
    public static BsonValue? GetConfig(string key)
    {
        using var db = new LiteDatabase(DbFile);
        var collection = db.GetCollection<ConfigModel>("configs");
        var value = collection.FindOne(x => x.Key == key);
        return value?.Value;
    }

    /// <summary>
    ///     获取指定key的配置
    /// </summary>
    /// <param name="key">要获取的的配置的key</param>
    /// <param name="defaultValue">当配置不存在时返回该默认值</param>
    /// <returns>存在则返回BsonValue格式，不存在则返回指定的默认数据</returns>
    public static BsonValue GetOrDefaultConfig(string key, BsonValue defaultValue)
    {
        var value = GetConfig(key);
        return value ?? defaultValue;
    }

    /// <summary>
    ///     添加或修改配置
    /// </summary>
    /// <param name="key">配置的key</param>
    /// <param name="value">配置的值</param>
    /// <returns>是否成功</returns>
    public static bool SetConfig(string key, BsonValue value)
    {
        using var db = new LiteDatabase(DbFile);
        var collection = db.GetCollection<ConfigModel>("configs");
        var config = collection.FindOne(x => x.Key == key);
        if (config is not null)
        {
            config.Value = value;
            return collection.Update(config);
        }

        collection.Insert(new ConfigModel
        {
            Key = key,
            Value = value
        });
        collection.EnsureIndex(x => x.Key);
        return true;
    }
}