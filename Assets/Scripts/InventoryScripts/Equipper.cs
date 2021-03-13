using System;
using System.Collections;
using System.Collections.Generic;
using InventoryScripts;
using UnityEngine;
using Zenject;

public class Equipper : MonoBehaviour
{
    public Transform MainHandTransform;
    public Transform OffHandTransform;
    public Transform HeadTransform;
    public Transform BodyTransform;
    public Transform GlovesTransform;
    public Transform LegsTransform;
    public Transform BootsTransform;
    [Inject] private PlayerEquipment _equipment;

    private void Start()
    {
        _equipment.SetEquipTransforms(this);
    }
}
