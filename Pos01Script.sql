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
Urun_Barkod Bigint,
Eklenme_Tarihi DateTime,
Son_Guncellenme_Tarihi DateTime,
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
Sepet_Numara int identity (1,1),
Urun_ID int,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
Urun_Barkod Bigint,
Satilma_Tarihi DateTime,
)

create table Satilan_Urunler(
Sepet_Numara int identity (1,1),
Urun_ID int,
Urun_Kategori nvarchar (100),
Urun_Marka nvarchar (100),
Urun_Adi nvarchar (100),
Urun_Adet int,
Urun_Fiyat money,
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
Satilma_Tarihi DateTime,
Iade_Tarihi DateTime
)

create table Kategori_Ekle(
Urun_ID int identity,
Urun_Kategori nvarchar (100),
Kategori_Eklenme_Tarihi DateTime,
)

create table Yetkilendirme(
Id int Primary Key identity,
Kullanici_Adi nvarchar(30),
Kullanici_Sifre nvarchar(4),
Kullanici_Yetki nvarchar(5),
)

go

Insert Into Yetkilendirme values ('Müdür','0','Admin')

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
update Stok_Urunler set Urun_Adet=Urun_Adet-@Urun_Adet where Urun_ID=@Urun_ID
end
else 
begin	
raiserror('Stok Yetersiz',16,1)
rollback tran 
end
end 

go

create trigger Adet_Artirma
on  Satistaki_Urunler
after delete
as
begin
declare @Urun_ID int,@Urun_Adet int
select @Urun_ID=Urun_ID,@Urun_Adet=Urun_Adet From deleted
update Stok_Urunler set Urun_Adet=Urun_Adet+@Urun_Adet where Urun_ID=@Urun_ID
end

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
RAISERROR('Ayný Barkod ikikez kullanýlmaz.', 16, 1);
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