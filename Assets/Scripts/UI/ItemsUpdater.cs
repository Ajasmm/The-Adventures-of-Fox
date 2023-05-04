using UnityEngine;
using TMPro;
using System.Collections;

public class ItemsUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text gems;
    [SerializeField] TMP_Text cherry;

    CollectionSystem collectionSystem;
    Coroutine initializeRoutine;

    private void OnEnable()
    {
        if(initializeRoutine != null) StopCoroutine(initializeRoutine);
        StartCoroutine(SyncWithPlayer());
    }

    private void OnDisable()
    {
        if (initializeRoutine != null) StopCoroutine(initializeRoutine);
        collectionSystem.OnItemCollected -= OnItemCollected;
    }

    private IEnumerator SyncWithPlayer()
    {
        while (GameManager.Instance.player == null) yield return null;

        collectionSystem = GameManager.Instance.player.GetComponent<CollectionSystem>();

        UpdateGem(collectionSystem.GetItemCount(CollectableType.Gems));
        UpdateCherry(collectionSystem.GetItemCount(CollectableType.Cherry));

        collectionSystem.OnItemCollected += OnItemCollected;
    }

    private void OnItemCollected(CollectableType type, int count)
    {
        switch (type)
        {
            case CollectableType.Cherry:
                UpdateCherry(count);
                break;
            case CollectableType.Gems:
                UpdateGem(count);
                break;
        }
    }
    private void UpdateGem(int count)
    {
        gems.text = count.ToString();
    }
    private void UpdateCherry(int count)
    {
        cherry.text = count.ToString();
    }
}
