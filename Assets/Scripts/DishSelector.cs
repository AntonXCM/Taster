using System;
using Taster.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class DishSelector : MonoBehaviour
{
    [SerializeField] Transform StandTransform;
    [SerializeField] float MoveSpeed;
    [SerializeField] Button LeftArrow, RightArrow;
    FoodHolder foodHolder;

    public int SelectID;

    public Action OnChangeDish;

    bool move;

    private void Awake() => ServiceLocator.Register(this);

    void Start()
    {
        foodHolder = ServiceLocator.Get<FoodHolder>();
        Invoke("ChangeDish", 0);
    }

    public void SwitchStandLeft()
    {
        if (SelectID <= 0) return;

        SelectID--;
        ChangeDish();
    }

    public void SwitchStandRight()
    {
        if (SelectID >= foodHolder.StandsCount - 1) return;

        SelectID++;
        ChangeDish();
    }

    public void ChangeDish()
    {
        move = true;
        LeftArrow.interactable = SelectID > 0;
        RightArrow.interactable = SelectID < foodHolder.StandsCount - 1;
        OnChangeDish?.Invoke();
    }

    void Update()
    {
        if (!move) return;

        float needX = SelectID * -7;
        float localSpeed = Mathf.Abs(StandTransform.position.x - needX) + 2f;

        if (StandTransform.position.x < needX)
            StandTransform.position = StandTransform.position + Vector3.right * localSpeed * MoveSpeed * Time.deltaTime;
        else
            StandTransform.position = StandTransform.position + Vector3.left * localSpeed * MoveSpeed * Time.deltaTime;

        if (Mathf.Abs(StandTransform.position.x - needX) < 0.1)
        {
            move = false;
            StandTransform.position = new Vector3(needX, 0, 0);
        }
    }
}
