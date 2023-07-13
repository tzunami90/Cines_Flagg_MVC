using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Cines_Flagg.Models
{
    public class MyContext : DbContext
    {
        //DbSet
        public DbSet<Usuario> usuarios { get; set; }

        public DbSet<Funcion> funciones { get; set; }

        public DbSet<Pelicula> peliculas { get; set; }

        public DbSet<Sala> salas { get; set; }

        public DbSet<UsuarioFuncion> UF { get; set; }

        //Constructor
        public MyContext() { } //Constructor vacio
        public MyContext(DbContextOptions<MyContext> optionsBuilder) : base(optionsBuilder) { } //Constructor que necesita MVC para interfaces.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Conection string usando appsettings en lugar del Resources de antes
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("UserContext");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creacion entidades/tablas
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(usuario => usuario.ID);
            modelBuilder.Entity<Funcion>()
                .ToTable("Funciones")
                .HasKey(funcion => funcion.ID);
            modelBuilder.Entity<Sala>()
                .ToTable("Sala")
                .HasKey(sala => sala.ID);
            modelBuilder.Entity<Pelicula>()
                .ToTable("Peliculas")
                .HasKey(pelicula => pelicula.ID);


            //Relacion PELICULA FUNCION uno a muchos
            modelBuilder.Entity<Funcion>()
                .HasOne(ObjFuncion => ObjFuncion.MiPelicula)
                .WithMany(ObjPelicula => ObjPelicula.MisFunciones)
                .HasForeignKey(ObjFuncion => ObjFuncion.idPelicula);

            //Relacion USUARIO FUNCION muchos a muchos
            modelBuilder.Entity<Usuario>()
                .HasMany(Usuarios => Usuarios.MisFunciones)
                .WithMany(Funciones => Funciones.Clientes)
                .UsingEntity<UsuarioFuncion>(
                    eup => eup.HasOne(UsuarioFuncion => UsuarioFuncion.MiFuncion).WithMany(funcion => funcion.UsuarioFuncion).HasForeignKey(usuario => usuario.idFuncion),
                    eup => eup.HasOne(UsuarioFuncion => UsuarioFuncion.MiUsuario).WithMany(usuario => usuario.UsuarioFuncion).HasForeignKey(usuario => usuario.idUsuario),
                    eup => eup.HasKey(key => new { key.idUsuario, key.idFuncion })
                );

            //Relacion SALA FUNCION uno a muchos
            modelBuilder.Entity<Funcion>()
                .HasOne(ObjetoFuncion => ObjetoFuncion.MiSala)
                .WithMany(ObjetoSala => ObjetoSala.MisFunciones)
                .HasForeignKey(ObjetoFuncion => ObjetoFuncion.idSala);


            //Propiedades de los datos (tipos dato SQL)
            modelBuilder.Entity<Usuario>(
                usr =>
                {
                    usr.Property(u => u.Nombre).HasColumnType("varchar(50)");
                    usr.Property(u => u.Apellido).HasColumnType("varchar(50)");
                    usr.Property(u => u.DNI).HasColumnType("varchar(50)");
                    usr.Property(u => u.Mail).HasColumnType("varchar(50)");
                    usr.Property(u => u.Password).HasColumnType("varchar(50)");
                    usr.Property(u => u.Bloqueado).HasColumnType("bit");
                    usr.Property(u => u.Credito).HasColumnType("float");
                    usr.Property(u => u.FechaNacimiento).HasColumnType("date");
                    usr.Property(u => u.EsAdmin).HasColumnType("bit");

                });

            modelBuilder.Entity<Sala>(
                usr =>
                {
                    usr.Property(s => s.Ubicacion).HasColumnType("varchar(100)");
                    usr.Property(s => s.Capacidad).HasColumnType("int");
                });

            modelBuilder.Entity<Pelicula>(
                usr =>
                {
                    usr.Property(p => p.Nombre).HasColumnType("varchar(50)");
                    usr.Property(p => p.Sinopsis).HasColumnType("varchar(200)");
                    usr.Property(p => p.Duracion).HasColumnType("int");
                    usr.Property(p => p.Poster).HasColumnType("varchar(max)");
                });

            modelBuilder.Entity<Funcion>(
                usr =>
                {
                    usr.Property(f => f.Fecha).HasColumnType("date");
                    usr.Property(f => f.Costo).HasColumnType("float");
                    usr.Property(f => f.idSala).HasColumnType("int");
                    usr.Property(f => f.idPelicula).HasColumnType("int");
                });

            //Creacion de un admin por defecto
            modelBuilder.Entity<Usuario>().HasData(
                new {ID = 1, Nombre = "Obdulio", Apellido="Gomez",DNI=45678964, Mail="m@mail", Password="123", IntentosFallidos = 0, Bloqueado=false, Credito=0.0, FechaNacimiento=new DateTime(1974,05,07), EsAdmin = true}
            );
        }
    }
}
