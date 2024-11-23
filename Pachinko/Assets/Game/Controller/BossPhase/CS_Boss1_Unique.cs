using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Boss1_Unique : CS_BossUnique
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override int DesisionFlag(int _val)
    {
        return -1;
    }
}
