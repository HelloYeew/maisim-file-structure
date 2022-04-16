﻿using Microsoft.EntityFrameworkCore;

namespace maisim_file_structure.Objects;

/// <summary>
/// Class that provide the essential track metadata that's normally bind to the beatmap.
/// </summary>
/// <seealso cref="Beatmap"/>
[Keyless]
public class TrackMetadata
{
    public string Title { get; set; }
    
    public string TitleUnicode { get; set; }

    public string Artist { get; set; }
    
    public string ArtistUnicode { get; set; }
    
    public string Source { get; set; }

    public string CoverPath { get; set; }

    public float Bpm { get; set; }
}