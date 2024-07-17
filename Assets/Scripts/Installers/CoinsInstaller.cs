using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoinsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Icoin>().To<CoinController>().AsSingle().NonLazy();
    }
}
