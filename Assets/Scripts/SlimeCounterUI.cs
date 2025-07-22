using TMPro;
using UnityEngine;

public class SlimeCounterUI : MonoBehaviour
{
    private TextMeshProUGUI txtMeshPro;
    void Start()
    {
        txtMeshPro = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (txtMeshPro != null)
        {
            txtMeshPro.text = "SlimeCount = " + SlimeCounter.Instance.SlimeCount;
        }
        else
        {
            Debug.LogWarning("TextMeshPro컴포넌트가 없습니다!");
        }
    }
}
