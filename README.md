# HotelBooking

**HotelBooking** – це веб-додаток для бронювання готелів та кімнат з підтримкою ролей користувачів (Admin, User) та JWT-аутентифікацією.

---

## 🚀 Початок роботи

Цей проєкт запускається за допомогою Docker, тому не потрібно нічого встановлювати локально окрім Docker та Docker Compose.

---

### 1. Клонування репозиторію

```bash
git clone https://github.com/V1TOSIK/Marketplace.git
cd Marketplace
```

---

### 2. Створення .env файлу

Скопіюйте приклад .env.example та заповніть власні дані:

Переконайтеся, що в .env вказані правильні налаштування для баз даних MySQL та PostgreSQL, а також JWT:

# MySQL
SERVER=mysql-db

PORT=3306

MYSQL_USER=mysql

MYSQL_PASSWORD=mysql

MYSQL_DATABASE=hotelbooking

MYSQL_ROOT_PASSWORD=rootpassword

ConnectionStrings__MySql=Server=${SERVER};Port=${PORT};Database=${MYSQL_DATABASE};User=${MYSQL_USER};Password=${MYSQL_PASSWORD};

# JWT
JwtOptions__SecretKey=YourSecretKeyHereYourSecretKeyHere

JwtOptions__ExpiryMinutes=600


---

### 3. Запуск проекту через Docker

```bash
docker-compose up --build -d
```

⚠️ Якщо бекенд впав відразу після запуску, не хвилюйся. Це часто відбувається через те, що база даних ще не піднялася.
Потрібно зачекати кілька секунд і підняти контейнер знову:

docker-compose restart hotelbooking-backend

---

### 4. Перевірка

```bash
http://localhost:3000
```
Бекенд API буде доступний на порту, який вказаний у docker-compose.yml.

---

### 5. Структура проєкту

HotelBooking.Api – Web API (backend)

HotelBooking.Application – CQRS, MediatR, Dto

HotelBooking.Domain – сутності, value objects

HotelBooking.Persistence – репозиторії, контекст БД

Frontend – Alpine.js localhost:3000

---

### 6. Користувацькі ролі

Admin – може створювати, редагувати та видаляти готелі та кімнати.

User – може переглядати готелі та бронювати кімнати.

---

### 7. Токени та JWT

Токен генерується через JwtService.
Термін життя токена визначається в .env через JwtOptions__ExpiryMinutes.
Наприклад, 600 = токен буде жити 10 годин.

---

### 8. Важливі команди Docker

Підняти всі сервіси у фоновому режимі
```bash
docker-compose up --build -d
```

Перегляд логів
```bash
docker-compose logs -f
```

Перезапуск окремого сервісу
```bash
docker-compose restart <service>
```

Зупинити та видалити контейнери
```bash
docker-compose down
```
### 9. Примітки

База даних може запускатися трохи довше за бекенд, тому іноді потрібно перезапускати API-контейнер.

Після запуску можна створювати користувачів, готелі, кімнати і тестувати бронювання.