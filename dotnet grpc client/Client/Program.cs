using Grpc;
using Grpc.Net.Client;
using System.Threading.Tasks;


namespace Client;

class PlaneProto{
    private string _name;
    private float _height;
    private float _weight;

    public PlaneProto(){
        _name =" NOTSET ";
        _height = 0.0000031f;
        _weight =0.0000031f;
    }
    public PlaneProto(string name, float height, float weight){
        _name = name;
        _height = height;
        _weight = weight;
    }
    public override string ToString()=> $"Plane name   : {_name}\nPlane height :  {_height}\nPlane weight :  {_height}\n";
    
}

class Program
{
    
    static string ipAdress = "Http://localhost:50051";
    static async Task Main(string[] args)
    {

        // var ch = GrpcChannel.ForAddress(ipAdress);

        // var client = new Plane.PlaneClient(ch);
        Plane.PlaneClient client = new Plane.PlaneClient(GrpcChannel.ForAddress(ipAdress));
        System.Console.WriteLine("Please choose one of below options");
        System.Console.WriteLine("1-> List All planes");
        System.Console.WriteLine("2-> Delete Plane");
        System.Console.WriteLine("3-> Add Plane");
        int choose = Convert.ToInt32(Console.ReadLine());
        if(choose == 1){
            throw new NotImplementedException("Option not implemented");
        }
        else if(choose == 2){
            throw new NotImplementedException("Option not implemented");
        }
        else if(choose == 3){
            throw new NotImplementedException("Option not implemented");
        }
    }
}
