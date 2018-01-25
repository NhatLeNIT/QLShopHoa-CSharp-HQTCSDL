--========================= DESCRIPTION DATABASE ===========================
-- KhachHang
-- NhomQuyen
-- ChucNang
-- PhanQuyen
-- NhanVien
-- LoaiHang
-- NhaCungCap
-- SanPham
-- NhapHang
-- ChiTietNhapHang
-- HoaDon
-- ChiTietHoaDon

--============================= DESIGN DATABASE =============================

CREATE DATABASE QuanLyShopHoa
GO

USE QuanLyShopHoa
GO

CREATE TABLE KhachHang
(
	IDKhachHang VARCHAR(50) PRIMARY KEY,
	HoTen NVARCHAR(50) NOT NULL,
	DienThoai NVARCHAR(50) NOT NULL,
	DiaChi NVARCHAR(100) NULL DEFAULT NULL,
	NgaySinh DATE NULL DEFAULT GETDATE(),
	GioiTinh NVARCHAR(5) NOT NULL,
	GhiChu NVARCHAR(1000) NULL DEFAULT NULL
)
GO


CREATE TABLE NhomQuyen
(
	IDNhom INT IDENTITY PRIMARY KEY,
	TenNhom NVARCHAR(100) NOT NULL,
	MoTa NVARCHAR(1000) NULL DEFAULT NULL,
)
GO

CREATE TABLE ChucNang
(
	IDChucNang VARCHAR(10) PRIMARY KEY,
	TenChucNang NVARCHAR(50)
)
GO

CREATE TABLE PhanQuyen
(
	IDNhom INT,
	IDChucNang VARCHAR(10),
	Xem BIT NOT NULL DEFAULT 0,
	Them BIT NOT NULL DEFAULT 0,
	Sua BIT NOT NULL DEFAULT 0,
	Xoa BIT NOT NULL DEFAULT 0
	PRIMARY KEY(IDNhom, IDChucNang),
	FOREIGN KEY(IDChucNang) REFERENCES ChucNang(IDChucNang),
	FOREIGN KEY(IDNhom) REFERENCES NhomQuyen(IDNhom)
)
GO

CREATE TABLE NhanVien
(
	IDNhanVien VARCHAR(50) PRIMARY KEY,
	TaiKhoan VARCHAR(30) NOT NULL UNIQUE,
	MatKhau VARCHAR(100) NOT NULL,
	HoTen NVARCHAR(50) NOT NULL,
	DienThoai NVARCHAR(50) NOT NULL,
	DiaChi NVARCHAR(100) NULL DEFAULT NULL,
	NgaySinh DATE NULL DEFAULT GETDATE(),
	GioiTinh NVARCHAR(5) NOT NULL,
	TrangThai BIT NOT NULL DEFAULT 1, -- 0: Không hoạt động, 1: Hoạt động
	IDNhom INT NOT NULL,
	GhiChu NVARCHAR(1000) NULL DEFAULT NULL,
	FOREIGN KEY (IDNhom) REFERENCES NhomQuyen(IDNhom)
)
GO



CREATE TABLE LoaiHang
(
	IDLoaiHang INT IDENTITY PRIMARY KEY,
	TenLoaiHang NVARCHAR(100) NOT NULL,
	MoTa NVARCHAR(1000) NULL DEFAULT NULL,
	TinhTrang BIT NOT NULL DEFAULT 1
)
GO

CREATE TABLE NhaCungCap
(
	IDNhaCungCap VARCHAR(50) PRIMARY KEY,
	TenNhaCungCap NVARCHAR(255) NOT NULL,
	DienThoai NVARCHAR(50) NOT NULL,
	DiaChi NVARCHAR(100) NULL DEFAULT NULL,
	GhiChu NVARCHAR(1000) NULL DEFAULT NULL
)
GO

CREATE TABLE DonViTinh
(
	IDDonViTinh INT IDENTITY PRIMARY KEY,
	TenDonViTinh NVARCHAR(100) NOT NULL,
	GhiChu NVARCHAR(1000)
)
GO

CREATE TABLE SanPham
(
	IDSanPham VARCHAR(50) PRIMARY KEY,
	TenSanPham NVARCHAR(255) NOT NULL,
	GiaVon FLOAT NOT NULL DEFAULT 0,
	GiaBan FLOAT NOT NULL DEFAULT 0,
	SoLuong INT NOT NULL DEFAULT 0,
	Hinh IMAGE NULL DEFAULT NULL,
	MoTa NVARCHAR(1000) NULL DEFAULT NULL,
	TrangThai BIT NOT NULL DEFAULT 1, -- 0: Không kinh doanh, 1: Đang kinh doanh
	IDNhaCungCap VARCHAR(50) NOT NULL,
	IDLoaiHang INT NOT NULL,
	IDDonViTinh INT NOT NULL,
	IDNhanVien VARCHAR(50) NOT NULL,
	FOREIGN KEY (IDNhaCungCap) REFERENCES NhaCungCap(IDNhaCungCap),
	FOREIGN KEY (IDLoaiHang) REFERENCES LoaiHang(IDLoaiHang),
	FOREIGN KEY (IDDonViTinh) REFERENCES DonViTinh(IDDonViTinh),
	FOREIGN KEY (IDNhanVien) REFERENCES NhanVien(IDNhanVien)
)
GO

CREATE TABLE NhapHang
(
	IDNhapHang VARCHAR(50) PRIMARY KEY,
	NgayNhap DATE NOT NULL DEFAULT GETDATE(),
	IDNhaCungCap VARCHAR(50) NOT NULL,
	IDNhanVien VARCHAR(50) NOT NULL,
	GhiChu NVARCHAR(1000) NULL DEFAULT NULL,
	SoLuongSanPham INT,
	TongTien FLOAT,
	FOREIGN KEY (IDNhaCungCap) REFERENCES NhaCungCap(IDNhaCungCap),
	FOREIGN KEY (IDNhanVien) REFERENCES NhanVien(IDNhanVien)
)
GO

