using Microsoft.EntityFrameworkCore;
using MkApi.Models;

namespace MkApi.Database;

public class MkApiDatabase : DbContext 
{
    public DbSet<UserModel> Users { get; set; }

    public MkApiDatabase(DbContextOptions options) : base(options)
    {
    }

}