namespace Assets.Scripts
{
    public enum InputMode
    {
        Accelerometer = 0,
        Touch
    }

    public enum DifficultyLevel
    {
        Easy = 0,
        Normal,
        Hard,
        Lunatic
    }

    /// <summary>
    /// Contains configuration data. 
    /// </summary>
    static class ConfigGlobal
    {
        public static double AccelDiplacementFactor = 1;
        public static InputMode InputMode = InputMode.Accelerometer;
        public static DifficultyLevel DifficultyLevel = DifficultyLevel.Normal;
    }
}
