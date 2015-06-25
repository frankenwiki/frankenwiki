using System;
using System.Collections.Generic;

namespace Frankenwiki.Plumbing
{
    internal struct Maybe<T> : IEquatable<Maybe<T>>
    {
        public static readonly Maybe<T> Nothing = new Maybe<T>();

        private readonly T _value;
        private readonly bool _hasValue;

        public bool HasValue
        {
            get { return _hasValue; }
        }

        public T Value
        {
            get
            {
                if (!_hasValue)
                {
                    throw new InvalidOperationException("Value is not present");
                }
                return _value;
            }
        }

        public bool Equals(Maybe<T> other)
        {
            return EqualityComparer<T>.Default.Equals(_value, other._value) && _hasValue.Equals(other._hasValue);
        }

        public static implicit operator Maybe<T>(Maybe<Maybe<T>> doubleMaybe)
        {
            return doubleMaybe.HasValue ? doubleMaybe.Value : Nothing;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return value.ToMaybe();
        }

        internal Maybe(T value)
        {
            _hasValue = true;
            _value = value;
        }

        public Maybe<TProperty> GetMaybe<TProperty>(Func<T, TProperty> predicate)
        {
            return _hasValue ? predicate(_value) : Maybe<TProperty>.Nothing;
        }

        public TProperty? GetNullable<TProperty>(Func<T, TProperty> predicate) where TProperty : struct
        {
            return _hasValue ? predicate(_value) : default(TProperty?);
        }

        public TProperty Get<TProperty>(Func<T, TProperty> predicate, TProperty fallback)
        {
            return _hasValue ? predicate(_value) : fallback;
        }

        public T GetValueOrDefault(T fallback)
        {
            return _hasValue ? _value : fallback;
        }
    }

    internal static class MaybeExtensions
    {
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return object.ReferenceEquals(value, null) ? Maybe<T>.Nothing : new Maybe<T>(value);
        }

        public static Maybe<T> ToMaybe<T>(this T? value) where T : struct
        {
            return !value.HasValue ? Maybe<T>.Nothing : new Maybe<T>(value.Value);
        }
    }
}