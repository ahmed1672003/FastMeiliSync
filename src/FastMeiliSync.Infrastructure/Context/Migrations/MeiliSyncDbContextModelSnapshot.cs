﻿// <auto-generated />
using System;
using FastMeiliSync.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FastMeiliSync.Infrastructure.Context.Migrations
{
    [DbContext(typeof(MeiliSyncDbContext))]
    partial class MeiliSyncDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.MeiliSearches.MeiliSearch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique();

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("MeiliSearche", "Public");
                });

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.Sources.Source", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Database")
                        .HasColumnType("text");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Database")
                        .IsUnique();

                    b.HasIndex("Label")
                        .IsUnique();

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Source", "Public");
                });

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.Syncs.Sync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("MeiliSearchId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SourceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Label")
                        .IsUnique();

                    b.HasIndex("MeiliSearchId");

                    b.HasIndex("SourceId", "MeiliSearchId")
                        .IsUnique();

                    b.ToTable("Sync", "Public");
                });

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.Syncs.Sync", b =>
                {
                    b.HasOne("FastMeiliSync.Domain.Entities.MeiliSearches.MeiliSearch", "MeiliSearch")
                        .WithMany("Syncs")
                        .HasForeignKey("MeiliSearchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FastMeiliSync.Domain.Entities.Sources.Source", "Source")
                        .WithMany("Syncs")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MeiliSearch");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.MeiliSearches.MeiliSearch", b =>
                {
                    b.Navigation("Syncs");
                });

            modelBuilder.Entity("FastMeiliSync.Domain.Entities.Sources.Source", b =>
                {
                    b.Navigation("Syncs");
                });
#pragma warning restore 612, 618
        }
    }
}
