using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    public delegate void Action();
    Player player;
    [SerializeField] GameObject swordTrail;
    [SerializeField] GameObject swing;
    [SerializeField] GameObject pierce;
    [SerializeField] GameObject bigPierce;
    [SerializeField] GameObject pushBack;

    public class ActionTreeNode
    {
        public ActionTreeNode(Action action)
        {
            this.action = action;
        }

        public Action action;
        public ActionTreeNode nextLeftActionNode;
        public ActionTreeNode nextRightActionNode;
    }

    public ActionTreeNode startingActionTreeNode;
    ActionTreeNode currentActionTreeNode;

    public static ActionSystem instance;
    private void Awake()
    {
        instance = this;
        player = Player.instance;

        startingActionTreeNode = new ActionTreeNode(null);

        startingActionTreeNode.nextLeftActionNode = new ActionTreeNode(Swing);
        var L = startingActionTreeNode.nextLeftActionNode;
        L.nextLeftActionNode = new ActionTreeNode(Swing);
        var LL = L.nextLeftActionNode;
        LL.nextLeftActionNode = new ActionTreeNode(Swing);
        LL.nextRightActionNode = new ActionTreeNode(PushBack);

        startingActionTreeNode.nextRightActionNode = new ActionTreeNode(Pierce);
        var R = startingActionTreeNode.nextRightActionNode;
        R.nextRightActionNode = new ActionTreeNode(Pierce);
        var RR = R.nextRightActionNode;
        RR.nextRightActionNode = new ActionTreeNode(BigPierce);

        ResetActionCombo();
    }

    void Start()
    {

    }

    public void LeftAction()
    {
        if (currentActionTreeNode.nextLeftActionNode == null)
            ResetActionCombo();

        currentActionTreeNode = currentActionTreeNode.nextLeftActionNode;
        currentActionTreeNode.action.Invoke();
    }

    public void RightAction()
    {
        if (currentActionTreeNode.nextRightActionNode == null)
            ResetActionCombo();

        currentActionTreeNode = currentActionTreeNode.nextRightActionNode;
        currentActionTreeNode.action.Invoke();
    }

    public void ResetActionCombo()
    {
        currentActionTreeNode = startingActionTreeNode;
    }

    public void Swing()
    {
        Attack tempAttack = Instantiate(swing, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.5f;
        tempAttack.movement = player.transform.up * 2;
        player.weaponAnimator.SetTrigger("Swing");
    }

    public void Pierce()
    {
        Attack tempAttack = Instantiate(pierce, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.2f;
        tempAttack.movement = player.transform.up * 2;
        player.weaponAnimator.SetTrigger("Pierce");
    }

    public void BigPierce()
    {
        Attack tempAttack = Instantiate(bigPierce, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.5f;
        tempAttack.movement = player.transform.up * 2;
        player.weaponAnimator.SetTrigger("Pierce");
    }

    public void PushBack()
    {
        Attack tempAttack = Instantiate(pushBack, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.5f;
        tempAttack.movement = player.transform.up * 2;
        player.weaponAnimator.SetTrigger("PushBack");
    }
}
