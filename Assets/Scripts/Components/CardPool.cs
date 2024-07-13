using UnityEngine;
using Zenject;

namespace CardGuess.Components
{
    public class CardPool : MonoBehaviour, ICardComponentGetter
    {
        public class Pool : MonoMemoryPool<Transform, CardPool>
        {
            protected override void OnCreated(CardPool item)
            {
                base.OnCreated(item);
                item.Init();
            }

            protected override void Reinitialize(Transform parent, CardPool item)
            {
                base.Reinitialize(parent, item);
                item.Reinitialize(parent);
            }

            protected override void OnDespawned(CardPool item)
            {
                base.OnDespawned(item);
                item.Despawn();
            }
        }
        
        private RectTransform _rectTransform;
        private Transform _transform;
        private int _cardId;

        [field: SerializeField] public CardView CardView { get; private set; }
        public int CardId => _cardId;

        public void SetId(int id)
        {
            _cardId = id;
        }

        private void Reinitialize(Transform parent)
        {
            _transform.SetParent(parent);
            _transform.position = Vector3.zero;
            _transform.rotation = Quaternion.identity;
            ResetSize();
            gameObject.SetActive(true);
        }

        private void Despawn()
        {
            _rectTransform.localScale = Vector3.one;
            gameObject.SetActive(false);
        }
        
        private void ResetSize()
        {
            _rectTransform.anchorMin = new Vector2(0, 0);
            _rectTransform.anchorMax = new Vector2(1, 1);
            _rectTransform.offsetMin = Vector2.zero;
            _rectTransform.offsetMax = Vector2.zero;
                
            _rectTransform.localScale = Vector3.one;
        }

        private void Init()
        {
            _rectTransform = GetComponent<RectTransform>();
            _transform = GetComponent<Transform>();
        }
    }   
}