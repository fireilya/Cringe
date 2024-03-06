using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : UIController
{
    private AudioSource _audioSource;

    [SerializeField]
    private Image[] _lifes;

    [SerializeField]
    private Sprite _loseLife;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Lose(int currentLife)
    {
        StartCoroutine(LostLife(currentLife));
    }

    private void UpdateLoseSprites(int currentLife)
    {
        for (var i = 0; i < currentLife; i++) _lifes[i].sprite = _loseLife;
    }

    private IEnumerator LostLife(int currentLife)
    {
        UpdateLoseSprites(currentLife);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _lifes[currentLife].sprite = _loseLife;
        _audioSource.Play();
        yield return new WaitForSeconds(3.16f);
        if (currentLife != 2)
        {
            RestartGame();
            yield break;
        }

        Cursor.lockState = CursorLockMode.None;
        UIModeFromTo(loseScreen.gameObject, gameOverScreen.gameObject);
    }
}