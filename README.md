# Proyecto Sistema de cines WEB "Cines FLAGG"
## Creadores:
F- Franco Ouro
L- Lucas Marenco
A- Alejandro Solodujin
G- Gaston Mansilla
G- German Pardo

***Proyecto Realizado:***
Sistema de cines (administraci�n con ABM, tipificacion de usuario administrador/cliente, busqueda y compra/devolucion de entradas de funciones)

------------------------------------------------------------------------------------------------------------------------------------------------------
***TIPIFICACI�N***

Tipos de Usuarios:
* Administrador: Al loguearse podra ingresar y maipular al ABM de Usuarios, Peliculas, Salas y Funciones. Ademas podr� ver la cartelera y operar como un usuario cliente.
* Cliente: Podra ver cartelera y comprar tickets para funcion seleccionada.
NOTA: AL crear un usuario nuevo siempre se registra desbloqueado (en la base de datos bloqueado = 0 por default). Solo el administrador al modificar puede bloquearlo (en la base de datos bloqueado = 1).

------------------------------------------------------------------------------------------------------------------------------------------------------
***FORMULARIOS WEB POR TIPO DE USUARIO***

El inicio de la web es nuestra CARTELERA (vista por peliculas). Pero para transaccionar en la misma el usuario debe estar registrado y loguearse.

Formularios del admin:
* Login/Registro (inicio): Permite registrarse o loguearse. Es el inicio de la aplicaci�n ya que es obligatorio estar registrado.
* Cartelera: Es el formulario que permite buscar las funciones existentes para la compra (funciones con fecha de HOY o superior). 
* ABM de Usuarios: Alta Baja y Modificacion de usuarios (solamente administrador)
* ABM de Peliculas: Alta Baja y Modificacion de peliculas (solamente administrador)
* ABM de Salas: Alta Baja y Modificacion de salas (solamente administrador)
* ABM de Funciones:Alta Baja y Modificacion de funciones (solamente administrador)
* Busqueda por sala o por pelicula (cartelera) (para buscar por sala o por pelicula las funciones existentes y comprar). Permitimos al admin la compra.
* Actualizar mis datos (Muestra datos del usuario, admite actualizar, y permite carga de credito. Tambien Acceso a Mis Funciones.)
* Mis funciones (Visualiza las funciones compradas por el usuario y permite devolver entrada si la funcion es del dia de HOY o posterior)

Formularios del cliente:
* Cartelera (busqueda por pelicula y compra. Solo se visualizan funciones con fecha IGUAL o MAYOR a la del dia ACTUAL)
* Busqueda x Sala (para buscar por sala las funciones existentes y comprar. Solo se visualizan funciones con fecha IGUAL o MAYOR a la del dia ACTUAL)
* Actualizar mis datos (Muestra datos del usuario, admite actualizar, y permite carga de credito. Tambien Acceso a Mis Funciones.)
* Mis funciones (Visualiza las funciones compradas por el usuario y permite devolver entrada si la funcion es del dia de HOY o posterior)

------------------------------------------------------------------------------------------------------------------------------------------------------
***TRANSACCIONES***

Login/Registro:
* Se valida que el DNI y el mail ingresados al registrarse no existan en la base de datos.
* Un usaurio se registra desbloqueado y NO administrador. Solamente un administrador puede ponerlo como administrador en el ABM de Usuarios.
* El login valida mail y contrase�a. Tras 3 intentos fallidos (que se suman en la base de datos) el usuario se bloquea. SOlamente un administrador puede desbloquear un usuario y resetear sus intentos en el ABM de Usuarios.
* Cuando un usuario se loguea se limpian sus intentos fallidos.

Carga de Cr�dito:
1- El usuario logueado debe presionar sobre boton MIS DATOS.
2- Dentro de la ventana de datos se observa la visualizacion del credito actual y un campo para cargar cr�dito.
3- Para cargar credito debe completar un monto en el campo "Monto a Cargar". El monto debe ser numerico, y mayor a 0. No puede estar vacio.
4- Luego de indicar el monto debe presionar el boton "Cargar Credito" para que se le acredite el saldo en su cuenta y se actualice la informacion (la cual actualiza en la base de datos y posteriormente el objeto y la lista usuarios).

