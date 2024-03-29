﻿// <auto-generated />
using System;
using Agenda.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agenda.Api.Migrations
{
    [DbContext(typeof(AgendaContext))]
    [Migration("20191113020710_InsertTelefoneTipo")]
    partial class InsertTelefoneTipo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Agenda.Api.Models.Agendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data");

                    b.Property<int?>("MedicoId");

                    b.Property<int?>("PacienteId");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("Agendamentos");
                });

            modelBuilder.Entity("Agenda.Api.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cep");

                    b.Property<string>("Cidade");

                    b.Property<string>("Estado");

                    b.Property<string>("Logradouro");

                    b.Property<int?>("Numero");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Agenda.Api.Models.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int?>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Rg");

                    b.Property<int?>("TelefoneId");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cpf");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TelefoneId");

                    b.ToTable("Medicos");
                });

            modelBuilder.Entity("Agenda.Api.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int?>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Rg");

                    b.Property<int?>("TelefoneId");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cpf");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TelefoneId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Agenda.Api.Models.Telefone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte?>("Ddd");

                    b.Property<int?>("Numero");

                    b.Property<byte?>("TipoId");

                    b.HasKey("Id");

                    b.HasIndex("TipoId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("Agenda.Api.Models.TelefoneTipo", b =>
                {
                    b.Property<byte>("Id");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("TelefoneTipos");
                });

            modelBuilder.Entity("Agenda.Api.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<int?>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Rg");

                    b.Property<int?>("TelefoneId");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cpf");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TelefoneId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Agenda.Api.Models.Agendamento", b =>
                {
                    b.HasOne("Agenda.Api.Models.Medico", "Medico")
                        .WithMany("Agendamentos")
                        .HasForeignKey("MedicoId");

                    b.HasOne("Agenda.Api.Models.Paciente", "Paciente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("PacienteId");
                });

            modelBuilder.Entity("Agenda.Api.Models.Medico", b =>
                {
                    b.HasOne("Agenda.Api.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("Agenda.Api.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("TelefoneId");
                });

            modelBuilder.Entity("Agenda.Api.Models.Paciente", b =>
                {
                    b.HasOne("Agenda.Api.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("Agenda.Api.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("TelefoneId");
                });

            modelBuilder.Entity("Agenda.Api.Models.Telefone", b =>
                {
                    b.HasOne("Agenda.Api.Models.TelefoneTipo", "Tipo")
                        .WithMany()
                        .HasForeignKey("TipoId");
                });

            modelBuilder.Entity("Agenda.Api.Models.Usuario", b =>
                {
                    b.HasOne("Agenda.Api.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.HasOne("Agenda.Api.Models.Telefone", "Telefone")
                        .WithMany()
                        .HasForeignKey("TelefoneId");
                });
#pragma warning restore 612, 618
        }
    }
}
