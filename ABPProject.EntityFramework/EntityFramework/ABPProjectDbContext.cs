using System.Data.Common;
using Abp.Zero.EntityFramework;
using ABPProject.Authorization.Roles;
using ABPProject.MultiTenancy;
using ABPProject.Users;
using ABPProject.Products;
using System.Data.Entity;
using ABPProject.Projects;
using ABPProject.SalesOrders;
using ABPProject.PurchaseOrders;
using ABPProject.Clients;
using ABPProject.InventSites;
using ABPProject.Contracts;
using ABPProject.Suppliers;

namespace ABPProject.EntityFramework
{
    public class ABPProjectDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Project> Projects { get; set; }
        public IDbSet<SalesOrder> SalesOrders { get; set; }
        public IDbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public IDbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public IDbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public IDbSet<UnitOfMeasureTranslation> UnitOfMeasureTranslations { get; set; }

        public IDbSet<Client> Clients { get; set; }
        public IDbSet<InventSite> InventSites { get; set; }
        public IDbSet<InventLocation> InventLocations { get; set; }
        public IDbSet<Contract> Contracts { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<InventBatch> InventBatchs { get; set; }




        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ABPProjectDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ABPProjectDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ABPProjectDbContext since ABP automatically handles it.
         */
        public ABPProjectDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ABPProjectDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public ABPProjectDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
