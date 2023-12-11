using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [SerializeField]
    private float upgradedSplashRange;

    [SerializeField]
    private float splashRange;

    public float SplashRange { get => splashRange; }


    public override bool Upgrade()
    {
        bool wasUpgraded = base.Upgrade();
        if (!wasUpgraded) return false;

        splashRange = upgradedSplashRange;
        return true;
    }

}
