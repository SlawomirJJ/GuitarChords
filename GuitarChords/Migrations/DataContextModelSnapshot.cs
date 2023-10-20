﻿// <auto-generated />
using System;
using GuitarChords;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuitarChords.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuitarChords.Models.Chord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Bar")
                        .HasColumnType("int");

                    b.Property<string>("ChordName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("FifthString")
                        .HasColumnType("int");

                    b.Property<int?>("FirstString")
                        .HasColumnType("int");

                    b.Property<int?>("FourthString")
                        .HasColumnType("int");

                    b.Property<int?>("SecondString")
                        .HasColumnType("int");

                    b.Property<int?>("SixthString")
                        .HasColumnType("int");

                    b.Property<int?>("ThirdString")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Chords");
                });
#pragma warning restore 612, 618
        }
    }
}
