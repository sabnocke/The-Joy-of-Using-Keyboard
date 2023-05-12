from __future__ import annotations
from typing import Protocol, List
from contextlib import suppress


class Observer(Protocol):
    """
    Generic Observer type
    """

    def update(self, subject: Subject) -> None:
        pass


class Subject:
    def __init__(self) -> None:
        self.observers: List[Observer] = []

    def attach(self, observer: Observer) -> None:
        if observer not in self.observers:
            self.observers.append(observer)

    def detach(self, observer: Observer) -> None:
        with suppress(ValueError):
            self.observers.remove(observer)

    def notify(self, modifier: Observer | None = None) -> None:
        for observer in self.observers:
            if modifier != observer:
                observer.update(self)


class Data(Subject):
    def __init__(self, name="") -> None:
        super().__init__()
        self.name = name
        self._data = 0

    @property
    def data(self) -> int:
        return self._data

    @data.setter
    def data(self, value: int):
        self._data = value
        self.notify()


class HexViewer:
    @staticmethod
    def update(subject: Data) -> None:
        print(f"HexViewer: Subject {subject.name} has data 0x{subject.data:x}", end="\n")


class DecimalViewer:
    @staticmethod
    def update(subject: Data) -> None:
        print(f"DecimalViewer: {subject.name} has data {subject.data}", end="\n")


def main() -> None:
    data1 = Data("Data 1")
    data2 = Data("Data 2")
    view1 = HexViewer()
    view2 = DecimalViewer()
    data1.attach(view1)
    data1.attach(view2)
    data2.attach(view2)
    data2.attach(view1)

    data1.data = 10
    data2.data = 15
    data1.data = 3

    data2.detach(view1)
    data1.detach(view1)

    data1.data = 14
    data2.data = 12


if __name__ == "__main__":
    main()
