using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using eHal.Models;
using System.ComponentModel.DataAnnotations;

namespace eHal.Models
{




    public interface IApplicationDbContext
    {
        IDbSet<CustomApplicationUser> CustomApplicationUsers { get; set; }
        IDbSet<Seller> Sellers { get; set; }
        IDbSet<Listing> Listings { get; set; }
        IDbSet<Adres> Addresses { get; set; }
        IDbSet<ProductType> ProductTypes { get; set; }
        IDbSet<Week> Weeks { get; set; }
        IDbSet<Review> Reviews { get; set; }

        int SaveChanges();

    }
    
    public interface IElement
    {
        int Id { get; set; }
    }


    



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IApplicationDbContext
    {
        
        public ApplicationDbContext()
            : base("Connection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, eHal.Migrations.Configuration>("Connection"));
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<CustomApplicationUser> CustomApplicationUsers { get; set; }
        public IDbSet<Seller> Sellers { get; set; }
        public IDbSet<Listing> Listings { get; set; }
        public IDbSet<Adres> Addresses { get; set; }
        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<Week> Weeks { get; set; }
        public IDbSet<Review> Reviews { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            
            modelBuilder.Entity<CustomApplicationUser>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<ApplicationUser>()
                        .HasKey(c => c.Id);

            modelBuilder.Entity<CustomApplicationUser>()
                        .HasRequired(a => a.ApplicaitonUser)
                        .WithOptional(b => b.CustomApplicationUser);
            
            modelBuilder.Entity<CustomApplicationUser>()
                        .HasOptional(a => a.seller)
                        .WithRequired(b => b.CustomApplicationUser);

            

            modelBuilder.Entity<ApplicationUser>().ToTable("Users").HasKey<int>(u => u.Id);
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles").HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins").HasKey<int>(ul => ul.UserId);
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims").HasKey<int>(uc => uc.Id);
            modelBuilder.Entity<CustomRole>().ToTable("Roles").HasKey<int>(r => r.Id);

            base.OnModelCreating(modelBuilder);


            //add Role seller
        }


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

    }
    /*
    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<CustomApplicationUser> CustomApplicationUsers { get; set; }
        public IDbSet<Seller> Sellers { get; set; }
        public IDbSet<Listing> Listings { get; set; }
        public IDbSet<Adres> Addresses { get; set; }
        public IDbSet<ProductType> ProductTypes { get; set; }
        public IDbSet<Week> Weeks { get; set; }
        public IDbSet<Review> Reviews { get; set; }
        

        public FakeApplicationDbContext()
        {
            this.CustomApplicationUsers = new FakeDbSet<CustomApplicationUser>();
            this.Sellers = new FakeDbSet<Seller>();
            this.Listings = new FakeDbSet<Listing>();
            this.ProductTypes = new FakeDbSet<ProductType>();
            this.Weeks = new FakeDbSet<Week>();
            this.Reviews = new FakeDbSet<Review>();
        }

        public int SaveChanges()
        {
            try
            {
                return 0;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }


    public class FakeDbSet<T> : System.Data.Entity.IDbSet<T> where T : class, IElement
    {
        private readonly List<T> list = new List<T>();

        public FakeDbSet()
        {
            list = new List<T>();
        }

        public FakeDbSet(IEnumerable<T> contents)
        {
            this.list = contents.ToList();
        }




        // union, take, find, elementat

        #region IDbSet<T> Members

        public IEnumerable<T> ToList()
        {
            return this.list;
        }

        public IQueryable<T> Where(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().Where(e);
        }

        public T Single(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().Single<T>(e);
        }
        public T First()
        {
            return this.list.AsQueryable<T>().First<T>();
        }
        public IQueryable<T> OrderBy(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().OrderBy(e);
        }

        public IQueryable<T> Reverse(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().Reverse<T>();
        }

        public int Sum(Expression<System.Func<T, int>> e)
        {
            return this.list.AsQueryable<T>().Sum<T>(e);
        }

        public bool Any(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().Any<T>(e);
        }

        public bool All(Expression<System.Func<T, bool>> e)
        {
            return this.list.AsQueryable<T>().All<T>(e);
        }



        public T Add(T entity)
        {
            
            if (list.AsQueryable().Any(c => c.Id == entity.Id))
            {
                entity.Id = list.AsQueryable().OrderByDescending(c => c.Id).First().Id;
            }
            this.list.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            this.list.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T Remove(T entity)
        {
            this.list.Remove(entity);
            return entity;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return this.list.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return this.list.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.list.AsQueryable().Provider; }
        }

        #endregion
    }*/
}