using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Models
{
    public partial class ProjectContext : DbContext
    {
        public ProjectContext()
        {
        }

        public ProjectContext(DbContextOptions<ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Expert> Experts { get; set; }
        public virtual DbSet<Recommend> Recommends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.name)
                    .HasMaxLength(80)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.cityId).HasColumnName("cityId");

                entity.Property(e => e.email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                //entity.Property(e => e.address)
                //    .HasMaxLength(150)
                //    .HasColumnName("address");

                entity.Property(e => e.imgUrl)
                    .HasMaxLength(150)
                    .HasColumnName("imgUrl");

                entity.Property(e => e.userPassword).HasColumnName("userPassword");

                entity.Property(e => e.userName)
                    .HasMaxLength(100)
                    .HasColumnName("userName");

                entity.Property(e => e.userType)
                    .HasColumnName("userType");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.cityId)
                    .HasConstraintName("FK__User__cityId__5DCAEF64").OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.subName).HasMaxLength(50).HasColumnName("subName");
                entity.Property(e => e.parent).HasColumnName("parent");
            });


            modelBuilder.Entity<Meeting>(entity =>
               {
                   entity.Property(e => e.id).HasColumnName("id");
                   entity.Property(e => e.date).HasColumnType("date").HasColumnName("date");
                   entity.Property(e => e.isApproved).HasColumnName("isApproved");

                   entity.Property(e => e.title).HasMaxLength(150).HasColumnName("title");

                   entity.Property(e => e.time).HasColumnName("time");

                   entity.Property(e => e.content).HasMaxLength(1500).HasColumnName("content");



                   entity.Property(e => e.idProf).HasColumnName("idProf");
                   entity.Property(e => e.idUser).HasColumnName("idUser");


                   entity.HasOne(d => d.User)
                       .WithMany(p => p.Meetings)
                       .HasForeignKey(d => d.idUser)
                       .HasConstraintName("FK__Meetings__idUser__59063A47").OnDelete(DeleteBehavior.Restrict);

                   entity.HasOne(d => d.Expert)
                       .WithMany(p => p.Meetings)
                       .HasForeignKey(d => d.idProf)
                       .HasConstraintName("FK__Meetings__idProf__5812160E").OnDelete(DeleteBehavior.Restrict);
               });

            modelBuilder.Entity<Expert>(entity =>
            {
                entity.ToTable("Expert");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.imgUrl).HasColumnName("imgUrl");

                entity.Property(e => e.phone).HasColumnName("phone");

                entity.Property(e => e.userName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.Property(e => e.description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.email)
                    .HasMaxLength(50)
                    .HasColumnName("email");


                entity.Property(e => e.userPassword)
                    .HasMaxLength(10)
                    .HasColumnName("userPassword");

                entity.Property(e => e.businessName)
                    .HasMaxLength(50)
                    .HasColumnName("businessName");

                entity.Property(e => e.firstName).HasMaxLength(50)
                    .HasColumnName("firstName");

                entity.Property(e => e.lastName)
                    .HasMaxLength(50)
                    .HasColumnName("lastName");
                entity.Property(e => e.enable)
                .HasColumnName("enable");
                entity.Property(e => e.proSubject)
             .HasColumnName("proSubject");

                entity.Property(e => e.cityId)
                                      .HasColumnName("cityId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Experts)
                    .HasForeignKey(d => d.proSubject)
                    .HasConstraintName("FK__Expert__proSubject__619B8048").OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.City)
                  .WithMany(p => p.Experts)
                  .HasForeignKey(d => d.cityId)
                  .HasConstraintName("FK__Expert__cityId__619B8048").OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<Recommend>(entity =>
            {
                entity.ToTable("Recommend");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.userId).HasColumnName("userId");

                entity.Property(e => e.content)
                    .HasMaxLength(300)
                    .HasColumnName("content");

                entity.Property(e => e.profId).HasColumnName("profId");

                entity.Property(e => e.isApproved).HasColumnName("isApproved");

                entity.Property(e => e.stars).HasColumnName("stars");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recommends)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("FK__Recommend__userId__5BE2A6F2").OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Expert)
                    .WithMany(p => p.Recommends)
                    .HasForeignKey(d => d.profId)
                    .HasConstraintName("FK__Recommend__profId__5AEE82B9").OnDelete(DeleteBehavior.Restrict);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
