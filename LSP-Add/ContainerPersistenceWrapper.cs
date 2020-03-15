namespace LSP_Add
{
    public class ContainerPersistenceWrapper : IContainer
    {
        private PersistenceObjects _persistenceObjects;

        public ContainerPersistenceWrapper()
        {
            _persistenceObjects = new PersistenceObjects();
        }

        public void Add(object o)
        {
            var persistableObject = (PersistableObject)o;
            _persistenceObjects.Add(persistableObject);
        }
        public object Remove(object o)
        {
            var persistableObject = (PersistableObject)o;
            var removeu = _persistenceObjects.Remove(persistableObject);
            if (removeu)
                return o;
            return null;
        }
    }
}