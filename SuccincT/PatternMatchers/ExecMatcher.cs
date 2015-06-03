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

        internal ExecMatcher(T item)
        {
            Item = item;
        }

        public ExecMatcher<T> Case(T value, Action action)
        {
            _cases.Add(new CaseDetails { Values = new List<T> { value }, Action = x => action() });
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
                throw new NoMatchException($"Match rules did not include a match for {Item}");
            }
        }

        internal T Item { get; }

        internal ExecMatcher<T> Case(List<T> values, Action action)
        {
            _cases.Add(new CaseDetails { Values = values, Action = x => action() });
            return this;
        }

        internal bool MatchExpressionAndActionIfFound()
        {
            var action = (from actionCase in _cases
                          where actionCase.Values.Any(value => EqualityComparer<T>.Default.Equals(Item, value))
                          select actionCase.Action).FirstOrDefault();

            if (action != null)
            {
                action(Item);
                return true;
            }
            return false;
        }
    }
}