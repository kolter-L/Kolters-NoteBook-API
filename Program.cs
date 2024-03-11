using NoteBook.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000", "https://proud-sand-0f68a3410.4.azurestaticapps.net", "https://www.kolter.io", "https://kolter.io")
        .AllowAnyMethod();

    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.MapGet("/get-all-notes", async () => await NotesRepository.GetNotesAsync());

app.MapGet("/get-note-by-id/{Id}", async (int Id) =>
{
    Note noteToReturn = await NotesRepository.GetNoteByIdAsync(Id);

    if (noteToReturn != null)
    {
        return Results.Ok(noteToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapPost("/create-note", async (Note noteToCreate) =>
{
    bool createSuccessful = await NotesRepository.CreateNoteAsync(noteToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Note Created.");
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapPut("/update-note", async (Note noteToUpdate) =>
{
    bool updateSuccessful = await NotesRepository.UpdateNoteAsync(noteToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Note Updated");
    }
    else
    {
        return Results.BadRequest();
    }
});

app.MapDelete("/delete-note", async (int Id) =>
{
    bool deleteSuccessful = await NotesRepository.DeleteNoteAsync(Id);

    if (deleteSuccessful)
    {
        return Results.Ok("Note Deleted");
    }
    else
    {
        return Results.BadRequest();
    }
});

app.Run();

