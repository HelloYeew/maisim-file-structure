﻿using System.Reflection;
using maisim_file_structure.Enum;
using maisim_file_structure.Objects;

namespace maisim_file_structure;

public class BeatmapDecoder
{
    public string filePath;

    public int version;

    public Beatmap beatmap;
    
    public TrackMetadata trackMetadata;
    
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
                string property = lines[0].Split(':')[0];
                string value = lines[0].Split(':')[1];
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
        }
    }
}