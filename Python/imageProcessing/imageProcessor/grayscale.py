import os
import pathlib
import random
import time
from collections import namedtuple
from multiprocessing import Pool
from typing import Generator, Tuple, Union, Any

import cv2
from numpy import ndarray, dtype, generic

RGB = namedtuple("RGB", ["red", "green", "blue"])
Point = namedtuple("Point", ["x", "y"])
type Composite = Tuple[Point, RGB]
type Matrix = Union[cv2.Mat | ndarray[Any, dtype[generic]] | ndarray]


class ImageProcessor:
    def __init__(self, image: Matrix):
        self.image: Matrix = image
        self.converted: Matrix = cv2.cvtColor(self.image, cv2.COLOR_BGR2RGB)

    def imageIterator(self) -> Generator[Composite, Any, None]:
        height, width, *rest = self.converted.shape
        for x in range(height):
            for y in range(width):
                yield Point(x, y), RGB(*self.converted[x][y])

    @staticmethod
    def trace(func):
        def wrapper(*args, **kwargs):
            now = time.time()
            result = func(*args, **kwargs)
            elapsed = time.time() - now
            print(f"Elapsed time: {elapsed:.3f}s")
            return result

        return wrapper

    @staticmethod
    def imageTransform(color: RGB) -> RGB:
        """
        This is a dot product to convert a color vector to gray color
        :param color: Collection of three values that correspond to RGB colors
        :return: Collection of three values that correspond to gray (they are all equal)
        """
        gray = color.red * 0.299 + color.green * 0.587 + color.blue * 0.114
        return RGB(gray, gray, gray)

    def processing(self, c: Composite) -> bool:
        """
        Consumes a Composite tuple and processes it adding it to the image
        :param c: composite tuple of RGB and Point
        :return: nothing
        """
        point, rgb = c
        gray = self.imageTransform(rgb)
        self.converted[point.x][point.y] = list(gray)
        # print(f"[{rgb.red} {rgb.green} {rgb.blue}] -> {self.converted[point.x][point.y]}")
        return True

    @trace
    def transform_seq(self, save=True):
        for item in self.imageIterator():
            self.processing(item)
        if save:
            self.save()
        else:
            cv2.imshow("image", self.converted)
            cv2.waitKey(0)
            cv2.destroyAllWindows()

    @trace
    def transform(self, save=True):
        with Pool() as pool:
            pool.starmap(self.processing, zip(self.imageIterator()))
            if save:
                self.save()
            else:
                cv2.imshow("image", self.converted)
                cv2.waitKey(0)
                cv2.destroyAllWindows()
        return True

    def save(
            self,
            name: Any = random.randrange(1, 100),
            extension: str = ".png",
            path: pathlib.Path = pathlib.Path.cwd(),
    ) -> bool:
        new_path = os.path.join(path, str(name) + extension)
        return cv2.imwrite(new_path, self.converted)


def main() -> None:
    img: Matrix = cv2.imread("../image (3).png")
    imp = ImageProcessor(img)
    imp.transform_seq(True)


if __name__ == "__main__":
    main()
