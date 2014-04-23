using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Lisa.Zuma.BlueJay.Web.Data.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.Web.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<DossierData> DossierRepository { get; }
        IGenericRepository<DossierDetailData> DossierDetailRepository { get; }
        IGenericRepository<NoteData> NoteRepository { get; }
        IGenericRepository<NoteMediaData> NoteMediaRepository { get; }
        ApplicationUserManager<UserData> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        void Save();
    }
}
