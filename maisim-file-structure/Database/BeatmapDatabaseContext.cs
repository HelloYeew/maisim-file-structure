using maisim_file_structure.Objects;
using Microsoft.EntityFrameworkCore;

namespace maisim_file_structure.Database;

public class MaisimDatabaseContext : DbContext
{
    public DbSet<Beatmap> Beatmaps { get; set; }
    public DbSet<BeatmapSet> BeatmapSets { get; set; }
    
    public string DbPath { get; set; }

    public MaisimDatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "beatmaps.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Filename={DbPath}");
    }
}