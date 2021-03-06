USE [master]
GO
/****** Object:  Database [QuanLyShopHoa]    Script Date: 09-Apr-17 21:01:16 ******/
CREATE DATABASE [QuanLyShopHoa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyShopHoa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\QuanLyShopHoa.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyShopHoa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\QuanLyShopHoa_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyShopHoa] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyShopHoa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyShopHoa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyShopHoa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyShopHoa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyShopHoa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyShopHoa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyShopHoa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyShopHoa] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyShopHoa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyShopHoa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyShopHoa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyShopHoa] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyShopHoa', N'ON'
GO
USE [QuanLyShopHoa]
GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_Delete]
@IDHoaDon VARCHAR(50)
AS
BEGIN
	DELETE ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Delete_ByIDSanPham]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_Delete_ByIDSanPham]
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--///////////////////////////// STORE ChiTietHoaDon

CREATE PROC [dbo].[sp_ChiTietHoaDon_Insert]
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT,
@DonGia FLOAT,
@IDDonViTinh INT
AS
BEGIN
	INSERT INTO ChiTietHoaDon ( IDHoaDon , IDSanPham , SoLuong , DonGia, IDDonViTinh )
	VALUES  ( @IDHoaDon , @IDSanPham , @SoLuong , @DonGia, @IDDonViTinh )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT HoaDon.IDKhachHang, HoTen, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan
	FROM ChiTietHoaDon, SanPham, HoaDon, KhachHang
	WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
		GROUP BY HoaDon.IDKhachHang, HoTen
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByMonth]
AS
BEGIN
	SELECT HoaDon.IDKhachHang, HoTen, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan
	FROM ChiTietHoaDon, SanPham, HoaDon, KhachHang
	WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND MONTH(NgayLap) = MONTH(GETDATE())
		GROUP BY HoaDon.IDKhachHang, HoTen
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_KHStatisticLN_ByWeek]
AS
BEGIN
	SELECT HoaDon.IDKhachHang, HoTen, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan
	FROM ChiTietHoaDon, SanPham, HoaDon, KhachHang
	WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
		GROUP BY HoaDon.IDKhachHang, HoTen
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_Select_ByID]
@IDHoaDon VARCHAR(50)
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham, ChiTietHoaDon.SoLuong, DonGia, (ChiTietHoaDon.SoLuong * DonGia) AS TongTien, TenDonViTinh
	FROM ChiTietHoaDon, SanPham, DonViTinh WHERE IDHoaDon = @IDHoaDon AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham AND ChiTietHoaDon.IDDonViTinh = DonViTinh.IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Select_ByIDSanPham]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROC [dbo].[sp_ChiTietHoaDon_Select_ByIDSanPham]
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticKH_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticKH_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDKhachHang) AS SoKhachHang, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticKH_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticKH_ByMonth]
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDKhachHang) AS SoKhachHang, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticKH_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticKH_ByWeek]
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDKhachHang) AS SoKhachHang, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticLN_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticLN_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
SELECT ChiTietHoaDon.IDSanPham, TenSanPham, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan, CONVERT(Decimal(4,2),((SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) * 100) / (SUM(ChiTietHoaDon.SoLuong * DonGia)), 3) AS TiSuat
FROM ChiTietHoaDon, SanPham, HoaDon
WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
	AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
	AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticLN_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticLN_ByMonth]
AS
BEGIN
SELECT ChiTietHoaDon.IDSanPham, TenSanPham, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan, CONVERT(Decimal(4,2),((SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) * 100) / (SUM(ChiTietHoaDon.SoLuong * DonGia)), 3) AS TiSuat
FROM ChiTietHoaDon, SanPham, HoaDon
WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
	AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
	AND MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticLN_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticLN_ByWeek]
AS
BEGIN
SELECT ChiTietHoaDon.IDSanPham, TenSanPham, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia) AS DoanhThu, SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon, (SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) AS LoiNhuan, CONVERT(Decimal(4,2),((SUM(ChiTietHoaDon.SoLuong * DonGia) - SUM(ChiTietHoaDon.SoLuong * GiaVon)) * 100) / (SUM(ChiTietHoaDon.SoLuong * DonGia)), 3) AS TiSuat
FROM ChiTietHoaDon, SanPham, HoaDon
WHERE ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
	AND ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
	AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticNV_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticNV_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDNhanVien) AS SoNhanVien, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticNV_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticNV_ByMonth]
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDNhanVien) AS SoNhanVien, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_StatisticNV_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_ChiTietHoaDon_StatisticNV_ByWeek]
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham,  COUNT(DISTINCT HoaDon.IDNhanVien) AS SoNhanVien, SUM(ChiTietHoaDon.SoLuong) AS SoLuongBan, SUM(ChiTietHoaDon.SoLuong * DonGia)  AS DoanhThu 
	FROM dbo.HoaDon, dbo.ChiTietHoaDon, dbo.SanPham
	WHERE ChiTietHoaDon.IDHoaDon = HoaDon.IDHoaDon
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY ChiTietHoaDon.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietHoaDon_Update_Quantity]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietHoaDon_Update_Quantity]
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE ChiTietHoaDon SET SoLuong = @SoLuong WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_Delete]
@IDNhapHang VARCHAR(50)
AS
BEGIN
	DELETE ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Delete_ByIDSanPham]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_Delete_ByIDSanPham]
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--///////////////////////////// STORE ChiTietNhapHang

CREATE PROC [dbo].[sp_ChiTietNhapHang_Insert]
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT,
@DonGia FLOAT,
@IDDonViTinh INT
AS
BEGIN
	INSERT INTO ChiTietNhapHang ( IDNhapHang , IDSanPham , SoLuong , DonGia, IDDonViTinh )
	VALUES  ( @IDNhapHang , @IDSanPham , @SoLuong , @DonGia, @IDDonViTinh )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT NhapHang.IDNhaCungCap, TenNhaCungCap, SUM(TongTien) AS TongTien, SUM(SoLuongSanPham) AS SoLuong FROM NhapHang, NhaCungCap
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND NgayNhap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY NhapHang.IDNhaCungCap, TenNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByMonth]
AS
BEGIN
	SELECT NhapHang.IDNhaCungCap, TenNhaCungCap, SUM(TongTien) AS TongTien, SUM(SoLuongSanPham) AS SoLuong FROM NhapHang, NhaCungCap
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND MONTH(NgayNhap) = MONTH(GETDATE())
	GROUP BY NhapHang.IDNhaCungCap, TenNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_NCCStatistic_ByWeek]
AS
BEGIN
	SELECT NhapHang.IDNhaCungCap, TenNhaCungCap, SUM(TongTien) AS TongTien, SUM(SoLuongSanPham) AS SoLuong FROM NhapHang, NhaCungCap
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
	AND NgayNhap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY NhapHang.IDNhaCungCap, TenNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_Select_ByID]
