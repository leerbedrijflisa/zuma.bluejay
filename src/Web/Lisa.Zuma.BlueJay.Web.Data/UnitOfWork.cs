using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisa.Zuma.BlueJay.Web.Data
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork(bool autoSaveStoreChanges = false)
        {
            this.autoSaveStoreChanges = autoSaveStoreChanges;
        }

        public GenericRepository<DossierData> DossierRepository
        {
            get
            {
                if (this.dossierRepository == null)
                {
                    this.dossierRepository = new GenericRepository<DossierData>(context);
                }

                return dossierRepository;
            }
        }

        public GenericRepository<DossierDetailData> DossierDetailRepository
        {
            get
            {
                if (this.dossierDetailRepository == null)
                {
                    this.dossierDetailRepository = new GenericRepository<DossierDetailData>(context);
                }

                return dossierDetailRepository;
            }
        }

        public GenericRepository<NoteData> NoteRepository
        {
            get
            {
                if (this.noteRepository == null)
                {
                    this.noteRepository = new GenericRepository<NoteData>(context);
                }

                return noteRepository;
            }
        }

        public GenericRepository<NoteMediaData> NoteMediaRepository
        {
            get
            {
                if (this.noteMediaRepository == null)
                {
                    this.noteMediaRepository = new GenericRepository<NoteMediaData>(context);
                }

                return noteMediaRepository;
            }
        }

        public UserManager<UserData> UserManager
        {
            get
            {
                if (this.userManager == null)
                {
                    var store = CreateUserStore();
                    store.AutoSaveChanges = this.autoSaveStoreChanges;
                    this.userManager = new UserManager<UserData>(store);
                }

                return userManager;
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

        private BlueJayContext context = new BlueJayContext("DefaultConnection");
        private GenericRepository<DossierData> dossierRepository;
        private GenericRepository<DossierDetailData> dossierDetailRepository;
        private GenericRepository<NoteData> noteRepository;
        private GenericRepository<NoteMediaData> noteMediaRepository;
        private UserManager<UserData> userManager;
        private bool disposed = false;
        private bool autoSaveStoreChanges = false;
    }
}