Proceso de compra de entradas:
1- El usuario se debe loguear (o registrar si no tiene usuario) por su MAIL y su contrase�a.
2- EL sistema lo redirige a la CARTELERA. 
2A-Alli podra seleccionar la pelicula que desee ver. Al hacerle clic lo redirige a todas las funciones existentes para dicha pelicula. 
Un campo de busqueda le permitir� filtrar por Sala, Fecha y costo y podra seleccionar cual desea comprar, indicar la cantidad y presionar COMPRAR.
2B- En lugar de buscar por pelicula podria ir al boton Buscar x Sala. Lo llevara a una pantalla de las salas del cine. Al elegir una sala le mostrar� todas las funciones de peliculas que se proyectan en dicha sala (solo se visualizan funciones con fecha mayor o igual a la del DIA).
Un campo de busqueda le permitir� filtrar por Sala, Fecha y costo y podra seleccionar cual desea comprar, indicar la cantidad y presionar COMPRAR.
NOTA: Para comprar entradas el usuario tiene que tener cr�dito previamente cargado y la sala debe tener cantidad disponible que solicita el usuario.
3- La compra se almacena en la lista de usuarios, funciones y usuariofuncion para consultas posteriores, luego de haberse registrado correctamente en la base de datos.
4- Pueden darse 2 casos:
a) El usuario compro nuevamente una entrada de una funcion que ya habia comprado: En ese caso se le resta el monto * cantidad de su cr�dito, y se hace una actualizacion a Usuario_Funcion por la cantidad comprada, tanto en la BD como en las listas.
b) El usuario compro una funcion nueva que no habia comprado anteriormente: En ese caso se le resta el monto * cantidad de su credito y se hace un insert o creacion en Usuario_Funcion y un Add en las listas correspondientes.

Visualizacion de compras:
1- Para ver las funciones compradas el usuario debe estar posicionado en la ventana de sus datos (donde los visualiza y actualiza y donde carga credito).
2- Dentro debera presionar el boton "MIS FUNCIONES" el cual lo redirigir� al formulario de sus funciones.
3- Dentro de esta ventana se muestra al usaurio todas las funciones que �l compro en la historia.

Proceso de devoluci�n de entradas:
1- Si desea puede devolver entradas. Solamente se permite devolucion de entradas de una fecha igual o posterior a la del dia actual. 
2- Para hacerlo debe estar en la ventana de MIS FUNCIONES y visualizar las funciones que compr�.
3- Simplemente debe indicar la cantidad en el campo Cantidad a Devolver de la funcion que desee.
4- La cantidad debe ser igual o menor a la cantidad comprada anteriormente.
Si la cantidad es MENOR, se actualiza la cantidad comprada en la base de datos (Tabla intermedia Usuario_Funcion).
Si la cantidad es IGUAL, se realiza una delete a la base de datos en dicho registro.
Si la fecha de funcion es anterior a la fecha actual no podra devolver la entrada.
5- Al devolver se le reintegra cr�dito al cliente en base al costo de la funcion * cantidad seleccionada en devoluci�n. Asi mismo se actualizan las listas correspondientes.

------------------------------------------------------------------------------------------------------------------------------------------------------
***EXTRAS***

Los siguientes puntos fueron adicionales a lo requerido en el proyecto:
* Sonido en transacciones:
- Al realizar un login exitoso podr� escuchar un sonido muy familiar de inicio de sesi�n.
- Al realizar un login erroneo podr� escuchar un sonido de error.
- Al realizar carga de cr�dito, podr� escuchar un sonido de una moneda.
- Al realizar una compra de una funcion podr� escuchar una musica exitosa.
- Al realizar una devolucion de una funci�n podr� escuchar un grito muy familiar.

* Timeout:
- El usuario tiene un timeout de 1000 segundos (aprox 15 min).
- Adicionalmente en Layout colocamos un timeout adicional que lo redirecciona al login.

* Sesiones:
- Almacenamiento de la sesion del usuario
- Almacenamiento del rol del usuario si es administrador o no
- Bloqueo de URLs por rol administrador o cliente. Asi mismo al entrar a una url restringida sin ser admin, te llevara a una url de acceso denegado con un contador de 5 segundos que te redirige a la cartelera.

* Agregados Est�ticos:
- Iconograf�a reemplazando ciertos botones de acci�n.
- Uso de bootstrap, css y javascript. 
- Modificaciones est�ticas en el Layout y la barra de navegaci�n.
- Edicion de la cartelera y otras vistas no generadas por scaffolding (login, registro, compra por sala, compra por pelicula, mis funciones, mis datos).

* Base de datos s�lida:
- Buena estructuraci�n de base de datos
- Gran cantidad de peliculas registradas todas con informacion real de sinopsis posters, etc.
- Gran cantidad de funciones creadas para ejemplo de exposici�n.
- Considerable cantidad de salas y usuarios.