CREATE TABLE ChiTietNhapHang
(
	IDNhapHang VARCHAR(50) NOT NULL,
	IDSanPham VARCHAR(50) NOT NULL,
	SoLuong INT NOT NULL DEFAULT 0,
	DonGia FLOAT NOT NULL DEFAULT 0,
	IDDonViTinh INT NOT NULL,
	PRIMARY KEY(IDNhapHang, IDSanPham),
	FOREIGN KEY (IDNhapHang) REFERENCES NhapHang(IDNhapHang),
	FOREIGN KEY (IDSanPham) REFERENCES SanPham(IDSanPham),
	FOREIGN KEY (IDDonViTinh) REFERENCES DonViTinh(IDDonViTinh)
)
GO

CREATE TABLE HoaDon
(
	IDHoaDon VARCHAR(50) NOT NULL PRIMARY KEY,
	NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
	IDNhanVien VARCHAR(50) NOT NULL,
	IDKhachHang VARCHAR(50) NOT NULL,
	TenNguoiNhan NVARCHAR(50),
	DiaChiNguoiNhan NVARCHAR(255),
	DienThoaiNguoiNhan NVARCHAR(20),
	NgayGiao DATETIME DEFAULT GETDATE(),
	TrangThaiThanhToan BIT,
	TrangThaiGiaoHang BIT,
	GhiChu NVARCHAR(1000),
	SoLuongSanPham INT,
	TongTien FLOAT,
	FOREIGN KEY (IDNhanVien) REFERENCES dbo.NhanVien(IDNhanVien),
	FOREIGN KEY (IDKhachHang) REFERENCES dbo.KhachHang(IDKhachHang)
)
GO

CREATE TABLE ChiTietHoaDon
(
	IDHoaDon VARCHAR(50) NOT NULL,
	IDSanPham VARCHAR(50) NOT NULL,
	SoLuong INT NOT NULL,
	DonGia FLOAT NOT NULL,
	IDDonViTinh INT NOT NULL,
	PRIMARY KEY(IDHoaDon, IDSanPham),
	FOREIGN KEY (IDHoaDon) REFERENCES dbo.HoaDon(IDHoaDon),
	FOREIGN KEY (IDSanPham) REFERENCES dbo.SanPham(IDSanPham),
	FOREIGN KEY (IDDonViTinh) REFERENCES DonViTinh(IDDonViTinh)
)
GO


--============================= FUNCTION =============================

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
 
CREATE FUNCTION F_START_OF_WEEK
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
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION TinhTongGiaVonNVTheoTuan(@IDNhanVien VARCHAR(20), @Date DATE)
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

CREATE FUNCTION TinhTongGiaVonNVTheoThang(@IDNhanVien VARCHAR(20), @Date DATE)
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

CREATE FUNCTION TinhTongGiaVonNVTheoNgay(@IDNhanVien VARCHAR(20),@NgayDau DATE, @NgayCuoi DATE)
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

CREATE FUNCTION TinhTongGiaVonBH(@NgayLap DATE)
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

--============================= CREATE STORE =============================

--///////////////////////////// STORE KhachHang

/*CREATE*/ ALTER PROC sp_KhachHang_Insert
@IDKhachHang VARCHAR(50),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@GhiChu NVARCHAR(1000)
AS
BEGIN
--BEGIN TRAN
--	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	INSERT INTO KhachHang( IDKhachHang , HoTen , DienThoai , DiaChi , NgaySinh , GioiTinh , GhiChu )
	VALUES  ( @IDKhachHang , @HoTen , @DienThoai , @DiaChi , @NgaySinh , @GioiTinh , @GhiChu )
	--COMMIT TRAN
END
GO

/*CREATE*/ ALTER PROC sp_KhachHang_Update
@IDKhachHang VARCHAR(50),
@HoTen NVARCHAR(50),
@DienThoai NVARCHAR(50),
@DiaChi NVARCHAR(100),
@NgaySinh DATE,
@GioiTinh NVARCHAR(5),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRAN
	UPDATE KhachHang SET HoTen = @HoTen, DienThoai = @DienThoai, DiaChi = @DiaChi, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, GhiChu = @GhiChu WHERE IDKhachHang = @IDKhachHang
	COMMIT  TRAN
END
GO

CREATE PROC sp_KhachHang_Delete
@IDKhachHang VARCHAR(50)
AS
BEGIN
	DELETE KhachHang WHERE IDKhachHang = @IDKhachHang
END
GO

/*CREATE*/ ALTER PROC sp_KhachHang_Select_All
AS
BEGIN
--SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
--BEGIN TRAN

--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRAN
--Lỗi không đọc lại được dữ liệu
--SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
--SET TRANSACTION ISOLATION LEVEL REPEATABLE READ

		--WAITFOR DELAY '00:00:05'
		SELECT * FROM KhachHang
	COMMIT TRAN
END
GO

CREATE PROC sp_KhachHang_Select_ByID
@IDKhachHang VARCHAR(50)
AS
BEGIN
	SELECT * FROM KhachHang WHERE IDKhachHang = @IDKhachHang
END
GO

--///////////////////////////// STORE NhomQuyen

CREATE PROC sp_NhomQuyen_Insert
@TenNhom NVARCHAR(100), 
@MoTa NVARCHAR(1000)
AS
BEGIN
	INSERT INTO NhomQuyen ( TenNhom, MoTa ) VALUES  ( @TenNhom, @MoTa )
END
GO

CREATE PROC sp_NhomQuyen_Update
@IDNhom INT,
@TenNhom NVARCHAR(100), 
@MoTa NVARCHAR(1000)
AS
BEGIN
	UPDATE NhomQuyen SET TenNhom = @TenNhom, MoTa = @MoTa WHERE IDNhom = @IDNhom
END
GO

