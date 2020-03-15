namespace LSP_Add
{
    public class PersistenceObjects
    {
        public void Add(PersistableObject persistableObject)
        {
            System.Console.WriteLine("Faz alguma coisa com o objeto");
        }

        public bool Remove(PersistableObject persistableObject)
        {
            return true;
        }
    }
}