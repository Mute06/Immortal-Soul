namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursourType();
        bool HandleRaycast(PlayerController CallingController);

    }

}