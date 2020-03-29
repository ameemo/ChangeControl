﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCC.Controllers
{
    public class Mensajes
    {
        public string[] MConfirmacion;
        public string[] MAdvertencia;
        public string[] MError;
        public Mensajes()
        {
            MConfirmacion = new string[] {"",
                                          "Bloqueo de usuario exitoso.",
                                          "Inicio de sesion exitoso.",
                                          "Se cerro con éxito la sesión.",
                                          "Contraseña cambiada con exito.",
                                          "Creación de servicio o aplicación exitoso.",
                                          "Bloqueo de servicio o aplicación exitoso.",
                                          "Creación de usuario exitoso.",
                                          "Control de cambio creado de manera exitosa.",
                                          "Control de cambio modificado de manera exitosa."
                                        };
            MAdvertencia = new string[] {"",
                                         "El archivo puede contener errores si no es compatible o no se cuentan con los permisos asignados por el administrador.",
                                         "Los usuarios sin rol asignado son considerados sólo como AVISO y no pueden acceder al sistema."
                                        };
            MError = new string[] {"",
                                   "No se puede acceder a los datos, contacte al administrador.",
                                   "El archivo no cumple las especificaciones. Sólo archivos: jpg, jpeg, png y pdf.",
                                   "Usuario y/o contraseña incorrectos, intentar nuevamente.",
                                   "Usuario bloqueado, contacte al administrador.",
                                   "La contraseña actual y la nueva son iguales, no se puede realizar el cambio.",
                                   "El archivo no cumple con las especificaciones. Sólo archivos: PDF.",
                                   "La dirección no es correcta, contacte al administrador.",
                                   "El control de cambio al que quiere acceder no se encuentra, contacte al administrador.",
                                   "Las notas de corrección no pueden ser mostradas, contacte al administrador.",
                                   "Ocurrio un problema al extraer la información, contacte al administrador.",
                                   "Servicio o aplicación ya existente.",
                                   "Usuario ya existente.",
                                   "El tiempo mínimo permitido es de 1 día.",
                                   "Las nuevas contraseñas no coinciden, no se puede realizar el cambio"
                                   };
        }
        public string getMConfirmacion(int numero)
        {
            return this.MConfirmacion[numero];
        }
        public string getMAdvertencia(int numero)
        {
            return this.MAdvertencia[numero];
        }
        public string getMError(int numero)
        {
            return this.MError[numero];
        }
    }
}