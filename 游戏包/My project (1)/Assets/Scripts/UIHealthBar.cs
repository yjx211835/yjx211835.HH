using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image mask;
   private float originalSize;

    
    public bool hasTask;
    //public bool ifCompleteTask;
    public int fixedNum;
    public static UIHealthBar instance{get;private set;}
    // Start is called before the first frame update
   private void Awake()
   {
    instance=this;
   }
   
    void Start()
    {
        originalSize=mask.rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()

    {
        
    }
    public void SetValue(float fillPercent)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,originalSize*fillPercent);
    }
}
