namespace Assets.Scripts.Common
{
    public class B64R
    {
        public static string Encrypt(string value)
        {
            var base64 = Base64.Encode(value);
            var chars = base64.ToCharArray();

            for (var i = 1; i < chars.Length; i += 2)
            {
                var c = chars[i];

                chars[i] = chars[i - 1];
                chars[i - 1] = c;
            }

            return new string(chars);
        }

        public static string Decrypt(string value)
        {
            var chars = value.ToCharArray();

            for (var i = 1; i < chars.Length; i += 2)
            {
                var c = chars[i];

                chars[i] = chars[i - 1];
                chars[i - 1] = c;
            }

            return Base64.Decode(new string(chars));
        }
    }
}