using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Handee.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Catigory> Catigories { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Testamonial> Testamonials { get; set; }
        public virtual DbSet<Userlogin> Userlogins { get; set; }
        public virtual DbSet<Userr> Userrs { get; set; }
        public virtual DbSet<Visa> Visas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR16_User31;PASSWORD=Test321;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR16_USER31")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<About>(entity =>
            {
                entity.HasKey(e => e.Hid)
                    .HasName("SYS_C00305804");

                entity.ToTable("ABOUT");

                entity.Property(e => e.Hid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("BOOK");

                entity.Property(e => e.Bookid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BOOKID");

                entity.Property(e => e.Anotherinfo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ANOTHERINFO");

                entity.Property(e => e.Bookdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BOOKDESC");

                entity.Property(e => e.Bookname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BOOKNAME");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRICE");
            });

            modelBuilder.Entity<Catigory>(entity =>
            {
                entity.HasKey(e => e.Catid)
                    .HasName("SYS_C00281447");

                entity.ToTable("CATIGORY");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATID");

                entity.Property(e => e.Catname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.Hid)
                    .HasName("SYS_C00305802");

                entity.ToTable("CONTRACT");

                entity.Property(e => e.Hid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Num)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("NUM");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("SYS_C00281949");

                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.Empid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EMPID");

                entity.Property(e => e.Empname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMPNAME");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.HasKey(e => e.Hid)
                    .HasName("SYS_C00305800");

                entity.ToTable("HOME");

                entity.Property(e => e.Hid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Catid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Isavilable)
                    .HasPrecision(1)
                    .HasColumnName("ISAVILABLE");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Cat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Catid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRODUCT_FK1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281441");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("SYS_C00281443");

                entity.ToTable("SALE");

                entity.Property(e => e.Sid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Datesold)
                    .HasColumnType("DATE")
                    .HasColumnName("DATESOLD");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281444");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281445");
            });

            modelBuilder.Entity<Testamonial>(entity =>
            {
                entity.HasKey(e => e.Tid)
                    .HasName("SYS_C00305796");

                entity.ToTable("TESTAMONIAL");

                entity.Property(e => e.Tid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TID");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testamonials)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ACCOUNT");
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.ToTable("USERLOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.Property(e => e.Usernsme)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNSME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281435");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlogins)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281434");
            });

            modelBuilder.Entity<Userr>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00281430");

                entity.ToTable("USERR");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Userrs)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281431");
            });

            modelBuilder.Entity<Visa>(entity =>
            {
                entity.ToTable("VISA");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Exp)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("EXP");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                entity.Property(e => e.Pin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PIN");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Visas)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C00281438");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
