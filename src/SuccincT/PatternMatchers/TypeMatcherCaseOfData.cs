using System;

namespace SuccincT.PatternMatchers
{
    public sealed class TypeMatcherCaseOfData<T, TResult>
    {
        public TypeMatcherCaseOfData(Type caseType, Func<T, bool> whereExpression, Func<T, TResult> doFunc)
        {
            CaseType = caseType;
            WhereExpression = whereExpression;
            DoFunc = doFunc;
        }

        public Type CaseType { get; }
        public Func<T, bool> WhereExpression { get; }
        public Func<T, TResult> DoFunc { get; }
    }
}