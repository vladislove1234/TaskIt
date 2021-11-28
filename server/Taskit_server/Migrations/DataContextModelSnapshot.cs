﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taskit_server.Db;

namespace Taskit_server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("TaskTeamMember", b =>
                {
                    b.Property<int>("PerformersId")
                        .HasColumnType("int");

                    b.Property<int>("TasksId")
                        .HasColumnType("int");

                    b.HasKey("PerformersId", "TasksId");

                    b.HasIndex("TasksId");

                    b.ToTable("TaskTeamMember");
                });

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

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamMemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TeamId");

                    b.HasIndex("TeamMemberId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TakenTaskModels.TakenTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TakenTasks");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

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

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FirendsId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskTeamMember", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.UserModels.TeamMember", null)
                        .WithMany()
                        .HasForeignKey("PerformersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taskit_server.Model.Entities.TaskModels.Task", null)
                        .WithMany()
                        .HasForeignKey("TasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.RoleModels.Role", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.TaskModels.Task", null)
                        .WithMany("Roles")
                        .HasForeignKey("TaskId");

                    b.HasOne("Taskit_server.Model.Entities.TeamModels.Team", null)
                        .WithMany("Roles")
                        .HasForeignKey("TeamId");

                    b.HasOne("Taskit_server.Model.Entities.UserModels.TeamMember", null)
                        .WithMany("Roles")
                        .HasForeignKey("TeamMemberId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TakenTaskModels.TakenTask", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.UserModels.User", null)
                        .WithMany("TaskenTasks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.HasOne("Taskit_server.Model.Entities.TeamModels.Team", "Team")
                        .WithMany("Tasks")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.TaskModels.Task", b =>
                {
                    b.Navigation("Roles");
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
                });

            modelBuilder.Entity("Taskit_server.Model.Entities.UserModels.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("TaskenTasks");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
