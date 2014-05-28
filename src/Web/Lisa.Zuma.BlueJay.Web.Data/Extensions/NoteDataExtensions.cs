using Lisa.Zuma.BlueJay.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.Web.Data.Extensions
{
    public static class NoteDataExtensions
    {
        /// <summary>
        /// Checks if this note contains the media with the id specified by <paramref name="id"/>.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="id">The id of the media to check for existance.</param>
        /// <returns></returns>
        public static bool HasMedia(this NoteData noteData, int id)
        {
            return noteData.Media.Count(m => m.Id == id) > 0;
        }

        /// <summary>
        /// Checks if this note contains the media specified by <paramref name="noteMediaData"/>.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="id">The media to check for existance.</param>
        /// <returns></returns>
        public static bool HasMedia(this NoteData noteData, NoteMediaData noteMediaData)
        {
            return noteData.HasMedia(noteMediaData.Id);
        }

        /// <summary>
        /// Gets the media identified by <paramref name="id"/>.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="id">The id of the media to retrieve.</param>
        /// <returns></returns>
        public static NoteMediaData GetMedia(this NoteData noteData, int id)
        {
            return noteData.Media.FirstOrDefault(m => m.Id == id);
        }

        /// <summary>
        /// Tries to get the media identified by <paramref name="id"/> and fills <paramref name="noteMediaData"/>
        /// with the retrieved media.
        /// </summary>
        /// <param name="noteData"></param>
        /// <param name="id">The id of the media to retrieve.</param>
        /// <param name="noteMediaData">The media to fill with the retrieval result.</param>
        /// <returns></returns>
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
