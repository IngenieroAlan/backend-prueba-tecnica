# Backend de la prueba tecnica
Este proyecto fue elaborado con las tecnologia de .NET 8, Entity Framework Core y MySql

### Nota
Se recomienda complementar el backend con el proyecto del siguiente repositorio:
- [frontend-prueba-tecnica](https://github.com/IngenieroAlan/frontend-prueba-tecnica)

## Instrucciones
Clona el proyecto ejecuta el siguiente comando.

```bash
  git clone https://github.com/IngenieroAlan/backend-prueba-tecnica.git
```

### Instalar el proyecto desde la terminal con .NET8
#### Nota: En windows, de aquí en adelante ejecutar desde la PowerShell.

Cambiamos la ruta de la consola a la del proyecto
```bash
  cd backend-prueba-tecnica/BackendPruebaTecnica
```

Restauramos las dependencias.
- Nota: Ejecutar el siguiente comando solo si es la primera vez que corres el proyeto.
```bash
  dotnet restore
```
Construimos el proyecto y comprobamos que no haya errores
```bash
  dotnet build
```
Ejecutamos el proyecto.
```bash
  dotnet run
```
### Instalar el proyecto desde Visual studio
- Desde nuestro explorador de archivos nos vamos a la carpeta del proyecto y damos doble clic en la solucion llamada "BackendPruebaTecnica.sln".
- Dentro de Visual Studio, selecciona la opcion http.
(Nota: Si deseas cambiar el protocolo http entonces deberas cambiar la ruta de la api en el frontend)
- Dar click en ejecutar

### Importante*
Este proyecto tiene dos opciones como base de datos, tiene la opcion de usar una base de datos remota (default) la cual las credenciales ya vienen configuradas y esta la version local la cual necesitaras hacer lo siguiente: 
- 1.- Abrir el archivo "appsettings.json" ubicado en la raiz del proyecto.
- 2.- Ir a "ConnectionStrings".
- 3.- En "ConnectionStrings" buscar "LocalConnection" y colocar tus credenciales.


Si estas usando el proyecto [frontend-prueba-tecnica](https://github.com/IngenieroAlan/frontend-prueba-tecnica) y no puedes loggearte. Revisa si tienes algun usuario registrado en la base de datos de ser lo contrario puedes crear uno ejecutando la siguiente query.
```
  INSERT INTO `pruebatecnicadb`.`users`
  (`UserName`,
  `Email`,
  `RegisterDate`)
  VALUES
  ('admin','admin@mail.com',now());

```

## Autor

- [@ingenieroAlan](https://www.github.com/ingenieroAlan)
