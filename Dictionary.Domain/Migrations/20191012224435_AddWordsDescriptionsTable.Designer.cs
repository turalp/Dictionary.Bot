﻿// <auto-generated />
using System;
using Dictionary.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dictionary.Domain.Migrations
{
    [DbContext(typeof(DictionaryContext))]
    [Migration("20191012224435_AddWordsDescriptionsTable")]
    partial class AddWordsDescriptionsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dictionary.Domain.Models.Description", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DescriptionId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Descriptions");
                });

            modelBuilder.Entity("Dictionary.Domain.Models.FullWord", b =>
                {
                    b.Property<Guid>("WordId");

                    b.Property<Guid>("DescriptionId");

                    b.Property<Guid>("Id")
                        .HasColumnName("WordDescriptionId");

                    b.HasKey("WordId", "DescriptionId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("DescriptionId");

                    b.ToTable("WordsDescriptions");
                });

            modelBuilder.Entity("Dictionary.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("UserId")
                        .HasColumnName("UserId1");

                    b.Property<string>("Vcard");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dictionary.Domain.Models.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WordId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("Dictionary.Domain.Models.FullWord", b =>
                {
                    b.HasOne("Dictionary.Domain.Models.Description", "Description")
                        .WithMany("WordDescriptions")
                        .HasForeignKey("DescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dictionary.Domain.Models.Word", "Word")
                        .WithMany("WordDescriptions")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