CREATE PROC sp_NhomQuyen_Delete
@IDNhom INT
AS
BEGIN
	DELETE NhomQuyen WHERE IDNhom = @IDNhom
END
GO

CREATE PROC sp_NhomQuyen_Select_All
AS
BEGIN
	SELECT * FROM NhomQuyen
END
GO

CREATE PROC sp_NhomQuyen_Select_ByID
@IDNhom INT
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom = @IDNhom
END
GO

CREATE PROC sp_NhomQuyen_NotInPhanQuyen
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom NOT IN (SELECT IDNhom FROM PhanQuyen)
END
GO

CREATE PROC sp_NhomQuyen_SelectInPhanQuyen
AS
BEGIN
	SELECT * FROM NhomQuyen WHERE IDNhom IN (SELECT IDNhom FROM PhanQuyen)
END
GO

--///////////////////////////// STORE ChucNang

CREATE PROC sp_ChucNang_Select_All
AS
BEGIN
	SELECT * FROM ChucNang
END
GO

CREATE PROC sp_ChucNang_Select_ByID
@IDChucNang VARCHAR(10)
AS
BEGIN
	SELECT * FROM dbo.ChucNang WHERE IDChucNang = @IDChucNang
END
GO


--///////////////////////////// STORE PhanQuyen

CREATE PROC sp_PhanQuyen_Insert
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

CREATE PROC sp_PhanQuyen_Delete
@IDNhom INT
AS
BEGIN
	DELETE PhanQuyen WHERE IDNhom = @IDNhom
END
GO


CREATE PROC sp_PhanQuyen_Select_ByIDNhom
@IDNhom INT
AS
BEGIN
	SELECT ChucNang.IDChucNang, TenChucNang, Xem, Them, Sua, Xoa FROM ChucNang INNER JOIN PhanQuyen ON PhanQuyen.IDChucNang = ChucNang.IDChucNang WHERE IDNhom = @IDNhom
END
GO

CREATE PROC sp_PhanQuyen_Select_ByIDNhomAndIDChucNang
@IDNhom INT,
@IDChucNang VARCHAR(10)
AS
BEGIN
	SELECT * FROM dbo.PhanQuyen WHERE IDNhom = @IDNhom AND IDChucNang = @IDChucNang
END
GO

CREATE PROC sp_PhanQuyen_Update
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

--///////////////////////////// STORE NhanVien

CREATE PROC sp_NhanVien_Insert
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

CREATE PROC sp_NhanVien_Update
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

CREATE PROC sp_NhanVien_Delete
@IDNhanVien VARCHAR(50)
AS
BEGIN
	DELETE NhanVien WHERE IDNhanVien = @IDNhanVien
END
GO

CREATE PROC sp_NhanVien_Select_All
AS
BEGIN
	SELECT * FROM NhanVien AS A, NhomQuyen AS B WHERE A.IDNhom = B.IDNhom
END
GO

CREATE PROC sp_NhanVien_Select_ByID
@IDNhanVien VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhanVien WHERE IDNhanVien = @IDNhanVien
END
GO

CREATE PROC sp_NhanVien_Select_ByUserName
@TaiKhoan VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhanVien WHERE TaiKhoan = @TaiKhoan
END
GO

CREATE PROC sp_NhanVien_Update_Password
@IDNhanVien VARCHAR(50),
@MatKhau VARCHAR(50)
AS
BEGIN
	UPDATE NhanVien SET MatKhau = @MatKhau WHERE IDNhanVien = @IDNhanVien
END
GO

--///////////////////////////// STORE LoaiHang

CREATE PROC sp_LoaiHang_Insert
@TenLoaiHang NVARCHAR(100),
@MoTa NVARCHAR(1000),
@TinhTrang BIT
AS
BEGIN
	INSERT INTO LoaiHang ( TenLoaiHang, MoTa, TinhTrang ) VALUES ( @TenLoaiHang, @MoTa, @TinhTrang )
END
GO

CREATE PROC sp_LoaiHang_Update
@IDLoaiHang INT,
@TenLoaiHang NVARCHAR(100),
@MoTa NVARCHAR(1000),
@TinhTrang BIT
AS
BEGIN
	UPDATE LoaiHang SET TenLoaiHang = @TenLoaiHang, MoTa = @MoTa, TinhTrang = @TinhTrang WHERE IDLoaiHang = @IDLoaiHang
END
GO

CREATE PROC sp_LoaiHang_Delete
@IDLoaiHang INT
AS
BEGIN
	DELETE LoaiHang WHERE IDLoaiHang = @IDLoaiHang
END
GO

CREATE PROC sp_LoaiHang_Select_All
AS
BEGIN
	SELECT * FROM LoaiHang
END
GO

CREATE PROC sp_LoaiHang_Select_ByID
@IDLoaiHang INT
AS
BEGIN
	SELECT * FROM LoaiHang WHERE IDLoaiHang = @IDLoaiHang
END
GO

CREATE PROC sp_LoaiHang_Select_Active
AS
BEGIN
	SELECT IDLoaiHang, TenLoaiHang FROM LoaiHang WHERE TinhTrang = 1
END
GO

--///////////////////////////// STORE NhaCungCap

CREATE PROC sp_NhaCungCap_Insert
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

CREATE PROC sp_NhaCungCap_Update
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

CREATE PROC sp_NhaCungCap_Delete
@IDNhaCungCap VARCHAR(50)
AS
BEGIN
	DELETE NhaCungCap WHERE IDNhaCungCap = @IDNhaCungCap
END
GO

CREATE PROC sp_NhaCungCap_Select_All
AS
BEGIN
	SELECT * FROM NhaCungCap
END
GO

CREATE PROC sp_NhaCungCap_Select_ByID
@IDNhaCungCap VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhaCungCap WHERE IDNhaCungCap = @IDNhaCungCap
END
GO

--///////////////////////////// STORE DonViTinh

