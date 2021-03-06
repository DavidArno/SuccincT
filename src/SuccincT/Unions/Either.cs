﻿using SuccincT.Functional;
using System;
using SuccincT.Options;
using SuccincT.Unions.PatternMatchers;

namespace SuccincT.Unions
{
    public readonly struct Either<TLeft, TRight>
    {
        private readonly TLeft _left;
        private readonly TRight _right;
        private readonly bool _isRight;

        public Either(TLeft left)
        {
            _left = left;
            _right = default!;
            _isRight = false;
            TryLeft = left is {} validLeft ? Option<TLeft>.Some(validLeft) : Option<TLeft>.None();
            TryRight = Option<TRight>.None();
        }

        public Either(TRight right)
        {
            _right = right;
            _left = default!;
            _isRight = true;
            TryLeft = Option<TLeft>.None();
            TryRight = right is {} validRight ? Option<TRight>.Some(validRight) : Option<TRight>.None();
        }

        public bool IsLeft => !_isRight;

        public TLeft Left => IsLeft ? _left : throw new InvalidOperationException("Doesn't contain a left value");

        public TRight Right => _isRight ? _right : throw new InvalidOperationException("Doesn't contain a right value");

        public Option<TLeft> TryLeft { get; }

        public Option<TRight> TryRight { get; }

        public IEitherFuncPatternMatcher<TLeft, TRight, TResult> Match<TResult>()
            => new EitherPatternMatcher<TLeft, TRight, TResult>(this);

        public IEitherActionPatternMatcher<TLeft, TRight> Match()
            => new EitherPatternMatcher<TLeft, TRight, Unit>(this);

        public void Deconstruct(out EitherState state, out TLeft leftValue, out TRight rightValue)
            => (state, leftValue, rightValue) = (IsLeft ? EitherState.Left : EitherState.Right, _left, _right);

        public override bool Equals(object obj) => obj is Either<TLeft, TRight> either && EithersEqual(this, either);

        public override int GetHashCode()
            => IsLeft ? _left is {} left ? left.GetHashCode() : 0 : _right is {} right ? right.GetHashCode() : 0;

        public static bool operator ==(Either<TLeft, TRight> x, Either<TLeft, TRight> y) => EithersEqual(x, y);

        public static bool operator !=(Either<TLeft, TRight> x, Either<TLeft, TRight> y) => !EithersEqual(x, y);

        public static implicit operator Either<TLeft, TRight>(TLeft value) => new Either<TLeft, TRight>(value);
        public static implicit operator Either<TLeft, TRight>(TRight value) => new Either<TLeft, TRight>(value);

        private static bool EithersEqual(Either<TLeft, TRight> x, Either<TLeft, TRight> y)
        {
            if (x.IsLeft != y.IsLeft) return false;

            if (x.IsLeft) return x._left is {} left ? left.Equals(y._left) : y._left is null;

            return x._right is {} right ? right.Equals(y._right) : y._right is null;
        }
    }
}
