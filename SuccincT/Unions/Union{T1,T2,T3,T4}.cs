namespace SuccincT.Unions
{
    public class Union<T1, T2, T3, T4>
    {
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;
        private readonly T4 _value4;

        public Union(T1 value)
        {
            _value1 = value;
            Case = Variant.Case1;
        }

        public Union(T2 value)
        {
            _value2 = value;
            Case = Variant.Case2;
        }

        public Union(T3 value)
        {
            _value3 = value;
            Case = Variant.Case3;
        }

        public Union(T4 value)
        {
            _value4 = value;
            Case = Variant.Case4;
        }

        public Variant Case { get; }

        public T1 Case1
        {
            get
            {
                if (Case == Variant.Case1) { return _value1; }
                throw new InvalidCaseException(Variant.Case1, Case);
            }
        }

        public T2 Case2
        {
            get
            {
                if (Case == Variant.Case2) { return _value2; }
                throw new InvalidCaseException(Variant.Case2, Case);
            }
        }

        public T3 Case3
        {
            get
            {
                if (Case == Variant.Case3) { return _value3; }
                throw new InvalidCaseException(Variant.Case3, Case);
            }
        }

        public T4 Case4
        {
            get
            {
                if (Case == Variant.Case4) { return _value4; }
                throw new InvalidCaseException(Variant.Case4, Case);
            }
        }
    }
}