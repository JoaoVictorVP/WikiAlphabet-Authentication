namespace Authentication.Server.Databases.Contracts;

public interface IDatabaseFactory<TDatabase>
{
    TDatabase Get();
}
