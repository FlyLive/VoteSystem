//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SClub.VoteSystem.LogicDemo.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class VoteProject
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public Nullable<int> VoteCount { get; set; }
        public string Logo { get; set; }
        public string Movie { get; set; }
        public string Image_F { get; set; }
        public string Image_S { get; set; }
        public string Message { get; set; }
    
        public virtual VoteActivity VoteActivity { get; set; }
    }
}