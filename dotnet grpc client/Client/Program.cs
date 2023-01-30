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

        var ch = GrpcChannel.ForAddress(ipAdress);

        var client = new Plane.PlaneClient(ch);

        System.Console.WriteLine("how many planes will you add");
        int toAdd = Convert.ToInt32(Console.ReadLine());
        for(int i = 0 ; i<toAdd ;i++){
           
           var plane = client.AddPlane(new PlaneAddReq{Name=$"name{i}",Height = float.Parse((new Random().NextDouble()*10).ToString()) ,Weight =float.Parse((new Random().NextDouble()*10).ToString())});
            System.Console.WriteLine($"response :{plane.RespMessage}");
        }

       
        var getplane =  client.getAllPlanes(new GetPlaneReq{Name= "Burak"});
        CancellationTokenSource token = new CancellationTokenSource();
        int isd = 0;
        while( await getplane.ResponseStream.MoveNext(token.Token)){
            PlaneProto curPlane = new PlaneProto(getplane.ResponseStream.Current.Name,getplane.ResponseStream.Current.Height,getplane.ResponseStream.Current.Weight);
            System.Console.WriteLine(isd +curPlane.ToString());
            isd++;
        }
        System.Console.WriteLine("wich id will you delete");
        int todel = Convert.ToInt32(Console.ReadLine());
        var delplane =  client.deletePlane(new delPlaneReq{Id = todel});

        System.Console.WriteLine($"response :{delplane.RepMessage}");

        getplane =  client.getAllPlanes(new GetPlaneReq{Name= "Burak"});
        token = new CancellationTokenSource();
        isd = 0;
        while( await getplane.ResponseStream.MoveNext(token.Token)){
            PlaneProto curPlane = new PlaneProto(getplane.ResponseStream.Current.Name,getplane.ResponseStream.Current.Height,getplane.ResponseStream.Current.Weight);
            System.Console.WriteLine(isd +curPlane.ToString());
            isd++;
        }

        // var plane = client.AddPlane(new PlaneAddReq{Name="name",Height = 32.2f,Weight = 3233.3f});
        // System.Console.WriteLine("plane = "+plane);

        // var PlaneCl = new Planes.PlanesClient(ch);
        // var Plane = PlaneCl.AddPlane(new PlaneAddReq{Name="planoskaskos",Height=32.1f,Weight=34.23f});
        // System.Console.WriteLine("plane = " + Plane.Message);
    }
}
