namespace LSPTransporte
{
    public class TransporteMotorizado : Transporte
    {
        public Motor Motor { get; set; }
        public virtual bool LigarMotor()
        {
            return Motor.Ligar();
        }
    }
}