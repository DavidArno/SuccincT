using SuccincT.Unions;

namespace SuccinctExamples
{
    public class Node
    {
        private readonly int _value;
        private readonly Union<Node, Tip> _left;
        private readonly Union<Node, Tip> _right;

        public Node(int value, Node left, Node right)
        {
            _value = value;
            _left = new Union<Node, Tip>(left);
            _right = new Union<Node, Tip>(right);
        }

        public Node(int value, Tip left, Node right)
        {
            _value = value;
            _left = new Union<Node, Tip>(left);
            _right = new Union<Node, Tip>(right);
        }

        public Node(int value, Node left, Tip right)
        {
            _value = value;
            _left = new Union<Node, Tip>(left);
            _right = new Union<Node, Tip>(right);
        }

        public Node(int value, Tip left, Tip right)
        {
            _value = value;
            _left = new Union<Node, Tip>(left);
            _right = new Union<Node, Tip>(right);
        }

        public int Value { get { return _value; } }
        public Union<Node, Tip> Left { get { return _left; } }
        public Union<Node, Tip> Right { get { return _right; } }
    }

    public class Tip { }

    public static class TreeUsingUnions
    {
        public static int SumTree(Union<Node, Tip> node)
        {
            return node.Match<int>().Case1().Do(x => x.Value + SumTree(x.Left) + SumTree(x.Right))
                                    .Case2().Do(0)
                                    .Result();
        }                                    
    }
}
