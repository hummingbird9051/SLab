using System.Collections;
using UnityEngine;

public class EndingPopup : MonoBehaviour
{
    public GameObject panel;
    public PopUpAnimation anim;
    void OnEnable()
    {
        SpawnManager.Instance.KingSlimeAppeared += AnimStarter;
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
