using UnityEngine;
using UnityEngine.UI;

namespace R3Test
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(DealDamageToPlayer);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(DealDamageToPlayer);
        }

        private void DealDamageToPlayer()
        {
            _player.TakeDamage();
        }
    }
}
