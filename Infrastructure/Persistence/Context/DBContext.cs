using System;
using System.Collections.Generic;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.IdPerson).HasName("persons_pk");

            entity.Property(e => e.IdPerson).UseIdentityAlwaysColumn();
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.IdSkill).HasName("skills_pk");

            entity.Property(e => e.IdSkill).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.IdPersonNavigation).WithMany(p => p.Skills)
                .HasForeignKey(d => d.IdPerson)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("skills_persons_idperson_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
