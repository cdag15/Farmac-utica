using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farmacéutica.Models.ViewModel
{
    public class EstadosViewModel
    {
        [UserExistAttribute2]
        
        public int ID_Estado { get; set; }
        
        public string Estado { get; set; }
    }

    
    public class  UserExistAttribute2 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (PruebaEntities db = new PruebaEntities())
            {
                int sEstado = (int)value;

                if (db.Estados.Where(d => d.ID_Estado == sEstado).Count() > 0)
                {
                    return new ValidationResult("Estado ya Existe");
                }
            }
            return ValidationResult.Success;
        }
    }
}