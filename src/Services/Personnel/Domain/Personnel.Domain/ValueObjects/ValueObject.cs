namespace Personnel.Domain.ValueObjects
{
    /// <summary>
    /// Представляет собой абстрактный базовый класс для объектов значений.
    /// Объекты значений являются неизменяемыми и сравниваются на основе значений их свойств, а не ссылок.
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Определяет, равен ли указанный объект текущему экземпляру ValueObject.
        /// </summary>
        /// <param name="obj">Объект для сравнения с текущим экземпляром ValueObject.</param>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Служит хэш-функцией по умолчанию для класса ValueObject.
        /// </summary>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    return HashCode.Combine(current, obj != null ? obj.GetHashCode() : 0);
                });
        }

        /// <summary>
        /// Определяет, равны ли два указанных экземпляра ValueObject.
        /// </summary>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Определяет, не равны ли два указанных экземпляра ValueObject.
        /// </summary>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }
    }
}