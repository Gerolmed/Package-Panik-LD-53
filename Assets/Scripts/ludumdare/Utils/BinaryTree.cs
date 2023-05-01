namespace LudumDare.Utils {

    public class BTreeNode<T> {

        public (T, T, float) Value { get; }

        public BTreeNode<T> Left { get; private set; } = null;
        public BTreeNode<T> Right { get; private set; } = null;

        public BTreeNode((T, T, float) value) {
            Value = value;
        }

        public void Insert((T, T, float) value) {
            if (value.Item3 < Value.Item3) {
                if (Left == null)
                    Left = new BTreeNode<T>(value);
                else
                    Left.Insert(value);
            } else {
                if (Right == null)
                    Right = new BTreeNode<T>(value);
                else
                    Right.Insert(value);
            }
        }

        public BTreeNode<T> Remove((T, T, float) value) {
            if(Value.Equals(value))
                return null;

            if(Left != null && value.Item3 < Value.Item3) {
                Left = Left.Remove(value);
            } else if (Right != null && value.Item3 >= Value.Item3) {
                Right = Right.Remove(value);
            }

            return this;
        }

        // Return: Current, Smallest
        public (BTreeNode<T>, BTreeNode<T>) GetAndRemoveSmallest() {
            if(Left != null) {
                var (newLeft, Smallest) = Left.GetAndRemoveSmallest();
                Left = newLeft;
                return (this, Smallest);
            }

            return (Right, this);
        }

    }

}