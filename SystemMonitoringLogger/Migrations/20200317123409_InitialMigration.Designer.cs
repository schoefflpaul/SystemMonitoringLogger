﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemMonitoringLogger.Data;

namespace SystemMonitoringLogger.Migrations
{
    [DbContext(typeof(SystemMonitoringLoggerContext))]
    [Migration("20200317123409_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemMonitoringLogger.Entities.Cpu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Baseclock")
                        .HasColumnType("int");

                    b.Property<int>("Currentclock")
                        .HasColumnType("int");

                    b.Property<int>("Utilisation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cpu");
                });

            modelBuilder.Entity("SystemMonitoringLogger.Entities.Ram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Max")
                        .HasColumnType("float");

                    b.Property<double>("Used")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Ram");
                });

            modelBuilder.Entity("SystemMonitoringLogger.Entities.SystemInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CpuId")
                        .HasColumnType("int");

                    b.Property<int?>("RamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CpuId");

                    b.HasIndex("RamId");

                    b.ToTable("SystemInfo");
                });

            modelBuilder.Entity("SystemMonitoringLogger.Entities.SystemInfo", b =>
                {
                    b.HasOne("SystemMonitoringLogger.Entities.Cpu", "Cpu")
                        .WithMany()
                        .HasForeignKey("CpuId");

                    b.HasOne("SystemMonitoringLogger.Entities.Ram", "Ram")
                        .WithMany()
                        .HasForeignKey("RamId");
                });
#pragma warning restore 612, 618
        }
    }
}