CREATE PROC sp_DonViTinh_Insert
@TenDonViTinh NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
	INSERT INTO DonViTinh ( TenDonViTinh, GhiChu )
	VALUES  ( @TenDonViTinh, @GhiChu ) 
END
GO

CREATE PROC sp_DonViTinh_Update
@IDDonViTinh INT,
@TenDonViTinh NVARCHAR(100),
@GhiChu NVARCHAR(1000)
AS
BEGIN
UPDATE DonViTinh SET TenDonViTinh = @TenDonViTinh, GhiChu = @GhiChu WHERE IDDonViTinh = @IDDonViTinh
END
GO

CREATE PROC sp_DonViTinh_Delete
@IDDonViTinh INT
AS
BEGIN
	DELETE DonViTinh WHERE IDDonViTinh = @IDDonViTinh
END
GO

CREATE PROC sp_DonViTinh_Select_All
AS
BEGIN
	SELECT * FROM DonViTinh
END
GO

CREATE PROC sp_DonViTinh_Select_All_UnRepeatable
@IdDonViTinh INT,
@Out1 NVARCHAR(100) OUTPUT,
@Out2 NVARCHAR(100) OUTPUT
AS
BEGIN
	BEGIN TRAN
		SELECT @Out1 = TenDonViTinh FROM DonViTinh WHERE IDDonViTinh = @IdDonViTinh
		WAITFOR DELAY '00:00:03'
		SELECT @Out2 = TenDonViTinh FROM DonViTinh WHERE IDDonViTinh = @IdDonViTinh
	COMMIT TRAN
END
GO

CREATE PROC sp_DonViTinh_Select_All_Fix_UnRepeatable
@IdDonViTinh INT,
@Out1 NVARCHAR(100) OUTPUT,
@Out2 NVARCHAR(100) OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL REPEATABLE READ  
	BEGIN TRAN
		SELECT @Out1 = TenDonViTinh FROM DonViTinh WHERE IDDonViTinh = @IdDonViTinh
		WAITFOR DELAY '00:00:03'
		SELECT @Out2 = TenDonViTinh FROM DonViTinh WHERE IDDonViTinh = @IdDonViTinh
		COMMIT TRAN
END
GO


CREATE PROC sp_DonViTinh_Select_All_Phantom
@Out1 NVARCHAR(100) OUTPUT,
@Out2 NVARCHAR(100) OUTPUT
AS
BEGIN
	BEGIN TRAN
		SELECT @Out1 = TenDonViTinh FROM DonViTinh
		WAITFOR DELAY '00:00:03'
		SELECT @Out2 = TenDonViTinh FROM DonViTinh
	COMMIT TRAN
END
GO

CREATE PROC sp_DonViTinh_Select_All_Fix_Phantom
@Out1 NVARCHAR(100) OUTPUT,
@Out2 NVARCHAR(100) OUTPUT
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE 
	BEGIN TRAN
		SELECT @Out1 = TenDonViTinh FROM DonViTinh
		WAITFOR DELAY '00:00:03'
		SELECT @Out2 = TenDonViTinh FROM DonViTinh
	COMMIT TRAN
END
GO

CREATE PROC sp_DonViTinh_Select_All_UnRepeatable_View
@IdDonViTinh INT
AS
BEGIN
	DECLARE @a NVARCHAR(100), @b NVARCHAR(100)
	EXEC sp_DonViTinh_Select_All_UnRepeatable @IdDonViTinh, @a OUTPUT, @b OUTPUT
	SELECT @a AS GiaTriCu, @b AS GiaTriMoi
END
GO

CREATE PROC sp_DonViTinh_Select_All_Fix_UnRepeatable_View
@IdDonViTinh INT
AS
BEGIN
	DECLARE @a NVARCHAR(100), @b NVARCHAR(100)
	EXEC sp_DonViTinh_Select_All_Fix_UnRepeatable @IdDonViTinh, @a OUTPUT, @b OUTPUT
	SELECT @a AS GiaTriCu, @b AS GiaTriMoi
END
GO


CREATE PROC sp_DonViTinh_Select_All_Phantom_View
AS
BEGIN
	DECLARE @a NVARCHAR(100), @b NVARCHAR(100)
	EXEC sp_DonViTinh_Select_All_Phantom @a OUTPUT, @b OUTPUT
	SELECT @a AS GiaTriCu, @b AS GiaTriMoi
END
GO

CREATE PROC sp_DonViTinh_Select_All_Fix_Phantom_View
AS
BEGIN
	DECLARE @a NVARCHAR(100), @b NVARCHAR(100)
	EXEC sp_DonViTinh_Select_All_Fix_Phantom @a OUTPUT, @b OUTPUT
	SELECT @a AS GiaTriCu, @b AS GiaTriMoi
END
GO

CREATE PROC sp_DonViTinh_Select_ByID
@IDDonViTinh INT
AS
BEGIN
	SELECT * FROM DonViTinh WHERE IDDonViTinh = @IDDonViTinh
END
GO

--///////////////////////////// STORE SanPham

CREATE PROC sp_SanPham_Insert
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

/*CREATE*/ ALTER PROC sp_SanPham_Update
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
--Loi lost update
	--BEGIN TRAN
	--SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
    --WAITFOR DELAY '00:00:03'
	UPDATE SanPham SET TenSanPham = @TenSanPham, GiaVon = @GiaVon, GiaBan = @GiaBan, SoLuong = @SoLuong, Hinh = @Hinh, MoTa = @MoTa, TrangThai = @TrangThai, IDNhaCungCap = @IDNhaCungCap, IDLoaiHang = @IDLoaiHang, IDDonViTinh = @IDDonViTinh, IDNhanVien = @IDNhanVien
	WHERE IDSanPham = @IDSanPham
	--COMMIT TRAN


	-- Demo đọc dữ liệu chưa commit----------------
	--WAITFOR DELAY '00:00:05'
	--ROLLBACK TRAN
	-----------------------------------------------
