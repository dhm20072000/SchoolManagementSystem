//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManamentSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class courseEntities : DbContext
    {
        public courseEntities()
            : base("name=courseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Enroll> Enrolls { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Student> Students { get; set; }
    }
}
