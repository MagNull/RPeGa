namespace DialogSystemScripts
{
    public interface ITalkable
    {
        void StartDialog(PlayerDialoger dialoger);
        void EndDialog();
    }
}
