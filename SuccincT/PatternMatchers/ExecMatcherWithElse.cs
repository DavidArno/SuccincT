using System;

namespace SuccincT.PatternMatchers
{
    public class ExecMatcherWithElse<T>
    {
        private readonly ExecMatcher<T> _execMatcher;
        private readonly Action<T> _action;

        public ExecMatcherWithElse(ExecMatcher<T> execMatcher, Action<T> action)
        {
            _execMatcher = execMatcher;
            _action = action;
        }

        public ExecMatcherWithElse(ExecMatcher<T> execMatcher, Action action)
        {
            _execMatcher = execMatcher;
            _action = x => action();
        }

        public void Exec()
        {
            if (!_execMatcher.MatchExpressionAndActionIfFound())
            {
                _action(_execMatcher.Item);
            }
        }
    }
}