@IDNhapHang VARCHAR(50)
AS
BEGIN
	SELECT ChiTietNhapHang.IDSanPham, TenSanPham, ChiTietNhapHang.SoLuong, DonGia, (ChiTietNhapHang.SoLuong * DonGia) AS TongTien, TenDonViTinh FROM ChiTietNhapHang, SanPham, DonViTinh WHERE IDNhapHang = @IDNhapHang AND ChiTietNhapHang.IDSanPham = SanPham.IDSanPham AND ChiTietNhapHang.IDDonViTinh = DonViTinh.IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Select_ByIDSanPham]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_Select_ByIDSanPham]
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT ChiTietNhapHang.IDSanPham, TenSanPham, COUNT(DISTINCT NhapHang.IDNhaCungCap) AS SoLuongNCC, SUM(ChiTietNhapHang.SoLuong) AS SoLuongSP, SUM(ChiTietNhapHang.SoLuong * DonGia) AS GiaTri
	FROM dbo.ChiTietNhapHang, dbo.NhapHang, dbo.SanPham
	WHERE ChiTietNhapHang.IDNhapHang = NhapHang.IDNhapHang
		AND ChiTietNhapHang.IDSanPham = SanPham.IDSanPham
		AND NgayNhap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY ChiTietNhapHang.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByMonth]
AS
BEGIN
	SELECT ChiTietNhapHang.IDSanPham, TenSanPham, COUNT(DISTINCT NhapHang.IDNhaCungCap) AS SoLuongNCC, SUM(ChiTietNhapHang.SoLuong) AS SoLuongSP, SUM(ChiTietNhapHang.SoLuong * DonGia) AS GiaTri
	FROM dbo.ChiTietNhapHang, dbo.NhapHang, dbo.SanPham
	WHERE ChiTietNhapHang.IDNhapHang = NhapHang.IDNhapHang
		AND ChiTietNhapHang.IDSanPham = SanPham.IDSanPham
		AND MONTH(NgayNhap) = MONTH(GETDATE())
	GROUP BY ChiTietNhapHang.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO



CREATE PROC [dbo].[sp_ChiTietNhapHang_StatisticNCC_ByWeek]
AS
BEGIN
	SELECT ChiTietNhapHang.IDSanPham, TenSanPham, COUNT(DISTINCT NhapHang.IDNhaCungCap) AS SoLuongNCC, SUM(ChiTietNhapHang.SoLuong) AS SoLuongSP, SUM(ChiTietNhapHang.SoLuong * DonGia) AS GiaTri
	FROM dbo.ChiTietNhapHang, dbo.NhapHang, dbo.SanPham
	WHERE ChiTietNhapHang.IDNhapHang = NhapHang.IDNhapHang
		AND ChiTietNhapHang.IDSanPham = SanPham.IDSanPham
		AND NgayNhap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY ChiTietNhapHang.IDSanPham, TenSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChiTietNhapHang_Update_Quantity]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChiTietNhapHang_Update_Quantity]
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE ChiTietNhapHang SET SoLuong = @SoLuong WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChucNang_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE ChucNang

CREATE PROC [dbo].[sp_ChucNang_Select_All]
AS
BEGIN
	SELECT * FROM ChucNang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_ChucNang_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_ChucNang_Select_ByID]
@IDChucNang VARCHAR(10)
AS
BEGIN
	SELECT * FROM dbo.ChucNang WHERE IDChucNang = @IDChucNang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DonViTinh_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_DonViTinh_Delete]
@IDDonViTinh INT
AS
BEGIN
	DELETE DonViTinh WHERE IDDonViTinh = @IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DonViTinh_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE DonViTinh

CREATE PROC [dbo].[sp_DonViTinh_Insert]
@TenDonViTinh NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	INSERT INTO DonViTinh ( TenDonViTinh, GhiChu )
	VALUES  ( @TenDonViTinh, @GhiChu ) 
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DonViTinh_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_DonViTinh_Select_All]
AS
BEGIN
	SELECT * FROM DonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DonViTinh_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_DonViTinh_Select_ByID]
@IDDonViTinh INT
AS
BEGIN
	SELECT * FROM DonViTinh WHERE IDDonViTinh = @IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DonViTinh_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_DonViTinh_Update]
@IDDonViTinh INT,
@TenDonViTinh NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
UPDATE DonViTinh SET TenDonViTinh = @TenDonViTinh, GhiChu = @GhiChu WHERE IDDonViTinh = @IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_BHStatistic_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[sp_HoaDon_BHStatistic_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT NgayLap, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonBH(NgayLap) AS GiaVon, (SUM(TongTien) - dbo.TinhTongGiaVonBH(NgayLap)) AS LoiNhuan
	FROM HoaDon
	WHERE NgayLap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY NgayLap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_BHStatistic_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_BHStatistic_ByMonth]
AS
BEGIN
	SELECT NgayLap, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonBH(NgayLap) AS GiaVon, (SUM(TongTien) - dbo.TinhTongGiaVonBH(NgayLap)) AS LoiNhuan
	FROM HoaDon
	WHERE MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY NgayLap
END


GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_BHStatistic_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_HoaDon_BHStatistic_ByWeek]
AS
BEGIN
	SELECT NgayLap, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonBH(NgayLap) AS GiaVon, (SUM(TongTien) - dbo.TinhTongGiaVonBH(NgayLap)) AS LoiNhuan
	FROM HoaDon
	WHERE NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY NgayLap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietBanHangTheoThoiGian]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietBanHangTheoThoiGian]
@NgayLap DATE
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, KhachHang.HoTen AS TenKhachHang, NhanVien.HoTen AS TenNhanVien, TongTien, SoLuongSanPham 
	FROM HoaDon, NhanVien, KhachHang
	WHERE NgayLap = @NgayLap
		AND HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Ngay]
@IDKhachHang VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDKhachHang = @IDKhachHang
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Thang]
@IDKhachHang VARCHAR(20)
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDKhachHang = @IDKhachHang
		AND MONTH(NgayLap) = MONTH(GETDATE())
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Tuan]
@IDKhachHang VARCHAR(20)
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDKhachHang = @IDKhachHang
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Ngay]
@IDNhanVien VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDNhanVien = @IDNhanVien
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Thang]
@IDNhanVien VARCHAR(20)
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDNhanVien = @IDNhanVien
		AND MONTH(NgayLap) = MONTH(GETDATE())
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietNhanVienTheoBanHang_Tuan]
@IDNhanVien VARCHAR(20)
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien
	FROM HoaDon
	WHERE IDNhanVien = @IDNhanVien
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Ngay]
@IDSanPham VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT HoaDon.IDKhachHang, KhachHang.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, KhachHang
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
		GROUP BY HoaDon.IDKhachHang, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Thang]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT HoaDon.IDKhachHang, KhachHang.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, KhachHang
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND MONTH(NgayLap) = MONTH(GETDATE())
		GROUP BY HoaDon.IDKhachHang, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoKhachHang_Tuan]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT HoaDon.IDKhachHang, KhachHang.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, KhachHang
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
		GROUP BY HoaDon.IDKhachHang, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoLoiNhuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoLoiNhuan]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT IDHoaDon, NgayLap, NgayGiao, SoLuongSanPham, TongTien, KhachHang.HoTen AS TenKhachHang, NhanVien.HoTen AS TenNhanVien
	FROM HoaDon, NhanVien, KhachHang
	WHERE IDHoaDon IN (
		SELECT IDHoaDon
		FROM dbo.ChiTietHoaDon
		WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
		ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Ngay]
