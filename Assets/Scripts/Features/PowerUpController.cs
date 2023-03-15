using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public Sprite powerUpSprite0;
    public Sprite powerUpSprite1;
    public Sprite powerUpSprite2;
    [Space]
    [Header("References:")]
    public TouchAndGo touchAndGo;

    public LayerMask boarLayer;

    public float speedBoostMultiplier = 2f;

    private SpriteRenderer spriteRenderer;
    private Coroutine powerUpCoroutine;

    private bool isSpeedBoostActive = false;
    private float originalMovementSpeed = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = powerUpSprite0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "yellowMilk" && spriteRenderer.sprite != powerUpSprite1)
        {
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);

                if (isSpeedBoostActive)
                {
                    isSpeedBoostActive = false;
                    touchAndGo.movementSpeed = originalMovementSpeed;
                }    
            }
            powerUpCoroutine = StartCoroutine(SetPowerUpSprite(powerUpSprite1, 10f));
        }

        if (collision.gameObject.tag == "blueMilk" && spriteRenderer.sprite != powerUpSprite2)
        {
            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
            }
            powerUpCoroutine = StartCoroutine(SetPowerUpSprite(powerUpSprite2, 10f));
        }
    }

    private IEnumerator SetPowerUpSprite(Sprite sprite, float duration)
    {
        spriteRenderer.sprite = sprite;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Boar"), sprite == powerUpSprite1);

        if (sprite == powerUpSprite2 && !isSpeedBoostActive)
        {
            // Зберегти оригінальне значення movementSpeed
            originalMovementSpeed = touchAndGo.movementSpeed;

            // Змінити movementSpeed на певний проміжок часу
            touchAndGo.movementSpeed *= speedBoostMultiplier;

            // Позначити, що Speed Boost активований
            isSpeedBoostActive = true;
        }

        yield return new WaitForSeconds(duration);

        spriteRenderer.sprite = powerUpSprite0;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Boar"), false);

        if (isSpeedBoostActive)
        {
            // Повернути оригінальне значення movementSpeed
            touchAndGo.movementSpeed = originalMovementSpeed;

            // Позначити, що Speed Boost неактивований
            isSpeedBoostActive = false;
        }

        powerUpCoroutine = null;
    }
}
