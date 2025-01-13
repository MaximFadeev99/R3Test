using ObservableCollections;
using UnityEngine;
using UnityEngine.UI;

namespace R3Test
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Button _addPlayerButton;
        [SerializeField] private Button _removePlayerButton;
        
        private readonly ObservableList<Player> _connectedPlayers = new();
        public IObservableCollection<Player> ConnectedPlayers => _connectedPlayers;

        private void OnEnable()
        {
            _addPlayerButton.onClick.AddListener(AddPlayer);
            _removePlayerButton.onClick.AddListener(RemovePlayer);
        }

        private void OnDisable()
        {
            _addPlayerButton.onClick.RemoveListener(AddPlayer);
            _removePlayerButton.onClick.RemoveListener(RemovePlayer);
        }

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                AddPlayer();
            }
        }

        private void RemovePlayer()
        {
            if (_connectedPlayers.Count <= 0)
                return;
            
            _connectedPlayers.RemoveAt(_connectedPlayers.Count - 1);
        }

        private void AddPlayer()
        {
            _connectedPlayers.Add(Instantiate(_playerPrefab));
        }
    }
}
