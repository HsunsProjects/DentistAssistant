using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DentistAssistant.Models
{
    public partial class DentistAssistantContext : DbContext
    {
        public DentistAssistantContext()
        {
        }

        public DentistAssistantContext(DbContextOptions<DentistAssistantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FdiDetails> FdiDetails { get; set; }
        public virtual DbSet<Fdis> Fdis { get; set; }
        public virtual DbSet<PatientRecords> PatientRecords { get; set; }
        public virtual DbSet<PatientSettings> PatientSettings { get; set; }
        public virtual DbSet<PhraseGroups> PhraseGroups { get; set; }
        public virtual DbSet<Phrases> Phrases { get; set; }
        public virtual DbSet<Qaa> Qaa { get; set; }
        public virtual DbSet<Qacategorys> Qacategorys { get; set; }
        public virtual DbSet<Qagroups> Qagroups { get; set; }
        public virtual DbSet<Qaq> Qaq { get; set; }
        public virtual DbSet<RecordUsers> RecordUsers { get; set; }
        public virtual DbSet<ShareTypes> ShareTypes { get; set; }
        public virtual DbSet<Shares> Shares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-39AOD1O\\HSUNSPROJECTS;Database=DentistAssistant;User Id=sa;Password=Eason0811;Trusted_Connection=True;Integrated Security=false;");
                //optionsBuilder.UseSqlServer("Server=Localhost\\BESTCHOICE;Database=DentistAssistant;User Id=bestchoice;Password=0937093374;Trusted_Connection=True;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<FdiDetails>(entity =>
            {
                entity.HasKey(e => new { e.FdiArea, e.FdiPosition, e.FdiId })
                    .HasName("PK_FdiDetail");

                entity.Property(e => e.FdiArea)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FdiPosition)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Fdi)
                    .WithMany(p => p.FdiDetails)
                    .HasForeignKey(d => d.FdiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FdiDetails_Fdis");
            });

            modelBuilder.Entity<Fdis>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Fm).HasColumnName("FM");

                entity.Property(e => e.La).HasColumnName("LA");

                entity.Property(e => e.Lb).HasColumnName("LB");

                entity.Property(e => e.Ll).HasColumnName("LL");

                entity.Property(e => e.Lr).HasColumnName("LR");

                entity.Property(e => e.Type)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ua).HasColumnName("UA");

                entity.Property(e => e.Ub).HasColumnName("UB");

                entity.Property(e => e.Ul).HasColumnName("UL");

                entity.Property(e => e.Ur).HasColumnName("UR");

                entity.HasOne(d => d.PatientRecord)
                    .WithMany(p => p.Fdis)
                    .HasForeignKey(d => d.PatientRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fdis_PatientRecords");
            });

            modelBuilder.Entity<PatientRecords>(entity =>
            {
                entity.Property(e => e.ArriveTime).HasColumnType("datetime");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DrArriveTime).HasColumnType("datetime");

                entity.Property(e => e.DrLeaveTime).HasColumnType("datetime");

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.PatientSettingId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.PtLeaveTime).HasColumnType("datetime");

                entity.Property(e => e.Room).HasMaxLength(50);

                entity.Property(e => e.UserNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientSetting)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.PatientSettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PatientRecords_PatientSettings");
            });

            modelBuilder.Entity<PatientSettings>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.FirstTimeTime).HasColumnType("datetime");

                entity.Property(e => e.Introduce).HasMaxLength(300);

                entity.Property(e => e.QadoctorNo)
                    .HasColumnName("QADoctorNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhraseGroups>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Phrases>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.PhraseGroup)
                    .WithMany(p => p.Phrases)
                    .HasForeignKey(d => d.PhraseGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Phrases_PhraseGroups");
            });

            modelBuilder.Entity<Qaa>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.Qaqid });

                entity.ToTable("QAA");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Qaqid).HasColumnName("QAQId");

                entity.Property(e => e.ValueDescription).HasMaxLength(200);

                entity.HasOne(d => d.Qaq)
                    .WithMany(p => p.Qaa)
                    .HasForeignKey(d => d.Qaqid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QAA_QAQ");
            });

            modelBuilder.Entity<Qacategorys>(entity =>
            {
                entity.ToTable("QACategorys");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Qagroups>(entity =>
            {
                entity.ToTable("QAGroups");

                entity.Property(e => e.QacategoryId).HasColumnName("QACategoryId");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Qacategory)
                    .WithMany(p => p.Qagroups)
                    .HasForeignKey(d => d.QacategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QAGroups_QACategorys");
            });

            modelBuilder.Entity<Qaq>(entity =>
            {
                entity.ToTable("QAQ");

                entity.Property(e => e.QagroupId).HasColumnName("QAGroupId");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ValueDescription).HasMaxLength(200);

                entity.HasOne(d => d.Qagroup)
                    .WithMany(p => p.Qaq)
                    .HasForeignKey(d => d.QagroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QAQ_QAGroups");
            });

            modelBuilder.Entity<RecordUsers>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.UserNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.PatientRecord)
                    .WithMany(p => p.RecordUsers)
                    .HasForeignKey(d => d.PatientRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecordUsers_PatientRecords");
            });

            modelBuilder.Entity<ShareTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Shares>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PatId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.ShareTypeId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ValueDescription).HasMaxLength(20);

                entity.HasOne(d => d.ShareType)
                    .WithMany(p => p.Shares)
                    .HasForeignKey(d => d.ShareTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shares_ShareTypes");
            });
        }
    }
}
