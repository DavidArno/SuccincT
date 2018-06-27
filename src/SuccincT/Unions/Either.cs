using System;
using SuccincT.Options;

namespace SuccincT.Unions
{
    public readonly struct Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;

        public Either(TLeft left) : this()
        {
            TryLeft = left == null ? Option<TLeft>.None() : Option<TLeft>.Some(left);
            TryRight = Option<TRight>.None();
            _left = left;
            IsLeft = true;
        }

        public Either(TRight right) : this()
        {
            TryLeft = Option<TLeft>.None();
            TryRight = right == null ? Option<TRight>.None() : Option<TRight>.Some(right);
            _right = right;
        }

        public bool IsLeft { get; }

        public TLeft Left => IsLeft ? _left : throw new InvalidOperationException("Doesn't contain a left value");

        public TRight Right => !IsLeft ? _right :throw new InvalidOperationException("Doesn't contain a right value");

        public Option<TLeft> TryLeft { get; }
        public Option<TRight> TryRight { get; }

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

        public static implicit operator Either<TLeft, TRight>(TLeft value) => new Either<TLeft, TRight>(value);
        public static implicit operator Either<TLeft, TRight>(TRight value) => new Either<TLeft, TRight>(value);
    }
}
