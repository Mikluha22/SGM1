using UnityEngine;

public static class LayerUnitl
 {
    public const string EnemyLayerName = "Enemy";
    public const string PlayerLayerName = "Player";
    public const string BulletLayerName = "Bullet";

    public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);
    public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);
    public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);

    public static bool IsBullet(GameObject gameObject)
    { 
        return gameObject.layer == BulletLayer;
    }
        

}
