using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InformationPanelConfigure : MonoBehaviour
{
    [SerializeField] private Image ProductImage;
    [SerializeField] private TextMeshProUGUI ProductName;
    [SerializeField] private Transform produceSortStartPos;
    [SerializeField] private Transform produceListParent;
    private List<GameObject> ListedProducePanels= new List<GameObject>();

    private void Start()
    {
        EventManager.onSelectedObjChanged += ConfigurePanel;
    }

    public void ConfigurePanel(ProductData productData)
    {
        ProductImage.sprite = productData.ProductSprite;
        ProductName.text = productData.ProductName;
        
        ClearProducePanels();
        
        if (productData.ProducedThings.Count > 0)
        {
            for (int i = 0; i < productData.ProducedThings.Count; i++)
            {
            GameObject ProducePanel = ObjectPool.Instance.GetFromPool("ProducePanel");
            ListedProducePanels.Add(ProducePanel);
            Debug.Log(ProducePanel.transform.GetChild(0));
            ProducePanel.transform.GetChild(0).GetComponent<Image>().sprite = productData.ProducedThings[i].ProductSprite;
            ProducePanel.transform.SetParent(produceListParent);
            ProducePanel.transform.localScale=Vector3.one;
            ProducePanel.transform.position = produceSortStartPos.position+ ProducePanel.transform.position * 1.5f * i;
            ProducePanel.SetActive(true);
            }
        }

    }

    public void ClearProducePanels()
    {
        foreach (var panel in ListedProducePanels)
        {
            ObjectPool.Instance.Deposit(panel);
        }
        ListedProducePanels.Clear();
    }
}
