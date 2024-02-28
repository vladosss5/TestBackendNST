﻿// <auto-generated />
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20240228111306_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Person", b =>
                {
                    b.Property<long>("IdPerson")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("IdPerson"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("IdPerson")
                        .HasName("persons_pk");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Infrastructure.Skill", b =>
                {
                    b.Property<long>("IdSkill")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("IdSkill"));

                    b.Property<long>("IdPerson")
                        .HasColumnType("bigint");

                    b.Property<short>("Level")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("IdSkill")
                        .HasName("skills_pk");

                    b.HasIndex("IdPerson");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Infrastructure.Skill", b =>
                {
                    b.HasOne("Infrastructure.Person", "IdPersonNavigation")
                        .WithMany("Skills")
                        .HasForeignKey("IdPerson")
                        .IsRequired()
                        .HasConstraintName("skills_persons_idperson_fk");

                    b.Navigation("IdPersonNavigation");
                });

            modelBuilder.Entity("Infrastructure.Person", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
