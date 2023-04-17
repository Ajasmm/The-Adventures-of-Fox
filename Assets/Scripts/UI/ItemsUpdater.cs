using UnityEngine;
using TMPro;


public class ItemsUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text gems;
    [SerializeField] TMP_Text cherry;

    private void OnEnable()
    {
        int count = 0;
        Inventory.Instance.GetItemCount(CollectableType.Gems, out count);
        UpdateGem(count);
        Inventory.Instance.OnGemUpdate += UpdateGem;

        count = 0;
        Inventory.Instance.GetItemCount(CollectableType.Cherry, out count);
        UpdateCherry(count);
        Inventory.Instance.OnCherryUpdate += UpdateCherry;
    }

    private void OnDisable()
    {
        Inventory instance = Inventory.Instance;
        if (instance == null) return;

        instance.OnGemUpdate -= UpdateGem;
        instance.OnCherryUpdate -= UpdateCherry;
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
