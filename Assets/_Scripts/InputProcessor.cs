using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace R3Test
{
    public class InputProcessor : MonoBehaviour
    {
        [SerializeField] private ButtonGroup _everyClickGroup;
        [SerializeField] private ExposedButtonGroup _exposedButtonGroup;

        //private IDisposable _disposableForSubscriptions;
        private readonly CompositeDisposable _compositeDisposable = new();
        private int _everyClickCounter = 0;
        
        private void Start()
        {
            //DisposableBuilder disposableBuilder = new DisposableBuilder();
            
            //Subscription with a lambda expression
            // _everyClickGroup.ClickSubject
            //     .Subscribe(_ =>
            //     {
            //         _everyClickCounter++;
            //         _everyClickGroup.Draw($"Every click counter: {_everyClickCounter}");
            //     })
            //     .AddTo(ref disposableBuilder);
            
            //Subscription with a method
            // _everyClickGroup.ClickSubject
            //     .Subscribe(OnEveryClick)
            //     .AddTo(ref disposableBuilder);

            _everyClickGroup.ClickSubject
                //.Where(message => message != string.Empty)
                .ThrottleLast(TimeSpan.FromSeconds(1f))
                .Subscribe(OnMessageReceived)
                .AddTo(_compositeDisposable);
                //.AddTo(ref disposableBuilder);
            
            //_disposableForSubscriptions = disposableBuilder.Build(); //DisposableBuilder can be built, 
                                                                        //but if we want to add subscriptions in other methods, it won't work
                                                                        //CompositeDisposable is better in this case
                                                                        
            _exposedButtonGroup.Button
                .OnClickAsObservable()
                .Subscribe(OnExposedButtonSignal)
                .AddTo(_compositeDisposable);
        }

        private void OnDestroy()
        {
            //_disposableForSubscriptions.Dispose();
            _compositeDisposable?.Dispose();
        }

        private void OnEveryClick(Unit _)
        {
            _everyClickCounter++;
            _everyClickGroup.Draw($"Every click counter: {_everyClickCounter}");
        }

        private void OnMessageReceived(string incomingMessage)
        {
            _everyClickCounter++;
            _everyClickGroup.Draw($"Every click counter: {incomingMessage} {_everyClickCounter}");
        }

        private void SendMessageWithDelay(string incomingMessage)
        {
            _everyClickCounter++;
            Observable
                .Timer(TimeSpan.FromSeconds(1f))
                .Subscribe(_ => _everyClickGroup.Draw($"Delayed message: {incomingMessage} {_everyClickCounter}"))
                .AddTo(_compositeDisposable);
        }
        
        private void StartSendingMessages(string _)
        {
            IDisposable disposable =null;
            
            disposable= Observable
                .Interval(TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    _everyClickCounter++;
                    _everyClickGroup.Draw($"Delayed message:  {_everyClickCounter}");
                    
                    if (_everyClickCounter >= 5)
                        disposable.Dispose();
                })
                .AddTo(_compositeDisposable);
        }

        private void OnExposedButtonSignal(Unit _)
        {
            _everyClickCounter++;
            _exposedButtonGroup.Draw($"Exposed button signal: {_everyClickCounter}");
        }
    }
}
