using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    delegate void Action();
    Player player;
    [SerializeField] GameObject swordTrail;

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

    void Swing()
    {
        player.weaponAnimator.SetTrigger("Swing");
    }
    void FirstSwing()
    {
        player.sword.damage = 1;
        print("ching!");
        Swing();
    }

    void SecondSwing()
    {
        player.sword.damage = 2;
        print("chang!!");
        Swing();
    }

    void ThirdSwing()
    {
        player.sword.damage = 3;
        print("pang!!!");
        Swing();
    }

    [SerializeField] float pierceSpeed = 10;
    void Pierce(float speed, float maxDistance)
    {
        player.weaponAnimator.SetTrigger("Pierce");
        // var tempTrail = Instantiate(swordTrail).transform;
        // tempTrail.position = transform.position;
        // tempTrail.right = player.lookDirection;
        // tempTrail.GetComponent<Projectile>().movementVector = player.lookDirection * speed;
        // tempTrail.GetComponent<Projectile>().maxDistance = maxDistance;
    }
    void FirstPierce()
    {
        player.sword.damage = 1;
        print("stab!");
        Pierce(pierceSpeed, 3);
    }

    void SecondPierce()
    {
        player.sword.damage = 2;
        print("Stab!!");
        Pierce(pierceSpeed * 1.5f, 4);
    }

    void ThirdPierce()
    {
        player.sword.damage = 3;
        print("STAB!!!");
        Pierce(pierceSpeed * 5, 5);
    }
}
