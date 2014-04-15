using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(bool autoSaveStoreChanges = false)
        {
            this.autoSaveStoreChanges = autoSaveStoreChanges;
        }

        public IGenericRepository<DossierData> DossierRepository
        {
            get
            {
                if (dossierRepo == null)
                {
                    dossierRepo = new GenericRepository<DossierData>(context);
                }

                return dossierRepo;
            }
        }

        public IGenericRepository<DossierDetailData> DossierDetailRepository
        {
            get
            {
                if (dossierDetailRepo == null)
                {
                    dossierDetailRepo = new GenericRepository<DossierDetailData>(context);
                }

                return dossierDetailRepo;
            }
        }

        public IGenericRepository<NoteData> NoteRepository
        {
            get
            {
                if (noteRepo == null)
                {
                    noteRepo = new GenericRepository<NoteData>(context);
                }

                return noteRepo;
            }
        }

        public IGenericRepository<NoteMediaData> NoteMediaRepository
        {
            get
            {
                if (noteMediaRepo == null)
                {
                    noteMediaRepo = new GenericRepository<NoteMediaData>(context);
                }

                return noteMediaRepo;
            }
        }        

        public ApplicationUserManager<UserData> UserManager
        {
            get
            {
                if (this.userManager == null)
                {
                    var store = CreateUserStore();
                    store.AutoSaveChanges = this.autoSaveStoreChanges;
                    this.userManager = new ApplicationUserManager<UserData>(store);
                }

                return userManager;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if (this.roleManager == null)
                {
                    var store = CreateRoleStore();
                    this.roleManager = new RoleManager<IdentityRole>(store);
                }

                return roleManager;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                context.Dispose();
            }

            this.disposed = true;
        }

        private UserStore<UserData> CreateUserStore()
        {
            return new UserStore<UserData>(context);
        }

        private RoleStore<IdentityRole> CreateRoleStore()
        {
            return new RoleStore<IdentityRole>(context);
        }

        private BlueJayContext context = new BlueJayContext("DefaultConnection");
        private ApplicationUserManager<UserData> userManager;
        private RoleManager<IdentityRole> roleManager;
        private bool disposed = false;
        private bool autoSaveStoreChanges = false;

        private GenericRepository<DossierData> dossierRepo;
        private GenericRepository<DossierDetailData> dossierDetailRepo;
        private GenericRepository<NoteData> noteRepo;
        private GenericRepository<NoteMediaData> noteMediaRepo;
    }
}
