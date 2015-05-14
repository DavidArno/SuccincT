using System;
using System.Collections.Generic;
using System.Linq;

namespace SuccincT.PatternMatchers
{
    public class ExecMatcher<T>
    {
        private class CaseDetails
        {
            public List<T> Values { get; set; }
            public Action<T> Action { get; set; }
        }

        private readonly List<CaseDetails> _cases = new List<CaseDetails>();
        private readonly T _item;

        internal ExecMatcher(T item)
        {
            _item = item;
        }

        public ExecMatcher<T> Case(T value, Action action)
        {
            _cases.Add(new CaseDetails { Values = new List<T> { value }, Action = (x) => action() });
            return this;
        }

        public ExecMatcherWithElse<T> Else(Action action)
        {
            return new ExecMatcherWithElse<T>(this, action);
        }

        public ExecMatcherWithElse<T> Else(Action<T> action)
        {
            return new ExecMatcherWithElse<T>(this, action);
        }

        public MatchExpressionBuilder<T> With(T value)
        {
            return new MatchExpressionBuilder<T>(this, value);
        }

        public void Exec()
        {
            if (!MatchExpressionAndActionIfFound())
            {
                throw new NoMatchException<T>(_item);
            }
        }

        internal T Item { get { return _item; } }

        internal ExecMatcher<T> Case(List<T> values, Action action)
        {
            _cases.Add(new CaseDetails { Values = values, Action = (x) => action() });
            return this;
        }

        internal bool MatchExpressionAndActionIfFound()
        {
            var action = (from actionCase in _cases
                          where actionCase.Values.Any(value => EqualityComparer<T>.Default.Equals(_item, value))
                          select actionCase.Action).FirstOrDefault();

            if (action != null)
            {
                action(_item);
                return true;
            }
            return false;
        }
    }
}