using System.Reflection;
using maisim_file_structure;
using maisim_file_structure.Database;
using maisim_file_structure.Enum;
using maisim_file_structure.Objects;
using maisim_file_structure.Objects.Notes;

#region mock objects

BeatmapSet mockBeatmapSet = new BeatmapSet()
{
    Creator = "Yeew",
    BeatmapSetID = 1,
    AudioFilename = "test.mp3",
    PreviewTime = 555,
};

TrackMetadata trackMetadata = new TrackMetadata()
{
    Title = "Lemon",
    TitleUnicode = "Lemon",
    Artist = "Kenshi Yonezu",
    ArtistUnicode = "米津玄師",
    Source = "",
    Bpm = 80,
    CoverPath = "lemon.jpg"
};

mockBeatmapSet.TrackMetadata = trackMetadata;

Beatmap mockBeatmapOne = new()
{
    TrackMetadata = trackMetadata,
    DifficultyLevel = DifficultyLevel.Basic,
    DifficultyRating = 10,
    NoteDesigner = "Yeew",
    BeatmapID = 1
};

Beatmap mockBeatmapTwo = new()
{
    TrackMetadata = trackMetadata,
    DifficultyLevel = DifficultyLevel.Advanced,
    DifficultyRating = 10,
    NoteDesigner = "Yeew",
    BeatmapID = 2
};

mockBeatmapSet.Beatmaps = new List<Beatmap>
{
    mockBeatmapOne,
    mockBeatmapTwo
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
// create a folder for store the beatmapset
// check if the folder exists
if (!Directory.Exists($"{mockBeatmapSet.BeatmapSetID} {mockBeatmapSet.TrackMetadata.Artist} - {mockBeatmapSet.TrackMetadata.Title}"))
{
    // if not, create it
    Directory.CreateDirectory($"{mockBeatmapSet.BeatmapSetID} {mockBeatmapSet.TrackMetadata.Artist} - {mockBeatmapSet.TrackMetadata.Title}");
}


// foreach in list of beatmaps in mockBeatmapSet
foreach (Beatmap beatmap in mockBeatmapSet.Beatmaps)
{
    // create a new StreamWriter using beatmap.BeatmapID as the filename
    using (StreamWriter file = File.CreateText($"{mockBeatmapSet.BeatmapSetID} {mockBeatmapSet.TrackMetadata.Artist} - {mockBeatmapSet.TrackMetadata.Title}/{beatmap.BeatmapID}.msbm"))
    {
        // Write version number at the beginning of the file
        file.WriteLine("maisim beatmap file version 1");
        file.WriteLine("");
        // Then add every property in beatmap object to the file using property name : value
        foreach (PropertyInfo property in beatmap.GetType().GetProperties())
        {
            // Write every property except beatmapmetadata
            if (property.Name != "TrackMetadata" && property.Name != "BeatmapSet")
            {
                file.WriteLine(property.Name + ": " + property.GetValue(beatmap));
            }
        }
        // Write all of the beatmap metadata in beatmap object
        file.WriteLine("");
        foreach (PropertyInfo property in beatmap.TrackMetadata.GetType().GetProperties())
        {
            file.WriteLine(property.Name + ": " + property.GetValue(beatmap.TrackMetadata));
        }
        // Write all of the beatmap set metadata in beatmapsets object
        file.WriteLine("");
        foreach (PropertyInfo property in mockBeatmapSet.GetType().GetProperties())
        {
            if (property.Name != "Beatmaps" && property.Name != "TrackMetadata")
            {
                file.WriteLine(property.Name + ": " + property.GetValue(mockBeatmapSet));
            }
        }
        // Write all of the notes in the note list
        file.WriteLine("");
        foreach (Note note in mockNoteList)
        {
            if (note is TapNote tapNote)
            {
                file.WriteLine("1," + NoteLaneExtension.GetNumberByNoteLane(tapNote.Lane) + "," + tapNote.TargetTime);
            }
        }
    }
    
    // Then create a new file for store the beatmapset data
    using (StreamWriter file = File.CreateText($"{mockBeatmapSet.BeatmapSetID} {mockBeatmapSet.TrackMetadata.Artist} - {mockBeatmapSet.TrackMetadata.Title}/{mockBeatmapSet.BeatmapSetID}.msbs"))
    {
        // Write version number at the beginning of the file
        file.WriteLine("maisim beatmap set file version 1");
        file.WriteLine("");
        // Then add every property in beatmap object to the file using property name : value
        foreach (PropertyInfo property in mockBeatmapSet.GetType().GetProperties())
        {
            // Write every property except beatmapmetadata
            if (property.Name != "Beatmaps" && property.Name != "TrackMetadata")
            {
                file.WriteLine(property.Name + ": " + property.GetValue(mockBeatmapSet));
            }
        }
        // Write all of the beatmap metadata in beatmap object
        file.WriteLine("");
        foreach (PropertyInfo property in mockBeatmapSet.TrackMetadata.GetType().GetProperties())
        {
            file.WriteLine(property.Name + ": " + property.GetValue(mockBeatmapSet.TrackMetadata));
        }
    }
}
#endregion

BeatmapsetDecoder decoder = new BeatmapsetDecoder(@"1 Kenshi Yonezu - Lemon/");
Console.WriteLine(decoder.version);

// print all of the properties in decoded beatmap
foreach (Beatmap beatmap in decoder.beatmapSet.Beatmaps)
{
    foreach (PropertyInfo property in beatmap.GetType().GetProperties())
    {
        Console.WriteLine(property.Name + " : " + property.GetValue(beatmap));
    }
    Console.WriteLine("");
}
Console.WriteLine("");
// print all of the properties in decoded beatmap metadata
foreach (PropertyInfo property in decoder.trackMetadata.GetType().GetProperties())
{
    Console.WriteLine(property.Name + " : " + property.GetValue(decoder.trackMetadata));
}
Console.WriteLine("");


#region EFCore test

// // Insert a new beatmap into the database
// var database = new MaisimDatabaseContext();
//
// // Create a new beatmap object and add it to the database
// database.Add(new Beatmap()
// {
//     TrackMetadata = null,
//     DifficultyLevel = DifficultyLevel.Basic,
//     DifficultyRating = 10,
//     NoteDesigner = "Yeew",
//     BeatmapID = 1
// });
// database.SaveChanges();
//
// // Read all of the beatmaps from the database
// var beatmaps = database.Beatmaps.ToList();
// foreach (Beatmap beatmap in beatmaps)
// {
//     Console.WriteLine(beatmap.BeatmapID + " " + beatmap.DifficultyLevel + " " + beatmap.DifficultyRating + " " + beatmap.NoteDesigner);
// }
//
// // Update the beatmap with the same beatmapID
// database.Beatmaps.Update(new Beatmap()
// {
//     TrackMetadata = trackMetadata,
//     DifficultyLevel = DifficultyLevel.Expert,
//     DifficultyRating = 20,
//     NoteDesigner = "Yeew",
//     BeatmapID = 1
// });
// database.SaveChanges();
//
// // Delete the beatmap with the same beatmapID
// database.Beatmaps.Remove(new Beatmap()
// {
//     BeatmapID = 1
// });
// database.SaveChanges();
//
// // Check if the beatmap is deleted
// beatmaps = database.Beatmaps.ToList();
// if (beatmaps.Count == 0)
// {
//     Console.WriteLine("Beatmap is deleted");
// }

#endregion

// TODO: Seperate some of the mess to a new class
// TODO: Add a unit test for the decoder and encoder
// TODO: Seperate the encoder and decoder to a new class