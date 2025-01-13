using System;
using TMPro;
using R3;
using UnityEngine;

namespace R3Test
{
    public class HealthDrawer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthField;
        [SerializeField] private Player _player;

        private void Awake()
        {
            _player.Health
                .Subscribe(DrawHealth)
                .AddTo(this);
        }

        private void DrawHealth(int currentHealth)
        {
            _healthField.text = currentHealth.ToString();
        }
    }
}
