create database Pos01

go

use Pos01

go

create table Ekli_Urunler(
Urun_ID int identity(1,1) Primary Key,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
Alis_Urun_Fiyat money,
Urun_Barkod Bigint,
Eklenme_Tarihi DateTime,
Son_Guncellenme_Tarihi DateTime,
Urun_Görsel varchar(100)
)

create table Stok_Urunler(
Urun_ID int Primary Key,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Emir_Adet int,
Urun_Fiyat money,
Urun_Barkod Bigint,
Eklenme_Tarihi DateTime,
Son_Guncellenme_Tarihi DateTime,
Emir_Eklenme_Tarihi DateTime,
)
create table Emir_Urunler(
Urun_ID int ,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Emir_Adet int,
Urun_Barkod Bigint,
)

create table Satistaki_Urunler(
Genel_Sepet_No int,
Sepet_Numara int identity (1,1),
Urun_ID int,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
Alis_Urun_Fiyat money,
Urun_Barkod Bigint,
Satilma_Tarihi DateTime,
Urun_Görsel varchar(100)
)

create table Parcali_Odeme_(
Parcali_sepet_No int primary key identity(1,1),
Genel_Sepet_No int,
Sepet_Numara int,
Odeme_Fiyat money,
Odeme_Tipleri nvarchar(20),
)

create table Genel_Odeme_(
Genel_Sepet_Odeme_No int identity(1,1),
Genel_Sepet_No int,
Odeme_Fiyat money,
Odeme_Tipleri nvarchar(20),
Odeme_Tarihi DateTime,
)

create table Satilan_Urunler(
Genel_Sepet_No int,
Sepet_Numara int Primary key,
Urun_ID int,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
Alis_Urun_Fiyat money,
Urun_Barkod Bigint,
Satilma_Tarihi DateTime,
)

create table Iade_Iptal_Urunler(
Sepet_Numara int,
Urun_ID int,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
Urun_Barkod Bigint,
Odeme_Tipleri nvarchar(20),
Satilma_Tarihi DateTime,
Iade_Tarihi DateTime
)

create table Kategori_Ekle(
Urun_ID int identity,
Urun_Kategori nvarchar (100),
Kategori_Eklenme_Tarihi DateTime,
)

create table Genel_Ayarlar(
Raporlardaki_Iade_Sistemi int,
Yazdirma_Islemi int,
Yazicim nvarchar(80),
RaporAdresi nvarchar(150),
)

create table Lisans(
LisansDurum int,
LisansKey nvarchar(19),
)

create table Yetkilendirme(
Id int Primary Key identity,
Kullanici_Adi nvarchar(30),
Kullanici_Sifre nvarchar(4),
Kullanici_Yetki nvarchar(10),
)

create table Kasa_Kapanis_Acilis(
Kasa_Gun_ID int Primary Key identity(1,1),
Kasa_Acilis nvarchar(30),
Kasa_Acilis_Toplam_Para money,
Acilis_Tarih Date,
Kasa_Kapanis nvarchar(30),
Kasa_Kapanis_Toplam_Para money,
Kapanis_Tarih Date,
)

go

Insert Into Yetkilendirme values ('Müdür','0','Admin')

go

Insert Into Genel_Ayarlar values ('0','0',null,null)

go

Insert Into Lisans values (null,null)

go

create trigger Kullanici_Adi_Konrol ON Yetkilendirme
For insert 
as
begin 
declare @KullaniciID int,  @KullaniciAdi nvarchar(50),@AyniVarmi int
SELECT @KullaniciID = Id, @KullaniciAdi= Kullanici_Adi from inserted
SELECT @AyniVarmi=COUNT(*) FROM Yetkilendirme Group By Kullanici_Adi having @KullaniciAdi=Kullanici_Adi
IF (@AyniVarmi>1)
BEGIN
RAISERROR('Kullanıcı adı mevcuttur.', 16, 1);
rollback tran 
Delete from Yetkilendirme where @KullaniciID=Id
END
END

go

create trigger Adet_Dusurme on Satistaki_Urunler
after insert
as
begin
declare @Urun_ID int,@Urun_Adet int,@Stok_Urun_Adet int
select @Urun_ID=Urun_ID,@Urun_Adet=Urun_Adet From inserted
select @Stok_Urun_Adet=Urun_Adet from Stok_Urunler where Urun_ID = @Urun_ID
if(@Stok_Urun_Adet > 0)
begin
update Stok_Urunler set Urun_Adet=Urun_Adet-1 where Urun_ID=@Urun_ID
end
else 
begin	
raiserror('Stok Yetersiz',16,1)
rollback tran 
end
end