@IDSanPham VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT HoaDon.IDNhanVien, NhanVien.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, NhanVien
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
		GROUP BY HoaDon.IDNhanVien, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Thang]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT HoaDon.IDNhanVien, NhanVien.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, NhanVien
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND MONTH(NgayLap) = MONTH(GETDATE())
		GROUP BY HoaDon.IDNhanVien, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_ChiTietSanPhamTheoNhanVien_Tuan]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT HoaDon.IDNhanVien, NhanVien.HoTen, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM HoaDon, NhanVien
	WHERE IDHoaDon IN (
			SELECT IDHoaDon
			FROM dbo.ChiTietHoaDon
			WHERE IDSanPham = @IDSanPham)
		AND HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
		GROUP BY HoaDon.IDNhanVien, HoTen
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_Delete]
@IDHoaDon VARCHAR(50)
AS
BEGIN
	DELETE HoaDon WHERE IDHoaDon = @IDHoaDon
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--///////////////////////////// STORE HoaDon

CREATE PROC [dbo].[sp_HoaDon_Insert]
@IDHoaDon VARCHAR(50),
@NgayLap DATE,
@IDNhanVien VARCHAR(50),
@IDKhachHang VARCHAR(50),
@TenNguoiNhan NVARCHAR(50),
@DiaChiNguoiNhan NVARCHAR(255),
@DienThoaiNguoiNhan NVARCHAR(20),
@NgayGiao DATE,
@TrangThaiThanhToan BIT,
@TrangThaiGiaoHang BIT,
@GhiChu NVARCHAR(1000),
@SoLuongSanPham INT,
@TongTien INT
AS
BEGIN
	INSERT INTO HoaDon ( IDHoaDon , NgayLap , IDNhanVien , IDKhachHang, TenNguoiNhan, DiaChiNguoiNhan, DienThoaiNguoiNhan, NgayGiao, TrangThaiThanhToan, TrangThaiGiaoHang, GhiChu, SoLuongSanPham, TongTien )
	VALUES  ( @IDHoaDon , @NgayLap , @IDNhanVien , @IDKhachHang, @TenNguoiNhan, @DiaChiNguoiNhan, @DienThoaiNguoiNhan, @NgayGiao, @TrangThaiThanhToan, @TrangThaiGiaoHang, @GhiChu, @SoLuongSanPham, @TongTien )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_NVStatistic_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_NVStatistic_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT HoaDon.IDNhanVien, HoTen, SUM(SoLuongSanPham) AS SoLuong, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonNVTheoNgay(HoaDon.IDNhanVien, @NgayDau, @NgayCuoi) AS GiaVon, (SUM(TongTien) -  dbo.TinhTongGiaVonNVTheoNgay(HoaDon.IDNhanVien, @NgayDau, @NgayCuoi)) AS LoiNhuan
	FROM HoaDon, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
	GROUP BY HoaDon.IDNhanVien, HoTen
	ORDER BY DoanhThu DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_NVStatistic_ByMonth]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_NVStatistic_ByMonth]
AS
BEGIN
	SELECT HoaDon.IDNhanVien, HoTen, SUM(SoLuongSanPham) AS SoLuong, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonNVTheoThang(HoaDon.IDNhanVien, GETDATE()) AS GiaVon, (SUM(TongTien) -  dbo.TinhTongGiaVonNVTheoThang(HoaDon.IDNhanVien, GETDATE())) AS LoiNhuan
	FROM HoaDon, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY HoaDon.IDNhanVien, HoTen
	ORDER BY DoanhThu DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_NVStatistic_ByWeek]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_HoaDon_NVStatistic_ByWeek]
AS
BEGIN
	SELECT HoaDon.IDNhanVien, HoTen, SUM(SoLuongSanPham) AS SoLuong, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonNVTheoTuan(HoaDon.IDNhanVien, GETDATE()) AS GiaVon, (SUM(TongTien) -  dbo.TinhTongGiaVonNVTheoTuan(HoaDon.IDNhanVien, GETDATE())) AS LoiNhuan
	FROM HoaDon, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
		AND NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY HoaDon.IDNhanVien, HoTen
	ORDER BY DoanhThu DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_Select_All]
AS
BEGIN
	SELECT IDHoaDon, KhachHang.HoTen AS TenKhachHang, KhachHang.DienThoai AS DienThoaiKhachHang, SoLuongSanPham, TongTien, NgayLap, NgayGiao, TrangThaiThanhToan, TrangThaiGiaoHang, HoaDon.GhiChu AS GhiChuHD, NhanVien.HoTen AS TenNhanVien
	FROM HoaDon, KhachHang, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
	AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Select_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_Select_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT IDHoaDon, KhachHang.HoTen AS TenKhachHang, KhachHang.DienThoai AS DienThoaiKhachHang, SoLuongSanPham, TongTien, NgayLap, NgayGiao, TrangThaiThanhToan, TrangThaiGiaoHang, HoaDon.GhiChu AS GhiChuHD, NhanVien.HoTen AS TenNhanVien
	FROM HoaDon, KhachHang, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
	AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
	AND NgayLap BETWEEN @NgayDau AND @NgayCuoi
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_HoaDon_Select_ByID]
@IDHoaDon VARCHAR(50)
AS
BEGIN
	SELECT * FROM HoaDon WHERE IDHoaDon = @IDHoaDon
END

