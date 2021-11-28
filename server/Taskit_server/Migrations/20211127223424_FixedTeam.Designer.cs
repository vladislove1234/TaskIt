﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taskit_server.Db;

namespace Taskit_server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211127223424_FixedTeam")]
    partial class FixedTeam
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Taskit_server.Model.Entities.RoleModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamMemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamMemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TeamModels.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.TeamMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FirendsId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FirendsId");

                    b.HasIndex("TaskId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.RoleModels.Role", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.TeamModels.Team", null)
                        .WithMany("Roles")
                        .HasForeignKey("TeamId");

                    b.HasOne("Taskit_server.Model.Entities.UserModels.TeamMember", null)
                        .WithMany("Roles")
                        .HasForeignKey("TeamMemberId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.UserModels.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Taskit_server.Model.Entities.TeamModels.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taskit_server.Model.Entities.UserModels.TeamMember", null)
                        .WithMany("Tasks")
                        .HasForeignKey("TeamMemberId");

                    b.Navigation("Author");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TeamModels.Team", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.UserModels.User", null)
                        .WithMany("Teams")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.TeamMember", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.TeamModels.Team", null)
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.User", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.UserModels.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("FirendsId");

                    b.HasOne("Taskit_server.Model.Entities.TaskModels.Task", null)
                        .WithMany("Performers")
                        .HasForeignKey("TaskId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.Navigation("Performers");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TeamModels.Team", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Tasks");

                    b.Navigation("TeamMembers");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.TeamMember", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
