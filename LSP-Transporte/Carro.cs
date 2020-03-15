namespace LSPTransporte
{
    //Antes da refatoração
    // public class Carro : Transporte
    public class Carro : TransporteMotorizado
    {
        private Tanque _tanque;
        public Carro(Tanque tanque)
        {
            _tanque = tanque;
        }

        public override bool LigarMotor()
        {
            if (_tanque.EstaVazio)
                return false;
            return base.LigarMotor();
        }
    }
}