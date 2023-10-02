from typing import Union, TypeVar, Optional, List, Tuple, Literal
from functools import singledispatch
from io import StringIO


class StringBuilder:
    def __init__(self):
        self.__build = StringIO()

    @property
    def construct(self):
        return self.__build.getvalue()

    @singledispatch
    def add(self, _s: str, before: bool = False) -> Literal[True]:
        if before:
            self.__build.seek(0)
        self.__build.write(_s)
        return True

    @add.register
    def add(self, _s: list, before: bool = False, sep: str = "") -> Literal[True]:
        if before:
            self.__build.seek(0)
        self.__build.write(sep.join(_s))
        return True


_T = TypeVar('_T')
Empty = (0,)
MatrixType = Optional[Union[List[List[int]], 'Matrix', tuple[int]]]


class Matrix:
    def __init__(
            self,
            value: MatrixType = Empty,
            rows=0,
            cols=0) -> None:

        self.rows, self.cols, self.value = self.preset(value, rows, cols)

    @staticmethod
    def preset(value: MatrixType, rows: int, cols: int) -> Tuple[int, int, MatrixType]:
        if value == Empty and rows == 0 and cols == 0:
            raise Exception("Must provide at least some information!!")
        _r = rows if rows > 0 else len(value)
        _c = cols if cols > 0 else len(value[0])
        val = value
        if value == Empty:
            _cols = [0 for _ in range(cols)]
            val = [_cols for _ in range(rows)]
        return _r, _c, val

    def __getitem__(self, cords: slice):
        return self.value[cords.start][cords.stop]

    def __setitem__(self, key, value: int):
        if isinstance(key, slice):
            if not key.step and key.step is not None:
                raise NotImplementedError(f"Step in slice does nothing in this case: step value={key.step}")
            self.value[key.start][key.stop] = value

    def __add__(self, other: "Matrix"):
        if self.rows != other.rows and self.cols != other.cols:
            raise Exception("Not compatible matrices: Wrong dimensions")
        plus = lambda x: [self[y:x] + other[y:x] for y in range(self.rows)]
        _tmp = [plus(_c) for _c in range(self.cols)]
        return Matrix(_tmp)

    def __matmul__(self, other: "Matrix") -> "Matrix":
        rows_a, cols_a = self.rows, self.cols
        rows_b, cols_b = other.rows, other.cols
        if cols_a != rows_b:
            raise Exception("Not compatible matrices")
        _row = lambda x, y: [self[x: _y] * other[x: _y] for _y in range(y)]
        _tmp_2 = [_row(_i, cols_a) for _i in range(rows_b)]
        return Matrix(_tmp_2, rows_b, cols_a)

    def __repr__(self) -> str:
        st = StringBuilder()
        for _c in range(self.cols):
            for _r in range(self.rows):
                st.add(str(self[_r: _c]) + " ")
            st.add("\n")
        return st.construct

    def __len__(self):
        _sum = 0
        for _c in range(self.cols):
            for _r in range(self.rows):
                _sum += _r
        return _sum


A = [[2, 3, 1], [3, 2, 2], [3, 2, 3]]
C = [[1, 3, 2], [3, 1, 2], [2, 2, 3]]
mat_a = Matrix(A, 3, 3)
mat_c = Matrix(C, 3, 3)
mat_f = Matrix(rows=3, cols=2)
print(mat_a)
print(mat_c)
mat_ca = mat_a + mat_c
mat_b = mat_a @ mat_c
print(mat_b)
print(mat_ca)
