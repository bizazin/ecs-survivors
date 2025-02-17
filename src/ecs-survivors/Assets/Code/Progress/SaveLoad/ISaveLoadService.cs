namespace Code.Progress.SaveLoad
{
    public interface ISaveLoadService
    {
        bool HasSavedProgress { get; }
        void SaveProgress();
        void LoadProgress();
        void CreateProgress();
    }
}