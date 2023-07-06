using LiteDB;

namespace EmailService.DataAccess.LiteDb
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}
