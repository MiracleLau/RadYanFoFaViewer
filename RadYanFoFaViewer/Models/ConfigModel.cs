using LiteDB;

namespace RadYanFoFaViewer.Models;

public class ConfigModel
{
    public int Id { get; set; }
    public string Key { get; set; }
    public BsonValue Value { get; set; }
}