END
GO


CREATE PROC sp_SanPham_Update_LostUpdate
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
	BEGIN TRAN
    WAITFOR DELAY '00:00:03'
	UPDATE SanPham SET TenSanPham = @TenSanPham, GiaVon = @GiaVon, GiaBan = @GiaBan, SoLuong = @SoLuong, Hinh = @Hinh, MoTa = @MoTa, TrangThai = @TrangThai, IDNhaCungCap = @IDNhaCungCap, IDLoaiHang = @IDLoaiHang, IDDonViTinh = @IDDonViTinh, IDNhanVien = @IDNhanVien
	WHERE IDSanPham = @IDSanPham
	COMMIT TRAN
END
GO


CREATE PROC sp_SanPham_Update_DirtyRead
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
	BEGIN TRAN
	UPDATE SanPham SET TenSanPham = @TenSanPham, GiaVon = @GiaVon, GiaBan = @GiaBan, SoLuong = @SoLuong, Hinh = @Hinh, MoTa = @MoTa, TrangThai = @TrangThai, IDNhaCungCap = @IDNhaCungCap, IDLoaiHang = @IDLoaiHang, IDDonViTinh = @IDDonViTinh, IDNhanVien = @IDNhanVien
	WHERE IDSanPham = @IDSanPham
	WAITFOR DELAY '00:00:05'
	ROLLBACK TRAN
END
GO



CREATE PROC sp_SanPham_Delete
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE SanPham WHERE IDSanPham = @IDSanPham
END
GO

CREATE PROC sp_SanPham_Select_All
AS
BEGIN
	SELECT * FROM SanPham
END
GO

CREATE PROC sp_SanPham_Select_ByStatus
AS
BEGIN
	
	SELECT * FROM SanPham WHERE TrangThai = 1 
END
GO

CREATE PROC sp_SanPham_Select_ByStatus_Quantity
AS
BEGIN
	
	SELECT * FROM SanPham WHERE TrangThai = 1  AND SoLuong > 0
END
GO

/*CREATE*/ ALTER PROC sp_SanPham_Select_ByID
@IDSanPham VARCHAR(50)
AS
BEGIN
	BEGIN TRAN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SELECT * FROM SanPham WHERE IDSanPham = @IDSanPham
	COMMIT TRAN
END
GO


CREATE PROC sp_SanPham_Select_ByID_Fix_DirtyRead
@IDSanPham VARCHAR(50)
AS
BEGIN
	BEGIN TRAN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SELECT * FROM SanPham WHERE IDSanPham = @IDSanPham
	COMMIT TRAN
END
GO

CREATE PROC sp_SanPham_Select_ByID_Quantity
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM SanPham WHERE IDSanPham = @IDSanPham AND SoLuong > 0
END
GO

CREATE PROC sp_SanPham_Select_AllData
AS
BEGIN
	SELECT IDSanPham, TenSanPham, GiaVon, GiaBan, SoLuong, Hinh, SanPham.MoTa, SanPham.TrangThai, TenLoaiHang, TenNhaCungCap, HoTen, TenDonViTinh FROM SanPham, NhaCungCap, LoaiHang, NhanVien, DonViTinh WHERE SanPham.IDLoaiHang = LoaiHang.IDLoaiHang AND SanPham.IDNhaCungCap = NhaCungCap.IDNhaCungCap AND SanPham.IDNhanVien = NhanVien.IDNhanVien AND SanPham.IDDonViTinh = DonViTinh.IDDonViTinh
END
GO

CREATE PROC sp_SanPham_UpdateQuantity
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE SanPham SET SoLuong += @SoLuong WHERE IDSanPham = @IDSanPham
END
GO

--------------------------------------------------------------------------------------
CREATE PROC sp_SanPham_UpdateQuantitySub
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	BEGIN tran
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	UPDATE SanPham SET SoLuong -= @SoLuong WHERE IDSanPham = @IDSanPham
	COMMIT TRAN
END
GO

--///////////////////////////// STORE NhapHang

CREATE PROC sp_NhapHang_Insert
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

CREATE PROC sp_NhapHang_Update
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

CREATE PROC sp_NhapHang_Delete
@IDNhapHang VARCHAR(50)
AS
BEGIN
	DELETE NhapHang WHERE IDNhapHang = @IDNhapHang
END
GO

CREATE PROC sp_NhapHang_Select_All
AS
BEGIN
	SELECT IDNhapHang, NgayNhap, TenNhaCungCap, HoTen, NhapHang.GhiChu, SoLuongSanPham, TongTien
	FROM NhapHang, NhaCungCap, NhanVien
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
	AND NhapHang.IDNhanVien = NhanVien.IDNhanVien
END
GO

CREATE PROC sp_NhapHang_Select_ByID
@IDNhapHang VARCHAR(50)
AS
BEGIN
	SELECT * FROM NhapHang WHERE IDNhapHang = @IDNhapHang
END
GO

CREATE PROC sp_NhapHang_Select_ByDate
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

CREATE PROC sp_NhapHang_ChiTietSanPhamTheoNCC_Tuan
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

CREATE PROC sp_NhapHang_ChiTietSanPhamTheoNCC_Thang
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

CREATE PROC sp_NhapHang_ChiTietSanPhamTheoNCC_Ngay
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


CREATE PROC sp_NhapHang_ChiTietNCCTheoNhapHang_Tuan
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

CREATE PROC sp_NhapHang_ChiTietNCCTheoNhapHang_Thang
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

CREATE PROC sp_NhapHang_ChiTietNCCTheoNhapHang_Ngay
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
--///////////////////////////// STORE ChiTietNhapHang

CREATE PROC sp_ChiTietNhapHang_Insert
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

CREATE PROC sp_ChiTietNhapHang_Delete
@IDNhapHang VARCHAR(50)
AS
BEGIN
	DELETE ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang
END
GO

