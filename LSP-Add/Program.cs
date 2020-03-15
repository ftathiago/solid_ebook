using System;

namespace LSP_Add
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = new ContainerPersistenceWrapper();
            UmObjeto umObjeto = new UmObjeto();
            container.Add(umObjeto);
        }
    }
}
