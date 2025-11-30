# 🎵 Fia Music PlayList Maker
## Proyecto final de la materia de Estructura de Datos de la carrera de Ingenieria en Sistemas Informaticos de la Universidad de El Salvador
En este proyecto pnemos en practica el conocicmiento acerca de la estructura Listas Circulares Doblemete Enlazadas.
Consumimos la API de Deezer para buscar canciones y las guardamos en una implemetacion propia de las lisas circulares doblemente enlazas, con la posibilidad de agregar canciones,  eliminar canciones, eliminar y agregar listas de reproduccion y exportarlas a un formato de texto plano.

Aplicación de consola para crear y gestionar playlists de música, con búsqueda de canciones usando la API pública de Deezer. Permite:
- Buscar canciones por título o artista.
- Crear y administrar hasta 10 playlists.
- Agregar y eliminar canciones de una playlist.
- Exportar playlists a archivos `.txt`.
- Importar playlists desde archivos `.txt`.

## 🚀 Características

- Búsqueda en Deezer:
  - Consulta `https://api.deezer.com/search?q=<query>` y muestra hasta 10 resultados.
- Gestión de playlists:
  - Estructura interna con lista doblemente enlazada (`ListaEnlazada`, `Node`, `Track`).
  - Operaciones: agregar al final, imprimir, eliminar por `Id`, buscar por título.
- Importación/Exportación:
  - Exporta a texto plano: primera línea es el nombre de la lista, cada canción en formato `Title - Artist - Album - Id`.
  - Importa desde un archivo `.txt` con el mismo formato.

## ⚙️ Requisitos

- Windows con .NET Framework 4.7.2 (o superior compatible).
- Visual Studio 2022.
- Conexión a Internet para la búsqueda en Deezer.

## 📋 Estructura del proyecto

- `Program.cs`: Menú principal y flujo de la aplicación.
- `DeezerManager.cs`: Integración con la API de Deezer y parseo de resultados.
- `ListaEnlazada.cs`, `Nodo.cs`, `Cancion.cs`:
  - Implementación de la lista enlazada y el modelo `Track`.

## 🎛️ Uso

Al ejecutar la app, se muestra un menú con las opciones:
1. Buscar canción en Deezer:
   - Ingresa título o artista, selecciona un resultado y añádelo a una playlist existente o crea una nueva.
2. Mostrar mis playlists:
   - Lista las playlists y muestra sus canciones.
3. Eliminar playlist o canciones:
   - Elimina una playlist completa o canciones individuales por `Id`.
4. Exportar mis listas:
   - Exporta una playlist a `NombreLista.txt`.
5. Importar lista:
   - Importa desde un `.txt` con el formato soportado.
6. Salir.

Ejemplos de importación sugeridos en la app:
- `coldplay hits.txt`
- `metalica hits.txt`

## 💻 Compilación y ejecución

1. En Visual Studio, asegúrate de que el proyecto es de tipo `Console Application`:
   - __Properties__ > __Application__ > __Output type__: “Console Application”.
2. Compila en modo Release:
   - Selecciona “Release” y usa __Build > Build Solution__.
3. Ejecutable:
   - `AppMusicListas\bin\Release\AppMusicListas.exe`.

Para distribuir, copia todo el contenido de `bin\Release\` (incluye `.exe`, `.config` y dependencias).

## Notas sobre la API de Deezer

- Se usa la API pública de Deezer sin autenticación para búsqueda.
- Los datos se consumen en formato JSON y se transforman a `Track`.
- Manejo de errores básico: conexión y JSON inválido.

## Créditos

- Datos de música proporcionados por la API pública de Deezer.
