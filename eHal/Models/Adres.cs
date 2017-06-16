using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eHal.Models
{
    public class Adres : IElement
    {
        public int Id { get; set; }

        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        [Required]
        public int sehirId { get; set; }
        [Required]
        public int ilceId { get; set; }
        [Required]
        public int koyMahId { get; set; }
        
        public string caddeSokak { get; set; }

        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public string fullAdress { get; set; }
        


        public void updateFullAdress()
        {
            AdresContext db = new AdresContext();

            string sehirAdi = db.Sehirler.ElementAt(this.sehirId).SehirAdi;
            string ilceAdi = db.Ilceler.ElementAt(this.ilceId).IlceAdi;
            string semt = db.SemtMah.ElementAt(this.koyMahId).SemtAdi;

            fullAdress = caddeSokak + "\n" + semt + "\n" + ilceAdi + " / " + sehirAdi;
        }
    }

    [Table("Sehirler")]
    public partial class Sehirler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sehirler()
        {
            Ilceler = new HashSet<Ilceler>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SehirId { get; set; }

        [Required]
        [StringLength(20)]
        public string SehirAdi { get; set; }

        public byte PlakaNo { get; set; }

        public int TelefonKodu { get; set; }

        public int RowNumber { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ilceler> Ilceler { get; set; }
    }

    [Table("SemtMah")]
    public partial class SemtMah
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SemtMahId { get; set; }

        [Required]
        [StringLength(60)]
        public string SemtAdi { get; set; }

        [Required]
        [StringLength(100)]
        public string MahalleAdi { get; set; }

        [Required]
        [StringLength(6)]
        public string PostaKodu { get; set; }

        public int ilceId { get; set; }

        public virtual Ilceler Ilceler { get; set; }
    }

    [Table("Ulkeler")]
    public partial class Ulkeler
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UlkeId { get; set; }

        [Required]
        [StringLength(2)]
        public string IkiliKod { get; set; }

        [Required]
        [StringLength(3)]
        public string UcluKod { get; set; }

        [Required]
        [StringLength(100)]
        public string UlkeAdi { get; set; }

        [Required]
        [StringLength(6)]
        public string TelKodu { get; set; }
    }


    [Table("Ilceler")]
    public partial class Ilceler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ilceler()
        {
            SemtMah = new HashSet<SemtMah>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ilceId { get; set; }

        public int SehirId { get; set; }

        [Required]
        [StringLength(60)]
        public string IlceAdi { get; set; }

        [Required]
        [StringLength(55)]
        public string SehirAdi { get; set; }

        public virtual Sehirler Sehirler { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SemtMah> SemtMah { get; set; }
    }


    public partial class AdresContext : DbContext
    {
        public AdresContext()
            : base("name=AzureConnection")
        {
        }

        public virtual DbSet<Ilceler> Ilceler { get; set; }
        public virtual DbSet<Sehirler> Sehirler { get; set; }
        public virtual DbSet<SemtMah> SemtMah { get; set; }
        public virtual DbSet<Ulkeler> Ulkeler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ilceler>()
                .HasMany(e => e.SemtMah)
                .WithRequired(e => e.Ilceler)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sehirler>()
                .HasMany(e => e.Ilceler)
                .WithRequired(e => e.Sehirler)
                .WillCascadeOnDelete(false);
        }
    }



}