//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KURSLAB3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Hospital_has_Medicament
    {
        public int Hospital_hospitalNumber { get; set; }
        public int Medicament_idMedicament { get; set; }
        [Required(ErrorMessage = "Некорректное значение.")]
        [Range(1,int.MaxValue, ErrorMessage = "Некорректное значение.")]
        public Nullable<int> countPackagesMedicament { get; set; }
    
        public virtual Hospital Hospital { get; set; }
        public virtual Medicament Medicament { get; set; }
    }
}
