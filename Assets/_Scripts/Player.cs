using R3;
using UnityEngine;

namespace R3Test
{
    public class Player : MonoBehaviour
    {
        private readonly ReactiveProperty<int> _health = new(5);
        //public ReadOnlyReactiveProperty<int> Health => _health; //ReadOnlyReactiveProperty gives an ability to read the assigned value,
                                                                    //but not to change it + Subscribe on changes
        public Observable<int> Health => _health; //Observable allows only subscription on changes

        public void TakeDamage()
        {
            _health.Value--;
        }
    }
}
