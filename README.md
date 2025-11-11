# 💱 Currency APP

Aplikacja webowa do przeliczania kursów walut oraz ich historii na podstawie danych pobranych z **NBP API**.  
Zbudowana w architekturze **Clean Architecture** z użyciem **.NET** po stronie backendu oraz **Angular** po stronie frontendowej.

---

## 📦 Technologie

**Backend:**  
- .NET 8  
- ASP.NET Core Web API  
- Serilog  
- FluentValidation  
- Clean Architecture  

**Frontend:**  
- Angular 17  
- TypeScript  
- RxJS  

**Źródło danych:**  
- [NBP API](https://api.nbp.pl/)

---

## ⚙️ Uruchomienie projektu lokalnie

### 1. Klonowanie repozytorium
```bash
git clone https://github.com/b0zenka/CurrencyApp.git
cd CurrencyApp
```

---

### 2. Backend (.NET)

#### Wejście do folderu backendu:
```bash
cd backend
```

#### Instalacja zależności:
```bash
dotnet restore
```

#### Uruchomienie aplikacji:
```bash
dotnet run --project src\CurrencyApp.WebApi\CurrencyApp.WebApi.csproj --launch-profile https
```

Aplikacja backendowa domyślnie uruchomi się pod adresem:  
👉 `https://localhost:7134`

---

### 3. Frontend (Angular)

#### Wejście do folderu frontendowego:
```bash
cd ../frontend
```

#### Instalacja zależności:
```bash
npm install
```

#### Uruchomienie aplikacji:
```bash
npm start
```

Frontend domyślnie dostępny będzie pod adresem:  
👉 `http://localhost:4200`

> Upewnij się, że backend działa przed uruchomieniem frontendu.  
> Adres API można zmienić w pliku `proxy.conf.json`.

---

## 🧩 Funkcjonalności

- Wybór dostawcy API (domyślnie NBP)  
- Wybór waluty źródłowej i docelowej  
- Wybór zakresu dat `od - do` (domyślnie dziś)  
- Wyświetlanie:
  - minimalnej wartości kursu  
  - maksymalnej wartości kursu  
  - średniej wartości kursu  
  - tabeli z wartościami dla każdego dnia  

---

## 🧱 Struktura projektu

```
CurrencyApp/
 ├── backend/
 │   ├── CurrencyApp.Application/
 │   ├── CurrencyApp.Domain/
 │   ├── CurrencyApp.Infrastructure/
 │   ├── CurrencyApp.WebApi/
 │   └── appsettings.json
 └── frontend/
     ├── src/
     │   └── app/
     ├── angular.json
     ├── package.json
     └── proxy.conf.json
```

---

## 🪣 Plik konfiguracyjny

Zawiera m.in.:
- adresy API,
- format daty (`yyyy-MM-dd`),
- sposób sortowania walut (po kodzie lub nazwie),
- konfigurację loggera (ścieżka, poziom logowania).

---

## 🧾 Logowanie błędów

Aplikacja korzysta z **Serilog**.  
Logi są zapisywane do pliku (ścieżka konfigurowalna w `appsettings.json`).

---

## 🧑‍💻 Autor

**Currency APP**  
Autor: *Bożena Mazur-Babiuch*  
