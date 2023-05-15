﻿// <auto-generated />
using System;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Pasajero", b =>
                {
                    b.Property<int>("PasajeroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasajeroId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumContactoEmergencia")
                        .HasColumnType("int");

                    b.Property<int>("ViajeId")
                        .HasColumnType("int");

                    b.HasKey("PasajeroId");

                    b.HasIndex("ViajeId");

                    b.ToTable("Pasajero", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Viaje", b =>
                {
                    b.Property<int>("ViajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ViajeId"));

                    b.Property<int>("CiudadDestino")
                        .HasColumnType("int");

                    b.Property<int>("CiudadOrigen")
                        .HasColumnType("int");

                    b.Property<string>("Duracion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaLlegada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSalida")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HorarioLlegada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HorarioSalida")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoViaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransporteId")
                        .HasColumnType("int");

                    b.HasKey("ViajeId");

                    b.ToTable("Viaje", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pasajero", b =>
                {
                    b.HasOne("Domain.Entities.Viaje", "Viaje")
                        .WithMany("Pasajeros")
                        .HasForeignKey("ViajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viaje");
                });

            modelBuilder.Entity("Domain.Entities.Viaje", b =>
                {
                    b.Navigation("Pasajeros");
                });
#pragma warning restore 612, 618
        }
    }
}
