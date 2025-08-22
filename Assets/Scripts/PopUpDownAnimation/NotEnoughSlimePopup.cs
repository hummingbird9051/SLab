using System.Collections;
using UnityEngine;

public class NotEnoughSlimePopup : MonoBehaviour
{
    public GameObject panel;
    public PopUpAnimation anim;
    void OnEnable()
    {
        PoolManager.Instance.NotEnoughSlimes += AnimStarter;
    }

    

    void AnimStarter()
    {
        StartCoroutine(NotEnoughAnimPopUpDown());
    }

    IEnumerator NotEnoughAnimPopUpDown()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(anim.DoPopDownAnimation());
        panel.SetActive(false);
    }

    
}
