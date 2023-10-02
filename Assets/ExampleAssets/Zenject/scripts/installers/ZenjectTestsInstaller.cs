using UnityEngine;
using Zenject;

namespace ExampleAssets.ZenjectAsset {
    public class ZenjectTestsInstaller : MonoInstaller {

        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _spawnPoint;

        public override void InstallBindings() {
            Container.
                Bind<Player>()
                //.FromComponentInNewPrefab(_playerPrefab)
                .FromNewComponentOnNewPrefab(_playerPrefab)
                .AsSingle();

            Container.Bind<Hp>().FromNew().AsSingle();
            }

        }
    }