using UnityEngine;

public static class LayerUnitl
 {
    public const string EnemyLayerName = "Enemy";
    public const string PlayerLayerName = "Player";
    public const string BulletLayerName = "Bullet";
    public const string PickUpLayerName = "PickUp";

    public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);
    public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);
    public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
    public static readonly int PickUpLayer = LayerMask.NameToLayer(PickUpLayerName);
    public static bool IsBullet(GameObject gameObject)
    { 
        return gameObject.layer == BulletLayer;
    }
   
    public static bool IsPickUp(GameObject gameObject)
    { 
        return gameObject.layer == PickUpLayer;
    }
}
