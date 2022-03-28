using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using maisim_file_structure.Enum;
using maisim_file_structure.Objects;
using maisim_file_structure.Objects.Notes;

namespace maisim_file_structure;

public class BeatmapDecoder
{
    public string filePath;

    public int version;

    public Beatmap beatmap;
    
    public TrackMetadata trackMetadata;

    public List<Note> Notes;

    public BeatmapDecoder(string filePath)
    {
        this.filePath = filePath;

        using (StreamReader file = File.OpenText(filePath))
        {
            // convert each line to a string and add it to the list
            string[] lines = file.ReadToEnd().Split('\n');
            
            // remove every \r from the lines
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\r", "");
            }
            
            // get the version from the first line
            version = int.Parse(lines[0].Split(' ')[4]);
            
            // remove the first and second array element (since second line is empty)
            lines = lines.Skip(2).ToArray();
            
            // import beatmap property from the lines to beatmap
            // format is property : value
            beatmap = new Beatmap();
            while (lines[0] != "")
            {
                // get the property and value
                // string property = lines[0].Split(':')[0];
                // use regex to split ": "
                string property = Regex.Split(lines[0], ": ")[0];
                string value = Regex.Split(lines[0], ": ")[1];
                // import the property and value to beatmap using propertyinfo
                PropertyInfo propertyInfo = beatmap.GetType().GetProperty(property);
                if (property == "DifficultyLevel")
                {
                    beatmap.DifficultyLevel = DifficultyLevelExtension.GetDifficultyLevel(value);
                }
                else
                {
                    propertyInfo.SetValue(beatmap, Convert.ChangeType(value, propertyInfo.PropertyType));
                }
                // remove the first element
                lines = lines.Skip(1).ToArray();
            }
            
            // skip the next line (empty)
            lines = lines.Skip(1).ToArray();
            
            // import track metadata from the lines to trackMetadata
            // format is property : value
            trackMetadata = new TrackMetadata();
            while (lines[0] != "")
            {
                // get the property and value
                string property = Regex.Split(lines[0], ": ")[0];
                string value = Regex.Split(lines[0], ": ")[1];
                // import the property and value to trackMetadata using propertyinfo
                PropertyInfo propertyInfo = trackMetadata.GetType().GetProperty(property);
                propertyInfo.SetValue(trackMetadata, Convert.ChangeType(value, propertyInfo.PropertyType));
                // remove the first element
                lines = lines.Skip(1).ToArray();
            }
            
            // skip the next line (empty)
            lines = lines.Skip(1).ToArray();
            Notes = new List<Note>();
            // import notes from the lines to notes
            while (lines[0] != "")
            {
                // each line has , to seperate the value
                // first value is the note type
                // split the entire line to a new array
                string[] values = lines[0].Split(',');
                if (values[0] == "1")
                {
                    Notes.Add(new TapNote
                    {
                        Lane = NoteLaneExtension.GetNoteLaneByNumber(Int32.Parse(values[1])),
                        TargetTime = float.Parse(values[2])
                    });
                }
                lines = lines.Skip(1).ToArray();
            }
        }
    }
}