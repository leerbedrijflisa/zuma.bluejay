using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Extensions
{
    public static class NoteDataExtensions
    {
        public static bool HasMedia(this NoteData noteData, int id)
        {
            return noteData.Media.Count(m => m.Id == id) > 0;
        }

        public static bool HasMedia(this NoteData noteData, NoteMediaData noteMediaData)
        {
            return noteData.HasMedia(noteMediaData.Id);
        }

        public static NoteMediaData GetMedia(this NoteData noteData, int id)
        {
            return noteData.Media.FirstOrDefault(m => m.Id == id);
        }

        public static bool TryGetMedia(this NoteData noteData, int id, out NoteMediaData noteMediaData)
        {
            if (!noteData.HasMedia(id))
            {
                noteMediaData = null;
                return false;
            }

            noteMediaData = noteData.GetMedia(id);
            return true;
        }
    }
}
