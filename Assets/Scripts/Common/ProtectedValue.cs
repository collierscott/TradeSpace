using System;
using System.Xml.Serialization;

namespace Assets.Scripts.Common
{
    public class ProtectedValue
    {
        public string Encrypted;

        private ProtectedValue()
        {
        }

        public ProtectedValue(object value)
        {
            Encrypted = B64R.Encrypt(Convert.ToString(value));
        }

        [XmlIgnore]
        public long Long
        {
            get { return long.Parse(B64R.Decrypt(Encrypted)); }
            set { Encrypted = B64R.Encrypt(Convert.ToString(value)); }
        }

        [XmlIgnore]
        public float Float
        {
            get { return float.Parse(B64R.Decrypt(Encrypted)); }
            set { Encrypted = B64R.Encrypt(Convert.ToString(value)); }
        }

        [XmlIgnore]
        public string String
        {
            get { return B64R.Decrypt(Encrypted); }
            set { Encrypted = B64R.Encrypt(value); }
        }

        [XmlIgnore]
        public DateTime DateTime
        {
            get { return DateTime.Parse(B64R.Decrypt(Encrypted)); }
            set { Encrypted = B64R.Encrypt(Convert.ToString(value)); }
        }
    }
}