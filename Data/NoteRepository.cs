
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace NoteBook.Data
{
    internal static class NotesRepository
    {
       internal async static Task<List<Note>> GetNotesAsync()
       {
            using (var db = new AppDBContext())
            {
                return await db.Notes.ToListAsync();
            }
       }



       internal async static Task<Note> GetNoteByIdAsync(int Id)
       {
            using (var db = new AppDBContext())
            {
                return await db.Notes.FirstOrDefaultAsync(note => note.Id == Id);
            }
           
       }

        internal async static Task<bool> CreateNoteAsync(Note noteToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Notes.AddAsync(noteToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateNoteAsync(Note noteToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Notes.Update(noteToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> DeleteNoteAsync(int Id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Note noteToDelete = await GetNoteByIdAsync(Id);

                    db.Remove(noteToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }


    }
}
