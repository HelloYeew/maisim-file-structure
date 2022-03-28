using System.Reflection;
using maisim_file_structure;
using maisim_file_structure.Enum;
using maisim_file_structure.Objects;
using maisim_file_structure.Objects.Notes;

#region mock objects
TrackMetadata trackMetadata = new TrackMetadata()
{
    Title = "Lemon",
    Artist = "Kenshi Yonezu",
    Bpm = 80,
    CoverPath = "Test/lemon.jpg"
};

Beatmap mockBeatmap = new()
{
    TrackMetadata = trackMetadata,
    DifficultyLevel = DifficultyLevel.Basic,
    DifficultyRating = 10,
    NoteDesigner = "peeppy"
};

List<Note> mockNoteList = new List<Note>()
{
    new TapNote()
    {
        Lane = NoteLane.Lane1,
        TargetTime = 5.2323f
    },
    new TapNote()
    {
        Lane = NoteLane.Lane2,
        TargetTime = 23.23f
    },
    new TapNote()
    {
        Lane = NoteLane.Lane4,
        TargetTime = 44.44f
    },
    new TapNote()
    {
        Lane = NoteLane.Lane3,
        TargetTime = 55.445f
    },
};
#endregion

#region create beatmap file
// create a new StreamWriter
using (StreamWriter file = File.CreateText(@"test.msbm"))
{
    // Write version number at the beginning of the file
    file.WriteLine("maisim beatmap file version 1");
    file.WriteLine("");
    // Then add every property in beatmap object to the file using property name : value
    foreach (PropertyInfo property in mockBeatmap.GetType().GetProperties())
    {
        // Write every property except beatmapmetadata
        if (property.Name != "TrackMetadata")
        {
            file.WriteLine(property.Name + ": " + property.GetValue(mockBeatmap));
        }
    }
    // Write all of the beatmap metadata in beatmap object
    file.WriteLine("");
    foreach (PropertyInfo property in mockBeatmap.TrackMetadata.GetType().GetProperties())
    {
        file.WriteLine(property.Name + ": " + property.GetValue(mockBeatmap.TrackMetadata));
    }
    file.WriteLine("");
    // Write all of the notes in the note list
    foreach (Note note in mockNoteList)
    {
        if (note is TapNote tapNote)
        {
            file.WriteLine("1," + NoteLaneExtension.GetNumberByNoteLane(tapNote.Lane) + "," + tapNote.TargetTime);
        }
    }
}
#endregion

BeatmapDecoder decoder = new BeatmapDecoder(@"test.msbm");
Console.WriteLine(decoder.version);
// print all of the properties in decoded beatmap
foreach (PropertyInfo property in decoder.beatmap.GetType().GetProperties())
{
    Console.WriteLine(property.Name + " : " + property.GetValue(decoder.beatmap));
}
Console.WriteLine("");
// print all of the properties in decoded beatmap metadata
foreach (PropertyInfo property in decoder.trackMetadata.GetType().GetProperties())
{
    Console.WriteLine(property.Name + " : " + property.GetValue(decoder.trackMetadata));
}
Console.WriteLine("");
// print all of the notes in the note list
foreach(Note note in decoder.Notes)
{
    if (note is TapNote tapNote)
    {
        Console.WriteLine(nameof(tapNote) + " " + NoteLaneExtension.GetNumberByNoteLane(tapNote.Lane) + "," + tapNote.TargetTime);
    }
}

// TODO: Seperate some of the mess to a new class
// TODO: Add a unit test for the decoder and encoder
// TODO: Seperate the encoder and decoder to a new class