GO
/****** Object:  StoredProcedure [dbo].[sp_HoaDon_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_HoaDon_Update]
@IDHoaDon VARCHAR(50),
@NgayLap DATE,
@IDNhanVien VARCHAR(50),
@IDKhachHang VARCHAR(50),
@TenNguoiNhan NVARCHAR(50),
@DiaChiNguoiNhan NVARCHAR(255),
@DienThoaiNguoiNhan NVARCHAR(20),
@NgayGiao DATE,
@TrangThaiThanhToan BIT,
@TrangThaiGiaoHang BIT,
@GhiChu NVARCHAR(1000),
@SoLuongSanPham INT,
@TongTien INT
AS
BEGIN
	UPDATE HoaDon SET NgayLap = @NgayLap, IDNhanVien = @IDNhanVien, IDKhachHang = @IDKhachHang,  TenNguoiNhan = @TenNguoiNhan, DiaChiNguoiNhan = @DiaChiNguoiNhan, DienThoaiNguoiNhan = @DienThoaiNguoiNhan, NgayGiao = @NgayGiao, TrangThaiThanhToan = @TrangThaiThanhToan, TrangThaiGiaoHang = @TrangThaiGiaoHang, GhiChu = @GhiChu, SoLuongSanPham = @SoLuongSanPham, TongTien = @TongTien WHERE IDHoaDon = @IDHoaDon
END

GO
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_KhachHang_Delete]
@IDKhachHang VARCHAR(50)
AS
BEGIN
	DELETE KhachHang WHERE IDKhachHang = @IDKhachHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--============================= CREATE STORE =============================

--///////////////////////////// STORE KhachHang

CREATE PROC [dbo].[sp_KhachHang_Insert]
@IDKhachHang VARCHAR(50),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	INSERT INTO KhachHang( IDKhachHang , HoTen , DienThoai , DiaChi , NgaySinh , GioiTinh , GhiChu )
	VALUES  ( @IDKhachHang , @HoTen , @DienThoai , @DiaChi , @NgaySinh , @GioiTinh , @GhiChu )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_KhachHang_Select_All]
AS
BEGIN
	SELECT * FROM KhachHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_KhachHang_Select_ByID]
@IDKhachHang VARCHAR(50)
AS
BEGIN
	SELECT * FROM KhachHang WHERE IDKhachHang = @IDKhachHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_KhachHang_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_KhachHang_Update]
@IDKhachHang VARCHAR(50),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	UPDATE KhachHang SET HoTen = @HoTen, DienThoai = @DienThoai, DiaChi = @DiaChi, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, GhiChu = @GhiChu WHERE IDKhachHang = @IDKhachHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_LoaiHang_Delete]
@IDLoaiHang INT
AS
BEGIN
	DELETE LoaiHang WHERE IDLoaiHang = @IDLoaiHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE LoaiHang

CREATE PROC [dbo].[sp_LoaiHang_Insert]
@TenLoaiHang NVARCHAR(100),
@MoTa NVARCHAR(1000),
@TinhTrang BIT
AS
BEGIN
	INSERT INTO LoaiHang ( TenLoaiHang, MoTa, TinhTrang ) VALUES ( @TenLoaiHang, @MoTa, @TinhTrang )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Select_Active]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_LoaiHang_Select_Active]
AS
BEGIN
	SELECT IDLoaiHang, TenLoaiHang FROM LoaiHang WHERE TinhTrang = 1
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_LoaiHang_Select_All]
AS
BEGIN
	SELECT * FROM LoaiHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_LoaiHang_Select_ByID]
@IDLoaiHang INT
AS
BEGIN
	SELECT * FROM LoaiHang WHERE IDLoaiHang = @IDLoaiHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LoaiHang_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_LoaiHang_Update]
@IDLoaiHang INT,
@TenLoaiHang NVARCHAR(100),
@MoTa NVARCHAR(1000),
@TinhTrang BIT
AS
BEGIN
	UPDATE LoaiHang SET TenLoaiHang = @TenLoaiHang, MoTa = @MoTa, TinhTrang = @TinhTrang WHERE IDLoaiHang = @IDLoaiHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhaCungCap_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhaCungCap_Delete]
@IDNhaCungCap VARCHAR(50)
AS
BEGIN
	DELETE NhaCungCap WHERE IDNhaCungCap = @IDNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhaCungCap_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE NhaCungCap

CREATE PROC [dbo].[sp_NhaCungCap_Insert]
@IDNhaCungCap VARCHAR(50),
@TenNhaCungCap NVARCHAR(255),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	INSERT INTO NhaCungCap ( IDNhaCungCap , TenNhaCungCap , DienThoai , DiaChi , GhiChu )
	VALUES  ( @IDNhaCungCap , @TenNhaCungCap , @DienThoai , @DiaChi , @GhiChu )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhaCungCap_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhaCungCap_Select_All]
AS
BEGIN
	SELECT * FROM NhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhaCungCap_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhaCungCap_Select_ByID]
@IDNhaCungCap VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhaCungCap WHERE IDNhaCungCap = @IDNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhaCungCap_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhaCungCap_Update]
@IDNhaCungCap VARCHAR(50),
@TenNhaCungCap NVARCHAR(255),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
UPDATE NhaCungCap SET TenNhaCungCap = @TenNhaCungCap, DienThoai = @DienThoai, DiaChi = @DiaChi, GhiChu = @GhiChu WHERE IDNhaCungCap = @IDNhaCungCap
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Delete]
@IDNhanVien VARCHAR(50)
AS
BEGIN
	DELETE NhanVien WHERE IDNhanVien = @IDNhanVien
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE NhanVien

CREATE PROC [dbo].[sp_NhanVien_Insert]
@IDNhanVien VARCHAR(50),
@TaiKhoan VARCHAR(30),
@MatKhau VARCHAR(100),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@TrangThai BIT,
@IDNhom INT,
@GhiChu NVARCHAR(1000)
AS
BEGIN
	INSERT INTO dbo.NhanVien ( IDNhanVien , TaiKhoan , MatKhau , HoTen , DienThoai , DiaChi , NgaySinh , GioiTinh , TrangThai , IDNhom , GhiChu )
	VALUES ( @IDNhanVien , @TaiKhoan , @MatKhau , @HoTen , @DienThoai , @DiaChi , @NgaySinh , @GioiTinh , @TrangThai , @IDNhom , @GhiChu )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Select_All]
AS
BEGIN
	SELECT * FROM NhanVien AS A, NhomQuyen AS B WHERE A.IDNhom = B.IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Select_ByID]
@IDNhanVien VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhanVien WHERE IDNhanVien = @IDNhanVien
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Select_ByUserName]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Select_ByUserName]
@TaiKhoan VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhanVien WHERE TaiKhoan = @TaiKhoan
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Update]
@IDNhanVien VARCHAR(50),
@MatKhau VARCHAR(100),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@TrangThai BIT,
@IDNhom INT,
@GhiChu NVARCHAR(1000)
AS
BEGIN
	UPDATE NhanVien
	SET MatKhau = @MatKhau, HoTen = @HoTen, DienThoai = @DienThoai, DiaChi = @DiaChi, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, TrangThai = @TrangThai, IDNhom = @IDNhom, GhiChu = @GhiChu
	WHERE IDNhanVien = @IDNhanVien
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhanVien_Update_Password]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhanVien_Update_Password]
@IDNhanVien VARCHAR(50),
@MatKhau VARCHAR(50)
AS
BEGIN
	UPDATE NhanVien SET MatKhau = @MatKhau WHERE IDNhanVien = @IDNhanVien
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Ngay]
@IDNhaCungCap VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT IDNhapHang, NgayNhap, SoLuongSanPham, TongTien
	FROM NhapHang
	WHERE IDNhaCungCap = @IDNhaCungCap
		AND NgayNhap BETWEEN @NgayDau AND @NgayCuoi
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Thang]
@IDNhaCungCap VARCHAR(20)
AS
BEGIN
	SELECT IDNhapHang, NgayNhap, SoLuongSanPham, TongTien
	FROM NhapHang
	WHERE IDNhaCungCap = @IDNhaCungCap
		AND MONTH(NgayNhap) = MONTH(GETDATE())
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_NhapHang_ChiTietNCCTheoNhapHang_Tuan]
@IDNhaCungCap VARCHAR(20)
AS
BEGIN
SELECT IDNhapHang, NgayNhap, SoLuongSanPham, TongTien
	FROM NhapHang
	WHERE IDNhaCungCap = @IDNhaCungCap
		AND NgayNhap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	ORDER BY TongTien DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Ngay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Ngay]
@IDSanPham VARCHAR(20),
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT NhaCungCap.IDNhaCungCap, TenNhaCungCap, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM NhapHang, NhaCungCap
	WHERE IDNhapHang IN (
			SELECT IDNhapHang
			FROM ChiTietNhapHang
			WHERE IDSanPham = @IDSanPham)
		AND NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND NgayNhap BETWEEN @NgayDau AND @NgayCuoi
		GROUP BY NhaCungCap.IDNhaCungCap, TenNhaCungCap
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Thang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Thang]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT NhaCungCap.IDNhaCungCap, TenNhaCungCap, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM NhapHang, NhaCungCap
	WHERE IDNhapHang IN (
			SELECT IDNhapHang
			FROM ChiTietNhapHang
			WHERE IDSanPham = @IDSanPham)
		AND NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND MONTH(NgayNhap) = MONTH(GETDATE())
		GROUP BY NhaCungCap.IDNhaCungCap, TenNhaCungCap
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Tuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_ChiTietSanPhamTheoNCC_Tuan]
@IDSanPham VARCHAR(20)
AS
BEGIN
	SELECT NhaCungCap.IDNhaCungCap, TenNhaCungCap, SUM(SoLuongSanPham) AS SoLuongBan, SUM(TongTien) AS GiaTri
	FROM NhapHang, NhaCungCap
	WHERE IDNhapHang IN (
			SELECT IDNhapHang
			FROM ChiTietNhapHang
			WHERE IDSanPham = @IDSanPham)
		AND NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND NgayNhap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
		GROUP BY NhaCungCap.IDNhaCungCap, TenNhaCungCap
		ORDER BY GiaTri DESC
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_Delete]
@IDNhapHang VARCHAR(50)
AS
BEGIN
	DELETE NhapHang WHERE IDNhapHang = @IDNhapHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE NhapHang

CREATE PROC [dbo].[sp_NhapHang_Insert]
@IDNhapHang VARCHAR(50),
@NgayNhap DATE,
@IDNhaCungCap VARCHAR(50),
@IDNhanVien VARCHAR(50),
@GhiChu NVARCHAR(1000),
@SoLuongSanPham INT,
@TongTien INT
AS
BEGIN
	INSERT INTO NhapHang ( IDNhapHang , NgayNhap , IDNhaCungCap , IDNhanVien , GhiChu, SoLuongSanPham, TongTien )
	VALUES  ( @IDNhapHang , @NgayNhap , @IDNhaCungCap , @IDNhanVien , @GhiChu, @SoLuongSanPham, @TongTien )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_Select_All]
AS
BEGIN
	SELECT IDNhapHang, NgayNhap, TenNhaCungCap, HoTen, NhapHang.GhiChu, SoLuongSanPham, TongTien
	FROM NhapHang, NhaCungCap, NhanVien
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
	AND NhapHang.IDNhanVien = NhanVien.IDNhanVien
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Select_ByDate]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_Select_ByDate]
@NgayDau DATE,
@NgayCuoi DATE
AS
BEGIN
	SELECT IDNhapHang, NgayNhap, TenNhaCungCap, HoTen, NhapHang.GhiChu, SoLuongSanPham, TongTien
	FROM NhapHang, NhaCungCap, NhanVien
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
	AND NhapHang.IDNhanVien = NhanVien.IDNhanVien
	AND NgayNhap BETWEEN @NgayDau AND @NgayCuoi
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_Select_ByID]
@IDNhapHang VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhapHang WHERE IDNhapHang = @IDNhapHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhapHang_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhapHang_Update]
@IDNhapHang VARCHAR(50),
@NgayNhap DATE,
@IDNhaCungCap VARCHAR(50),
@IDNhanVien VARCHAR(50),
@GhiChu NVARCHAR(1000),
@SoLuongSanPham INT,
@TongTien INT
AS
BEGIN
	UPDATE NhapHang SET NgayNhap = @NgayNhap, IDNhaCungCap = @IDNhaCungCap, IDNhanVien = @IDNhanVien, GhiChu = @GhiChu, SoLuongSanPham = @SoLuongSanPham, TongTien = @TongTien WHERE IDNhapHang = @IDNhapHang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_Delete]
@IDNhom INT
AS
BEGIN
	DELETE NhomQuyen WHERE IDNhom = @IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE NhomQuyen

CREATE PROC [dbo].[sp_NhomQuyen_Insert]
@TenNhom NVARCHAR(100), 
@MoTa NVARCHAR(1000)
AS
BEGIN
	INSERT INTO NhomQuyen ( TenNhom, MoTa ) VALUES  ( @TenNhom, @MoTa )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_NotInPhanQuyen]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_NotInPhanQuyen]
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom NOT IN (SELECT IDNhom FROM PhanQuyen)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_Select_All]
AS
BEGIN
	SELECT * FROM NhomQuyen
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_Select_ByID]
@IDNhom INT
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom = @IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_SelectInPhanQuyen]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_SelectInPhanQuyen]
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom IN (SELECT IDNhom FROM PhanQuyen)
END

GO
/****** Object:  StoredProcedure [dbo].[sp_NhomQuyen_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_NhomQuyen_Update]
@IDNhom INT,
@TenNhom NVARCHAR(100), 
@MoTa NVARCHAR(1000)
AS
BEGIN
	UPDATE NhomQuyen SET TenNhom = @TenNhom, MoTa = @MoTa WHERE IDNhom = @IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PhanQuyen_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_PhanQuyen_Delete]
@IDNhom INT
AS
BEGIN
	DELETE PhanQuyen WHERE IDNhom = @IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PhanQuyen_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


--///////////////////////////// STORE PhanQuyen

CREATE PROC [dbo].[sp_PhanQuyen_Insert]
@IDNhom INT, 
@IDChucNang NVARCHAR(10),
@Xem BIT,
@Them BIT,
@Sua BIT,
@Xoa BIT
AS
BEGIN
	INSERT INTO PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( @IDNhom, @IDChucNang, @Xem, @Them, @Sua, @Xoa )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PhanQuyen_Select_ByIDNhom]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROC [dbo].[sp_PhanQuyen_Select_ByIDNhom]
@IDNhom INT
AS
BEGIN
	SELECT ChucNang.IDChucNang, TenChucNang, Xem, Them, Sua, Xoa FROM ChucNang INNER JOIN PhanQuyen ON PhanQuyen.IDChucNang = ChucNang.IDChucNang WHERE IDNhom = @IDNhom
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PhanQuyen_Select_ByIDNhomAndIDChucNang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_PhanQuyen_Select_ByIDNhomAndIDChucNang]
@IDNhom INT,
@IDChucNang VARCHAR(10)
AS
BEGIN
	SELECT * FROM dbo.PhanQuyen WHERE IDNhom = @IDNhom AND IDChucNang = @IDChucNang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_PhanQuyen_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_PhanQuyen_Update]
@IDNhom INT,
@IDChucNang VARCHAR(10),
@Xem BIT,
@Them BIT,
@Sua BIT,
@Xoa BIT
AS
BEGIN
	UPDATE PhanQuyen SET Xem = @Xem, Them = @Them, Sua = @Sua, Xoa = @Xoa WHERE IDNhom = @IDNhom AND IDChucNang = @IDChucNang
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Delete]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Delete]
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE SanPham WHERE IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Insert]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

--///////////////////////////// STORE SanPham

CREATE PROC [dbo].[sp_SanPham_Insert]
@IDSanPham VARCHAR(50),
@TenSanPham NVARCHAR(255),
@GiaVon FLOAT,
@GiaBan FLOAT,
@SoLuong INT,
@Hinh IMAGE,
@MoTa NVARCHAR(1000),
@TrangThai BIT,
@IDNhaCungCap VARCHAR(50),
@IDLoaiHang INT,
@IDDonViTinh INT,
@IDNhanVien VARCHAR(50)
AS
BEGIN
	INSERT INTO SanPham ( IDSanPham , TenSanPham , GiaVon , GiaBan , SoLuong , Hinh , MoTa , TrangThai , IDNhaCungCap , IDLoaiHang , IDDonViTinh, IDNhanVien )
	VALUES  ( @IDSanPham , @TenSanPham , @GiaVon , @GiaBan , @SoLuong , @Hinh , @MoTa , @TrangThai , @IDNhaCungCap , @IDLoaiHang , @IDDonViTinh, @IDNhanVien )
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_All]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_All]
AS
BEGIN
	SELECT * FROM SanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_AllData]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_AllData]
AS
BEGIN
	SELECT IDSanPham, TenSanPham, GiaVon, GiaBan, SoLuong, Hinh, SanPham.MoTa, SanPham.TrangThai, TenLoaiHang, TenNhaCungCap, HoTen, TenDonViTinh FROM SanPham, NhaCungCap, LoaiHang, NhanVien, DonViTinh WHERE SanPham.IDLoaiHang = LoaiHang.IDLoaiHang AND SanPham.IDNhaCungCap = NhaCungCap.IDNhaCungCap AND SanPham.IDNhanVien = NhanVien.IDNhanVien AND SanPham.IDDonViTinh = DonViTinh.IDDonViTinh
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_ByID]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_ByID]
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM SanPham WHERE IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_ByID_Quantity]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_ByID_Quantity]
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM SanPham WHERE IDSanPham = @IDSanPham AND SoLuong > 0
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_ByStatus]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_ByStatus]
AS
BEGIN
	SELECT * FROM SanPham WHERE TrangThai = 1 
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Select_ByStatus_Quantity]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Select_ByStatus_Quantity]
AS
BEGIN
	SELECT * FROM SanPham WHERE TrangThai = 1  AND SoLuong > 0
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_Update]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_Update]
@IDSanPham VARCHAR(50),
@TenSanPham NVARCHAR(255),
@GiaVon FLOAT,
@GiaBan FLOAT,
@SoLuong INT,
@Hinh IMAGE,
@MoTa NVARCHAR(1000),
@TrangThai BIT,
@IDNhaCungCap VARCHAR(50),
@IDLoaiHang INT,
@IDDonViTinh INT,
@IDNhanVien VARCHAR(50)
AS
BEGIN
	UPDATE SanPham SET TenSanPham = @TenSanPham, GiaVon = @GiaVon, GiaBan = @GiaBan, SoLuong = @SoLuong, Hinh = @Hinh, MoTa = @MoTa, TrangThai = @TrangThai, IDNhaCungCap = @IDNhaCungCap, IDLoaiHang = @IDLoaiHang, IDDonViTinh = @IDDonViTinh, IDNhanVien = @IDNhanVien
	WHERE IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_UpdateQuantity]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_UpdateQuantity]
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE SanPham SET SoLuong += @SoLuong WHERE IDSanPham = @IDSanPham
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SanPham_UpdateQuantitySub]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROC [dbo].[sp_SanPham_UpdateQuantitySub]
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE SanPham SET SoLuong -= @SoLuong WHERE IDSanPham = @IDSanPham
END

GO
/****** Object:  UserDefinedFunction [dbo].[F_START_OF_WEEK]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
 
CREATE FUNCTION [dbo].[F_START_OF_WEEK]
(
 @DATE datetime,
 -- Sun = 1, Mon = 2, Tue = 3, Wed = 4
 -- Thu = 5, Fri = 6, Sat = 7
 -- Default to Sunday
 @WEEK_START_DAY int = 1 
)
/*
Find the fisrt date on or before @DATE that matches 
day of week of @WEEK_START_DAY.
*/
returns datetime
as
begin
declare @START_OF_WEEK_DATE datetime
declare @FIRST_BOW datetime
 
