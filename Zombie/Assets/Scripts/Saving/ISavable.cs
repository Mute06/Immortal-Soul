namespace RPG.Saving
{
    public interface ISavable
    {
        void RestoreState(object state);
        object CaptureState();
    }

}