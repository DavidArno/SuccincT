using System;

namespace SuccincT.PatternMatchers
{
    internal class TypeMatcherCaseHandler<T, TCaseType, TResult> :
        ITypeMatcherCaseHandler<ITypeMatcher<T, TResult>, TCaseType, TResult>,
        IFuncWhereHandler<ITypeMatcher<T, TResult>, TCaseType, TResult> where TCaseType : T
    {
        private readonly ITypeMatcher<T, TResult> _typeMatcher;
        private readonly Action<TypeMatcherCaseOfData<T, TResult>> _dataRecorder;
        private Func<T, bool> _whereExpression = x => true;

        public TypeMatcherCaseHandler(TypeMatcher<T, TResult> typeMatcher,
                                      Action<TypeMatcherCaseOfData<T, TResult>> dataRecorder)
        {
            _typeMatcher = typeMatcher;
            _dataRecorder = dataRecorder;
        }

        public IFuncWhereHandler<ITypeMatcher<T, TResult>, TCaseType, TResult> Where(Func<TCaseType, bool> expression)
        {
            _whereExpression = x => expression((TCaseType)x!);
            return this;
        }

        public ITypeMatcher<T, TResult> Do(TResult value)
        {
            _dataRecorder(new TypeMatcherCaseOfData<T, TResult>(typeof(TCaseType),
                                                                _whereExpression,
                                                                _ => value));
            return _typeMatcher;
        }

        public ITypeMatcher<T, TResult> Do(Func<TCaseType, TResult> func)
        {
            _dataRecorder(new TypeMatcherCaseOfData<T, TResult>(typeof(TCaseType),
                                                                _whereExpression,
                                                                x => func((TCaseType)x!)));
            return _typeMatcher;
        }
    }
}