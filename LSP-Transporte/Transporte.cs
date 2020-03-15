namespace LSPTransporte
{
    //Antes da refatoração
    // public class Transporte
    // {
    //     public string Nome { get; set; }
    //     public Motor Motor { get; set; }
    //     public int Velocidade { get; set; }
    //     public virtual bool LigarMotor()
    //     {
    //         return Motor.Ligar();
    //     }
    // }

    //Apos a refatoração    
    public class Transporte
    {
        public string Nome { get; set; }
        public int Velocidade { get; set; }
    }
}
