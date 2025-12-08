using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaObject : InteractiveObject
{
    public override void Deactivate()
    {
        GameObject uiPanel = GameObject.Find("UIPanel");
        if (uiPanel == null)
        {
            Debug.LogError("UIPanel 오브젝트를 찾을 수 없습니다!");
            return;
        }


        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);

        GameObject sliderInstance = Instantiate(sliderPrefab, uiPanel.transform);
        sliderInstance.transform.position = screenPos;
        HoldSlider holdSlider = sliderInstance.GetComponent<HoldSlider>();
        if (holdSlider == null)
        {
            Debug.LogError("슬라이더 프리팹에 HoldSlider 스크립트가 없습니다!");
            return;
        }

        holdSlider.Init(() =>
{
   playerAnim.SetBool("Collect", false);
   onDeactivateAction?.Invoke();
   ItemGet();
});
    }

    public override void ItemGet()
    {

        InventoryManager.Instance.SetItemActive("ax", true);

    }

}
