﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cw13.Services;

namespace cw13.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("cw13.Models.Klient", b =>
                {
                    b.Property<int>("IdKlient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKlient");

                    b.ToTable("Klienci");
                });

            modelBuilder.Entity("cw13.Models.Pracownik", b =>
                {
                    b.Property<int>("IdPacownika")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPacownika");

                    b.ToTable("Pracownicy");
                });

            modelBuilder.Entity("cw13.Models.WyrobCukierniczy", b =>
                {
                    b.Property<int>("IdWyrobuCukierniczego")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("CenaZaSzt")
                        .HasColumnType("real");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Typ")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdWyrobuCukierniczego");

                    b.ToTable("WyrobyCukiernicze");
                });

            modelBuilder.Entity("cw13.Models.Zamowienie", b =>
                {
                    b.Property<int>("IdZamowienia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataPrzyjecia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataRealizacji")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KlientIdKlient")
                        .HasColumnType("int");

                    b.Property<int?>("PracownikIdPacownika")
                        .HasColumnType("int");

                    b.Property<string>("Uwagi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdZamowienia");

                    b.HasIndex("KlientIdKlient");

                    b.HasIndex("PracownikIdPacownika");

                    b.ToTable("Zamowienia");
                });

            modelBuilder.Entity("cw13.Models.ZamowienieWyrobCukierniczy", b =>
                {
                    b.Property<int>("IdZamowienie")
                        .HasColumnType("int");

                    b.Property<int>("IdWyrobCukierniczy")
                        .HasColumnType("int");

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.Property<string>("Uwagi")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdZamowienie", "IdWyrobCukierniczy");

                    b.HasIndex("IdWyrobCukierniczy");

                    b.ToTable("ZamowieniaWyrobyCukiernicze");
                });

            modelBuilder.Entity("cw13.Models.Zamowienie", b =>
                {
                    b.HasOne("cw13.Models.Klient", "Klient")
                        .WithMany("Zamowienia")
                        .HasForeignKey("KlientIdKlient");

                    b.HasOne("cw13.Models.Pracownik", "Pracownik")
                        .WithMany("Zamowienia")
                        .HasForeignKey("PracownikIdPacownika");
                });

            modelBuilder.Entity("cw13.Models.ZamowienieWyrobCukierniczy", b =>
                {
                    b.HasOne("cw13.Models.WyrobCukierniczy", "WyrobCukierniczy")
                        .WithMany("ZamowieniaWyrobCukiernicze")
                        .HasForeignKey("IdWyrobCukierniczy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cw13.Models.Zamowienie", "Zamowienie")
                        .WithMany("ZamowieniaWyrobyCukiernicze")
                        .HasForeignKey("IdZamowienie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
