﻿// <auto-generated />
using System;
using Leagues.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Leagues.Migrations
{
    [DbContext(typeof(LeagueContext))]
    [Migration("20230122213534_EndStart")]
    partial class EndStart
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Leagues.Models.Conference", b =>
                {
                    b.Property<string>("ConferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LeagueId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ConferenceId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Conference", (string)null);
                });

            modelBuilder.Entity("Leagues.Models.Division", b =>
                {
                    b.Property<string>("DivisionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConferenceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DivisionId");

                    b.HasIndex("ConferenceId");

                    b.ToTable("Division", (string)null);
                });

            modelBuilder.Entity("Leagues.Models.League", b =>
                {
                    b.Property<string>("LeagueId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LeagueId");

                    b.ToTable("League", (string)null);
                });

            modelBuilder.Entity("Leagues.Models.Player", b =>
                {
                    b.Property<string>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("College")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Depth")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DraftPick")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DraftRound")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DraftYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Experience")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Height")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("TeamId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Weight")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("Leagues.Models.Team", b =>
                {
                    b.Property<string>("TeamId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DivisionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("Loss")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PointsAgainst")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PointsFor")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Stadium")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Tie")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Win")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TeamId");

                    b.HasIndex("DivisionId");

                    b.ToTable("Team", (string)null);
                });

            modelBuilder.Entity("Leagues.Models.Conference", b =>
                {
                    b.HasOne("Leagues.Models.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId");

                    b.Navigation("League");
                });

            modelBuilder.Entity("Leagues.Models.Division", b =>
                {
                    b.HasOne("Leagues.Models.Conference", "Conference")
                        .WithMany()
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");
                });

            modelBuilder.Entity("Leagues.Models.Player", b =>
                {
                    b.HasOne("Leagues.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Leagues.Models.Team", b =>
                {
                    b.HasOne("Leagues.Models.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Leagues.Models.Team", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
