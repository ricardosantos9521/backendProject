﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Backend.Database;

namespace Project.Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190705171745_files")]
    partial class files
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Project.Backend.Database.AccountTables.Identity", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasMaxLength(100);

                    b.Property<string>("Issuer")
                        .HasMaxLength(50);

                    b.Property<Guid>("UniqueId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("SubjectId", "Issuer");

                    b.ToTable("Identity");
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.Profile", b =>
                {
                    b.Property<Guid>("UniqueId");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Picture");

                    b.HasKey("UniqueId");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.RefreshToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpiresUtc");

                    b.Property<DateTime>("IssuedUtc");

                    b.Property<Guid>("SessionId");

                    b.HasKey("Token");

                    b.HasIndex("SessionId")
                        .IsUnique();

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.Session", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FirstLogin");

                    b.Property<DateTime>("LastLogin");

                    b.Property<Guid>("UniqueId");

                    b.HasKey("SessionId");

                    b.HasIndex("UniqueId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("Project.Backend.Database.AdminTables.Admin", b =>
                {
                    b.Property<Guid>("UniqueId");

                    b.HasKey("UniqueId");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.File", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Bytes")
                        .IsRequired();

                    b.Property<string>("ContentType")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("FileLength");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<bool>("IsPublic");

                    b.Property<Guid>("OwnedByUniqueId");

                    b.HasKey("FileId");

                    b.HasIndex("OwnedByUniqueId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.Read", b =>
                {
                    b.Property<Guid>("FileId");

                    b.Property<Guid>("UniqueId");

                    b.Property<Guid>("SharedByUniqueId");

                    b.HasKey("FileId", "UniqueId");

                    b.HasIndex("SharedByUniqueId");

                    b.HasIndex("UniqueId");

                    b.ToTable("ReadPermissions");
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.Write", b =>
                {
                    b.Property<Guid>("FileId");

                    b.Property<Guid>("UniqueId");

                    b.Property<Guid>("SharedByUniqueId");

                    b.HasKey("FileId", "UniqueId");

                    b.HasIndex("SharedByUniqueId");

                    b.HasIndex("UniqueId");

                    b.ToTable("WritePermissions");
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.Profile", b =>
                {
                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "Identity")
                        .WithOne("Profile")
                        .HasForeignKey("Project.Backend.Database.AccountTables.Profile", "UniqueId")
                        .HasPrincipalKey("Project.Backend.Database.AccountTables.Identity", "UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.RefreshToken", b =>
                {
                    b.HasOne("Project.Backend.Database.AccountTables.Session", "Session")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Project.Backend.Database.AccountTables.RefreshToken", "SessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.AccountTables.Session", b =>
                {
                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "Identity")
                        .WithMany("Sessions")
                        .HasForeignKey("UniqueId")
                        .HasPrincipalKey("UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.AdminTables.Admin", b =>
                {
                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "Identity")
                        .WithOne("Admin")
                        .HasForeignKey("Project.Backend.Database.AdminTables.Admin", "UniqueId")
                        .HasPrincipalKey("Project.Backend.Database.AccountTables.Identity", "UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.File", b =>
                {
                    b.HasOne("Project.Backend.Database.AccountTables.Profile", "OwnedBy")
                        .WithMany("OwnedByMe")
                        .HasForeignKey("OwnedByUniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.Read", b =>
                {
                    b.HasOne("Project.Backend.Database.FilesTables.File", "File")
                        .WithMany("ReadPermissions")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "SharedByIdentity")
                        .WithMany("SharedByMeReadPermissions")
                        .HasForeignKey("SharedByUniqueId")
                        .HasPrincipalKey("UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "Identity")
                        .WithMany("ReadPermissions")
                        .HasForeignKey("UniqueId")
                        .HasPrincipalKey("UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Project.Backend.Database.FilesTables.Write", b =>
                {
                    b.HasOne("Project.Backend.Database.FilesTables.File", "File")
                        .WithMany("WritePermissions")
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "SharedByIdentity")
                        .WithMany("SharedByMeWritePermissions")
                        .HasForeignKey("SharedByUniqueId")
                        .HasPrincipalKey("UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Project.Backend.Database.AccountTables.Identity", "Identity")
                        .WithMany("WritePermissions")
                        .HasForeignKey("UniqueId")
                        .HasPrincipalKey("UniqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
