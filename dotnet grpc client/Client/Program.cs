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

class User{
    private string name;
    private string password;
    public string Name { get {
        return name;
    } set{
        name = value;
    }}
    public string Password{
        get{
            return password;
        }
        set{
            password = value;
        }
    }

    public User()
    {
        name = " NOTSET ";
        password = " NOTSET ";
    }
    public User(string name,string password){
        this.name = name;
        this.password = password;
    }

}

class Program
{
    static User[] users = {new User("brkCanbul","fsrTrasbond"),new User("sultanAhmet","gs1905"),new User("sultanFatih","trabzon1491")};
    static bool Login(User toLogin){
        foreach(User user in users){
            if( user.Name == toLogin.Name && user.Password == toLogin.Password ){
                return true;
            }
        }
        throw new Exception("Wrong username or password\n\nPlease try again");
    }
    
    static string ipAdress = "Http://localhost:50051";
    static async Task Main(string[] args)
    {

        // var ch = GrpcChannel.ForAddress(ipAdress);
        // var client = new Plane.PlaneClient(ch);
        User user;
        while(true){
            try{
                System.Console.WriteLine("please enter your user name");
                string uName = Console.ReadLine();
                string pass = Console.ReadLine();
                user = new User(uName,pass);     
                Login(user);
                break;

            } catch (Exception e){
                System.Console.WriteLine(e.Message.ToString());
                continue;
            }
        }
        
        Plane.PlaneClient client = new Plane.PlaneClient(GrpcChannel.ForAddress(ipAdress));
        System.Console.WriteLine("Please choose one of below options");
        System.Console.WriteLine("1-> List All planes");
        System.Console.WriteLine("2-> Delete Plane");
        System.Console.WriteLine("3-> Add Plane");
        int choose = Convert.ToInt32(Console.ReadLine());
        
        if(choose == 1){
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            var planetask = client.getAllPlanes(new GetPlaneReq{Name = user.Name});
            while(planetask.ResponseStream.MoveNext()){}
        }
        else if(choose == 2){
            throw new NotImplementedException("Option not implemented");
        }
        else if(choose == 3){
            throw new NotImplementedException("Option not implemented");
        }
    }
}
