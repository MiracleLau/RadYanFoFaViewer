using LiteDB;

namespace RadYanFoFaViewer.Utils;

public class Database
{
    private const string DbFile = @"data.db";
    protected LiteDatabase Db;

    protected Database()
    {
        Db = new LiteDatabase(DbFile);
    }
}