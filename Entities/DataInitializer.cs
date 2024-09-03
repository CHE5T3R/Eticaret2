using Eticaret.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Eticaret.Entities
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var categories = new List<Category>()
            {
                new Category(){Name="Kamera", Description="Kamera Ürünleri"},
                new Category(){Name="Bilgisayar", Description="Bilgisayar Ürünleri"},
                new Category(){Name="Elektronik", Description="Elektronik Ürünleri"},
                new Category(){Name="Telefon", Description="Telefon Ürünleri"},
                new Category(){Name="Beyaz Eşya", Description="Beyaz Eşya Ürünleri"}
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();


            var products = new List<Product>()
            {
                new Product(){Name="Nikon P950 Coolpix Fotoğraf Makinesi", Description="Bu kamera çok iyi. Uzun açıklama. Uzun açıklama. Uzun açıklama. Uzun açıklama.", Price=40000, Stock=10, IsApproved=true, CategoryId=1, IsHome = true, Image="camera.jpg"},
                new Product(){Name="Nikon D3500 Af-P 18-55MM Vr Fotoğraf Makinesi", Description="Bu daha iyi", Price=21000, Stock=9, IsApproved=true, CategoryId=1, IsHome=false, Image="camera.jpg"},
                new Product(){Name="Nikon D3500 Body Dijital Slr Fotoğraf Makinesi", Description="Bu ondan da iyi", Price=16341, Stock=8, IsApproved=true, CategoryId=1, IsHome=false, Image="camera.jpg"},
                new Product(){Name="Nikon D3400 18-105MM Vr Fotoğraf Makinesi (Karfo Karacasulu Garantili)", Description="En iyisi bu", Price=23500, Stock=7, IsApproved=true, CategoryId=1, IsHome=false, Image="camera.jpg"},
                new Product(){Name="Nikon D5500 + 18-55MM Vr Lens Dijital Slr Fotoğraf Makinesi", Description="Bu kötü", Price=28066.50, Stock=6, IsApproved=false, CategoryId=1, IsHome=false, Image="camera.jpg"},

                new Product(){Name="Lenovo LOQ 12.Nesil Core i5 12450HX-Arc A530M 4Gb-8Gb-512Gb Ssd-15.6-W11", Description="Bu bilgisayar çok iyi", Price=22999, Stock=10, IsApproved=true, CategoryId=2, IsHome=false, Image="computer.jpg"},
                new Product(){Name="Lenovo Ideapad Slim 3 12.Nesil Core i5 12450H-8Gb-512Gb Ssd-16inc-W11", Description="Bu daha iyi", Price=17499, Stock=9, IsApproved=true, CategoryId=2, IsHome=false, Image="computer.jpg"},
                new Product(){Name="Lenovo Ideapad 3 12.Nesil Core i3 1215U-8Gb-256Gb Ssd-15.6inc-W11", Description="Bu ondan da iyi", Price=11999, Stock=8, IsApproved=true, CategoryId=2, IsHome = true, Image="computer.jpg"},
                new Product(){Name="Lenovo Ideapad Slim 3 12.Nesil Core i5 12450H-8Gb-512Gb Ssd-15.6inc-W11", Description="En iyisi bu", Price=17499, Stock=7, IsApproved=true, CategoryId=2, IsHome=true, Image="computer.jpg"},
                new Product(){Name="Lenovo Ideapad 1 Celeron N4020-4Gb-128Gb Ssd-15.6inc-W11", Description="Bu kötü", Price=6999, Stock=6, IsApproved=false, CategoryId=2, IsHome = false, Image="computer.jpg"},

                new Product(){Name="Bistopia Taşınabilir Mini Fan Soğutucu Masa Üstü Vantilatör Usb Şarjlı Pratik", Description="Bu alet çok iyi", Price=275, Stock=10, IsApproved=true, CategoryId=3, IsHome = false, Image="electronicDevices.jpg"},
                new Product(){Name="Sony Playstation 5 Slim Digital Edition 1 Tb Oyun Konsolu 2. Dualsense (İTHALATÇI GARANTİLİ)", Description="Bu daha iyi", Price=22900, Stock=9, IsApproved=true, CategoryId=3, IsHome = false, Image="electronicDevices.jpg"},
                new Product(){Name="JBL Tune 560bt Siyah Wireless Bluetooth Kulak Üstü Kulaklık", Description="Bu ondan da iyi", Price=1580, Stock=8, IsApproved=true, CategoryId=3, IsHome = false, Image="electronicDevices.jpg"},
                new Product(){Name="Arzum Okka Minio Kahve Makinesi Türk Kahvesi OK004 Bakır 4 Fincan Kapasiteli", Description="En iyisi bu", Price=1206, Stock=7, IsApproved=true, CategoryId=3, IsHome=true, Image="electronicDevices.jpg"},
                new Product(){Name="Einhell TP-CW 18 Li BL - Solo, Kömürsüz Akülü Darbeli Somun Sıkma", Description="Bu kötü", Price=2387.35, Stock=6, IsApproved=false, CategoryId=3, IsHome = false, Image="electronicDevices.jpg"},

                new Product(){Name="Apple iPhone 12 128 GB", Description="Bu telefon çok iyi", Price=29999, Stock=10, IsApproved=true, CategoryId=4, IsHome = true, Image="phone.jpg"},
                new Product(){Name="Apple iPhone 13 128 GB", Description="Bu daha iyi", Price=38699, Stock=9, IsApproved=true, CategoryId=4, IsHome = false, Image="phone.jpg"},
                new Product(){Name="Apple iPhone 15 128 GB", Description="Bu ondan da iyi", Price=51199, Stock=8, IsApproved=true, CategoryId=4, IsHome=true, Image="phone.jpg"},
                new Product(){Name="Nokia 3310 Kamerasız Telefon", Description="En iyisi bu", Price=1250, Stock=7, IsApproved=true, CategoryId=4, IsHome=true, Image="phone.jpg"},
                new Product(){Name="HONOR 200 Pro 512 GB 12 GB Ram (Honor Türkiye Garantili)", Description="Bu kötü", Price=39999, Stock=6, IsApproved=false, CategoryId=4, IsHome=false, Image="phone.jpg"},

                new Product(){Name="BSUF 5000 MEB Mini Fırın", Description="Bu ürün çok iyi", Price=6849, Stock=10, IsApproved=true, CategoryId=5, IsHome=true, Image="beyazEşya.jpg"},
                new Product(){Name="CM 9102 BMG Çamaşır Makinesi", Description="Bu daha iyi", Price=30839, Stock=9, IsApproved=true, CategoryId=5, IsHome=false, Image="beyazEşya.jpg"},
                new Product(){Name="CM 9120 BI Çamaşır Makinesi", Description="Bu ondan da iyi", Price=36.229, Stock=8, IsApproved=true, CategoryId=5, IsHome=false, Image="beyazEşya.jpg"},
                new Product(){Name="7371 JEB Çekmeceli Derin Dondurucu", Description="En iyisi bu", Price=24489, Stock=7, IsApproved=true, CategoryId=5, IsHome=false, Image="beyazEşya.jpg"},
                new Product(){Name="3298 JE Sandık Tipi Derin Dondurucu", Description="Bu kötü", Price=17999, Stock=6, IsApproved=false, CategoryId=5, IsHome=false, Image="beyazEşya.jpg"}

            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();


            base.Seed(context);
        }
    }
}