using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : UIController
{
    [SerializeField]
    private Image[] _lifes;
    [SerializeField]
    private Sprite _loseLife;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Lose(int currentLife)
    {
        StartCoroutine(LostLife(currentLife));
    }

    private void UpdateLoseSprites(int currentLife)
    {
        for (var i = 0; i < currentLife; i++)
        {
            _lifes[i].sprite = _loseLife;
        }
    }

    private IEnumerator LostLife(int currentLife)
    {
        UpdateLoseSprites(currentLife);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _lifes[currentLife].sprite = _loseLife;
        _audioSource.Play();
        yield return new WaitForSeconds(3.16f);
        if (currentLife!=2)
        {
            RestartGame();
            yield break;
        }
        UIModeFromTo(loseScreen.gameObject, gameOverScreen.gameObject);

    }
}