CREATE PROC sp_ChiTietNhapHang_Delete_ByIDSanPham
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END
GO

CREATE PROC sp_ChiTietNhapHang_Select_ByID
@IDNhapHang VARCHAR(50)
AS
BEGIN
	SELECT ChiTietNhapHang.IDSanPham, TenSanPham, ChiTietNhapHang.SoLuong, DonGia, (ChiTietNhapHang.SoLuong * DonGia) AS TongTien, TenDonViTinh FROM ChiTietNhapHang, SanPham, DonViTinh WHERE IDNhapHang = @IDNhapHang AND ChiTietNhapHang.IDSanPham = SanPham.IDSanPham AND ChiTietNhapHang.IDDonViTinh = DonViTinh.IDDonViTinh
END
GO

CREATE PROC sp_ChiTietNhapHang_Select_ByIDSanPham
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM ChiTietNhapHang WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END
GO

CREATE PROC sp_ChiTietNhapHang_Update_Quantity
@IDNhapHang VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE ChiTietNhapHang SET SoLuong = @SoLuong WHERE IDNhapHang = @IDNhapHang AND IDSanPham = @IDSanPham
END
GO



CREATE PROC sp_ChiTietNhapHang_StatisticNCC_ByWeek
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

CREATE PROC sp_ChiTietNhapHang_StatisticNCC_ByMonth
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

CREATE PROC sp_ChiTietNhapHang_StatisticNCC_ByDate
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

CREATE PROC sp_ChiTietNhapHang_NCCStatistic_ByWeek
AS
BEGIN
	SELECT NhapHang.IDNhaCungCap, TenNhaCungCap, SUM(TongTien) AS TongTien, SUM(SoLuongSanPham) AS SoLuong FROM NhapHang, NhaCungCap
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
	AND NgayNhap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY NhapHang.IDNhaCungCap, TenNhaCungCap
END
GO


CREATE PROC sp_ChiTietNhapHang_NCCStatistic_ByMonth
AS
BEGIN
	SELECT NhapHang.IDNhaCungCap, TenNhaCungCap, SUM(TongTien) AS TongTien, SUM(SoLuongSanPham) AS SoLuong FROM NhapHang, NhaCungCap
	WHERE NhapHang.IDNhaCungCap = NhaCungCap.IDNhaCungCap
		AND MONTH(NgayNhap) = MONTH(GETDATE())
	GROUP BY NhapHang.IDNhaCungCap, TenNhaCungCap
END
GO

CREATE PROC sp_ChiTietNhapHang_NCCStatistic_ByDate
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
--///////////////////////////// STORE HoaDon

CREATE PROC sp_HoaDon_Insert
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

CREATE PROC sp_HoaDon_Update
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

CREATE PROC sp_HoaDon_Delete
@IDHoaDon VARCHAR(50)
AS
BEGIN
	DELETE HoaDon WHERE IDHoaDon = @IDHoaDon
END
GO

CREATE PROC sp_HoaDon_Select_All
AS
BEGIN
	SELECT IDHoaDon, KhachHang.HoTen AS TenKhachHang, KhachHang.DienThoai AS DienThoaiKhachHang, SoLuongSanPham, TongTien, NgayLap, NgayGiao, TrangThaiThanhToan, TrangThaiGiaoHang, HoaDon.GhiChu AS GhiChuHD, NhanVien.HoTen AS TenNhanVien
	FROM HoaDon, KhachHang, NhanVien
	WHERE HoaDon.IDNhanVien = NhanVien.IDNhanVien
	AND HoaDon.IDKhachHang = KhachHang.IDKhachHang
END
GO


CREATE PROC sp_HoaDon_Select_ByID
@IDHoaDon VARCHAR(50)
AS
BEGIN
	SELECT * FROM HoaDon WHERE IDHoaDon = @IDHoaDon
END
GO

CREATE PROC sp_HoaDon_Select_ByDate
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


CREATE PROC sp_HoaDon_NVStatistic_ByWeek
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

CREATE PROC sp_HoaDon_NVStatistic_ByMonth
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

CREATE PROC sp_HoaDon_NVStatistic_ByDate
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


CREATE PROC sp_HoaDon_BHStatistic_ByWeek
AS
BEGIN
	SELECT NgayLap, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonBH(NgayLap) AS GiaVon, (SUM(TongTien) - dbo.TinhTongGiaVonBH(NgayLap)) AS LoiNhuan
	FROM HoaDon
	WHERE NgayLap BETWEEN dbo.F_START_OF_WEEK(GETDATE(),2) AND GETDATE()
	GROUP BY NgayLap
END
GO

CREATE PROC sp_HoaDon_BHStatistic_ByMonth
AS
BEGIN
	SELECT NgayLap, SUM(TongTien) AS DoanhThu, dbo.TinhTongGiaVonBH(NgayLap) AS GiaVon, (SUM(TongTien) - dbo.TinhTongGiaVonBH(NgayLap)) AS LoiNhuan
	FROM HoaDon
	WHERE MONTH(NgayLap) = MONTH(GETDATE())
	GROUP BY NgayLap
END

GO
CREATE PROC sp_HoaDon_BHStatistic_ByDate
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

CREATE PROC sp_HoaDon_ChiTietBanHangTheoThoiGian
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoLoiNhuan
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoNhanVien_Tuan
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoNhanVien_Thang
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoNhanVien_Ngay
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


CREATE PROC sp_HoaDon_ChiTietSanPhamTheoKhachHang_Tuan
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoKhachHang_Thang
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

CREATE PROC sp_HoaDon_ChiTietSanPhamTheoKhachHang_Ngay
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


CREATE PROC sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Tuan
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

CREATE PROC sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Thang
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

CREATE PROC sp_HoaDon_ChiTietKhachHangTheoLoiNhuan_Ngay
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

CREATE PROC sp_HoaDon_ChiTietNhanVienTheoBanHang_Tuan
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

