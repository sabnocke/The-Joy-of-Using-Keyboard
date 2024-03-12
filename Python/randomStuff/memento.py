from copy import deepcopy, copy
from typing import Callable, List


def memento(obj: object, deep: bool = False) -> Callable[[], None]:
    """

    :param obj: object to create memento of
    :param deep: if deepcopy should be done
    :return: Actually returns reference to function, thus it still needs to be called later on
    """
    state = deepcopy(obj.__dict__) if deep else copy(obj.__dict__)

    def restore() -> None:
        obj.__dict__.clear()
        obj.__dict__.update(state)

    return restore


class Transaction:
    deep = False
    states: List[Callable[[], None]] = []

    def __init__(self, deep, *targets):
        self.deep = deep
        self.targets = targets
        self.commit()

    def commit(self):
        self.states = [memento(target, self.deep) for target in self.targets]
        return None

    def rollback(self):
        for a_state in self.states:
            a_state()
        return None


class Transactional:
    def __init__(self, method):
        self.method = method

    def __get__(self, obj, T):
        def transaction(*args, **kwargs):
            state = memento(obj)
            try:
                return self.method(obj, *args, **kwargs)
            except Exception as e:
                state()
                raise e


class NumObj:
    def __init__(self, value):
        self.value = value

    def __repr__(self):
        return f"<{self.__class__.__name__}: {self.value!r}>"

    def increment(self):
        self.value += 1

    @Transactional
    def do_stuff(self):
        self.value = "1111"
        self.increment()


def main() -> None:  # sourcery skip: extract-method
    no = NumObj(-1)
    print(no)
    a_transaction = Transaction(True, no)

    try:
        _extracted_from_main_(no)
        a_transaction.commit()
        print("transaction commited")
        _extracted_from_main_(no)
        no.value += "x"
        print(no)
    except Exception:
        a_transaction.rollback()
        print("rolled back")
        print(no)

    print("now doing stuff")
    try:
        no.do_stuff()
    except Exception:
        print("=> doing stuff failed")
        import sys
        import traceback
        traceback.print_exc(file=sys.stdout)

    finally:
        print(no)


def _extracted_from_main_(no: NumObj) -> None:
    for _ in range(3):
        no.increment()
        print(no)


if __name__ == "__main__":
    main()
