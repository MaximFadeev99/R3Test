using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace R3Test
{
    internal class ExposedButtonGroup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        
        [field: SerializeField] public Button Button { get; private set; }

        internal void Draw(string info)
        {
            _textField.text = info;
        }
        
        internal void Draw()
        {
            _textField.text = "Some info";
        }
    }
}
