using UnityEngine;
using System;

public class InputProcessor : MonoBehaviour
{


    public static bool POLL_SWIPE_INPUT;
    public static bool POLL_TAP_INPUT;
    public const float SWIPE_TIMER = 0.01f;
    public const float SWIPE_DISTANCE = 50f;

    void Start()
    {
        POLL_SWIPE_INPUT = false;
        POLL_TAP_INPUT = false;
    }

    void Update()
    {
        if (POLL_SWIPE_INPUT)
        {
            PollSwipeInput();
        }
        if (POLL_TAP_INPUT)
        {
            PollTapInput();
        }
    }

    private void PollSwipeInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            HandHeldSwipeInput();
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            DesktopSwipeInput();
        }
    }

    private void PollTapInput()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            HandHeldTapInput();
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            DesktopTapInput();
        }
    }

    #region Swipe Input
    private Vector2 initialTouch;
    private bool isSwipeInputStarted;
    private float swipeInputCounter = SWIPE_TIMER;
    private void DesktopSwipeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwipeInputStarted = true;
            swipeInputCounter = SWIPE_TIMER;
            initialTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (Input.GetMouseButton(0))
        {
            if (isSwipeInputStarted)
            {
                if (swipeInputCounter > 0)
                {
                    swipeInputCounter -= Time.deltaTime;
                }
                else if (Vector2.Distance(Input.mousePosition, initialTouch) > SWIPE_DISTANCE)
                {
                    isSwipeInputStarted = false;
                    swipeInputCounter = SWIPE_TIMER;
                    float swipeSpeed = 10;
                    float angle = (Input.mousePosition.y - initialTouch.y) / (Input.mousePosition.x - initialTouch.x);
                    SwipeDirection swipeDirection = SwipeDirection.NONE;
                    if (Mathf.Abs(angle) > 1)
                    {
                        if (Input.mousePosition.y - initialTouch.y < 0)
                        {
                            swipeDirection = SwipeDirection.DOWN;
                        }
                        else if (Input.mousePosition.y - initialTouch.y > 0)
                        {
                            swipeDirection = SwipeDirection.UP;
                        }
                    }
                    else
                    {
                        if (Input.mousePosition.x - initialTouch.x < 0)
                        {
                            swipeDirection = SwipeDirection.LEFT;
                        }
                        else if (Input.mousePosition.x - initialTouch.x > 0)
                        {
                            swipeDirection = SwipeDirection.RIGHT;
                        }
                    }
                    TriggerSwipeEvent(new SwipeEventArgs(new Vector2(Input.mousePosition.x, Input.mousePosition.y), swipeSpeed, initialTouch, swipeDirection));
                }
            }
        }
        else
        {
            isSwipeInputStarted = false;
            swipeInputCounter = SWIPE_TIMER;
        }
    }

    private void HandHeldSwipeInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isSwipeInputStarted = true;
                swipeInputCounter = SWIPE_TIMER;
                initialTouch = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                isSwipeInputStarted = false;
                swipeInputCounter = SWIPE_TIMER;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                if (isSwipeInputStarted)
                {
                    if (swipeInputCounter > 0)
                    {
                        swipeInputCounter -= Time.deltaTime;
                    }
                    else if (Vector2.Distance(Input.GetTouch(0).position, initialTouch) > SWIPE_DISTANCE)
                    {

                        isSwipeInputStarted = false;
                        swipeInputCounter = SWIPE_TIMER;
                        float swipeSpeed = Input.GetTouch(0).deltaPosition.magnitude;
                        float angle = (Input.GetTouch(0).position.y - initialTouch.y) / (Input.GetTouch(0).position.x - initialTouch.x);
                        SwipeDirection swipeDirection = SwipeDirection.NONE;
                        if(Mathf.Abs(angle) > 1)
                        {
                            if (Input.GetTouch(0).position.y - initialTouch.y < 0)
                            {
                                swipeDirection = SwipeDirection.DOWN;
                            }
                            else if (Input.GetTouch(0).position.y - initialTouch.y > 0)
                            {
                                swipeDirection = SwipeDirection.UP;
                            }
                        }
                        else
                        {
                            if (Input.GetTouch(0).position.x - initialTouch.x < 0)
                            {
                                swipeDirection = SwipeDirection.LEFT;
                            }
                            else if (Input.GetTouch(0).position.x - initialTouch.x > 0)
                            {
                                swipeDirection = SwipeDirection.RIGHT;
                            }
                        }
                        TriggerSwipeEvent(new SwipeEventArgs(Input.GetTouch(0).position, swipeSpeed, initialTouch, swipeDirection));
                    }
                }
            }
        }
    }
    #endregion


    #region Tap Input
    
    private void HandHeldTapInput()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TriggerTapEvent(new TapEventArgs(Input.GetTouch(0).position));
            }
        }
    }

    private void DesktopTapInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TriggerTapEvent(new TapEventArgs(Input.mousePosition));
        }
    }

    #endregion

    #region Input Events
    public static event EventHandler<SwipeEventArgs> SwipeEvent;
    public enum SwipeDirection { UP, DOWN, LEFT, RIGHT, NONE }
    public class SwipeEventArgs : EventArgs
    {

        public Vector2 endPoint;
        public float swipeSpeed;
        public Vector2 tapPoint;
        public SwipeDirection swipeDirection;
        public SwipeEventArgs(Vector2 endPoint, float swipeSpeed, Vector2 tapPoint, SwipeDirection swipeDirection)
        {
            this.endPoint = endPoint;
            this.swipeSpeed = swipeSpeed;
            this.tapPoint = tapPoint;
            this.swipeDirection = swipeDirection;
        }
    }

    private static void TriggerSwipeEvent(SwipeEventArgs args)
    {
        if (SwipeEvent != null)
        {
            SwipeEvent(null, args);
        }
    }


    public static event EventHandler<TapEventArgs> TapEvent;

    public class TapEventArgs : EventArgs
    {
        public Vector2 tapPoint;
        public TapEventArgs(Vector2 tapPoint)
        {
            this.tapPoint = tapPoint;
        }
    }

    private static void TriggerTapEvent(TapEventArgs eArgs)
    {
        if (TapEvent != null)
        {
            TapEvent(null, eArgs);
        }
    }

    #endregion
}
