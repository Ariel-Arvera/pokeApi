# PokeAPI - API REST de Pokémon (.NET 8)

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

API REST construída con .NET 8 orientada a aprender conceptos de desarrollo backend. Ideal para desarrolladores junior que wanten entender APIs REST, Clean Architecture e inyección de dependencias.

## 🚀 Estructura del Proyecto

```
PokeApi/
├── Domain/
│   ├── Entities/        # Entidades del negocio (Pokemon, Move)
│   └── Enums/          # Tipos de Pokémon (Fire, Water, etc.)
├── Application/
│   ├── DTOs/           # Objetos de transferencia de datos
│   ├── Interfaces/     # Contratos para repositorios
│   └── Services/       # Lógica de negocio
├── Infrastructure/
│   └── Repositories/  # Implementaciones en memoria
└── Controllers/        # Endpoints HTTP
```

## 📋 Endpoints

### Pokémon
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/pokemon` | Obtiene todos los Pokémon |
| GET | `/api/pokemon/{id}` | Obtiene un Pokémon específico |
| POST | `/api/pokemon` | Crea un nuevo Pokémon |
| DELETE | `/api/pokemon/{id}` | Elimina un Pokémon |

### Movimientos
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/move` | Obtiene todos los movimientos |
| POST | `/api/move` | Crea un nuevo movimiento |

### Batalla
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/battle/calculate-damage` | Calcula el daño entre Pokémon |

## 🧠 Cómo funciona el cálculo de daño

La fórmula implementada sigue el sistema clásico de Pokémon:

```
damage = (((2 * nivel / 5 + 2) * poder * ataque / defensa) / 50 + 2) * efectividad * aleatorio
```

**Componentes:**
- **Nivel**: Nivel del Pokémon atacante
- **Poder**: Power del movimiento usado
- **Ataque/Defensa**: Estadísticas relevantes según el tipo de movimiento
- **Efectividad**: Tabla de tipos (Fire vs Grass = 2x, Fire vs Water = 0.5x)
- **Aleatorio**: Factor entre 0.85 y 1.0

## 🏗️ Decisiones de Arquitectura

### ¿Por qué Clean Architecture?
- **Separación de responsabilidades**: Cada capa tiene un propósito claro
- **Testabilidad**: Los servicios pueden probarse independientemente
- **Mantenibilidad**: Código más fácil de modificar y entender

### ¿Por qué repositorios en memoria?
- Sin configuración de base de datos
- Ideal para aprendizaje y pruebas
- Fácil de reemplazar por una BD real luego

## 📦 DTOs vs Entidades

- **Entidades**: Representan el modelo de dominio (内部)
- **DTOs**: Objetos que se transfieren por la API (externo)

Esto protege la lógica interna y permite cambios sin afectar a los clientes.

## 🛠️ Ejecutar el proyecto

```bash
cd PokeApi
dotnet run
```

Swagger disponible en: `http://localhost:5000/`

## 📚 Conceptos aprendidos

1. **Controllers** - Manejo de solicitudes HTTP
2. **Servicios** - Lógica de negocio desacoplada
3. **Inyección de dependencias** - (`AddScoped`, `AddSingleton`)
4. **Patrón Repository** - Abstracción de acceso a datos
5. **DTOs** - Transferencia de datos entre capas
6. **Swagger/OpenAPI** - Documentación automática
7. **Validación de modelos** - `[ApiController]`, `[Required]`

## 🔄 Mejoras futuras (para aprender más)

- Agregar base de datos (SQL Server, PostgreSQL, MongoDB)
- Agregar autenticación con JWT
- Implementar validación con FluentValidation
- Agregar Unit Tests con xUnit
- Implementar Logging
- Agregar Health Checks