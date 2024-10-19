namespace Enrypted_ThreadPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the file path(.txt): ");
            string filePath = Console.ReadLine()!;

            Console.Write("Enter the encryption key:");
            string key = Console.ReadLine()!;

            if (!filePath.EndsWith(".txt"))
            {
                Console.WriteLine("The file must be a .txt file.");
                return;
            }

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    string content = File.ReadAllText(filePath);

                    char[] encryptedContent = new char[content.Length];

                    for (int i = 0; i < content.Length; i++)
                    {
                        encryptedContent[i] = (char)(content[i] ^ key[i % key.Length]);
                    }

                    string encryptedFilePath = Path.GetFileNameWithoutExtension(filePath) + "Encrypted.txt";

                    File.WriteAllText(encryptedFilePath, new string(encryptedContent));

                    Console.WriteLine("File Encrypted Successfully. Encrypted file saved as: " + encryptedFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred during encryption: " + ex.Message);
                }
            });

            Console.WriteLine("Encrypting file in the background...");
            Console.ReadLine(); 

        }
    }
}
