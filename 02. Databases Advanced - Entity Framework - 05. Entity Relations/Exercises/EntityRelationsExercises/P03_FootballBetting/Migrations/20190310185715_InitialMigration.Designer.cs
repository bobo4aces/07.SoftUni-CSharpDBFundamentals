﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P03_FootballBetting.Data;

namespace P03_FootballBetting.Migrations
{
    [DbContext(typeof(FootballBettingContext))]
    [Migration("20190310185715_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Bet", b =>
                {
                    b.Property<int>("BetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnName("Amount")
                        .HasColumnType("money");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("DateTime")
                        .HasColumnType("date");

                    b.Property<int>("GameId")
                        .HasColumnName("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Prediction")
                        .IsRequired()
                        .HasColumnName("Prediction")
                        .HasColumnType("char")
                        .IsFixedLength(true)
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.Property<int>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("int");

                    b.HasKey("BetId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(true)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ColorId");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AwayTeamBetRate")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnName("AwayTeamBetRate")
                        .HasColumnType("decimal(15,2)");

                    b.Property<int>("AwayTeamGoals")
                        .HasColumnName("AwayTeamGoals")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnName("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("DateTime")
                        .HasColumnType("date");

                    b.Property<decimal>("DrawBetRate")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnName("DrawBetRate")
                        .HasColumnType("decimal(15,2)");

                    b.Property<decimal>("HomeTeamBetRate")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnName("HomeTeamBetRate")
                        .HasColumnType("decimal(15,2)");

                    b.Property<int>("HomeTeamGoals")
                        .HasColumnName("HomeTeamGoals")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnName("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnName("Result")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(5)
                        .IsUnicode(false);

                    b.HasKey("GameId");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsInjured")
                        .HasColumnName("IsInjured")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("PositionId")
                        .HasColumnName("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("SquadNumber")
                        .HasColumnName("SquadNumber")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnName("TeamId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.PlayerStatistic", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnName("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnName("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Assists")
                        .HasColumnName("Assists")
                        .HasColumnType("int");

                    b.Property<int>("MinutesPlayed")
                        .HasColumnName("MinutesPlayed")
                        .HasColumnType("int");

                    b.Property<int>("ScoredGoals")
                        .HasColumnName("ScoredGoals")
                        .HasColumnType("int");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerStatistics");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget")
                        .HasColumnName("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnName("Initials")
                        .HasColumnType("char")
                        .IsFixedLength(true)
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnName("LogoUrl")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("PrimaryKitColorId")
                        .HasColumnName("PrimaryKitColorId")
                        .HasColumnType("int");

                    b.Property<int>("SecondaryKitColorId")
                        .HasColumnName("SecondaryKitColorId")
                        .HasColumnType("int");

                    b.Property<int>("TownId")
                        .HasColumnName("TownId")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.HasIndex("PrimaryKitColorId");

                    b.HasIndex("SecondaryKitColorId");

                    b.HasIndex("TownId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Town", b =>
                {
                    b.Property<int>("TownId");

                    b.Property<int>("CountryId")
                        .HasColumnName("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("TownId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance")
                        .HasColumnName("Balance")
                        .HasColumnType("money");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("Password")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("Username")
                        .HasColumnType("varchar")
                        .IsFixedLength(false)
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Bet", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Game", "Game")
                        .WithMany("Bets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P03_FootballBetting.Data.Models.User", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Game", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Team", "AwayTeam")
                        .WithMany("AwayGames")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("P03_FootballBetting.Data.Models.Team", "HomeTeam")
                        .WithMany("HomeGames")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Player", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Position", "Position")
                        .WithMany("Players")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P03_FootballBetting.Data.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.PlayerStatistic", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Game", "Game")
                        .WithMany("PlayerStatistics")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("P03_FootballBetting.Data.Models.Player", "Player")
                        .WithMany("PlayerStatistics")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Team", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Color", "PrimaryKitColor")
                        .WithMany("PrimaryKitTeams")
                        .HasForeignKey("PrimaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("P03_FootballBetting.Data.Models.Color", "SecondaryKitColor")
                        .WithMany("SecondaryKitTeams")
                        .HasForeignKey("SecondaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("P03_FootballBetting.Data.Models.Town", "Town")
                        .WithMany("Teams")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("P03_FootballBetting.Data.Models.Town", b =>
                {
                    b.HasOne("P03_FootballBetting.Data.Models.Country", "Country")
                        .WithMany("Towns")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
