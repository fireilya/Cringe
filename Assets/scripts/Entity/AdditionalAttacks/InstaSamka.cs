using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class InstaSamka : MonoBehaviour
{
    [SerializeField]
    private TMP_Text hitText;

    private PlayableDirector m_Director;
    public string message = "Типа";

    public void DoHit()
    {
        var text = Instantiate(hitText, transform.position, Quaternion.identity);
        text.text = message;
    }
}