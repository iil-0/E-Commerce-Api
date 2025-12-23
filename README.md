# E-Commerce API

.NET 9 ile geliştirilmiş REST API projesi. Temel e-ticaret işlemlerini (kullanıcı, kategori, ürün, sipariş) yönetir.

## Proje Yapısı

```
ECommerce.Api/
├── Common/          # ApiResponse wrapper
├── Context/         # DbContext ve Seeder
├── Controllers/     # REST API controller'ları
├── DTOs/            # Data Transfer Objects
├── Endpoints/       # Minimal API endpoint'leri
├── Enums/           # Sabit değerler
├── Interfaces/      # Service interface'leri
├── Middlewares/     # Exception handling
├── Models/          # Entity sınıfları
├── Services/        # Business logic
└── Program.cs       # Uygulama başlangıcı
```

## Mimari

```
HTTP İsteği
    ↓
Controller / Minimal API
    ↓
Service (Business Logic)
    ↓
DbContext (Entity Framework)
    ↓
PostgreSQL
```

## Kurulum

1. PostgreSQL'de `ecommerce_db` veritabanı oluştur
2. `appsettings.json` içindeki connection string'i güncelle
3. Migration uygula:
```bash
dotnet ef database update
```
4. Projeyi çalıştır:
```bash
dotnet run
```
5. Swagger: http://localhost:5185/swagger

## Endpoint'ler

### Auth (Token gerektirmez)
| Method | URL | Açıklama |
|--------|-----|----------|
| POST | /api/auth/register | Kayıt ol |
| POST | /api/auth/login | Giriş yap |

### Categories (Token gerekli)
| Method | URL | Açıklama |
|--------|-----|----------|
| GET | /api/categories | Tümünü listele |
| GET | /api/categories/{id} | Tek kayıt |
| POST | /api/categories | Yeni ekle |
| PUT | /api/categories/{id} | Güncelle |
| DELETE | /api/categories/{id} | Sil |

### Products (Token gerekli)
| Method | URL | Açıklama |
|--------|-----|----------|
| GET | /api/products | Tümünü listele |
| GET | /api/products/{id} | Tek kayıt |
| POST | /api/products | Yeni ekle |
| PUT | /api/products/{id} | Güncelle |
| DELETE | /api/products/{id} | Sil |

### Users (Token gerekli)
| Method | URL | Açıklama |
|--------|-----|----------|
| GET | /api/users | Tümünü listele |
| GET | /api/users/{id} | Tek kayıt |
| POST | /api/users | Yeni ekle |
| PUT | /api/users/{id} | Güncelle |
| DELETE | /api/users/{id} | Sil |

### Orders (Token gerekli)
| Method | URL | Açıklama |
|--------|-----|----------|
| GET | /api/orders | Tümünü listele |
| GET | /api/orders/{id} | Tek kayıt |
| POST | /api/orders | Yeni ekle |
| PUT | /api/orders/{id} | Güncelle |
| DELETE | /api/orders/{id} | Sil |

### Minimal API
Aynı işlemler `/api/minimal/categories` ve `/api/minimal/products` altında da mevcut.

## API Response Formatı

Tüm cevaplar şu formatta döner:

```json
{
  "success": true,
  "message": "İşlem başarılı",
  "data": { ... }
}
```

Hata durumunda:
```json
{
  "success": false,
  "message": "Hata açıklaması",
  "data": null
}
```

## JWT Kullanımı

1. `/api/auth/login` ile giriş yap
2. Response'dan `token` değerini al
3. İsteklerde header'a ekle: `Authorization: Bearer {token}`

## Özellikler

- JWT Authentication
- Soft Delete (IsDeleted)
- Global Exception Handling
- Serilog ile Logging
- Swagger/OpenAPI
- Entity Framework Core + PostgreSQL
- Seed Data (otomatik başlangıç verileri)

## Test Kullanıcıları

Uygulama başladığında otomatik oluşturulur:

| Email | Şifre | Rol |
|-------|-------|-----|
| admin@ecommerce.com | admin123 | Admin |
| user@ecommerce.com | user123 | User |
