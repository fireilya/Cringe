using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private readonly Vector3 _offset = new(-0.8425192f, 0, 0);

    [SerializeField]
    private GameObject _health;

    private Vector3 _lastHeartPosition = new(8.435f + 0.8425192f, -4.555f, 0);
    private List<GameObject> instantiateObjects;

    private void Start()
    {
    }

    public void UpdateHealth(int health)
    {
        if (instantiateObjects != null)
            foreach (var gameObject in instantiateObjects)
            {
                Destroy(gameObject);
                _lastHeartPosition -= _offset;
            }

        instantiateObjects = new List<GameObject>();
        for (var i = 0; i < health; i++)
        {
            _lastHeartPosition += _offset;
            instantiateObjects.Add(Instantiate(_health, _lastHeartPosition, new Quaternion(0, 0, 0, 0)));
        }
    }

    public void Hit()
    {
        Destroy(instantiateObjects[^1]);
        _lastHeartPosition -= _offset;
        instantiateObjects.RemoveAt(instantiateObjects.Count - 1);
    }
}