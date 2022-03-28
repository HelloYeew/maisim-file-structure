namespace maisim_file_structure.Objects;

public class BeatmapSet
{
    public TrackMetadata TrackMetadata { get; set; }
    
    public string Creator { get; set; }
 
    public int BeatmapSetID { get; set; }

    public List<Beatmap> Beatmaps { get; set; }
    
    public string AudioFilename { get; set; }
    
    public int PreviewTime { get; set; }
}