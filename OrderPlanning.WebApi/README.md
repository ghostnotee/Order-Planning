# Exel dosyası düzenleme:

Kullanılan teknolojiler:

.Net 6, EntityFrameworkCore 6.0.4, ClosedXML, Swagger.

→ Müşteriden excel olarak gelen sipariş formu import edildi.

→ Gelen talep in memory database’e yazıldı.

→ Geçici bir Order listesi oluşturuldu.

→ Minimum sipariş adedinden düşük gelen sipariş adedi; o ürün için toplanıp birleştirildi.

→ Sipariş adedi MOQ’ya eşit yada büyükse yeni bir kayıt oluşturuldu.

→ Oluşturulan liste yine ClosedXML ile excel oluşturulup export edildi. (Sürüyor)