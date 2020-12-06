namespace Reader
{
    public class ReverseCryptor : ICryptor
    {
        public string Decrypt(string content)
        {
            string result = string.Empty;

            for (int i = content.Length - 1; i >= 0; i--)
            {
                result += content[i];
            }

            return result;
        }
    }

    public interface ICryptor
    {
        string Decrypt(string content);
    }
}