using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionTreeUI : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject line;
    [SerializeField] Transform treeRoot;
    [SerializeField] Sprite[] sprites;

    Dictionary<ActionSystem.Action, Sprite> spriteDict = new Dictionary<ActionSystem.Action, Sprite>();

    class ActionTreeNodeUI
    {
        public ActionTreeNodeUI()
        {

        }

        public ActionTreeNodeUI nextLeftActionNode;
        public ActionTreeNodeUI nextRightActionNode;
    }

    private void Awake()
    {
        spriteDict[ActionSystem.instance.Swing] = sprites[0];
        spriteDict[ActionSystem.instance.Pierce] = sprites[1];
        spriteDict[ActionSystem.instance.BigPierce] = sprites[2];
        spriteDict[ActionSystem.instance.PushBack] = sprites[3];
    }

    void Start()
    {
        PlaceIcons(Player.instance.actionSystem.startingActionTreeNode, treeRoot);
    }

    void PlaceIcons(ActionSystem.ActionTreeNode node, Transform parent)
    {
        if (node.nextLeftActionNode != null)
        {
            GameObject leftNode = Instantiate(image, parent);
            leftNode.GetComponent<Image>().sprite = spriteDict[node.nextLeftActionNode.action];
            leftNode.transform.localPosition = new Vector3(-Mathf.Abs(parent.transform.localPosition.x / 2), 300);

            if (parent.name != "Root")
            {
                GameObject leftLine = Instantiate(line, transform);
                leftLine.GetComponent<LineRenderer>().SetPosition(0, parent.position);
                leftLine.GetComponent<LineRenderer>().SetPosition(1, leftNode.transform.position);
            }

            PlaceIcons(node.nextLeftActionNode, leftNode.transform);
        }

        if (node.nextRightActionNode != null)
        {
            GameObject rightNode = Instantiate(image, parent);
            rightNode.GetComponent<Image>().sprite = spriteDict[node.nextRightActionNode.action];
            rightNode.transform.localPosition = new Vector3(Mathf.Abs(parent.transform.localPosition.x / 2), 300);

            if (parent.name != "Root")
            {
                GameObject rightLine = Instantiate(line, transform);
                rightLine.GetComponent<LineRenderer>().SetPosition(0, parent.position);
                rightLine.GetComponent<LineRenderer>().SetPosition(1, rightNode.transform.position);
            }

            PlaceIcons(node.nextRightActionNode, rightNode.transform);
        }
    }
}
