using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace R3Test
{
    internal class ButtonGroup : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textField;
        
        internal readonly Subject<string> ClickSubject = new();
        //Subject is bad because its OnNext method is exposed to other scripts,
        //but if made private, other scripts won't be able to subscribe
        
        private readonly string NotEmpty = nameof(NotEmpty);

        private string _lastMessage;
        private int _clickCounter = 0;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnEveryButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnEveryButtonClick);
        }

        internal void Draw(string info)
        {
            _textField.text = info;
        }
        
        internal void Draw()
        {
            _textField.text = "Some info";
        }

        private void OnEveryButtonClick()
        {
            _lastMessage = _lastMessage == string.Empty ? NotEmpty : string.Empty;
            _clickCounter++;
            ClickSubject.OnNext(_lastMessage);
            
            // if (_clickCounter == 10)      
            //     ClickSubject.OnCompleted(); //completely disables the stream
        }
    }
}
