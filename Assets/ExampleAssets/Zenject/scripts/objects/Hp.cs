
namespace ExampleAssets.ZenjectAsset {
    public class Hp {

        private int _hp;
        public int Value => _hp;

        public Hp() {
            _hp = 777;
            }

        public void IncreaseHp(int hp) {
            _hp -= hp;
            }

        }
    }