from dataclasses import dataclass
from inspect import getfullargspec
from typing import Any, Union


class NamespaceException(Exception):
    pass


class OperationError(Exception):
    pass


@dataclass(frozen=True)
class Signature:
    _module: Any
    _class: Any
    _name: Any
    argc: int


class Function(object):
    def __init__(self, fn) -> None:
        self.fn = fn

    def __call__(self, *args, **kwargs):
        fn = Namespace.get_instance().get(self.fn, *args)
        if not fn:
            raise NamespaceException("No function found!")
        return fn(*args, **kwargs)

    def sig(self, args: Union[tuple, None] = None):
        argc = len(args or [])
        if not args:
            args = getfullargspec(self.fn)
            argc = len(args.args)
        sig = Signature(
            self.fn.__module__,
            self.fn.__class__,
            self.fn.__name__,
            argc,
        )
        return sig or args


class Namespace(object):
    __instance: Union["Namespace", None] = None

    def __init__(self):
        if self.__instance is None:
            self.function_map = dict()
            Namespace.__instance = self
        else:
            raise NamespaceException("cannot instantiate a virtual Namespace again")

    @staticmethod
    def get_instance() -> "Namespace":
        if Namespace.__instance is None:
            Namespace()
        return Namespace.__instance

    def register(self, fn):
        __fun = Function(fn)
        self.function_map[__fun.sig()] = fn
        return __fun

    def get(self, fn, *args):
        __fun = Function(fn)
        return self.function_map.get(__fun.sig(args=args))


def overload(fn):
    return Namespace.get_instance().register(fn)
