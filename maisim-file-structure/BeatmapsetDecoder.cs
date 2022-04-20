using System.Text.RegularExpressions;
using maisim_file_structure.Objects;

namespace maisim_file_structure;

public class BeatmapsetDecoder
{
    public BeatmapSet beatmapSet = new BeatmapSet();
    
    public string DirectoryPath;

    public int version;
    
    public TrackMetadata trackMetadata;
    
    public BeatmapsetDecoder(string DirectoryPath)
    {
        this.DirectoryPath = DirectoryPath;
        string MsbsPath = "";

        // Find the file that is using msbs extension in filepath folder
        string[] files = Directory.GetFiles(Path.GetDirectoryName(DirectoryPath), "*.msbs");
        // print all the files in the directory
        if (files.Length == 0)
        {
            throw new Exception("No msbs file found in directory");
        }
        else if (files.Length > 1)
        {
            throw new Exception("More than one msbs file found in directory");
        }
        else
        {
            MsbsPath = files[0];
        }
        
        // Read the file
        using (StreamReader file = File.OpenText(MsbsPath))
        {
            // convert each line to a string and add it to the list
            string[] lines = file.ReadToEnd().Split('\n');
            
            // remove every \r from the lines
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace("\r", "");
            }
            
            // get the version from first line
            version = int.Parse(lines[0].Split(' ')[5]);
            
            // remove the first and second array element (since second line is empty)
            lines = lines.Skip(2).ToArray();
            
            // import beatmapset properties
            // format is property: value
            while (lines[0] != "")
            {
                string property = Regex.Split(lines[0], ": ")[0];
                string value = Regex.Split(lines[0], ": ")[1];
                // The property name is exactly match with the property name in BeatmapSet class
                // to prevent error on convert to int, we must detect that is the property is a int ot not
                if (beatmapSet.GetType().GetProperty(property).PropertyType == typeof(int))
                {
                    beatmapSet.GetType().GetProperty(property).SetValue(beatmapSet, int.Parse(value));
                }
                else
                {
                    beatmapSet.GetType().GetProperty(property).SetValue(beatmapSet, value);
                }
                lines = lines.Skip(1).ToArray();
            }
            
            // skip the empty line
            lines = lines.Skip(1).ToArray();
            
            // import track metadata from the lines to trackMetadata
            // format is property: value
            trackMetadata = new TrackMetadata();
            while (lines[0] != "")
            {
                // split the line into property and value
                string property = Regex.Split(lines[0], ": ")[0];
                string value = Regex.Split(lines[0], ": ")[1];
                // The property name is exactly match with the property name in TrackMetadata class
                if (trackMetadata.GetType().GetProperty(property).PropertyType == typeof(int))
                {
                    trackMetadata.GetType().GetProperty(property).SetValue(trackMetadata, int.Parse(value));
                } else if (trackMetadata.GetType().GetProperty(property).PropertyType == typeof(float))
                {
                    trackMetadata.GetType().GetProperty(property).SetValue(trackMetadata, float.Parse(value));
                }
                else
                {
                    trackMetadata.GetType().GetProperty(property).SetValue(trackMetadata, value);
                }
                // trackMetadata.GetType().GetProperty(property).SetValue(trackMetadata, value);
                lines = lines.Skip(1).ToArray();
            }
        }
        
        beatmapSet.Beatmaps = new List<Beatmap>();
        decodeBeatmap();
    }

    public void decodeBeatmap()
    {
        // just using beatmapdecoder class to decode the beatmap
        
        // get all file that's end with .msbm extension in the directory
        string[] files = Directory.GetFiles(Path.GetDirectoryName(DirectoryPath), "*.msbm");
        if (files.Length == 0)
        {
            throw new Exception("No msbm file found in directory");
        }
        else
        {
            foreach (var beatmapPath in files)
            {
                BeatmapDecoder decoder = new BeatmapDecoder(beatmapPath);
                beatmapSet.Beatmaps.Add(decoder.beatmap);
            }
        }
    }
}