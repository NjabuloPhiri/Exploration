namespace SemaphoreJamSession

{
    internal class Program
    {
        // Declare musicians array and initialize to size 5
        static String[] musicians = {"Michael Jackson", "Kaytranada", "Andre Benjamin", "Dollar Brand", "Beyonce"};
        
        #pragma warning disable
        private static Semaphore semaphoreMicrophone;
        static void Main(string[] args)
        {
            // Define semaphore for microphone
            semaphoreMicrophone = new Semaphore(initialCount: 0, maximumCount: 10);
            Thread[] t = new Thread[5];
            // Create N musician threads (or processes)
            for (int i = 1; i < musicians.Length; i++)
            {
                t[i] = new Thread(Think);
                t[i].Start();

                t[i] = new Thread(Sing);
                t[i].Start();

                t[i] = new Thread(ReleaseMicrophone);
                t[i].Start();
            }
            Console.Read();
        }


        // Define musician states
        static void Think()
        {
            // While another musician uses the microphone, wait.
            // During this state, the waiting musician works on
            // their writing and polishes their cadence
            
            Console.WriteLine("{0} is working on his lyrics...", ShuffleArray(musicians));

            
            Thread.Sleep(1000);
            
            semaphoreMicrophone.Release();
        }

        static void Sing()
        {
            // When the musician has access to the microphone,
            // they execute their performance for the required 
            // piece of the song.
            
            Console.WriteLine("{0} enters the booth!", ShuffleArray(musicians));

            Thread.Sleep(1000);
            semaphoreMicrophone.Release();
        }

        static void ReleaseMicrophone()
        {
            // When the musician is done recording their piece,
            // they release the microphone and go back to
            // polishing their writing (i.e. thinking).
            
            Console.WriteLine("{0} is leaving the booth", ShuffleArray(musicians));

            Thread.Sleep(1000);
            semaphoreMicrophone.Release();
        }

        static string ShuffleArray<T>(T[] musicians)
        {
            Random random = new Random();
            for (int i = musicians.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = musicians[i];
                musicians[i] = musicians[j];
                musicians[j] = temp;
            }
            return String.Join(Environment.NewLine, musicians);
        }
    }


    // TO-DO: 
    // 1. Clean up the code
    // 2. Fix the ShuffleArray method to be
    // in line with expected behavior.
}
