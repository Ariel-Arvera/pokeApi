# PokeAPI - API REST de Pokémon (.NET 8)
Este proyecto fue realizado con el unico objetivo de estudiar la arquitectura y creracion de una API rest Basica y escalable, Se utilizaron documentaciones de StackOverflow y herramientas de IA solo para las consultas sobre errores, la sintaxis y creacion del proyecto corrio absolutamente por mi cuenta.
-Ariel Vera.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

API REST construida con .NET 8 pensada como proyecto de aprendizaje backend. El objetivo es entender cómo estructurar una API real aplicando buenas prácticas como separación por capas, inyección de dependencias y diseño limpio, sin añadir complejidad innecesaria.

---

## Estructura del Proyecto

```
PokeApi/
├── Domain/
│   ├── Entities/        # Modelos principales del dominio (Pokemon, Move)
│   └── Enums/           # Tipos de Pokémon (Fire, Water, etc.)
├── Application/
│   ├── DTOs/            # Objetos que viajan hacia/desde la API
│   ├── Interfaces/      # Contratos (repositorios, servicios)
│   └── Services/        # Lógica de negocio (cálculo de daño, etc.)
├── Infrastructure/
│   └── Repositories/    # Implementaciones en memoria
└── Controllers/         # Endpoints HTTP expuestos
```

### Explicación rápida

- **Domain**: Contiene las entidades y reglas principales. No depende de .NET ni de librerías externas.
- **Application**: Coordina la lógica de negocio mediante servicios e interfaces.
- **Infrastructure**: Implementa detalles técnicos (en este caso, almacenamiento en memoria).
- **Controllers**: Punto de entrada de la API. Reciben peticiones y devuelven respuestas.

---

## Endpoints

### Pokémon

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/pokemon` | Devuelve todos los Pokémon almacenados |
| GET | `/api/pokemon/{id}` | Devuelve un Pokémon por id |
| POST | `/api/pokemon` | Crea un nuevo Pokémon |
| DELETE | `/api/pokemon/{id}` | Elimina un Pokémon |

---

### Movimientos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/move` | Lista todos los movimientos |
| POST | `/api/move` | Crea un movimiento |

---

### Batalla

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/battle/calculate-damage` | Calcula el daño entre dos Pokémon |

Este endpoint no persiste información. Solo ejecuta la lógica y devuelve el resultado.

---

## Cálculo de daño

La lógica implementa una fórmula simplificada basada en los juegos originales:

```
damage = (((2 * nivel / 5 + 2) * poder * ataque / defensa) / 50 + 2) 
         * efectividad 
         * aleatorio
```

### Qué significa cada parte

- **Nivel**: Nivel del Pokémon atacante
- **Poder**: Fuerza del movimiento
- **Ataque / Defensa**: Estadísticas base de los Pokémon
- **Efectividad**: Relación entre tipos (por ejemplo, fuego contra planta hace más daño)
- **Aleatorio**: Factor entre 0.85 y 1.0 para introducir variación

---

## Decisiones de arquitectura

### Separación por capas

Se utiliza una estructura inspirada en Clean Architecture para evitar mezclar responsabilidades:

- La lógica de negocio no depende de la API
- Los controladores no contienen lógica compleja
- Los datos se abstraen mediante interfaces

---

### Uso de repositorios en memoria

No se utiliza base de datos:

- Se almacenan datos en listas en memoria
- Permite ejecutar el proyecto sin configuración adicional
- Es suficiente para demostrar funcionamiento en una entrevista

---

## DTOs vs Entidades

- **Entidades**: Modelo interno del sistema
- **DTOs**: Datos que viajan por la API

Separarlos evita exponer la lógica interna.

---

## Ejecutar el proyecto

```
cd PokeApi
dotnet run
```

Swagger:

```
http://localhost:5000/swagger
```

---

## Conceptos aplicados

1. Controllers
2. Servicios
3. Inyección de dependencias
4. Repository Pattern
5. DTOs
6. Swagger
7. Validación básica

---

## Posibles mejoras

- Base de datos
- JWT
- Validaciones avanzadas
- Tests
- Logging
