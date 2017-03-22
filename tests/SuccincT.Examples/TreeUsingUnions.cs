using SuccincT.Unions;

namespace SuccincT.Examples
{
    public class Node
    {
        public Node(int value, Node left, Node right)
        {
            Value = value;
            Left = new Union<Node, Tip>(left);
            Right = new Union<Node, Tip>(right);
        }

        public Node(int value, Tip left, Tip right)
        {
            Value = value;
            Left = new Union<Node, Tip>(left);
            Right = new Union<Node, Tip>(right);
        }

        public int Value { get; }
        public Union<Node, Tip> Left { get; }
        public Union<Node, Tip> Right { get; }
    }

    public class Tip { }

    public static class TreeUsingUnions
    {
        public static int SumTree(Union<Node, Tip> node) => 
            node.Match<int>()
                .Case1().Do(x => x.Value + SumTree(x.Left) + SumTree(x.Right))
                .Case2().Do(0)
                .Result();
    }
}