go

create trigger Stok_Urunler_Adet_Kontrol on Stok_Urunler
after update
as
begin
declare @Urun_ID int,@Stok_Urun_Adet int
select @Urun_ID=Urun_ID From inserted
select @Stok_Urun_Adet=Urun_Adet from Stok_Urunler where Urun_ID = @Urun_ID
if(@Stok_Urun_Adet < 0)
begin
raiserror('Stok Yetersiz',16,1)
rollback tran 
end
end 

go

create trigger Satistaki_Urunler_Adet_Kontrol
on Satistaki_Urunler
after update 
as
begin
declare @Urun_ID int ,@Urun_Fiyat decimal,@Urun_adet int ,@Urun_Fiyat_Satis decimal
select @Urun_ID=Urun_ID,@Urun_adet=Urun_Adet,@Urun_Fiyat_Satis=Urun_Fiyat from inserted
select @Urun_Fiyat = Urun_Fiyat From Stok_Urunler Where @Urun_ID = Urun_ID
if(@Urun_Fiyat_Satis/@Urun_adet < @Urun_Fiyat)
begin
update Satistaki_Urunler set Urun_Fiyat = Urun_Fiyat+@Urun_Fiyat Where @Urun_ID = Urun_ID
end
else
begin
update Satistaki_Urunler set Urun_Fiyat = Urun_Fiyat-@Urun_Fiyat Where @Urun_ID = Urun_ID
end
end

go

create trigger Satis_Adet_Artirma
on  Satistaki_Urunler
after insert
as
begin
declare @Urun_ID int,@Urun_Adet int,@Urun_adet_kac int,@Sepet_Numara int,@Urun_Fiyat decimal
select @Urun_ID=Urun_ID,@Urun_Adet=Urun_Adet,@Urun_Fiyat=Urun_Fiyat From inserted
select @Urun_adet_kac=COUNT(Urun_ID) from Satistaki_Urunler where Urun_ID = @Urun_ID 
if(@Urun_adet_kac > 1)
begin 
update Satistaki_Urunler set Urun_Adet=Urun_Adet+@Urun_Adet where Urun_ID=@Urun_ID
select @Sepet_Numara=max(Sepet_Numara) from Satistaki_Urunler
ALTER TABLE Satistaki_Urunler DISABLE TRIGGER Toplu_Silme_Kontrol
delete from Satistaki_Urunler where  @Sepet_Numara = Sepet_Numara
end
ALTER TABLE Satistaki_Urunler ENABLE TRIGGER Toplu_Silme_Kontrol
end

go


create trigger Parcali_odeme_fiyat_duzen
on  Parcali_Odeme_
after insert
as
begin
declare @Genel_Sepet_No int,@Urun_Adet int,@fiyat decimal,@sepet_no int
select @Genel_Sepet_No=Genel_Sepet_No ,@sepet_no=Sepet_Numara from inserted
if(@sepet_no != 0)
begin
select @Urun_Adet=Urun_Adet,@fiyat=Urun_Fiyat From Satistaki_Urunler Where @Genel_Sepet_No = Genel_Sepet_No
update Parcali_Odeme_ set Odeme_Fiyat=@fiyat/@Urun_Adet where @Genel_Sepet_No=Genel_Sepet_No
end
end

go 

CREATE TRIGGER Toplu_Silme_Kontrol
ON Satistaki_Urunler
AFTER DELETE
AS
BEGIN
    -- Silinecek ürünlerin listesi "deleted" tablosundan alınır
    DECLARE @Urun_ID INT, @Urun_Adet INT, @Stok_Adet INT;

    DECLARE cursor_silinecek_urunler CURSOR FOR
    SELECT Urun_ID, Urun_Adet FROM deleted;

    OPEN cursor_silinecek_urunler;

    FETCH NEXT FROM cursor_silinecek_urunler INTO @Urun_ID, @Urun_Adet;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Stoktaki miktarı al
        SELECT @Stok_Adet = Urun_Adet FROM Stok_Urunler WHERE Urun_ID = @Urun_ID;

        -- Stoktaki ürün miktarını güncelle (adetleri toplu olarak artır)
        UPDATE Stok_Urunler
        SET Urun_Adet = Urun_Adet + @Urun_Adet
        WHERE Urun_ID = @Urun_ID;

        FETCH NEXT FROM cursor_silinecek_urunler INTO @Urun_ID, @Urun_Adet;
    END;

    CLOSE cursor_silinecek_urunler;
    DEALLOCATE cursor_silinecek_urunler;
END;

go

