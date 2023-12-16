using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TVPrograms.Data
{
    public partial class LimeHdTvContext : DbContext
    {
        public LimeHdTvContext()
        {
        }

        public LimeHdTvContext(DbContextOptions<LimeHdTvContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Channel> Channels { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=LimeHdTv;Username=postgres;Password=ihesop69");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>(entity =>
            {
                entity.ToTable("channels", "Tv");

                entity.Property(e => e.ChannelId)
                    .ValueGeneratedNever()
                    .HasColumnName("channel_id");

                entity.Property(e => e.Img)
                    .HasColumnType("character varying")
                    .HasColumnName("img");

                entity.Property(e => e.LargeImg)
                    .HasColumnType("character varying")
                    .HasColumnName("large_img");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.SmallImg)
                    .HasColumnType("character varying")
                    .HasColumnName("small_img");

                entity.Property(e => e.Sort).HasColumnName("sort");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("events", "Tv");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ChannelId).HasColumnName("channel_id");

                entity.Property(e => e.Date)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("events_channel_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
