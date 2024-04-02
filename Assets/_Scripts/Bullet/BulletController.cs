using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : HighMonoBehaviour
{
    [SerializeField] private BulletScriptable bulletScriptable;
    public BulletScriptable BulletScriptable { get => bulletScriptable;}
}
