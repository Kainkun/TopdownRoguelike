using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    delegate void Action();
    Player player;
    [SerializeField] GameObject swordTrail;
    [SerializeField] GameObject swing;
    [SerializeField] GameObject pierce;

    class ActionTreeNode
    {
        public ActionTreeNode(Action action)
        {
            this.action = action;
        }

        public Action action;
        public ActionTreeNode nextLeftActionNode;
        public ActionTreeNode nextRightActionNode;
    }

    ActionTreeNode startingActionTreeNode;
    ActionTreeNode currentActionTreeNode;

    void Start()
    {
        player = Player.instance;

        startingActionTreeNode = new ActionTreeNode(null);

        startingActionTreeNode.nextLeftActionNode = new ActionTreeNode(FirstSwing);
        startingActionTreeNode.nextLeftActionNode.nextLeftActionNode = new ActionTreeNode(SecondSwing);
        startingActionTreeNode.nextLeftActionNode.nextLeftActionNode.nextLeftActionNode = new ActionTreeNode(ThirdSwing);

        startingActionTreeNode.nextRightActionNode = new ActionTreeNode(FirstPierce);
        startingActionTreeNode.nextRightActionNode.nextRightActionNode = new ActionTreeNode(SecondPierce);
        startingActionTreeNode.nextRightActionNode.nextRightActionNode.nextRightActionNode = new ActionTreeNode(ThirdPierce);

        ResetActionCombo();
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

    void Swing(float speed = 1)
    {
        Attack tempAttack = Instantiate(swing, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.5f;
        player.weaponAnimator.SetTrigger("Swing");
        player.weaponAnimator.speed = speed;
    }
    void FirstSwing()
    {
        //print("ching!");
        Swing();
    }

    void SecondSwing()
    {
        //print("chang!!");
        Swing();
    }

    void ThirdSwing()
    {
        //print("pang!!!");
        Swing(2);
    }

    [SerializeField] float pierceSpeed = 10;
    void Pierce(float speed = 1)
    {
        Attack tempAttack = Instantiate(pierce, transform.position, transform.rotation).GetComponent<Attack>();
        tempAttack.duration = 0.2f;
        player.weaponAnimator.SetTrigger("Pierce");
        player.weaponAnimator.speed = speed;
        // var tempTrail = Instantiate(swordTrail).transform;
        // tempTrail.position = transform.position;
        // tempTrail.right = player.lookDirection;
        // tempTrail.GetComponent<Projectile>().movementVector = player.lookDirection * speed;
        // tempTrail.GetComponent<Projectile>().maxDistance = maxDistance;
    }
    void FirstPierce()
    {
        //print("stab!");
        Pierce();
    }

    void SecondPierce()
    {
        //print("Stab!!");
        Pierce();
    }

    void ThirdPierce()
    {
        //print("STAB!!!");
        Pierce(2);
    }
}
