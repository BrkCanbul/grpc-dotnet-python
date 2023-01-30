from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Optional as _Optional

DESCRIPTOR: _descriptor.FileDescriptor

class GetPlaneReq(_message.Message):
    __slots__ = ["name"]
    NAME_FIELD_NUMBER: _ClassVar[int]
    name: str
    def __init__(self, name: _Optional[str] = ...) -> None: ...

class GetPlaneResp(_message.Message):
    __slots__ = ["height", "name", "weight"]
    HEIGHT_FIELD_NUMBER: _ClassVar[int]
    NAME_FIELD_NUMBER: _ClassVar[int]
    WEIGHT_FIELD_NUMBER: _ClassVar[int]
    height: float
    name: str
    weight: float
    def __init__(self, name: _Optional[str] = ..., height: _Optional[float] = ..., weight: _Optional[float] = ...) -> None: ...

class PlaneAddReq(_message.Message):
    __slots__ = ["height", "name", "weight"]
    HEIGHT_FIELD_NUMBER: _ClassVar[int]
    NAME_FIELD_NUMBER: _ClassVar[int]
    WEIGHT_FIELD_NUMBER: _ClassVar[int]
    height: float
    name: str
    weight: float
    def __init__(self, name: _Optional[str] = ..., height: _Optional[float] = ..., weight: _Optional[float] = ...) -> None: ...

class PlaneAddResp(_message.Message):
    __slots__ = ["respMessage"]
    RESPMESSAGE_FIELD_NUMBER: _ClassVar[int]
    respMessage: str
    def __init__(self, respMessage: _Optional[str] = ...) -> None: ...

class delPlaneRep(_message.Message):
    __slots__ = ["repMessage"]
    REPMESSAGE_FIELD_NUMBER: _ClassVar[int]
    repMessage: str
    def __init__(self, repMessage: _Optional[str] = ...) -> None: ...

class delPlaneReq(_message.Message):
    __slots__ = ["id"]
    ID_FIELD_NUMBER: _ClassVar[int]
    id: int
    def __init__(self, id: _Optional[int] = ...) -> None: ...
