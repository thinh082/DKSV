using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DKSV.Models.Entities;

public partial class myContext : DbContext
{
    public myContext()
    {
    }

    public myContext(DbContextOptions<myContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DangKy> DangKies { get; set; }

    public virtual DbSet<GiangVien> GiangViens { get; set; }

    public virtual DbSet<HocKy> HocKies { get; set; }

    public virtual DbSet<LopHoc> LopHocs { get; set; }

    public virtual DbSet<MonHoc> MonHocs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<SinhVien> SinhViens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DangKy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DangKy__3214EC07ABE33B1A");

            entity.ToTable("DangKy");

            entity.HasIndex(e => new { e.IdLop, e.IdSinhVien }, "UQ_DangKy").IsUnique();

            entity.Property(e => e.Diem).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.NgayDangKy).HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("enrolled");

            entity.HasOne(d => d.IdLopNavigation).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.IdLop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKy__IdLop__4316F928");

            entity.HasOne(d => d.IdSinhVienNavigation).WithMany(p => p.DangKies)
                .HasForeignKey(d => d.IdSinhVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DangKy__IdSinhVi__440B1D61");
        });

        modelBuilder.Entity<GiangVien>(entity =>
        {
            entity.HasKey(e => e.IdNguoiDung).HasName("PK__GiangVie__FEE82D40773E8BED");

            entity.ToTable("GiangVien");

            entity.HasIndex(e => e.MaGv, "UQ__GiangVie__2725AEF21E7AD9BD").IsUnique();

            entity.Property(e => e.IdNguoiDung).ValueGeneratedNever();
            entity.Property(e => e.HocVi).HasMaxLength(50);
            entity.Property(e => e.Khoa).HasMaxLength(120);
            entity.Property(e => e.MaGv)
                .HasMaxLength(20)
                .HasColumnName("MaGV");

            entity.HasOne(d => d.IdNguoiDungNavigation).WithOne(p => p.GiangVien)
                .HasForeignKey<GiangVien>(d => d.IdNguoiDung)
                .HasConstraintName("FK__GiangVien__IdNgu__2D27B809");
        });

        modelBuilder.Entity<HocKy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HocKy__3214EC07EF301C2C");

            entity.ToTable("HocKy");

            entity.HasIndex(e => e.MaHocKy, "UQ__HocKy__1EB551119552B599").IsUnique();

            entity.Property(e => e.MaHocKy).HasMaxLength(20);
        });

        modelBuilder.Entity<LopHoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LopHoc__3214EC07E8C3C525");

            entity.ToTable("LopHoc");

            entity.HasIndex(e => new { e.IdMonHoc, e.IdHocKy, e.MaLop }, "UQ_Lop").IsUnique();

            entity.Property(e => e.GioBatDau).HasPrecision(0);
            entity.Property(e => e.GioKetThuc).HasPrecision(0);
            entity.Property(e => e.MaLop).HasMaxLength(10);
            entity.Property(e => e.Phong).HasMaxLength(50);
            entity.Property(e => e.SiSoToiDa).HasDefaultValue(50);
            entity.Property(e => e.Thu)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.HasOne(d => d.IdGiangVienNavigation).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.IdGiangVien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHoc__IdGiangV__3D5E1FD2");

            entity.HasOne(d => d.IdHocKyNavigation).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.IdHocKy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHoc__IdHocKy__3C69FB99");

            entity.HasOne(d => d.IdMonHocNavigation).WithMany(p => p.LopHocs)
                .HasForeignKey(d => d.IdMonHoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LopHoc__IdMonHoc__3B75D760");
        });

        modelBuilder.Entity<MonHoc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonHoc__3214EC0713F504C5");

            entity.ToTable("MonHoc");

            entity.HasIndex(e => e.MaMon, "UQ__MonHoc__3A5B29A9A40CFD3D").IsUnique();

            entity.Property(e => e.MaMon).HasMaxLength(20);
            entity.Property(e => e.TenMon).HasMaxLength(200);
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NguoiDun__3214EC076E0A8865");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__A9D1053489446A2E").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.HoTen).HasMaxLength(120);
            entity.Property(e => e.IsGiaoVien).HasColumnName("isGiaoVien");
            entity.Property(e => e.IsSinhVien).HasColumnName("isSinhVien");
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<SinhVien>(entity =>
        {
            entity.HasKey(e => e.IdNguoiDung).HasName("PK__SinhVien__FEE82D40A74FDF26");

            entity.ToTable("SinhVien");

            entity.HasIndex(e => e.MaSv, "UQ__SinhVien__2725081B68B84646").IsUnique();

            entity.Property(e => e.IdNguoiDung).ValueGeneratedNever();
            entity.Property(e => e.MaSv)
                .HasMaxLength(20)
                .HasColumnName("MaSV");
            entity.Property(e => e.Nganh).HasMaxLength(120);

            entity.HasOne(d => d.IdNguoiDungNavigation).WithOne(p => p.SinhVien)
                .HasForeignKey<SinhVien>(d => d.IdNguoiDung)
                .HasConstraintName("FK__SinhVien__IdNguo__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
