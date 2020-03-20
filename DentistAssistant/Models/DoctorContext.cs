using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DentistAssistant.Models
{
    public partial class DoctorContext : DbContext
    {
        public DoctorContext()
        {
        }

        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-R4Q0L8N\\HSUNSPROJECTS;Database=Doctor;User Id=sa;Password=Eason0811;Trusted_Connection=True;Integrated Security=false;");
                //optionsBuilder.UseSqlServer("Server=Localhost\\BESTCHOICE;Database=Doctor;User Id=bestchoice;Password=0937093374;Trusted_Connection=True;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasKey(e => e.PatNo);

                entity.HasIndex(e => e.Doctor)
                    .HasName("FK_Doctor");

                entity.HasIndex(e => e.Enable)
                    .HasName("idx_Enable");

                entity.HasIndex(e => e.Id)
                    .HasName("idx_Patients_ID");

                entity.HasIndex(e => new { e.PatNo, e.Enable })
                    .HasName("idx_Enable_PatNo");

                entity.HasIndex(e => new { e.PatNo, e.Doctor, e.Enable })
                    .HasName("idx_Doctor_Enable_PatNo");

                entity.Property(e => e.PatNo)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Addr1).HasMaxLength(60);

                entity.Property(e => e.Addr2).HasMaxLength(60);

                entity.Property(e => e.BigSickEnd)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.BigSickNo)
                    .HasColumnName("BigSickNO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.BigSickStart)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.BlackList)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Blood)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.BurdenNo)
                    .HasColumnName("BurdenNO")
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('H10A')");

                entity.Property(e => e.CardId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Doctor)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DrugAllergy).HasMaxLength(100);

                entity.Property(e => e.Education)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1')");

                entity.Property(e => e.EmHuman).HasMaxLength(20);

                entity.Property(e => e.EmPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmRelationShip)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.EmergPhone)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.EnDate).HasColumnType("date");

                entity.Property(e => e.Enable).HasDefaultValueSql("((1))");

                entity.Property(e => e.ExDate).HasColumnType("date");

                entity.Property(e => e.ExemptAmt)
                    .HasColumnType("numeric(8, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FamilyTag).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstDate).HasColumnType("date");

                entity.Property(e => e.FirstDoc)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FirstTeeth)
                    .HasMaxLength(52)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ImDate).HasColumnType("date");

                entity.Property(e => e.Introducer).HasMaxLength(20);

                entity.Property(e => e.IntroducerNote).HasMaxLength(20);

                entity.Property(e => e.IsAreca)
                    .HasColumnName("isAreca")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IsFreezeData).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGenUpload).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSmoke)
                    .HasColumnName("isSmoke")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IsTrackPat).HasDefaultValueSql("((0))");

                entity.Property(e => e.Job)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LastDate).HasColumnType("date");

                entity.Property(e => e.LastDoc)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LookAmtDate).HasColumnType("date");

                entity.Property(e => e.MailDate).HasColumnType("date");

                entity.Property(e => e.Mark)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Matter).HasMaxLength(150);

                entity.Property(e => e.MessageSet)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('1,1,1,1,1,1,1,0')");

                entity.Property(e => e.Minority).HasMaxLength(20);

                entity.Property(e => e.Moble1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Moble2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.MobleC).HasMaxLength(100);

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.Property(e => e.NotesEx).HasMaxLength(1000);

                entity.Property(e => e.Notice)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.OPatNo)
                    .HasColumnName("o_PatNo")
                    .HasMaxLength(14);

                entity.Property(e => e.OeNotes)
                    .HasColumnName("OE_Notes")
                    .HasMaxLength(4000);

                entity.Property(e => e.OeNotesEx)
                    .HasColumnName("OE_NotesEx")
                    .HasMaxLength(1000);

                entity.Property(e => e.Oral).HasMaxLength(30);

                entity.Property(e => e.OralDate).HasColumnType("datetime");

                entity.Property(e => e.OtherNote).HasMaxLength(1000);

                entity.Property(e => e.PatLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.PatName).HasMaxLength(20);

                entity.Property(e => e.PicPath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.Questions).HasMaxLength(1000);

                entity.Property(e => e.QuickCode).HasMaxLength(100);

                entity.Property(e => e.ScDate).HasColumnType("date");

                entity.Property(e => e.Sex).HasMaxLength(1);

                entity.Property(e => e.Sp)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SpType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.SpValidDate).HasColumnType("date");

                entity.Property(e => e.SpatNo)
                    .HasColumnName("SPatNo")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.TelC)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TelH)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Vip)
                    .HasColumnName("VIP")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Zip1)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Zip2)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserNo);

                entity.HasIndex(e => new { e.UserNo, e.Enable })
                    .HasName("idx_UserNO_Enable");

                entity.Property(e => e.UserNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Addr1).HasMaxLength(60);

                entity.Property(e => e.Addr2).HasMaxLength(60);

                entity.Property(e => e.AdoctorId)
                    .HasColumnName("ADoctorID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ApplyDoctor).HasMaxLength(25);

                entity.Property(e => e.ArrDate).HasColumnType("date");

                entity.Property(e => e.Birth).HasColumnType("date");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DealHistory)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1,1,1,1,1,1,1,1,1,1,1,1,1,1,1')");

                entity.Property(e => e.DeleteDate).HasColumnType("datetime");

                entity.Property(e => e.DeleteUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorCardNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorDetail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DrugHistory)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.EmpCard)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Enable).HasDefaultValueSql("((1))");

                entity.Property(e => e.GoogleAccess).HasMaxLength(500);

                entity.Property(e => e.GoogleAuthen).HasDefaultValueSql("((0))");

                entity.Property(e => e.GoogleIssued).HasMaxLength(20);

                entity.Property(e => e.GoogleNewWork)
                    .HasColumnName("googleNewWork")
                    .HasMaxLength(300);

                entity.Property(e => e.GoogleRefresh).HasMaxLength(500);

                entity.Property(e => e.GoogleSynC)
                    .HasColumnName("googleSynC")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GoogleSynCtime)
                    .HasColumnName("googleSynCTime")
                    .HasDefaultValueSql("((5))");

                entity.Property(e => e.GoogleTokenType).HasMaxLength(50);

                entity.Property(e => e.Googlemail)
                    .HasColumnName("googlemail")
                    .HasMaxLength(100);

                entity.Property(e => e.Googlepass)
                    .HasColumnName("googlepass")
                    .HasMaxLength(100);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Is14)
                    .HasColumnName("is14")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Is15)
                    .HasColumnName("is15")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Is16)
                    .HasColumnName("is16")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsClinicOwner)
                    .HasColumnName("isClinicOwner")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsFdi)
                    .HasColumnName("isFDI")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsGenUpload).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsNoRepChkCode)
                    .HasColumnName("isNoRepChkCode")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsNurseTeach).HasColumnName("isNurseTeach");

                entity.Property(e => e.IsOralPmd)
                    .HasColumnName("isOralPMD")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsReSmoke).HasColumnName("isReSmoke");

                entity.Property(e => e.IsTourMount)
                    .HasColumnName("isTourMount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsUserLogin)
                    .HasColumnName("isUserLogin")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsXerostomia)
                    .HasColumnName("isXerostomia")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Layout)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveDate).HasColumnType("date");

                entity.Property(e => e.License)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MarkColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('1,1,1,1,1,1,1,1,1,1,1')");

                entity.Property(e => e.Moble1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Moble2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(50);

                entity.Property(e => e.OrderDoctor).HasMaxLength(500);

                entity.Property(e => e.OrderViewDay)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((7))");

                entity.Property(e => e.OrderViewSec)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.Pass)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PhraseValue).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhraseValue1).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhraseValue2).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhraseValue3).HasDefaultValueSql("((0))");

                entity.Property(e => e.QueryPat)
                    .HasColumnType("numeric(1, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Role)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Skill).HasMaxLength(20);

                entity.Property(e => e.Tel1)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserImg1)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserImg2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(25);

                entity.Property(e => e.Zip1)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Zip2)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });
        }
    }
}
