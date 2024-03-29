//using Mono.Cecil;
using System.Collections;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    public GameObject player;
    public GameObject currentTrail;
    public GameObject redTrail;
    public GameObject blueTrail;
    public GameObject yellowTrail;
    public Sprite redPencil;
    public Sprite bluePencil;
    public Sprite yellowPencil;
    public float speed;
    public float playerOffsetValue;

    private SpriteRenderer spriteRenderer;
    private Vector3 playerOffset;
    private bool followingPlayer = true;
    private bool startPosition = false;

    private bool canAttack = true;
    private bool canSkill = true;

    private bool redAttack = false;
    private bool blueAttack = false;
    private bool yellowAttack = false;

    [Header("Red Attack")]
    public float redStartAttackLength = 2f;
    public float redAttackLength = 2f;
    public float redAttackRange = 45f;

    [Header("Blue Attack")]
    public float blueStartAttackLength = 2f;
    public float blueAttackLength = 2f;
    public float blueAttackOffset = 1f;

    [Header("Yellow Attack")]
    public float yellowStartAttackLength = 2f;
    public float yellowAttackLength = 2f;
    public float yellowAttackOffset = 1f;
    public float yellowAttackRange = 15f;

    private bool redSkill = false;
    private bool blueSkill = false;
    private bool yellowSkill = false;

    [Header("Red Skill")]
    public float redStartSkillLength = 2f;
    public float redSkillLength = 2f;
    public float redSkillRange = 45f;

    [Header("Blue Skill")]
    public float blueStartSkillLength = 2f;
    public float blueSkillLength = 2f;
    public float blueSkillRange = 45f;

    [Header("Yellow Skill")]
    public float yellowStartSkillLength = 2f;
    public float yellowSkillLength = 2f;
    public float yellowSkillRange = 45f;

    private Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        spriteRenderer.sprite = redPencil;
    }

    void FixedUpdate()
    {
         if(AttributeManager.Instance.paused) { return; };
        // Follow Player
        if (followingPlayer){ FollowPlayer(); }
        // Red Attack
        if(redAttack) { RedAttackAction(); }
        // Red Skill
        if (redSkill) { RedSkillAction(); }
        // Blue Attack
        if (blueAttack){ BlueAttackAction(); }
        // Blue Skill
        if (blueSkill) { BlueSkillAction(); }
        // Yellow Attack
        if (yellowAttack) { YellowAttackAction(); }
        // Yellow Skill
        if (yellowSkill) { YellowSkillAction(); }
    }

    public void PerformRedAttack()
    {
        if (!canAttack) return;
        canAttack = false;
        canSkill = false;
        redAttack = true;
        startPosition = true;
        followingPlayer = false;
    }

    public void PerformBlueAttack()
    {
        if (!canAttack) return;
        canAttack = false;
        canSkill = false;
        blueAttack = true;
        startPosition = true;
        followingPlayer = false;
    }

    public void PerformYellowAttack()
    {
        if (!canAttack) return;
        canAttack = false;
        canSkill = false;
        yellowAttack = true;
        startPosition = true;
        followingPlayer = false;
    }

    public void PerformRedSkill()
    {
        if (!canSkill) return;
        canAttack = false;
        canSkill = false;
        redSkill = true;
        startPosition = true;
        followingPlayer = false;
    }

    public void PerformBlueSkill()
    {
        if (!canSkill) return;
        canAttack = false;
        canSkill = false;
        blueSkill = true;
        startPosition = true;
        followingPlayer = false;
    }

    public void PerformYellowSkill()
    {
        if (!canSkill) return;
        canAttack = false;
        canSkill = false;
        yellowSkill = true;
        startPosition = true;
        followingPlayer = false;
    }

    Vector3 CalculateCoords(float radius, float angle, Vector3 center)
    {
        float angleRadians = Mathf.Deg2Rad * angle;

        float x = center.x + radius * Mathf.Cos(angleRadians);
        float y = center.y + radius * Mathf.Sin(angleRadians);

        float z = center.z;

        return new Vector3(x, y, z);
    }
    public void ChangeTrailRed()
    {
        currentTrail.SetActive(false);
        currentTrail = redTrail;
        spriteRenderer.sprite = redPencil;
        currentTrail.SetActive(true);
    }
    public void ChangeTrailBlue()
    {
        currentTrail.SetActive(false);
        currentTrail = blueTrail;
        spriteRenderer.sprite = bluePencil;
        currentTrail.SetActive(true);
    }
    public void ChangeTrailYellow()
    {
        currentTrail.SetActive(false);
        currentTrail = yellowTrail;
        spriteRenderer.sprite = yellowPencil;
        currentTrail.SetActive(true);
    }

    private void FollowPlayer()
    {
        Vector2 offset = CalculateCoords(playerOffsetValue,
                this.transform.eulerAngles.z + 90, Vector3.zero);

        playerOffset = new Vector3(offset.x, offset.y, 0);

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position + playerOffset, speed);

        Vector2 result = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

        this.transform.rotation = (Quaternion.Euler(0f, 0f, Mathf.Atan2(result.y, result.x) * Mathf.Rad2Deg));
        this.transform.Rotate(0, 0, -90);
    }

    private void RedAttackAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + redAttackRange));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        redAttackLength -= Time.deltaTime;

        if (redAttackLength < (redStartAttackLength / 2))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (360 - redAttackRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (redAttackLength <= 0)
        {
            redAttackLength = redStartAttackLength;
            redAttack = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }

    private void RedSkillAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        redSkillLength -= Time.deltaTime;

        if (redSkillLength < (redStartSkillLength / 2) && redSkillLength > (redStartSkillLength / 3))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (90 - redSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (redSkillLength < (redStartSkillLength / 3) && redSkillLength > (redStartSkillLength / 4))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (redSkillRange + 2)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (redSkillLength < (redStartSkillLength / 4) && redSkillLength > (redStartSkillLength / 5))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (270 - redSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (redSkillLength <= 0)
        {
            redSkillLength = redStartSkillLength;
            redSkill = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }

    private void BlueAttackAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue - blueAttackOffset,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        blueAttackLength -= Time.deltaTime;

        if (blueAttackLength < (blueStartAttackLength / 2))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue + blueAttackOffset,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (blueAttackLength <= 0)
        {
            blueAttackLength = blueStartAttackLength;
            blueAttack = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }

    private void BlueSkillAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        blueSkillLength -= Time.deltaTime;

        if (blueSkillLength < (blueStartSkillLength / 2) && blueSkillLength > (blueStartSkillLength / 3))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (90 - blueSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (blueSkillLength < (blueStartSkillLength / 3) && blueSkillLength > (blueStartSkillLength / 4))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (blueSkillRange + 2)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (blueSkillLength < (blueStartSkillLength / 4) && blueSkillLength > (blueStartSkillLength / 5))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (270 - blueSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (blueSkillLength <= 0)
        {
            blueSkillLength = blueStartSkillLength;
            blueSkill = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }
    private void YellowAttackAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + yellowAttackRange));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue - yellowAttackOffset,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        yellowAttackLength -= Time.deltaTime;

        if (yellowAttackLength < (yellowStartAttackLength / 2))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (360 - yellowAttackRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue + yellowAttackOffset,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (yellowAttackLength <= 0)
        {
            yellowAttackLength = yellowStartAttackLength;
            yellowAttack = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }

    private void YellowSkillAction()
    {
        if (startPosition)
        {
            startPosition = false;

            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        yellowSkillLength -= Time.deltaTime;

        if (yellowSkillLength < (yellowStartSkillLength / 2) && yellowSkillLength > (yellowStartSkillLength / 3))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (90 - yellowSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (yellowSkillLength < (yellowStartSkillLength / 3) && yellowSkillLength > (yellowStartSkillLength / 4))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (yellowSkillRange + 2)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (yellowSkillLength < (yellowStartSkillLength / 4) && yellowSkillLength > (yellowStartSkillLength / 5))
        {
            // Calculate Rotation
            Vector2 resultStartPos = InputSystem.Instance.MousePosWorldPoint(mainCamera, this.transform) - player.transform.position;

            this.transform.rotation = (Quaternion.Euler(0f, 0f,
                (Mathf.Atan2(resultStartPos.y, resultStartPos.x) * Mathf.Rad2Deg) + (270 - yellowSkillRange)));
            this.transform.Rotate(0, 0, -90);

            // Calculate Position
            Vector2 offsetStartPos = CalculateCoords(playerOffsetValue,
            this.transform.eulerAngles.z + 90, Vector3.zero);

            playerOffset = new Vector3(offsetStartPos.x, offsetStartPos.y, 0);

            transform.position = player.transform.position + playerOffset;
        }

        if (yellowSkillLength <= 0)
        {
            yellowSkillLength = yellowStartSkillLength;
            yellowSkill = false;
            followingPlayer = true;
            canAttack = true;
            canSkill = true;
        }
    }
}
