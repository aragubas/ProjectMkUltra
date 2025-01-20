using Microsoft.EntityFrameworkCore;
using MkApi.Models;

namespace MkApi.Database;

public class MkApiDatabase : DbContext 
{
    public DbSet<UserModel> Users { get; set; }
    string m_DbPath { get; }

    public MkApiDatabase(DbContextOptions options) : base(options)
    {
        m_DbPath = $"Data Source={Path.Join(Environment.CurrentDirectory, "Database.sqlite")}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(m_DbPath);

}