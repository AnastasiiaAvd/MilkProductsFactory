//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Milk
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingredients
    {
        public int idIngredient { get; set; }
        public int product { get; set; }
        public int rawMaterial { get; set; }
        public int amount { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual RawMaterials RawMaterials { get; set; }
    }
}