-- Check for valid day of week
if @WEEK_START_DAY between 1 and 7
 begin
 -- Find first day on or after 1753/1/1 (-53690)
 -- matching day of week of @WEEK_START_DAY
 -- 1753/1/1 is earliest possible SQL Server date.
 select @FIRST_BOW = convert(datetime,-53690+((@WEEK_START_DAY+5)%7))
 -- Verify beginning of week not before 1753/1/1
 if @DATE >= @FIRST_BOW
 begin
 select @START_OF_WEEK_DATE = 
 dateadd(dd,(datediff(dd,@FIRST_BOW,@DATE)/7)*7,@FIRST_BOW)
 end
 end
 
return @START_OF_WEEK_DATE
 
end
 

GO
/****** Object:  UserDefinedFunction [dbo].[TinhTongGiaVonBH]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[TinhTongGiaVonBH](@NgayLap DATE)
RETURNS FLOAT
AS
BEGIN
	RETURN
	(SELECT SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon
	FROM ChiTietHoaDon, SanPham
	WHERE IDHoaDon IN (SELECT IDHoaDon FROM HoaDon WHERE NgayLap = @NgayLap
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		)
	)
END

GO
/****** Object:  UserDefinedFunction [dbo].[TinhTongGiaVonNVTheoNgay]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[TinhTongGiaVonNVTheoNgay](@IDNhanVien VARCHAR(20),@NgayDau DATE, @NgayCuoi DATE)
RETURNS FLOAT
AS
BEGIN
	RETURN
	(SELECT SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon
	FROM ChiTietHoaDon, SanPham
	WHERE IDHoaDon IN (SELECT IDHoaDon FROM HoaDon WHERE IDNhanVien = @IDNhanVien AND NgayLap BETWEEN @NgayDau AND @NgayCuoi)
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		)
END

GO
/****** Object:  UserDefinedFunction [dbo].[TinhTongGiaVonNVTheoThang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[TinhTongGiaVonNVTheoThang](@IDNhanVien VARCHAR(20), @Date DATE)
RETURNS FLOAT
AS
BEGIN
	RETURN
	(SELECT SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon
	FROM ChiTietHoaDon, SanPham
	WHERE IDHoaDon IN (SELECT IDHoaDon FROM HoaDon WHERE IDNhanVien = @IDNhanVien AND MONTH(NgayLap) = MONTH(@Date))
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		)
END

GO
/****** Object:  UserDefinedFunction [dbo].[TinhTongGiaVonNVTheoTuan]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE FUNCTION [dbo].[TinhTongGiaVonNVTheoTuan](@IDNhanVien VARCHAR(20), @Date DATE)
RETURNS FLOAT
AS
BEGIN
	RETURN
	(SELECT SUM(ChiTietHoaDon.SoLuong * GiaVon) AS TongGiaVon
	FROM ChiTietHoaDon, SanPham
	WHERE IDHoaDon IN (SELECT IDHoaDon FROM HoaDon WHERE IDNhanVien = @IDNhanVien AND NgayLap BETWEEN dbo.F_START_OF_WEEK(@Date,2) AND @Date)
		AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham
		)
END

GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[IDHoaDon] [varchar](50) NOT NULL,
	[IDSanPham] [varchar](50) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[IDDonViTinh] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDHoaDon] ASC,
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChiTietNhapHang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChiTietNhapHang](
	[IDNhapHang] [varchar](50) NOT NULL,
	[IDSanPham] [varchar](50) NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[IDDonViTinh] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhapHang] ASC,
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChucNang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChucNang](
	[IDChucNang] [varchar](10) NOT NULL,
	[TenChucNang] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDChucNang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DonViTinh]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonViTinh](
	[IDDonViTinh] [int] IDENTITY(1,1) NOT NULL,
	[TenDonViTinh] [nvarchar](100) NOT NULL,
	[GhiChu] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDDonViTinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[IDHoaDon] [varchar](50) NOT NULL,
	[NgayLap] [datetime] NOT NULL,
	[IDNhanVien] [varchar](50) NOT NULL,
	[IDKhachHang] [varchar](50) NOT NULL,
	[TenNguoiNhan] [nvarchar](50) NULL,
	[DiaChiNguoiNhan] [nvarchar](255) NULL,
	[DienThoaiNguoiNhan] [nvarchar](20) NULL,
	[NgayGiao] [datetime] NULL,
	[TrangThaiThanhToan] [bit] NULL,
	[TrangThaiGiaoHang] [bit] NULL,
	[GhiChu] [nvarchar](1000) NULL,
	[SoLuongSanPham] [int] NULL,
	[TongTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[IDKhachHang] [varchar](50) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [nvarchar](5) NOT NULL,
	[GhiChu] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoaiHang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHang](
	[IDLoaiHang] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiHang] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](1000) NULL,
	[TinhTrang] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDLoaiHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[IDNhaCungCap] [varchar](50) NOT NULL,
	[TenNhaCungCap] [nvarchar](255) NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[GhiChu] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhaCungCap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhanVien](
	[IDNhanVien] [varchar](50) NOT NULL,
	[TaiKhoan] [varchar](30) NOT NULL,
	[MatKhau] [varchar](100) NOT NULL,
	[HoTen] [nvarchar](50) NOT NULL,
	[DienThoai] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[GioiTinh] [nvarchar](5) NOT NULL,
	[TrangThai] [bit] NOT NULL,
	[IDNhom] [int] NOT NULL,
	[GhiChu] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhapHang]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhapHang](
	[IDNhapHang] [varchar](50) NOT NULL,
	[NgayNhap] [date] NOT NULL,
	[IDNhaCungCap] [varchar](50) NOT NULL,
	[IDNhanVien] [varchar](50) NOT NULL,
	[GhiChu] [nvarchar](1000) NULL,
	[SoLuongSanPham] [int] NULL,
	[TongTien] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhapHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhomQuyen]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomQuyen](
	[IDNhom] [int] IDENTITY(1,1) NOT NULL,
	[TenNhom] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhanQuyen]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhanQuyen](
	[IDNhom] [int] NOT NULL,
	[IDChucNang] [varchar](10) NOT NULL,
	[Xem] [bit] NOT NULL,
	[Them] [bit] NOT NULL,
	[Sua] [bit] NOT NULL,
	[Xoa] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDNhom] ASC,
	[IDChucNang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 09-Apr-17 21:01:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[IDSanPham] [varchar](50) NOT NULL,
	[TenSanPham] [nvarchar](255) NOT NULL,
	[GiaVon] [float] NOT NULL,
	[GiaBan] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[Hinh] [image] NULL,
	[MoTa] [nvarchar](1000) NULL,
	[TrangThai] [bit] NOT NULL,
	[IDNhaCungCap] [varchar](50) NOT NULL,
	[IDLoaiHang] [int] NOT NULL,
	[IDDonViTinh] [int] NOT NULL,
	[IDNhanVien] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'banhang', N'Bán Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'baocao', N'Quản Lý Báo Cáo')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'donvitinh', N'Quản Lý Đơn Vị Tính')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'khachhang', N'Quản Lý khách Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'loaihang', N'Quản Lý Loại Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'nhacungcap', N'Quản Lý Nhà Cung Cấp')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'nhaphang', N'Nhập Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'qlbanhang', N'Quản Lý Bán Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'qlnhaphang', N'Quản Lý Nhập Hàng')
INSERT [dbo].[ChucNang] ([IDChucNang], [TenChucNang]) VALUES (N'sanpham', N'Quản Lý Sản Phẩm')
INSERT [dbo].[KhachHang] ([IDKhachHang], [HoTen], [DienThoai], [DiaChi], [NgaySinh], [GioiTinh], [GhiChu]) VALUES (N'KH000001', N'Nguyễn Văn A', N'0123456789', N'Phạm Ngũ Lão', CAST(0xAB3C0B00 AS Date), N'Nam', N'')
SET IDENTITY_INSERT [dbo].[LoaiHang] ON 

INSERT [dbo].[LoaiHang] ([IDLoaiHang], [TenLoaiHang], [MoTa], [TinhTrang]) VALUES (1, N'Hoa Sinh Nhật', N'Hoa sinh nhật', 1)
INSERT [dbo].[LoaiHang] ([IDLoaiHang], [TenLoaiHang], [MoTa], [TinhTrang]) VALUES (2, N'Hoa Tặng Mẹ', N'', 1)
SET IDENTITY_INSERT [dbo].[LoaiHang] OFF
INSERT [dbo].[NhaCungCap] ([IDNhaCungCap], [TenNhaCungCap], [DienThoai], [DiaChi], [GhiChu]) VALUES (N'NCC000001', N'Nhà cung cấp 1', N'132154544', N'Phạm Ngũ Lão', N'')
INSERT [dbo].[NhanVien] ([IDNhanVien], [TaiKhoan], [MatKhau], [HoTen], [DienThoai], [DiaChi], [NgaySinh], [GioiTinh], [TrangThai], [IDNhom], [GhiChu]) VALUES (N'NV000001', N'admin', N'c4ca4238a0b923820dcc509a6f75849b', N'Lê Quí Nhất', N'0968403428', N'Phú Thứ', CAST(0xAB3C0B00 AS Date), N'Nam', 1, 1, N'')
SET IDENTITY_INSERT [dbo].[NhomQuyen] ON 

INSERT [dbo].[NhomQuyen] ([IDNhom], [TenNhom], [MoTa]) VALUES (1, N'Quản Trị Hệ Thống', N'')
SET IDENTITY_INSERT [dbo].[NhomQuyen] OFF
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'banhang', 1, 0, 0, 0)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'baocao', 1, 0, 0, 0)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'donvitinh', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'khachhang', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'loaihang', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'nhacungcap', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'nhaphang', 1, 0, 0, 0)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'qlbanhang', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'qlnhaphang', 1, 1, 1, 1)
INSERT [dbo].[PhanQuyen] ([IDNhom], [IDChucNang], [Xem], [Them], [Sua], [Xoa]) VALUES (1, N'sanpham', 1, 1, 1, 1)
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__NhanVien__D5B8C7F01A3AAB10]    Script Date: 09-Apr-17 21:01:17 ******/
ALTER TABLE [dbo].[NhanVien] ADD UNIQUE NONCLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietNhapHang] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[ChiTietNhapHang] ADD  DEFAULT ((0)) FOR [DonGia]
GO
ALTER TABLE [dbo].[HoaDon] ADD  DEFAULT (getdate()) FOR [NgayLap]
GO
ALTER TABLE [dbo].[HoaDon] ADD  DEFAULT (getdate()) FOR [NgayGiao]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT (NULL) FOR [DiaChi]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT (getdate()) FOR [NgaySinh]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT (NULL) FOR [GhiChu]
GO
ALTER TABLE [dbo].[LoaiHang] ADD  DEFAULT (NULL) FOR [MoTa]
GO
ALTER TABLE [dbo].[LoaiHang] ADD  DEFAULT ((1)) FOR [TinhTrang]
GO
ALTER TABLE [dbo].[NhaCungCap] ADD  DEFAULT (NULL) FOR [DiaChi]
GO
ALTER TABLE [dbo].[NhaCungCap] ADD  DEFAULT (NULL) FOR [GhiChu]
GO
ALTER TABLE [dbo].[NhanVien] ADD  DEFAULT (NULL) FOR [DiaChi]
GO
ALTER TABLE [dbo].[NhanVien] ADD  DEFAULT (getdate()) FOR [NgaySinh]
GO
ALTER TABLE [dbo].[NhanVien] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[NhanVien] ADD  DEFAULT (NULL) FOR [GhiChu]
GO
ALTER TABLE [dbo].[NhapHang] ADD  DEFAULT (getdate()) FOR [NgayNhap]
GO
ALTER TABLE [dbo].[NhapHang] ADD  DEFAULT (NULL) FOR [GhiChu]
GO
ALTER TABLE [dbo].[NhomQuyen] ADD  DEFAULT (NULL) FOR [MoTa]
GO
ALTER TABLE [dbo].[PhanQuyen] ADD  DEFAULT ((0)) FOR [Xem]
GO
ALTER TABLE [dbo].[PhanQuyen] ADD  DEFAULT ((0)) FOR [Them]
GO
ALTER TABLE [dbo].[PhanQuyen] ADD  DEFAULT ((0)) FOR [Sua]
GO
ALTER TABLE [dbo].[PhanQuyen] ADD  DEFAULT ((0)) FOR [Xoa]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT ((0)) FOR [GiaVon]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT ((0)) FOR [GiaBan]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT (NULL) FOR [Hinh]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT (NULL) FOR [MoTa]
GO
ALTER TABLE [dbo].[SanPham] ADD  DEFAULT ((1)) FOR [TrangThai]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([IDDonViTinh])
REFERENCES [dbo].[DonViTinh] ([IDDonViTinh])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([IDHoaDon])
REFERENCES [dbo].[HoaDon] ([IDHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([IDSanPham])
GO
ALTER TABLE [dbo].[ChiTietNhapHang]  WITH CHECK ADD FOREIGN KEY([IDDonViTinh])
REFERENCES [dbo].[DonViTinh] ([IDDonViTinh])
GO
ALTER TABLE [dbo].[ChiTietNhapHang]  WITH CHECK ADD FOREIGN KEY([IDNhapHang])
REFERENCES [dbo].[NhapHang] ([IDNhapHang])
GO
ALTER TABLE [dbo].[ChiTietNhapHang]  WITH CHECK ADD FOREIGN KEY([IDSanPham])
REFERENCES [dbo].[SanPham] ([IDSanPham])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([IDKhachHang])
REFERENCES [dbo].[KhachHang] ([IDKhachHang])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([IDNhom])
REFERENCES [dbo].[NhomQuyen] ([IDNhom])
GO
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD FOREIGN KEY([IDNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([IDNhaCungCap])
GO
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
ALTER TABLE [dbo].[PhanQuyen]  WITH CHECK ADD FOREIGN KEY([IDChucNang])
REFERENCES [dbo].[ChucNang] ([IDChucNang])
GO
ALTER TABLE [dbo].[PhanQuyen]  WITH CHECK ADD FOREIGN KEY([IDNhom])
REFERENCES [dbo].[NhomQuyen] ([IDNhom])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDDonViTinh])
REFERENCES [dbo].[DonViTinh] ([IDDonViTinh])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDLoaiHang])
REFERENCES [dbo].[LoaiHang] ([IDLoaiHang])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([IDNhaCungCap])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([IDNhanVien])
REFERENCES [dbo].[NhanVien] ([IDNhanVien])
GO
USE [master]
GO
ALTER DATABASE [QuanLyShopHoa] SET  READ_WRITE 
GO
