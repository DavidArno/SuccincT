using SuccincT.Options;
using System;

namespace SuccincT.Functional
{
    public struct Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;
        private readonly bool _isRight;
        private Option<TLeft> _tryLeft;
        private Option<TRight> _tryRight;

        public Either(TLeft left)
        {
            _left = left;
            _right = default(TRight);
            _isRight = false;
            _tryLeft = null;
            _tryRight = null;
        }

        public Either(TRight right)
        {
            _right = right;
            _left = default(TLeft);
            _isRight = true;
            _tryLeft = null;
            _tryRight = null;
        }

        public bool IsLeft => !_isRight;

        public TLeft Left => IsLeft ? _left : throw new InvalidOperationException("Doesn't contain a left value");

        public TRight Right => !IsLeft ? _right : throw new InvalidOperationException("Doesn't contain a right value");

        public Option<TLeft> TryLeft => _tryLeft ??
            (_tryLeft = IsLeft ? Option<TLeft>.Some(_left) : Option<TLeft>.None());

        public Option<TRight> TryRight => _tryRight ?? 
            (_tryRight = !IsLeft ? Option<TRight>.Some(_right) : Option<TRight>.None());

        public override bool Equals(object obj) => obj is Either<TLeft,TRight> && this == (Either<TLeft, TRight>)obj;

        public override int GetHashCode()
        {
            if (IsLeft && _left != null) return _left.GetHashCode();
            if (!IsLeft && _right != null) return _right.GetHashCode();
            return 0;
        }

        public static bool operator ==(Either<TLeft, TRight> x, Either<TLeft, TRight> y)
        {
            if (x.IsLeft != y.IsLeft) return false;
            if (x.IsLeft && x._left == null) return y._left == null;
            if (!x.IsLeft && x._right == null) return y._right == null;
            return x.IsLeft ? x._left.Equals(y._left) : x._right.Equals(y._right);
        }

        public static bool operator !=(Either<TLeft, TRight> x, Either<TLeft, TRight> y) => !(x == y);
    }
}
