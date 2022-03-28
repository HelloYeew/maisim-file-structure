namespace maisim_file_structure.Objects.Notes;

public enum NoteLane
{
    Lane1,
    Lane2,
    Lane3,
    Lane4,
    Lane5,
    Lane6,
    Lane7,
    Lane8
}

public static class NoteLaneExtension
{
    public static NoteLane GetNoteLaneByNumber(int lane)
    {
        switch (lane)
        {
            case 1:
                return NoteLane.Lane1;
            case 2:
                return NoteLane.Lane2;
            case 3:
                return NoteLane.Lane3;
            case 4:
                return NoteLane.Lane4;
            case 5:
                return NoteLane.Lane5;
            case 6:
                return NoteLane.Lane6;
            case 7:
                return NoteLane.Lane7;
            case 8:
                return NoteLane.Lane8;
            default:
                throw new ArgumentOutOfRangeException(nameof(lane));
        }
    }

    public static int GetNumberByNoteLane(NoteLane noteLane)
    {
        switch (noteLane)
        {
            case NoteLane.Lane1:
                return 1;
            case NoteLane.Lane2:
                return 2;
            case NoteLane.Lane3:
                return 3;
            case NoteLane.Lane4:
                return 4;
            case NoteLane.Lane5:
                return 5;
            case NoteLane.Lane6:
                return 6;
            case NoteLane.Lane7:
                return 7;
            case NoteLane.Lane8:
                return 8;
            default:
                throw new ArgumentOutOfRangeException(nameof(noteLane));
        }
    }
}