create trigger Iade_Alma
on  Satilan_Urunler
after delete
as
begin
declare @Urun_ID int,@Urun_Adet int
select @Urun_ID=Urun_ID,@Urun_Adet=Urun_Adet From deleted
update Stok_Urunler set Urun_Adet=Urun_Adet+@Urun_Adet where Urun_ID=@Urun_ID
end

go

CREATE TRIGGER Barkod_Karsilastir ON Ekli_Urunler
for INSERT
AS
BEGIN
DECLARE @Urun_id INT, @Urun_barkod BIGINT, @Ayni_Varmi INT;
SELECT @Urun_id = Urun_ID, @Urun_barkod = Urun_Barkod FROM inserted;
SELECT @Ayni_Varmi=COUNT(*) FROM Ekli_Urunler Group By Urun_Barkod having @Urun_barkod=Urun_Barkod
IF (@Ayni_Varmi>1)
BEGIN
RAISERROR('Aynı Barkod ikikez kullanılmaz.', 16, 1);
rollback tran 
Delete from Ekli_Urunler where @Urun_id=Urun_ID
END
END

go


create trigger Adet_Kontrol on Stok_Urunler 
for update
as 
begin 
declare @Urun_ID int,@Urun_Adet int,@Emir_Adet int
select @Urun_ID=Urun_ID ,@Urun_Adet=Urun_Adet,@Emir_Adet=Emir_Adet from inserted 
select @Urun_Adet=Urun_Adet,@Emir_Adet=Emir_Adet from Stok_Urunler where @Urun_ID=Urun_ID
IF(@Urun_Adet=@Emir_Adet) 
BEGIN 
INSERT INTO Emir_Urunler(Urun_ID, Urun_Kategori, Urun_Marka, Urun_Adi, Urun_Adet,Emir_Adet,Urun_Barkod) SELECT Stok_Urunler.Urun_ID,Stok_Urunler.Urun_Kategori,Stok_Urunler.Urun_Marka,Stok_Urunler.Urun_Adi,Stok_Urunler.Urun_Adet,Stok_Urunler.Emir_Adet,Stok_Urunler.Urun_Barkod FROM Stok_Urunler LEFT JOIN Emir_Urunler ON Stok_Urunler.Urun_ID = Emir_Urunler.Urun_ID WHERE @Urun_ID=Stok_Urunler.Urun_ID
END
END


go 

create trigger Iade_Alma_Genel_Odeme
on  Genel_Odeme_
after insert
as
begin
declare @Odeme_Tipi nvarchar(20),@Genel_Sepet_Odeme_No int
select @Odeme_Tipi=Odeme_Tipleri,@Genel_Sepet_Odeme_No=Genel_Sepet_Odeme_No From inserted
IF(@Odeme_Tipi='İADE')
begin
update Genel_Odeme_ set Odeme_Fiyat = -1*Odeme_Fiyat WHERE Genel_Sepet_Odeme_No=@Genel_Sepet_Odeme_No
END
END



go 

CREATE TRIGGER Acilan_Kasa_Aynimi 
ON Kasa_Kapanis_Acilis
AFTER INSERT 
AS 
BEGIN 
    DECLARE @Kasa_id INT, @Acilis_Tarih DATE, @Acilis_Tarih_Varmi_Ayni INT

    SELECT @Acilis_Tarih = Acilis_Tarih, @Kasa_id = Kasa_Gun_ID FROM inserted

    SELECT  @Acilis_Tarih_Varmi_Ayni = COUNT(Acilis_Tarih) FROM Kasa_Kapanis_Acilis GROUP BY Acilis_Tarih HAVING @Acilis_Tarih = Acilis_Tarih

    IF (@Acilis_Tarih_Varmi_Ayni > 1)
    BEGIN
        RAISERROR('Bu açılış daha önce yapılmış. Tekrar yapılamaz.', 16, 1);
        ROLLBACK TRAN;
        DELETE FROM Kasa_Kapanis_Acilis WHERE @Kasa_id = Kasa_Gun_ID;
    END
END

go

Create Trigger Kapanacak_Kasa_Varmi
on Kasa_Kapanis_Acilis
after update 
As Begin 
Declare @Kapanis_Tarih date , @Acilis_Tarih_Varmi date
Select Kapanis_Tarih=@Kapanis_Tarih From inserted
Select  @Acilis_Tarih_Varmi = Min(Acilis_Tarih) From Kasa_Kapanis_Acilis
IF(@Kapanis_Tarih != @Acilis_Tarih_Varmi)
Begin
RAISERROR('Gün açılışı yapılmadan, kapanış yapılamaz.', 16, 1);
Rollback tran 
END
END

delete from Genel_Odeme_