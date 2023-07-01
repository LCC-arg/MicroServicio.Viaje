﻿// <auto-generated />
using System;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestructure.Migrations
{
    [DbContext(typeof(ViajeContext))]
    [Migration("20230701141158_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
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

                    b.Property<int>("AsientosDisponibles")
                        .HasColumnType("int");

                    b.Property<string>("Duracion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaLlegada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSalida")
                        .HasColumnType("datetime2");

                    b.Property<int>("Precio")
                        .HasColumnType("int");

                    b.Property<string>("TipoViaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransporteId")
                        .HasColumnType("int");

                    b.HasKey("ViajeId");

                    b.ToTable("Viaje", (string)null);

                    b.HasData(
                        new
                        {
                            ViajeId = 1,
                            AsientosDisponibles = 50,
                            Duracion = "1 Hora",
                            FechaLlegada = new DateTime(2023, 10, 5, 19, 33, 39, 514, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 10, 5, 20, 33, 39, 514, DateTimeKind.Local),
                            Precio = 15000,
                            TipoViaje = "Ida y vuelta",
                            TransporteId = 1
                        },
                        new
                        {
                            ViajeId = 2,
                            AsientosDisponibles = 60,
                            Duracion = "2 Hora",
                            FechaLlegada = new DateTime(2023, 11, 5, 17, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 11, 5, 19, 0, 0, 0, DateTimeKind.Local),
                            Precio = 12000,
                            TipoViaje = "Ida",
                            TransporteId = 2
                        },
                        new
                        {
                            ViajeId = 3,
                            AsientosDisponibles = 360,
                            Duracion = "5 Hora",
                            FechaLlegada = new DateTime(2023, 12, 5, 12, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 12, 5, 17, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida y vuelta",
                            TransporteId = 3
                        },
                        new
                        {
                            ViajeId = 4,
                            AsientosDisponibles = 30,
                            Duracion = "12 Horas",
                            FechaLlegada = new DateTime(2023, 7, 4, 19, 33, 39, 514, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 5, 7, 33, 39, 514, DateTimeKind.Local),
                            Precio = 11000,
                            TipoViaje = "Ida",
                            TransporteId = 19
                        },
                        new
                        {
                            ViajeId = 5,
                            AsientosDisponibles = 50,
                            Duracion = "12 Horas",
                            FechaLlegada = new DateTime(2023, 8, 4, 19, 33, 39, 514, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 8, 5, 7, 33, 39, 514, DateTimeKind.Local),
                            Precio = 8500,
                            TipoViaje = "Ida",
                            TransporteId = 20
                        },
                        new
                        {
                            ViajeId = 6,
                            AsientosDisponibles = 50,
                            Duracion = "5 Horas",
                            FechaLlegada = new DateTime(2023, 5, 4, 12, 33, 39, 514, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 5, 5, 7, 33, 39, 514, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 21
                        },
                        new
                        {
                            ViajeId = 7,
                            AsientosDisponibles = 40,
                            Duracion = "10 Horas",
                            FechaLlegada = new DateTime(2023, 8, 4, 17, 33, 39, 514, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 8, 5, 7, 33, 39, 514, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 22
                        },
                        new
                        {
                            ViajeId = 8,
                            AsientosDisponibles = 35,
                            Duracion = "4 Horas",
                            FechaLlegada = new DateTime(2023, 6, 5, 17, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 6, 5, 13, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 23
                        },
                        new
                        {
                            ViajeId = 9,
                            AsientosDisponibles = 30,
                            Duracion = "4 Horas",
                            FechaLlegada = new DateTime(2023, 5, 5, 12, 30, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 5, 5, 8, 30, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 24
                        },
                        new
                        {
                            ViajeId = 10,
                            AsientosDisponibles = 60,
                            Duracion = "3 Horas",
                            FechaLlegada = new DateTime(2023, 7, 6, 6, 15, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 6, 3, 15, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 25
                        },
                        new
                        {
                            ViajeId = 11,
                            AsientosDisponibles = 50,
                            Duracion = "4 Horas",
                            FechaLlegada = new DateTime(2023, 4, 6, 12, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 4, 6, 8, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 26
                        },
                        new
                        {
                            ViajeId = 12,
                            AsientosDisponibles = 30,
                            Duracion = "5 Horas",
                            FechaLlegada = new DateTime(2023, 7, 6, 17, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 6, 12, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 27
                        },
                        new
                        {
                            ViajeId = 13,
                            AsientosDisponibles = 30,
                            Duracion = "4 Horas",
                            FechaLlegada = new DateTime(2023, 7, 7, 7, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 7, 3, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 28
                        },
                        new
                        {
                            ViajeId = 14,
                            AsientosDisponibles = 45,
                            Duracion = "5 Horas",
                            FechaLlegada = new DateTime(2023, 7, 7, 14, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 7, 9, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 29
                        },
                        new
                        {
                            ViajeId = 15,
                            AsientosDisponibles = 20,
                            Duracion = "5 Horas",
                            FechaLlegada = new DateTime(2023, 7, 7, 18, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 7, 13, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 30
                        },
                        new
                        {
                            ViajeId = 16,
                            AsientosDisponibles = 15,
                            Duracion = "2 Horas",
                            FechaLlegada = new DateTime(2023, 7, 8, 5, 30, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 8, 3, 30, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 31
                        },
                        new
                        {
                            ViajeId = 17,
                            AsientosDisponibles = 10,
                            Duracion = "8 Horas",
                            FechaLlegada = new DateTime(2023, 7, 8, 12, 0, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 8, 4, 0, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 26
                        },
                        new
                        {
                            ViajeId = 18,
                            AsientosDisponibles = 5,
                            Duracion = "3 Horas",
                            FechaLlegada = new DateTime(2023, 7, 8, 14, 30, 0, 0, DateTimeKind.Local),
                            FechaSalida = new DateTime(2023, 7, 8, 11, 30, 0, 0, DateTimeKind.Local),
                            Precio = 8000,
                            TipoViaje = "Ida",
                            TransporteId = 33
                        });
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