namespace CardGuess.Components
{
    public interface ICardComponentGetter
    {
        public CardView CardView { get; }
        public int CardId { get; }
    }   
}