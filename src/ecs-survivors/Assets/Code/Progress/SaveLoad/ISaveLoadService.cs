namespace Code.Progress.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        void LoadProgress();
        bool HasSavedProgress { get; }
        void CreateProgress();
    }
}