CREATE PROC sp_HoaDon_ChiTietNhanVienTheoBanHang_Thang
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

CREATE PROC sp_HoaDon_ChiTietNhanVienTheoBanHang_Ngay
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
--///////////////////////////// STORE ChiTietHoaDon

CREATE PROC sp_ChiTietHoaDon_Insert
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

CREATE PROC sp_ChiTietHoaDon_Delete
@IDHoaDon VARCHAR(50)
AS
BEGIN
	DELETE ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon
END
GO

CREATE PROC sp_ChiTietHoaDon_Delete_ByIDSanPham
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	DELETE ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END
GO



CREATE PROC sp_ChiTietHoaDon_Select_ByIDSanPham
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50)
AS
BEGIN
	SELECT * FROM ChiTietHoaDon WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END
GO

CREATE PROC sp_ChiTietHoaDon_Select_ByID
@IDHoaDon VARCHAR(50)
AS
BEGIN
	SELECT ChiTietHoaDon.IDSanPham, TenSanPham, ChiTietHoaDon.SoLuong, DonGia, (ChiTietHoaDon.SoLuong * DonGia) AS TongTien, TenDonViTinh
	FROM ChiTietHoaDon, SanPham, DonViTinh WHERE IDHoaDon = @IDHoaDon AND ChiTietHoaDon.IDSanPham = SanPham.IDSanPham AND ChiTietHoaDon.IDDonViTinh = DonViTinh.IDDonViTinh
END
GO

CREATE PROC sp_ChiTietHoaDon_Update_Quantity
@IDHoaDon VARCHAR(50),
@IDSanPham VARCHAR(50),
@SoLuong INT
AS
BEGIN
	UPDATE ChiTietHoaDon SET SoLuong = @SoLuong WHERE IDHoaDon = @IDHoaDon AND IDSanPham = @IDSanPham
END
GO


CREATE PROC sp_ChiTietHoaDon_StatisticLN_ByWeek
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

CREATE PROC sp_ChiTietHoaDon_StatisticLN_ByMonth
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

CREATE PROC sp_ChiTietHoaDon_StatisticLN_ByDate
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

CREATE PROC sp_ChiTietHoaDon_KHStatisticLN_ByWeek
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

CREATE PROC sp_ChiTietHoaDon_KHStatisticLN_ByMonth
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

CREATE PROC sp_ChiTietHoaDon_KHStatisticLN_ByDate
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


CREATE PROC sp_ChiTietHoaDon_StatisticNV_ByWeek
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

CREATE PROC sp_ChiTietHoaDon_StatisticNV_ByMonth
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

CREATE PROC sp_ChiTietHoaDon_StatisticNV_ByDate
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

CREATE PROC sp_ChiTietHoaDon_StatisticKH_ByWeek
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

CREATE PROC sp_ChiTietHoaDon_StatisticKH_ByMonth
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

CREATE PROC sp_ChiTietHoaDon_StatisticKH_ByDate
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





--============================= INSERT DATA =============================

--///////////////////////////// INSERT LoaiHang
INSERT LoaiHang ( TenLoaiHang, MoTa, TinhTrang )
VALUES  ( N'Hoa Sinh Nhật', N'Hoa sinh nhật', 1 )
INSERT LoaiHang ( TenLoaiHang, MoTa, TinhTrang )
VALUES  ( N'Hoa Tặng Mẹ', N'', 1 )

--///////////////////////////// INSERT KhachHang
INSERT KhachHang ( IDKhachHang , HoTen , DienThoai , DiaChi , NgaySinh , GioiTinh , GhiChu )
VALUES  ( 'KH000001' , N'Nguyễn Văn A' , N'0123456789' , N'Phạm Ngũ Lão' , GETDATE() , 'Nam' , N'' )

--///////////////////////////// INSERT NhaCungCap
INSERT NhaCungCap ( IDNhaCungCap , TenNhaCungCap , DienThoai , DiaChi , GhiChu )
VALUES  ( 'NCC000001' , N'Nhà cung cấp 1' , N'132154544' , N'Phạm Ngũ Lão' , N'' )

--///////////////////////////// INSERT ChucNang
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'loaihang', N'Quản Lý Loại Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'sanpham', N'Quản Lý Sản Phẩm' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'khachhang', N'Quản Lý khách Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'nhacungcap', N'Quản Lý Nhà Cung Cấp' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'donvitinh', N'Quản Lý Đơn Vị Tính' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'banhang', N'Bán Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'nhaphang', N'Nhập Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'qlnhaphang', N'Quản Lý Nhập Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'qlbanhang', N'Quản Lý Bán Hàng' )
INSERT ChucNang ( IDChucNang, TenChucNang ) VALUES  ( 'baocao', N'Quản Lý Báo Cáo' )


--///////////////////////////// INSERT NhomQuyen
INSERT NhomQuyen ( TenNhom, MoTa ) VALUES  ( N'Quản Trị Hệ Thống', N'')

--///////////////////////////// INSERT NhanVien
INSERT NhanVien ( IDNhanVien , TaiKhoan , MatKhau , HoTen , DienThoai , DiaChi , NgaySinh , GioiTinh , TrangThai , IDNhom , GhiChu )
VALUES  ( 'NV000001' , 'admin' , 'c4ca4238a0b923820dcc509a6f75849b' , N'Lê Quí Nhất' , N'0968403428' , N'Phú Thứ' , GETDATE() , N'Nam' , 1 , 1 , N'' )

--///////////////////////////// INSERT PhanQuyen
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'banhang', 1, 0, 0, 0 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'baocao', 1, 0, 0, 0 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'donvitinh', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'khachhang', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'loaihang', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'nhacungcap', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'nhaphang', 1, 0, 0, 0 )
-- INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'nhanvien', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'qlbanhang', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'qlnhaphang', 1, 1, 1, 1 )
INSERT PhanQuyen ( IDNhom, IDChucNang, Xem, Them, Sua, Xoa ) VALUES  ( 1,  'sanpham', 1, 1, 1, 1 )


