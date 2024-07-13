using System.Collections.Generic;

namespace CardGuess.Components
{
    public interface ICardPositionStorageGetter
    {
        IReadOnlyList<ICardPositionGetter> CardPositions { get; }
    }
}