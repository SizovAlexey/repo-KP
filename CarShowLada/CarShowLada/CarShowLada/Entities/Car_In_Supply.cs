//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarShowLada.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car_In_Supply
    {
        public int id_Supply { get; set; }
        public int id_Car { get; set; }
        public Nullable<int> Count_in_supply { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Supply Supply { get; set; }
    }
}
