//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Education_Practike.DBConnection
{
    using System;
    using System.Collections.Generic;
    
    public partial class Engineer
    {
        public int Tab_Number { get; set; }
        public string Speciality { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}