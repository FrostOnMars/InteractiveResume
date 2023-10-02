//namespace InteractiveResume.Model;
//using Microsoft.EntityFrameworkCore;

//public class DbContext :DataSources
//{
//    public DbSet<AcademicEducation> AcademicEducations { get; set; }
//    public DbSet<SchoolName> Schools { get; set; }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<AcademicEducation>()
//            .HasOne(a => a.School)
//            .WithMany()
//            .HasForeignKey(a => a.SchoolID);

//        modelBuilder.Entity<Achievement>()
//            .HasOne(a => a.AcademicEducation)
//            .WithMany(e => e.Achievements)
//            .HasForeignKey(a => a.SchoolID);

//        modelBuilder.Entity<AcademicEducation>()
//            .HasMany(e => e.ProfessionalEducations)
//            .WithOne(p => p.AcademicEducation)
//            .HasForeignKey(p => p.AcademicEducationId);
//    }
//}