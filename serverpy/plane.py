from concurrent import futures 
import grpc
import planes_pb2
import planes_pb2_grpc
from pydantic import BaseModel




class Plane():
    name:str
    height:float
    weight:float
    def __init__(self,fromReq:planes_pb2.PlaneAddReq):
        self.name = fromReq.name
        self.height = fromReq.height
        self.weight = fromReq.weight
    def toGetResp(self):
        return planes_pb2.GetPlaneResp(name=self.name,height=self.height,weight=self.weight)
    
planeList: list[Plane] = []

class PlaneServicer(planes_pb2_grpc.PlaneServicer):
    def AddPlane(self, request, context):
        print(f"request recieved : {request}")
        planeList.append(Plane(request))
        resp = planes_pb2.PlaneAddResp(respMessage="process done Successfully")
        return resp
    def getAllPlanes(self, request, context):
        for item in planeList:
            yield item.toGetResp()
    def deletePlane(self, request:planes_pb2.delPlaneReq, context):
        if planeList[request.id]:
            planeList.pop(request.id)
            return planes_pb2.delPlaneRep(repMessage="Plane Succesfully Deleted")
        return planes_pb2.delPlaneRep(repMessage="Could'nt Fount plane with index")
def serv():
    sv = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    planes_pb2_grpc.add_PlaneServicer_to_server(PlaneServicer(),sv)
    sv.add_insecure_port("[::]:50051")
    sv.start()
    print(f"server start at port 50051")
    sv.wait_for_termination()
if __name__ == '__main__':
    serv()
