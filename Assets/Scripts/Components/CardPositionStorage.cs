using System.Collections.Generic;
using UnityEngine;

namespace CardGuess.Components
{
    public class CardPositionStorage : MonoBehaviour, ICardPositionStorageGetter
    {
        [SerializeField] private List<FieldCardPosition> _fieldCardPositions;

        public IReadOnlyList<ICardPositionGetter> CardPositions => _fieldCardPositions;
    }
}