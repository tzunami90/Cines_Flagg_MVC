# Proyecto Sistema de cines WEB "Cines FLAGG"
## Creadores:
F- Franco Ouro
L- Lucas Marenco
A- Alejandro Solodujin
G- Gaston Mansilla
G- German Pardo

***Proyecto Realizado:***
Sistema de cines (administración con ABM, tipificacion de usuario admin/cliente, busqueda y compra/devolucion de entradas de funciones)

------------------------------------------------------------------------------------------------------------------------------------------------------
***TIPIFICACIÓN***

Tipos de Usuarios:
* Administrador: Al loguearse podra ingresar y maipular al ABM de Usuarios, Peliculas, Salas y Funciones. Ademas podrá ver la cartelera.
* Cliente: Podra ver cartelera y comprar tickets para funcion seleccionada.
NOTA: AL crear un usuario nuevo siempre se registra desbloqueado (en la base de datos bloqueado = 0 por default). Solo el administrador al modificar puede bloquearlo (en la base de datos bloqueado = 1).

------------------------------------------------------------------------------------------------------------------------------------------------------
***FORMULARIOS WEB POR TIPO DE USUARIO***

Formularios del admin:
* Login/Registro (inicio): Permite registrarse o loguearse. Es el inicio de la aplicación ya que es obligatorio estar registrado.
* Cartelera: Es el formulario que permite buscar las funciones existentes para la compra (funciones con fecha de HOY o superior). 
* ABM de Usuarios: Alta Baja y Modificacion de usuarios (solamente administrador)
* ABM de Peliculas: Alta Baja y Modificacion de peliculas (solamente administrador)
* ABM de Salas: Alta Baja y Modificacion de salas (solamente administrador)
* ABM de Funciones:Alta Baja y Modificacion de funciones (solamente administrador)
* Busqueda avanzada (para buscar por sala o por pelicula las funciones existentes y comprar). Permitimos al admin la compra.
* Actualizar mis datos (Muestra datos del usuario, admite actualizar, y permite carga de credito. Tambien Acceso a Mis Funciones.)
* Mis funciones (Visualiza las funciones compradas por el usuario y permite devolver entrada si la funcion es del dia de HOY o posterior)

Formularios del cliente:
* Cartelera (busqueda por pelicula y compra. Solo se visualizan funciones con fecha IGUAL o MAYOR a la del dia ACTUAL)
* Busqueda x Sala (para buscar por sala las funciones existentes y comprar. Solo se visualizan funciones con fecha IGUAL o MAYOR a la del dia ACTUAL)
* Actualizar mis datos (Muestra datos del usuario, admite actualizar, y permite carga de credito. Tambien Acceso a Mis Funciones.)
* Mis funciones (Visualiza las funciones compradas por el usuario y permite devolver entrada si la funcion es del dia de HOY o posterior)

------------------------------------------------------------------------------------------------------------------------------------------------------
***TRANSACCIONES***

Carga de Crédito:
1- El usuario logueado debe presionar sobre boton MIS DATOS.
2- Dentro de la ventana de datos se observa la visualizacion del credito actual y un campo para cargar crédito.
3- Para cargar credito debe completar un monto en el campo "Monto a Cargar". El monto debe ser numerico, y mayor a 0. No puede estar vacio.
4- Luego de indicar el monto debe presionar el boton "Cargar Credito" para que se le acredite el saldo en su cuenta y se actualice la informacion (la cual actualiza en la base de datos y posteriormente el objeto y la lista usuarios).

Proceso de compra de entradas:
1- El usuario se debe loguear (o registrar si no tiene usuario) por su MAIL y su contraseña.
2- EL sistema lo redirige a la CARTELERA. 
2A-Alli podra seleccionar la pelicula que desee ver. Al hacerle clic lo redirige a todas las funciones existentes para dicha pelicula. 
Un campo de busqueda le permitirá filtrar por Sala, Fecha y costo y podra seleccionar cual desea comprar, indicar la cantidad y presionar COMPRAR.
2B- En lugar de buscar por pelicula podria ir al boton Buscar x Sala. Lo llevara a una pantalla de las salas del cine. Al elegir una sala le mostrará todas las funciones de peliculas que se proyectan en dicha sala (solo se visualizan funciones con fecha mayor o igual a la del DIA).
Un campo de busqueda le permitirá filtrar por Sala, Fecha y costo y podra seleccionar cual desea comprar, indicar la cantidad y presionar COMPRAR.
NOTA: Para comprar entradas el usuario tiene que tener crédito previamente cargado y la sala debe tener cantidad disponible que solicita el usuario.
3- La compra se almacena en la lista de usuarios, funciones y usuariofuncion para consultas posteriores, luego de haberse registrado correctamente en la base de datos.
4- Pueden darse 2 casos:
a) El usuario compro nuevamente una entrada de una funcion que ya habia comprado: En ese caso se le resta el monto * cantidad de su crédito, y se hace una actualizacion a Usuario_Funcion por la cantidad comprada, tanto en la BD como en las listas.
b) El usuario compro una funcion nueva que no habia comprado anteriormente: En ese caso se le resta el monto * cantidad de su credito y se hace un insert o creacion en Usuario_Funcion y un Add en las listas correspondientes.

Visualizacion de compras:
1- Para ver las funciones compradas el usuario debe estar posicionado en la ventana de sus datos (donde los visualiza y actualiza y donde carga credito).
2- Dentro debera presionar el boton "MIS FUNCIONES" el cual lo redirigirá al formulario de sus funciones.
3- Dentro de esta ventana se muestra al usaurio todas las funciones que él compro en la historia.

Proceso de devolución de entradas:
1- Si desea puede devolver entradas. Solamente se permite devolucion de entradas de una fecha igual o posterior a la del dia actual. 
2- Para hacerlo debe estar en la ventana de MIS FUNCIONES y visualizar las funciones que compró.
3- Simplemente debe indicar la cantidad en el campo Cantidad a Devolver de la funcion que desee.
4- La cantidad debe ser igual o menor a la cantidad comprada anteriormente.
Si la cantidad es MENOR, se actualiza la cantidad comprada en la base de datos (Tabla intermedia Usuario_Funcion).
Si la cantidad es IGUAL, se realiza una delete a la base de datos en dicho registro.
Si la fecha de funcion es anterior a la fecha actual no podra devolver la entrada.
5- Al devolver se le reintegra crédito al cliente en base al costo de la funcion * cantidad seleccionada en devolución. Asi mismo se actualizan las listas correspondientes.


//