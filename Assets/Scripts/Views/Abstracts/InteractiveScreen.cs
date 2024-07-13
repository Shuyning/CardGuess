namespace CardGuess.View
{
    public abstract class InteractiveScreen : Screen
    {
        public virtual void Show()
        {
            ChangeCanvasGroupState(1f, fadeTime, true);
        }

        public virtual void Hide(bool isFade = true)
        {
            ChangeCanvasGroupState(0f,
                isFade ? fadeTime : 0f, false);
        }
    }   
}