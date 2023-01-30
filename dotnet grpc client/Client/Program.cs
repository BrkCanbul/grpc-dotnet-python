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
    public PlaneAddReq toAddRequest() => new PlaneAddReq{Name =_name,Height =_height,Weight=_weight};
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
    static bool  Login(User toLogin){
        foreach(User user in users){
            if( user.Name == toLogin.Name && user.Password == toLogin.Password ){
                for(int i = 0 ;i<100;i++){
                    Console.Write("-");
                    Task.Delay(10).Wait();

                }
                Console.WriteLine(">");
                Task.Delay(500).Wait();
                System.Console.WriteLine("connected succesfully");
                Task.Delay(500).Wait();
                return true;
            }
        }
        throw new Exception("Wrong username or password\nPlease try again");
    }
    
    static string ipAdress = "Http://localhost:50051";
    static async Task Main(string[] args)
    {

        // var ch = GrpcChannel.ForAddress(ipAdress);
        // var client = new Plane.PlaneClient(ch);
        User user;
            while(true){
                try{
                    System.Console.Write("please enter your user name : ");
                    string uName = Console.ReadLine();
                    System.Console.Write("Please enter your password  :");
                    string pass = Console.ReadLine();
                    user = new User(uName,pass);     
                    Console.Clear();
                    Login(user);
                    break;
                    
                } catch (Exception e){
                    System.Console.WriteLine(e.Message.ToString());
                    continue;
                }
            }

        while(true){
            
            
            Plane.PlaneClient client = new Plane.PlaneClient(GrpcChannel.ForAddress(ipAdress));
            System.Console.WriteLine("Please choose one of below options");
            System.Console.WriteLine("1-> List All planes");
            System.Console.WriteLine("2-> Delete Plane");
            System.Console.WriteLine("3-> Add Plane");
            int choose = Convert.ToInt32(Console.ReadLine());
            
            if(choose == 1){
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                var planetask = client.getAllPlanes(new GetPlaneReq{Name = user.Name});
                System.Console.WriteLine("Planes:\n\n");
                while( await planetask.ResponseStream.MoveNext(tokenSource.Token)){
                    PlaneProto plane = new PlaneProto(planetask.ResponseStream.Current.Name,
                    planetask.ResponseStream.Current.Height,planetask.ResponseStream.Current.Weight);

                    System.Console.WriteLine(plane.ToString());
                }

            }
            else if(choose == 2){
                System.Console.WriteLine("please enter the index of the plane");
                int toDel = Convert.ToInt32(Console.ReadLine());
                var deltask = client.deletePlane(new delPlaneReq{Id=toDel});
                System.Console.WriteLine($"server Responsed {deltask.RepMessage}");
            }
            else if(choose == 3){
                System.Console.WriteLine("\n\nPlease Enter the name of plane : ");
                string nm = Console.ReadLine();
                System.Console.WriteLine("Please enter height of plane:");
                double hg = Convert.ToDouble(Console.ReadLine());
                System.Console.WriteLine("Please enter weight of plane:");
                double wg = Convert.ToInt32(Console.ReadLine());
                var pl = new PlaneProto(nm,float.Parse(hg.ToString()),float.Parse(wg.ToString()));
                var addTask = client.AddPlane(pl.toAddRequest());
                System.Console.WriteLine($"server responsed :{addTask.RespMessage}");
            }
            System.Console.WriteLine("Do you want to continue (Y/n)");
            char continuer = Convert.ToChar(Console.ReadLine());
            if(continuer == 'n' || continuer == 'N'){
                break;
            }
        }
    }
}
