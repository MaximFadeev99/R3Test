using ObservableCollections;
using R3;
using UnityEngine;

namespace R3Test
{
    public class PlayerChangeLogger : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        private void Awake()
        {
            _playerController.ConnectedPlayers
                .ObserveAdd()
                .Subscribe(OnPlayerAdded)
                .AddTo(this);
            _playerController.ConnectedPlayers
                .ObserveRemove()
                .Subscribe(OnPlayerRemoved)
                .AddTo(this);
        }

        private void OnPlayerAdded(CollectionAddEvent<Player> addEvent)
        {
            Debug.Log($"New player added: {addEvent.Value.name}");
        }

        private void OnPlayerRemoved(CollectionRemoveEvent<Player> removeEvent)
        {
            Debug.Log($"Player removed: {removeEvent.Value.name}");
        }
    }
}
