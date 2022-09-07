using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoSingleTon<ObjectPool>
{
    //pool
    Queue<GameObject> exTextPool = new Queue<GameObject>();
    Queue<GameObject> goodTextPool = new Queue<GameObject>();
    Queue<GameObject> badTextPool = new Queue<GameObject>();
    Queue<GameObject> meleeNotePool = new Queue<GameObject>();

    //components
    GameObject textParant;
    GameObject textPrefab = null;
    GameObject meleeNotePrefab = null;
    //GameObject rangeNotePrefab = null;
    //GameObject bulletNotePrefab = null;
    //GameObject meleeEnemyPrefab = null;
    //GameObject rangeEnemyPrefab = null;
    //GameObject bulletPrefab = null;

    private void Awake()
    {
        textParant = GameObject.Find("UI");

        meleeNotePrefab = Resources.Load<GameObject>("MeleeNote");
        //rangeNotePrefab = Resources.Load<GameObject>("RangeNote");
        //bulletNotePrefab = Resources.Load<GameObject>("BulletNote");
        //meleeEnemyPrefab = Resources.Load<GameObject>("MeleeEnemy");
        //rangeEnemyPrefab = Resources.Load<GameObject>("RangeEnemy");
        //bulletPrefab = Resources.Load<GameObject>("Bullet");
    }

    public GameObject PopObject(Vector3 position, GameObject prefab, string name)//, string name)
    {
        GameObject instObject = null;

        if (name == "meleeNote")
        {
            if (meleeNotePool.Count == 0)
            {
                instObject = Instantiate(prefab, position, Quaternion.identity, this.transform);
                instObject.gameObject.SetActive(true);
                return instObject;
            }
            else
            {
                instObject = meleeNotePool.Dequeue();
                instObject.transform.position = position;
                instObject.transform.rotation = Quaternion.identity;
                instObject.gameObject.SetActive(true);
            }
        }
        else if (name == "checkExText")
        {
            if (exTextPool.Count == 0)
            {
                instObject = Instantiate(prefab, position, Quaternion.identity, textParant.transform);
                instObject.gameObject.SetActive(true);
                return instObject;
            }
            else
            {
                instObject = exTextPool.Dequeue();
                instObject.transform.position = position;
                instObject.transform.rotation = Quaternion.identity;
                instObject.gameObject.SetActive(true);
            }
        }
        else if (name == "checkGoodText")
        {
            if (goodTextPool.Count == 0)
            {
                instObject = Instantiate(prefab, position, Quaternion.identity, textParant.transform);
                instObject.gameObject.SetActive(true);
                return instObject;
            }
            else
            {
                instObject = goodTextPool.Dequeue();
                instObject.transform.position = position;
                instObject.transform.rotation = Quaternion.identity;
                instObject.gameObject.SetActive(true);
            }
        }
        else if (name == "checkBadText")
        {
            if (badTextPool.Count == 0)
            {
                instObject = Instantiate(prefab, position, Quaternion.identity, textParant.transform);
                instObject.gameObject.SetActive(true);
                return instObject;
            }
            else
            {
                instObject = badTextPool.Dequeue();
                instObject.transform.position = position;
                instObject.transform.rotation = Quaternion.identity;
                instObject.gameObject.SetActive(true);
            }
        }
        return instObject;
    }

    public void PushObject(GameObject _object, string name)
    {
        if (name == "meleeNote")
        {
            _object.SetActive(false);
            meleeNotePool.Enqueue(_object);
        }
        else if (name == "checkExText")
        {
            _object.SetActive(false);
            exTextPool.Enqueue(_object);
        }
        else if (name == "checkGoodText")
        {
            _object.SetActive(false);
            goodTextPool.Enqueue(_object);
        }
        else if (name == "checkBadText")
        {
            _object.SetActive(false);
            badTextPool.Enqueue(_object);
        }

    }
}
