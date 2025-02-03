using System;
using Taster.Gameplay;
using UnityEngine;

public class DishSelector : MonoBehaviour
{
    [SerializeField] Transform StandTransform;
    [SerializeField] float MoveSpeed;
    FoodGenerator foodGenerator;

    public int SelectID;

    public Action OnChangeDish;

    bool move;

    private void Awake() => ServiceLocator.Register(this);

    void Start()
    {
        foodGenerator = ServiceLocator.Get<FoodGenerator>();
    }

    public void SwitchStandLeft()
    {
        if (SelectID <= 0) return;

        SelectID--;
        move = true;
        OnChangeDish?.Invoke();
    }

    public void SwitchStandRight()
    {
        if (SelectID >= foodGenerator.StandsCount-1) return;

        SelectID++;
        move = true;
        OnChangeDish?.Invoke();
    }

    void Update()
    {
        if (!move) return;

        float needX = SelectID * -9;
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
