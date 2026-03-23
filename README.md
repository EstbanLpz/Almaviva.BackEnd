Sistema de Gestión de Empleados (Almaviva)
Este proyecto es una aplicación web de tipo Full Stack diseñada para gestionar registros de empleados. Permite realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) con seguridad mediante tokens JWT.

Tecnologías Utilizadas
Backend: .NET 8 / C# (Web API)

Frontend: Angular 17+ (Standalone Components)

Seguridad: JSON Web Tokens (JWT) para autenticación.

Estilos: Bootstrap 5 para una interfaz responsiva.

Estructura del Proyecto
1. Backend (.NET Web API)
EmpleadosController.cs: Contiene los Endpoints de la API. Maneja la lógica de login, generación de tokens y manipulación de la lista de empleados.

Empleado.cs: Modelo de datos que define las propiedades (Id, Nombre, Apellido, etc.).

Program.cs: Configuración global (CORS, Autenticación JWT y serialización JSON).

2. Frontend (Angular)
Empleado.service.ts: Servicio que se comunica con la API mediante HttpClient.

Empleados.component.ts: Lógica de la interfaz; maneja el formulario, la tabla y la reactividad.

Auth.service.ts: Gestiona el almacenamiento del token en el localStorage y el estado de la sesión.

Cómo Correr el Proyecto
Paso 1: Levantar el Backend
Abre la carpeta del servidor en Visual Studio o VS Code.

Asegúrate de tener instalado el SDK de .NET 8.

Ejecuta el comando:

Bash
dotnet run
La API estará disponible en http://localhost:5023. Puedes ver la documentación en http://localhost:5023/swagger.

Paso 2: Levantar el Frontend
Navega a la carpeta del proyecto Angular en la terminal.

Instala las dependencias (solo la primera vez):

Bash
npm install
Inicia el servidor de desarrollo:

Bash
ng serve
Abre tu navegador en http://localhost:4200.

Cómo Funciona el Código
Seguridad y Flujo de Datos
Login: El usuario envía credenciales a /api/Empleados/login. Si son correctas (admin / 12345), el servidor devuelve un token firmado.

Persistencia: Angular guarda ese token y lo envía en el Header de cada petición (Authorization: Bearer <token>).

CORS: El servidor permite peticiones específicamente desde localhost:4200 para evitar bloqueos del navegador.

Inmutabilidad: Al actualizar la lista, se usa el operador spread [...data]. Esto le indica a Angular que el array es nuevo y debe redibujar la tabla de inmediato.

Case Sensitivity: El backend está configurado para respetar las mayúsculas (PropertyNamingPolicy = null), asegurando que el Id de C# coincida con el Id de TypeScript.

Reglas de Negocio
Borrado Lógico: Al "Borrar" un empleado, no se elimina de la lista; su propiedad Estado cambia a false (Inactivo).

Validación: No se permiten registros con campos de Nombre, Apellido o Email vacíos.

Credenciales de Acceso
Usuario: admin

Contraseña: 12345
