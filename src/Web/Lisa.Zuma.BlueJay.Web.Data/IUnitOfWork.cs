using Lisa.Zuma.BlueJay.Web.Data.Entities;
using Microsoft.AspNet.Identity;
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
        UserManager<UserData> UserManager { get; }

        void Save();
    }
}
