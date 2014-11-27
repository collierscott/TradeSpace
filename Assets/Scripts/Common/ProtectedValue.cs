using System;
using SimpleJSON;

namespace Assets.Scripts.Common
{
    public class ProtectedValue
    {
        private readonly string _protected;

        private ProtectedValue(string value)
        {
            _protected = value;
        }

        public ProtectedValue(object value)
        {
            _protected = B64R.Encode(Convert.ToString(value));
        }

        #region Implicit

        public static implicit operator ProtectedValue(int value)
        {
            return new ProtectedValue(value);
        }

        public static implicit operator ProtectedValue(long value)
        {
            return new ProtectedValue(value);
        }

        public static implicit operator ProtectedValue(string value)
        {
            return new ProtectedValue(B64R.Encode(value));
        }

        public static implicit operator ProtectedValue(DateTime value)
        {
            return new ProtectedValue(value);
        }

        public static implicit operator ProtectedValue(float value)
        {
            return new ProtectedValue(value);
        }

        public static implicit operator ProtectedValue(double value)
        {
            return new ProtectedValue(value);
        }

        #endregion

        #region Types

        public int Int
        {
            get { return int.Parse(B64R.Decode(_protected)); }
        }

        public long Long
        {
			get { return string.IsNullOrEmpty(_protected) || _protected==" "?0: long.Parse(B64R.Decode(_protected)); }
        }

        public float Float
        {
            get { return float.Parse(B64R.Decode(_protected)); }
        }

        public string String
        {
            get { return B64R.Decode(_protected); }
        }

        public DateTime DateTime
        {
            get { return DateTime.Parse(B64R.Decode(_protected)); }
        }

        #endregion

        #region JSON

        public JSONData ToJson()
        {
            return new JSONData(_protected);
        }

        public static ProtectedValue FromJson(JSONNode json)
        {
            return new ProtectedValue(json.Value);
        }

        #endregion

        #region Common

        public override int GetHashCode()
        {
            return _protected != null ? _protected.GetHashCode() : 0;
        }

        public static bool operator !=(ProtectedValue a, ProtectedValue b)
        {
            return !(a == b);
        }

        public static bool operator ==(ProtectedValue a, ProtectedValue b)
        {
            if ((ReferenceEquals(null, a) && !ReferenceEquals(null, b)) || (!ReferenceEquals(null, a) && ReferenceEquals(null, b)))
            {
                return false;
            }

            if (ReferenceEquals(null, a))
            {
                return true;
            }

            return a._protected == b._protected;
        }

        public static ProtectedValue operator ++(ProtectedValue value)
        {
            return value.Long + 1;
        }

        public static ProtectedValue operator --(ProtectedValue value)
        {
            return value.Long - 1;
        }

        public static ProtectedValue operator +(ProtectedValue a, ProtectedValue b)
        {
            return a.Long + b.Long;
        }

        public static ProtectedValue operator -(ProtectedValue a, ProtectedValue b)
        {
            return a.Long - b.Long;
        }

        public bool Equals(ProtectedValue other)
        {
            return this == other;
        }

        public static bool operator >(ProtectedValue a, ProtectedValue b)
        {
            return a.Long > b.Long;
        }

        public static bool operator <(ProtectedValue a, ProtectedValue b)
        {
            return a.Long < b.Long;
        }

        public static bool operator >=(ProtectedValue a, ProtectedValue b)
        {
            return a.Long >= b.Long;
        }

        public static bool operator <=(ProtectedValue a, ProtectedValue b)
        {
            return a.Long <= b.Long;
        }

        public override bool Equals(object value)
        {
            if (ReferenceEquals(null, value)) return false;

            return value as ProtectedValue == this;
        }

        public ProtectedValue Copy()
        {
            return new ProtectedValue(_protected);
        }

        public override string ToString()
        {
            return String;
        }

        #endregion
    }
}