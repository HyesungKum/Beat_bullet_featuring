using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleTon<ObjectPool>
{
    //components
    Queue<GameObject> objectPool = new Queue<GameObject>();
    GameObject meleeNotePrefab = null;
    //GameObject rangeNotePrefab = null;
    //GameObject bulletNotePrefab = null;
    //GameObject meleeEnemyPrefab = null;
    //GameObject rangeEnemyPrefab = null;
    //GameObject bulletPrefab = null;

    private void Awake()
    {
        meleeNotePrefab = Resources.Load<GameObject>("MeleeNote");
        //rangeNotePrefab = Resources.Load<GameObject>("RangeNote");
        //bulletNotePrefab = Resources.Load<GameObject>("BulletNote");
        //meleeEnemyPrefab = Resources.Load<GameObject>("MeleeEnemy");
        //rangeEnemyPrefab = Resources.Load<GameObject>("RangeEnemy");
        //bulletPrefab = Resources.Load<GameObject>("Bullet");
    }

    public GameObject PopObject(Vector3 position, GameObject prefab)//, string name)
    {
        GameObject instObject = null;

        if (objectPool.Count == 0)
        {
            instObject = Instantiate(prefab, position, Quaternion.identity, this.transform);
            instObject.gameObject.SetActive(true);
            return instObject;
        }
        else
        {
            instObject = objectPool.Dequeue();
            instObject.transform.position = position;
            instObject.transform.rotation = Quaternion.identity;
            instObject.gameObject.SetActive(true);
        }

        return instObject;
    }

    public void PushObject(GameObject _object)
    {
        _object.SetActive(false);
        objectPool.Enqueue(_object);
    }
}