SELECT * FROM dbo.PhanQuyen


----------------------------TRIGGER-------------------------------------
------ TRIGGER TABLE HoaDon-----
ALTER TRIGGER tg_CHECK_Insert_NGAYGIAO  -- Kiểm tra ngày giao hàng không được nhỏ hơn ngày đặt
ON HoaDon FOR INSERT
AS 
BEGIN 
	DECLARE @NG SMALLDATETIME,@NL SMALLDATETIME 
	SET @NG = (SELECT NgayGiao FROM INSERTED )
	SET @NL = (SELECT NgayLap FROM INSERTED) 
	
	IF(@NG < @NL  )
		BEGIN
			 PRINT -2;
			rollback TRAN
		END
END
GO


 --XÓA HÓA ĐƠN TỰ ĐỘNG XÓA CTHĐ
ALTER TRIGGER tg_HoaDon_Del
ON HoaDon INSTEAD OF DELETE
AS BEGIN
	DELETE ChiTietHoaDon WHERE IDHoaDon IN (SELECT IDHoaDon FROM deleted)
	DELETE HoaDon WHERE IDHoaDon IN (SELECT IDHoaDon FROM deleted)
END
GO

 --XÓA HÓA ĐƠN TỰ ĐỘNG XÓA CTNH
ALTER TRIGGER tg_NhapHang_Del
ON NhapHang INSTEAD OF DELETE
AS BEGIN
	DELETE ChiTietNhapHang WHERE IDNhapHang IN (SELECT IDNhapHang FROM deleted)
	DELETE NhapHang WHERE IDNhapHang IN (SELECT IDNhapHang FROM deleted)
END
GO

	------------- TRIGGER TABLE PhanQuyen
CREATE TRIGGER tg_NhomQuyen_Del ---không được xóa 1 nhóm quyền khi đã phân quyền
ON NhomQuyen Instead of Delete
AS BEGIN
DECLARE @IDNHOMQUYEN INT
SET @IDNHOMQUYEN = (SELECT IDNhom FROM deleted)
if((SELECT count(*) From PhanQuyen WHERE IDNhom = @IDNHOMQUYEN) > 0)
		BEGIN
		ROLLBACK TRAN
		END
ELSE
	BEGIN
		DELETE NhomQuyen WHERE IDNhom = @IDNHOMQUYEN
	END
END
GO

----------TRIGGER XOA Nhan Vien thi xoa het cac bang co lien quan
ALTER TRIGGER tg_NhanVien_Del
ON NhanVien INSTEAD OF DELETE
AS
BEGIN
	DELETE HoaDon WHERE IDNhanVien IN (SELECT IDNhanVien FROM Deleted)
	DELETE NhapHang WHERE IDNhanVien IN (SELECT IDNhanVien FROM Deleted)
	DELETE SanPham WHERE IDNhanVien IN (SELECT IDNhanVien FROM Deleted)
	DELETE NhanVien WHERE IDNhanVien IN (SELECT IDNhanVien FROM Deleted)
END
GO

----------TRIGGER XOA San Pham thi xoa het cac bang co lien quan
ALTER TRIGGER tg_SanPham_Del
ON SanPham INSTEAD OF DELETE
AS
BEGIN
	DELETE ChiTietHoaDon WHERE IDSanPham IN (SELECT IDSanPham FROM Deleted)
	DELETE ChiTietNhapHang WHERE IDSanPham IN (SELECT IDSanPham FROM Deleted)
	DELETE SanPham WHERE IDSanPham IN (SELECT IDSanPham FROM Deleted)
END
GO

----------TRIGGER XOA Khach Hang thi xoa het cac bang co lien quan
CREATE TRIGGER tg_KhachHang_Del
ON KhachHang INSTEAD OF DELETE
AS
BEGIN
	DELETE HoaDon WHERE IDKhachHang IN (SELECT IDKhachHang FROM Deleted)
	DELETE KhachHang WHERE IDKhachHang IN (SELECT IDKhachHang FROM Deleted)
END
GO


----------TRIGGER XOA Nha Cung Cap thi xoa het cac bang co lien quan
CREATE TRIGGER tg_NhaCungCap_Del
ON NhaCungCap INSTEAD OF DELETE
AS
BEGIN
	DELETE SanPham WHERE IDNhaCungCap IN (SELECT IDNhaCungCap FROM Deleted)
	DELETE NhapHang WHERE IDNhaCungCap IN (SELECT IDNhaCungCap FROM Deleted)
	DELETE NhaCungCap WHERE IDNhaCungCap IN (SELECT IDNhaCungCap FROM Deleted)
END
GO

----------TRIGGER XOA LoaiHang thi xoa het cac bang co lien quan
CREATE TRIGGER tg_LoaiHang_Del
ON LoaiHang INSTEAD OF DELETE
AS
BEGIN
	DELETE SanPham WHERE IDLoaiHang IN (SELECT IDLoaiHang FROM Deleted)
	DELETE LoaiHang WHERE IDLoaiHang IN (SELECT IDLoaiHang FROM Deleted)
END
GO

----------TRIGGER XOA DonViTinh thi xoa het cac bang co lien quan
CREATE TRIGGER tg_DonViTinh_Del
ON DonViTinh INSTEAD OF DELETE
AS
BEGIN
	DELETE SanPham WHERE IDDonViTinh IN (SELECT IDDonViTinh FROM Deleted)
	DELETE ChiTietNhapHang WHERE IDDonViTinh IN (SELECT IDDonViTinh FROM Deleted)
	DELETE ChiTietHoaDon WHERE IDDonViTinh IN (SELECT IDDonViTinh FROM Deleted)
	DELETE DonViTinh WHERE IDDonViTinh IN (SELECT IDDonViTinh FROM Deleted)
END
GO

