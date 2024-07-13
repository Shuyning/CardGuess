using System.Collections.Generic;
using CardGuess.Components;

namespace CardGuess.Controllers
{
    public interface IGameCardGetter
    {
        public IReadOnlyDictionary<int, CardPool> ActiveActiveCards { get; }
    }
}