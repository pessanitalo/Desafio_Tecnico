namespace Desafio_Tecnico.Domain.ValueObjects
{
    public abstract class ValueObject
    {
        // Compara dois ValueObjects pelo valor, não pela referência
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject)obj;
            return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => new { x, y }.GetHashCode())
                .GetHashCode();
        }

        // Retorna os valores para comparação
        protected abstract IEnumerable<object> GetAtomicValues();

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
    }
}
