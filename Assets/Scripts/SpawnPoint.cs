using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Spawner _spawner;

    public bool IsFree { get; private set; } = true;

    private void OnEnable()
    {
        _spawner = GetComponentInParent<Spawner>();

        if (_spawner != null)
            _spawner.Init(this);
        else
            Debug.Log("Спавнер не обнаружен!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            IsFree = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            IsFree = true;
    }
}
