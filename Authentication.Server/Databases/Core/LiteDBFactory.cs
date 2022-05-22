using Authentication.Server.Databases.Contracts;
using LiteDB;

namespace Authentication.Server.Databases.Core;

public class LiteDBFactory : IDatabaseFactory<LiteDatabase>
{
    static LiteDatabase? _singleton;

    private readonly string _connectionString;

    public LiteDBFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public LiteDatabase Get()
    {
        if (_singleton is null)
            _singleton = new LiteDatabase(_connectionString);
        return _singleton;
    }
}
