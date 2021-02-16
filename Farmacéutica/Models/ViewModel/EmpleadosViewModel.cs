using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacéutica.Models.ViewModel
{
    public class EmpleadosViewModel
    {
        
        [StringLength(100)]
        [Required]
        [UserExist]
        
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [StringLength(12, ErrorMessage = "El número es demasiado largo")]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Correo { get; set; }
        public int ID_Estado { get; set; }
}

    public class UserExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (PruebaEntities db= new PruebaEntities())
            {
                string sCorreo = (string)value;

                if (db.Empleados.Where(d => d.Correo == sCorreo).Count() > 0)
                {
                    return new ValidationResult("Usuario ya Existe");
                }
            }
                return ValidationResult.Success;
        }
    }
}