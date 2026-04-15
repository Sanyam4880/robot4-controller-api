using Microsoft.EntityFrameworkCore;
using robot4_controller_api.Models;

namespace robot4_controller_api.Persistence;

public partial class RobotContext : DbContext
{
    public RobotContext()
    {
    }

    public RobotContext(DbContextOptions<RobotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RobotCommand> RobotCommands { get; set; }
    public virtual DbSet<Map> Maps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RobotCommand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_robotcommand");

            entity.ToTable("robotcommand");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");

            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");

            entity.Property(e => e.IsMoveCommand)
                .HasColumnName("ismovecommand");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Map>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_map");

            entity.ToTable("map");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.CreatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");

            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");

            entity.Property(e => e.Columns)
                .HasColumnName("columns");

            entity.Property(e => e.Rows)
                .HasColumnName("rows");

            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");

            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}