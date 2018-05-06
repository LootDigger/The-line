using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    #region Variables

    const float SPEED = 15f;
    const float DISPLACEMENT_X = 400f;
    const float FLICKER_RATE = 0.2f;
    const float BALL_FLICKER_RATE = 0.1f;
    const float BALL_SKALE_CHANGING_TIME = 0.3f;
    const float MAX_BALL_SCALE = 3f;
    const float MIN_BALL_SCALE = 0.5f;
    const float MOUSE_ACTIVITY_MAXY_COORD = 240f;


    [SerializeField] private float showDeathMenuDelayTime;
    [SerializeField] private float disableBoosterDelayTime;
    [SerializeField] private float restartLvlDelayTime;
    [SerializeField] private float backToMortableTime;


    private Vector3 position;
    private Vector3 normalScale;
    private Vector3 maxScale;
    private Vector3 minScale;
    private Color endColor;
    private Color startColor;
    private Color tileFlickerColor;
    private bool isIMmortal;
    private bool isReadyToJump;
    #endregion



    #region Properties

    public bool IsInJump
    {
        get;
        set;
    }


    public bool IsIMmortalProperty
    {
        get
        {
            return isIMmortal;
        }
        set
        {
            isIMmortal = value;
        }

    }


    public bool IsReadyToJumpProperty
    {
        get
        {
            return isReadyToJump;
        }
        set
        {
            isReadyToJump = value;
        }
    }

    #endregion



    #region Unity lifecycle

    private void Start()
    {
        normalScale = new Vector3(1, 1, 1);
        maxScale = new Vector3(MAX_BALL_SCALE, MAX_BALL_SCALE, MAX_BALL_SCALE);
        minScale = new Vector3(MIN_BALL_SCALE, MIN_BALL_SCALE, MIN_BALL_SCALE);
        position = new Vector3(-44, 300, -5);
        isIMmortal = false;
        isReadyToJump = false;
        IsInJump = false;
        endColor = Color.red;
        startColor = Color.white;
        tileFlickerColor = Color.yellow;
        showDeathMenuDelayTime = 2f;
        disableBoosterDelayTime = 5f;
        restartLvlDelayTime = 1.5f;
        backToMortableTime = 1f;
    }


    private void Update()
    {
        BallControll();        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("tile") && !isIMmortal)
        {
            collision.gameObject.GetComponent<TweenColor>().style = Style.PingPong;
            TweenColor.SetColor(collision.gameObject, tileFlickerColor, FLICKER_RATE);
            ScoreController.instance.StopAllCoroutines();
            TileSpawner.Instance.StopAllCoroutines();
            StopAllCoroutines();
            GameConditionsController.Instance.IsAlive = false;
            Scheduler.Instance.CallMethodWithDelay(this.gameObject, ShowDeathMenu, showDeathMenuDelayTime);
        }

        if (collision.transform.CompareTag("tile") && isIMmortal && !IsInJump)
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.transform.CompareTag("Booster"))
        {
            ScoreController.instance.SetTimerStarted();
            Scheduler.Instance.CallMethodWithDelay(this.gameObject, ScoreController.instance.DiactivateTimer, disableBoosterDelayTime);
            if(collision.gameObject.GetComponent<Booster>().IsImmortableBoosterProperty)
            {                
                collision.gameObject.SetActive(false);
                isIMmortal = true;
                Scheduler.Instance.CallMethodWithDelay(this, SetMortalable, disableBoosterDelayTime);
                flicker();
            }
            else
            {
                collision.gameObject.SetActive(false);
                ReduceSize();
                Scheduler.Instance.CallMethodWithDelay(this.gameObject, WaitForSetNormalScale, disableBoosterDelayTime);
            }
        }

    }

    #endregion



    #region Public methods

    public void flicker()
    {
        GetComponent<TweenColor>().style = Style.Loop;
        TweenColor.SetColor(this.gameObject, endColor, BALL_FLICKER_RATE);
    }


    public void ReduceSize()
    {
        TweenScale.SetScale(this.gameObject, minScale, BALL_SKALE_CHANGING_TIME);
    }

    #endregion



    #region Private methods

    void BallControll()
    {
        if (GameConditionsController.Instance.IsStartedProperty && GameConditionsController.Instance.IsAlive)
        {
            if (Input.mousePosition.y <= MOUSE_ACTIVITY_MAXY_COORD)
            {
                transform.position = position;
                position.x = Input.mousePosition.x;
                position.x -= DISPLACEMENT_X;
                transform.position = position;

                
            }
            if (isReadyToJump && Input.GetKey(KeyCode.Space))
            {
                IsInJump = true;
                isIMmortal = true;
                ChangeScale();
            }
        }
    }
    

    void ShowDeathMenu()
    {
        GameConditionsController.Instance.ShowDeathMenu();        
        Scheduler.Instance.CallMethodWithDelay(this.gameObject, TileSpawner.Instance.RestartLvl, restartLvlDelayTime);       
    }


    void ChangeScale()
    {
        TweenScale.SetScale(this.gameObject, maxScale, BALL_SKALE_CHANGING_TIME);
        Scheduler.Instance.CallMethodWithDelay(this, SetMortalable, 1f);       
        isReadyToJump = false;        
    }
    

    void SetMortalable()
    {
        isIMmortal = false;
        IsInJump = false;
        TweenScale.SetScale(this.gameObject, normalScale, BALL_SKALE_CHANGING_TIME);   
        TweenColor.SetColor(this.gameObject, startColor, 0);
        GetComponent<TweenColor>().style = Style.Once;
    }


    void WaitForSetNormalScale()
    {
        TweenScale.SetScale(this.gameObject, normalScale, BALL_SKALE_CHANGING_TIME);
    }

    #endregion

}
