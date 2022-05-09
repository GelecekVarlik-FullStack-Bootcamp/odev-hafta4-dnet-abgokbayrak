using BMS.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BMS.Dal.Concrete.EntityFramework.Context
{
    public partial class BmsContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public BmsContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public BmsContext(DbContextOptions<BmsContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<Authority> Authorities { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestMessage> RequestMessages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.ToTable("Authority");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<DepartmentSubject>(entity =>
            {
                entity.ToTable("DepartmentSubject");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentSubjects)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentSubject_Department");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AuthorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Authority");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Context)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Department");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Request_Employee");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_Request_Priority");

                entity.HasOne(d => d.RequestSubject)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_RequestSubject");

                entity.HasOne(d => d.Requester)
                    .WithMany(p => p.RequestRequesters)
                    .HasForeignKey(d => d.RequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Employee1");
            });

            modelBuilder.Entity<RequestMessage>(entity =>
            {
                entity.ToTable("RequestMessage");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestMessages)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestMessage_RequestMessage");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.RequestMessages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequestMessage_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
