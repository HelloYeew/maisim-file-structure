namespace maisim_file_structure.Enum;

public enum DifficultyLevel
{
    Basic,
    Advanced,
    Expert,
    Master,
    Remaster
}

public class DifficultyLevelExtension
{
    public static DifficultyLevel GetDifficultyLevel(string difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case "Basic":
                return DifficultyLevel.Basic;
            case "Advanced":
                return DifficultyLevel.Advanced;
            case "Expert":
                return DifficultyLevel.Expert;
            case "Master":
                return DifficultyLevel.Master;
            case "Remaster":
                return DifficultyLevel.Remaster;
            default:
                return DifficultyLevel.Basic;
        }
    }
}