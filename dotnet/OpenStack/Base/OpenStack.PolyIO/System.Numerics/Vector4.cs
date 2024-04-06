﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
    /// <summary>Represents a vector with four single-precision floating-point values.</summary>
    public partial struct Vector4<T> : IEquatable<Vector4<T>>, IFormattable where T : IComparable<T>, IEquatable<T>
    {
        /// <summary>The X component of the vector.</summary>
        public T X;

        /// <summary>The Y component of the vector.</summary>
        public T Y;

        /// <summary>The Z component of the vector.</summary>
        public T Z;

        /// <summary>The W component of the vector.</summary>
        public T W;

        internal const int Count = 4;

        public static readonly Dictionary<char, Func<T, T, T>> Ops = new Dictionary<char, Func<T, T, T>>();

        /// <summary>Creates a new <see cref="System.Numerics.Vector4<T>" /> object whose four elements have the same value.</summary>
        /// <param name="value">The value to assign to all four elements.</param>
        public Vector4(T value) : this(value, value, value, value) { }

        /// <summary>Creates a   new <see cref="System.Numerics.Vector4<T>" /> object from the specified <see cref="System.Numerics.Vector2<T>" /> object and a Z and a W component.</summary>
        /// <param name="value">The vector to use for the X and Y components.</param>
        /// <param name="z">The Z component.</param>
        /// <param name="w">The W component.</param>
        public Vector4(Vector2<T> value, T z, T w) : this(value.X, value.Y, z, w) { }

        /// <summary>Constructs a new <see cref="System.Numerics.Vector4<T>" /> object from the specified <see cref="System.Numerics.Vector3<T>" /> object and a W component.</summary>
        /// <param name="value">The vector to use for the X, Y, and Z components.</param>
        /// <param name="w">The W component.</param>
        public Vector4(Vector3<T> value, T w) : this(value.X, value.Y, value.Z, w) { }

        /// <summary>Creates a vector whose elements have the specified values.</summary>
        /// <param name="x">The value to assign to the <see cref="System.Numerics.Vector4<T>.X" /> field.</param>
        /// <param name="y">The value to assign to the <see cref="System.Numerics.Vector4<T>.Y" /> field.</param>
        /// <param name="z">The value to assign to the <see cref="System.Numerics.Vector4<T>.Z" /> field.</param>
        /// <param name="w">The value to assign to the <see cref="System.Numerics.Vector4<T>.W" /> field.</param>
        public Vector4(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>Constructs a vector from the given <see cref="ReadOnlySpan{Single}" />. The span must contain at least 4 elements.</summary>
        /// <param name="values">The span of elements to assign to the vector.</param>
        public Vector4(ReadOnlySpan<T> values)
        {
            if (values.Length < 4) throw new ArgumentOutOfRangeException(nameof(values));

            this = Unsafe.ReadUnaligned<Vector4<T>>(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(values)));
        }

        /// <summary>Gets a vector whose 4 elements are equal to zero.</summary>
        /// <value>A vector whose four elements are equal to zero (that is, it returns the vector <c>(0,0,0,0)</c>.</value>
        public static Vector4<T> Zero
        {
            get => default;
        }

        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The index of the element to get or set.</param>
        /// <returns>The the element at <paramref name="index" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
        public T this[int index]
        {
            get => GetElement(this, index);
            set => this = WithElement(this, index, value);
        }

        /// <summary>Gets the element at the specified index.</summary>
        /// <param name="vector">The vector of the element to get.</param>
        /// <param name="index">The index of the element to get.</param>
        /// <returns>The value of the element at <paramref name="index" />.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
        internal static T GetElement(Vector4<T> vector, int index)
        {
            if ((uint)index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

            return GetElementUnsafe(ref vector, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static T GetElementUnsafe(ref Vector4<T> vector, int index)
        {
            Debug.Assert(index >= 0 && index < Count);
            return Unsafe.Add(ref Unsafe.As<Vector4<T>, T>(ref vector), index);
        }

        /// <summary>Sets the element at the specified index.</summary>
        /// <param name="vector">The vector of the element to get.</param>
        /// <param name="index">The index of the element to set.</param>
        /// <param name="value">The value of the element to set.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index" /> was less than zero or greater than the number of elements.</exception>
        internal static Vector4<T> WithElement(Vector4<T> vector, int index, T value)
        {
            if ((uint)index >= Count) throw new ArgumentOutOfRangeException(nameof(index));

            Vector4<T> result = vector;
            SetElementUnsafe(ref result, index, value);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetElementUnsafe(ref Vector4<T> vector, int index, T value)
        {
            Debug.Assert(index >= 0 && index < Count);
            Unsafe.Add(ref Unsafe.As<Vector4<T>, T>(ref vector), index) = value;
        }

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The summed vector.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Addition" /> method defines the addition operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator +(Vector4<T> left, Vector4<T> right)
        {
            return new Vector4<T>(
                Ops['+'](left.X, right.X),
                Ops['+'](left.Y, right.Y),
                Ops['+'](left.Z, right.Z),
                Ops['+'](left.Z, right.W)
            );
        }

        /// <summary>Divides the first vector by the second.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector that results from dividing <paramref name="left" /> by <paramref name="right" />.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Division" /> method defines the division operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator /(Vector4<T> left, Vector4<T> right)
        {
            return new Vector4<T>(
                Ops['/'](left.X, right.X),
                Ops['/'](left.Y, right.Y),
                Ops['/'](left.Z, right.Z),
                Ops['/'](left.Z, right.W)
            );
        }

        /// <summary>Divides the specified vector by a specified scalar value.</summary>
        /// <param name="value1">The vector.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Division" /> method defines the division operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator /(Vector4<T> value1, T value2)
        {
            return value1 / new Vector4<T>(value2);
        }

        /// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns><see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
        /// <remarks>Two <see cref="System.Numerics.Vector4<T>" /> objects are equal if each element in <paramref name="left" /> is equal to the corresponding element in <paramref name="right" />.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector4<T> left, Vector4<T> right)
        {
            return (left.X.Equals(right.X))
                && (left.Y.Equals(right.Y))
                && (left.Z.Equals(right.Z))
                && (left.W.Equals(right.W));
        }

        /// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns><see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector4<T> left, Vector4<T> right)
        {
            return !(left == right);
        }

        /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The element-wise product vector.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Multiply" /> method defines the multiplication operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator *(Vector4<T> left, Vector4<T> right)
        {
            return new Vector4<T>(
                Ops['*'](left.X, right.X),
                Ops['*'](left.Y, right.Y),
                Ops['*'](left.Z, right.Z),
                Ops['*'](left.Z, right.W)
            );
        }

        /// <summary>Multiplies the specified vector by the specified scalar value.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Multiply" /> method defines the multiplication operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator *(Vector4<T> left, T right)
        {
            return left * new Vector4<T>(right);
        }

        /// <summary>Multiplies the scalar value by the specified vector.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Multiply" /> method defines the multiplication operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator *(T left, Vector4<T> right)
        {
            return right * left;
        }

        /// <summary>Subtracts the second vector from the first.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector that results from subtracting <paramref name="right" /> from <paramref name="left" />.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_Subtraction" /> method defines the subtraction operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator -(Vector4<T> left, Vector4<T> right)
        {
            return new Vector4<T>(
                Ops['-'](left.X, right.X),
                Ops['-'](left.Y, right.Y),
                Ops['-'](left.Z, right.Z),
                Ops['-'](left.Z, right.W)
            );
        }

        /// <summary>Negates the specified vector.</summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>The negated vector.</returns>
        /// <remarks>The <see cref="System.Numerics.Vector4<T>.op_UnaryNegation" /> method defines the unary negation operation for <see cref="System.Numerics.Vector4<T>" /> objects.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> operator -(Vector4<T> value)
        {
            return Zero - value;
        }

        /// <summary>Adds two vectors together.</summary>
        /// <param name="left">The first vector to add.</param>
        /// <param name="right">The second vector to add.</param>
        /// <returns>The summed vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Add(Vector4<T> left, Vector4<T> right)
        {
            return left + right;
        }

        /// <summary>Restricts a vector between a minimum and a maximum value.</summary>
        /// <param name="value1">The vector to restrict.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Clamp(Vector4<T> value1, Vector4<T> min, Vector4<T> max)
        {
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            return Min(Max(value1, min), max);
        }

        /// <summary>Divides the first vector by the second.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Divide(Vector4<T> left, Vector4<T> right)
        {
            return left / right;
        }

        /// <summary>Divides the specified vector by a specified scalar value.</summary>
        /// <param name="left">The vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The vector that results from the division.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Divide(Vector4<T> left, T divisor)
        {
            return left / divisor;
        }

        /// <summary>Returns the dot product of two vectors.</summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Dot(Vector4<T> vector1, Vector4<T> vector2)
        {
            return Ops['+'](Ops['+'](Ops['+'](
                Ops['*'](vector1.X, vector2.X),
                Ops['*'](vector1.Y, vector2.Y)),
                Ops['*'](vector1.Z, vector2.Z)),
                Ops['*'](vector1.W, vector2.W));
        }

        /// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The maximized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Max(Vector4<T> value1, Vector4<T> value2)
        {
            return new Vector4<T>(
                (value1.X.CompareTo(value2.X) > 0) ? value1.X : value2.X,
                (value1.Y.CompareTo(value2.Y) > 0) ? value1.Y : value2.Y,
                (value1.Z.CompareTo(value2.Z) > 0) ? value1.Z : value2.Z,
                (value1.W.CompareTo(value2.W) > 0) ? value1.W : value2.W
            );
        }

        /// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The minimized vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Min(Vector4<T> value1, Vector4<T> value2)
        {
            return new Vector4<T>(
                (value1.X.CompareTo(value2.X) < 0) ? value1.X : value2.X,
                (value1.Y.CompareTo(value2.Y) < 0) ? value1.Y : value2.Y,
                (value1.Z.CompareTo(value2.Z) < 0) ? value1.Z : value2.Z,
                (value1.W.CompareTo(value2.W) < 0) ? value1.W : value2.W
            );
        }

        /// <summary>Returns a new vector whose values are the product of each pair of elements in two specified vectors.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The element-wise product vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Multiply(Vector4<T> left, Vector4<T> right)
        {
            return left * right;
        }

        /// <summary>Multiplies a vector by a specified scalar.</summary>
        /// <param name="left">The vector to multiply.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Multiply(Vector4<T> left, T right)
        {
            return left * right;
        }

        /// <summary>Multiplies a scalar value by a specified vector.</summary>
        /// <param name="left">The scaled value.</param>
        /// <param name="right">The vector.</param>
        /// <returns>The scaled vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Multiply(T left, Vector4<T> right)
        {
            return left * right;
        }

        /// <summary>Negates a specified vector.</summary>
        /// <param name="value">The vector to negate.</param>
        /// <returns>The negated vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Negate(Vector4<T> value)
        {
            return -value;
        }

        /// <summary>Subtracts the second vector from the first.</summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>The difference vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4<T> Subtract(Vector4<T> left, Vector4<T> right)
        {
            return left - right;
        }

        /// <summary>Copies the elements of the vector to a specified array.</summary>
        /// <param name="array">The destination array.</param>
        /// <remarks><paramref name="array" /> must have at least four elements. The method copies the vector's elements starting at index 0.</remarks>
        /// <exception cref="System.NullReferenceException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
        /// <exception cref="System.RankException"><paramref name="array" /> is multidimensional.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(T[] array)
        {
            // We explicitly don't check for `null` because historically this has thrown `NullReferenceException` for perf reasons
            if (array.Length < Count) throw new ArgumentException("DestinationTooShort");

            Unsafe.WriteUnaligned(ref Unsafe.As<T, byte>(ref array[0]), this);
        }

        /// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
        /// <param name="array">The destination array.</param>
        /// <param name="index">The index at which to copy the first element of the vector.</param>
        /// <remarks><paramref name="array" /> must have a sufficient number of elements to accommodate the four vector elements. In other words, elements <paramref name="index" /> through <paramref name="index" /> + 3 must already exist in <paramref name="array" />.</remarks>
        /// <exception cref="System.NullReferenceException"><paramref name="array" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index" /> is less than zero.
        /// -or-
        /// <paramref name="index" /> is greater than or equal to the array length.</exception>
        /// <exception cref="System.RankException"><paramref name="array" /> is multidimensional.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(T[] array, int index)
        {
            // We explicitly don't check for `null` because historically this has thrown `NullReferenceException` for perf reasons
            if ((uint)index >= (uint)array.Length) throw new ArgumentOutOfRangeException("IndexMustBeLess");
            if ((array.Length - index) < Count) throw new ArgumentException("DestinationTooShort");

            Unsafe.WriteUnaligned(ref Unsafe.As<T, byte>(ref array[index]), this);
        }

        /// <summary>Copies the vector to the given <see cref="Span{T}" />. The length of the destination span must be at least 4.</summary>
        /// <param name="destination">The destination span which the values are copied into.</param>
        /// <exception cref="System.ArgumentException">If number of elements in source vector is greater than those available in destination span.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Span<T> destination)
        {
            if (destination.Length < Count) throw new ArgumentException("DestinationTooShort");

            Unsafe.WriteUnaligned(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(destination)), this);
        }

        /// <summary>Attempts to copy the vector to the given <see cref="Span{Single}" />. The length of the destination span must be at least 4.</summary>
        /// <param name="destination">The destination span which the values are copied into.</param>
        /// <returns><see langword="true" /> if the source vector was successfully copied to <paramref name="destination" />. <see langword="false" /> if <paramref name="destination" /> is not large enough to hold the source vector.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryCopyTo(Span<T> destination)
        {
            if (destination.Length < Count) return false;

            Unsafe.WriteUnaligned(ref Unsafe.As<T, byte>(ref MemoryMarshal.GetReference(destination)), this);
            return true;
        }

        /// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
        /// <param name="other">The other vector.</param>
        /// <returns><see langword="true" /> if the two vectors are equal; otherwise, <see langword="false" />.</returns>
        /// <remarks>Two vectors are equal if their <see cref="System.Numerics.Vector4<T>.X" />, <see cref="System.Numerics.Vector4<T>.Y" />, <see cref="System.Numerics.Vector4<T>.Z" />, and <see cref="System.Numerics.Vector4<T>.W" /> elements are equal.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(Vector4<T> other)
        {
            return X.Equals(other.X)
                && Y.Equals(other.Y)
                && Z.Equals(other.Z)
                && W.Equals(other.W);
        }

        /// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true" /> if the current instance and <paramref name="obj" /> are equal; otherwise, <see langword="false" />. If <paramref name="obj" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
        /// <remarks>The current instance and <paramref name="obj" /> are equal if <paramref name="obj" /> is a <see cref="System.Numerics.Vector4<T>" /> object and their corresponding elements are equal.</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object obj)
        {
            return (obj is Vector4<T> other) && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>The hash code.</returns>
        public override readonly int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        /// <summary>Returns the string representation of the current instance using default formatting.</summary>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>This method returns a string in which each element of the vector is formatted using the "G" (general) format string and the formatting conventions of the current thread culture. The "&lt;" and "&gt;" characters are used to begin and end the string, and the current culture's <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" /> property followed by a space is used to separate each element.</remarks>
        public override readonly string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
        /// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>This method returns a string in which each element of the vector is formatted using <paramref name="format" /> and the current culture's formatting conventions. The "&lt;" and "&gt;" characters are used to begin and end the string, and the current culture's <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" /> property followed by a space is used to separate each element.</remarks>
        /// <related type="Article" href="/dotnet/standard/base-types/standard-numeric-format-strings">Standard Numeric Format Strings</related>
        /// <related type="Article" href="/dotnet/standard/base-types/custom-numeric-format-strings">Custom Numeric Format Strings</related>
        public readonly string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
        /// <param name="format">A standard or custom numeric format string that defines the format of individual elements.</param>
        /// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the current instance.</returns>
        /// <remarks>This method returns a string in which each element of the vector is formatted using <paramref name="format" /> and <paramref name="formatProvider" />. The "&lt;" and "&gt;" characters are used to begin and end the string, and the format provider's <see cref="System.Globalization.NumberFormatInfo.NumberGroupSeparator" /> property followed by a space is used to separate each element.</remarks>
        /// <related type="Article" href="/dotnet/standard/base-types/standard-numeric-format-strings">Standard Numeric Format Strings</related>
        /// <related type="Article" href="/dotnet/standard/base-types/custom-numeric-format-strings">Custom Numeric Format Strings</related>
        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;

            return $"<{X}{separator} {Y}{separator} {Z}{separator} {W}>";
        }
    }
}
