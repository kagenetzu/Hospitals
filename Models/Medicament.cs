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

    public partial class Medicament
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicament()
        {
            this.Hospital_has_Medicament = new HashSet<Hospital_has_Medicament>();
            this.Pharmacy_has_Medicament = new HashSet<Pharmacy_has_Medicament>();
        }
    
        public int idMedicament { get; set; }
        [Required(ErrorMessage = "Некорректное значение.")]
        public string nameMedicament { get; set; }
        [Required(ErrorMessage = "Некорректное значение.")]
        public string firmManufacturer { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hospital_has_Medicament> Hospital_has_Medicament { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pharmacy_has_Medicament> Pharmacy_has_Medicament { get; set; }
    }
}
